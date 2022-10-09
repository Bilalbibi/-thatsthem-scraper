using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Dataflow;
using thatsthem_scraper.Models;

namespace thatsthem_scraper
{
    public partial class MainForm : Form
    {
        public bool LogToUi = true;
        public bool LogToFile = true;
        private Dictionary<string, string> _config;
        private readonly string _path = Application.StartupPath;
        List<string> _urls;
        int _count = 1;
        int _NumberOfUrls;
        List<Profile> _profiles = new List<Profile>();
        string _json;
        //List<string> _userAgents = new List<string>();
        HttpCaller _caller = new HttpCaller();


        private int _maxConcurrency;
        public MainForm()
        {
            InitializeComponent();
            Reporter.OnLog += OnLog;
            Reporter.OnError += OnError;
            Reporter.OnProgress += OnProgress;
        }
        private void OnProgress(object sender, (int nbr, int total, string message) e)
        {
            Display($"{e.message} {e.nbr} / {e.total}");
            SetProgress(e.nbr * 100 / e.total);
        }

        private void OnError(object sender, string e)
        {
            ErrorLog(e);
        }
        public delegate void SetProgressD(int x);
        public void SetProgress(int x)
        {
            if (InvokeRequired)
            {
                Invoke(new SetProgressD(SetProgress), x);
                return;
            }
            if ((x <= 100))
            {
                ProgressB.Value = x;
            }
        }

        private void OnLog(object sender, string e)
        {
            Display(e);
            NormalLog(e);
        }
        #region UIFunctions
        public delegate void WriteToLogD(string s, Color c);
        public void WriteToLog(string s, Color c)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new WriteToLogD(WriteToLog), s, c);
                    return;
                }
                if (LogToUi)
                {
                    if (DebugT.Lines.Length > 5000)
                    {
                        DebugT.Text = "";
                    }
                    DebugT.SelectionStart = DebugT.Text.Length;
                    DebugT.SelectionColor = c;
                    DebugT.AppendText(DateTime.Now.ToString(Utility.SimpleDateFormat) + " : " + s + Environment.NewLine);
                }
                Console.WriteLine(DateTime.Now.ToString(Utility.SimpleDateFormat) + @" : " + s);
                if (LogToFile)
                {
                    File.AppendAllText(_path + "/data/log.txt", DateTime.Now.ToString(Utility.SimpleDateFormat) + @" : " + s + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void NormalLog(string s)
        {
            WriteToLog(s, Color.Black);
        }
        public void ErrorLog(string s)
        {
            WriteToLog(s, Color.Red);
        }
        public void SuccessLog(string s)
        {
            WriteToLog(s, Color.Green);
        }
        public void CommandLog(string s)
        {
            WriteToLog(s, Color.Blue);
        }
        public void SaveControls(Control parent)
        {
            try
            {
                foreach (Control x in parent.Controls)
                {
                    #region Add key value to disctionarry

                    if (x.Name.EndsWith("I"))
                    {
                        switch (x)
                        {
                            case CheckBox _:
                            case RadioButton _:
                                _config.Add(x.Name, ((CheckBox)x).Checked + "");
                                break;
                            case TextBox _:
                            case RichTextBox _:
                                _config.Add(x.Name, x.Text);
                                break;
                            case NumericUpDown _:
                                _config.Add(x.Name, ((NumericUpDown)x).Value + "");
                                break;
                            default:
                                Console.WriteLine(@"could not find a type for " + x.Name);
                                break;
                        }
                    }
                    #endregion
                    SaveControls(x);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void SaveConfig()
        {
            _config = new Dictionary<string, string>();
            SaveControls(this);
            try
            {
                File.WriteAllText("config.txt", JsonConvert.SerializeObject(_config, Formatting.Indented));
            }
            catch (Exception e)
            {
                ErrorLog(e.ToString());
            }
        }
        public delegate void DisplayD(string s);
        public void Display(string s)
        {
            if (InvokeRequired)
            {
                Invoke(new DisplayD(Display), s);
                return;
            }
            displayT.Text = s;
        }

        #endregion
        private async void Start(object sender, EventArgs e)
        {
            //var pp = await GetProfile(new Inputs { Url = "https://thatsthem.com/name/Reid-Hines/5730-Rolyat-Rd.-Milton-FL", Cookie = "PHPSESSID=PTYu98TsQ-pimJww5HezoB%2CYNqYhLyaLR9JO2anKkK%2CTF6Hb" }, 0);
            //_profiles = JsonConvert.DeserializeObject<List<Profile>>(File.ReadAllText("profiles.txt"));
            //_profiles.Save($"Saved Profiles after an unexpected{DateTime.Now:dd_MM_yyyy_hh_mm_ss}.xlsx", "sheet1");
            if (KeyApI.Text == "")
            {
                MessageBox.Show("Please add your API key");
                return;
            }
            if (inputI.Text == "")
            {
                MessageBox.Show("Please select the urls input txt file");
                return;
            }
            _urls = File.ReadAllLines(inputI.Text).ToList();
            try
            {
                await Task.Run(MainWork);
            }
            catch (Exception ex)
            {
                if (ex.Message == "ERROR_WRONG_USER_KEY")
                {
                    MessageBox.Show("API key not valid please select a valid API key");
                    return;
                }
                MessageBox.Show("Fatal error: " + ex.ToString() + " Please contact The developper");
                if (SaveScrapedData.Checked)
                {
                    _profiles.Save($"Saved Profiles after an unexpected{DateTime.Now:dd_MM_yyyy_hh_mm_ss}.xlsx", "sheet1");
                    File.WriteAllText("last line we scraped.txt", _profiles.Count + "");
                }
                Application.Exit();
            }
            _profiles.Save($"Profiles{DateTime.Now:dd_MM_yyyy_hh_mm_ss}.xlsx", "sheet1");
            if (OpenFile.Checked)
            {
                try
                {
                    var p = new Process();
                    p.StartInfo = new ProcessStartInfo($"Profiles{DateTime.Now:dd_MM_yyyy_hh_mm_ss}.xlsx")
                    {
                        UseShellExecute = true
                    };
                    p.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error occred while opening the output file\"{$"Profiles{DateTime.Now:dd_MM_yyyy_hh_mm_ss}.xlsx"}\" ===> {ex.ToString()}");
                }
            }
            startB.Enabled = true;
            OpenFile.Enabled = true;
        }

        private async Task MainWork()
        {
            Invoke(new Action(() => startB.Enabled = false));
            Invoke(new Action(() => OpenFile.Enabled = false));
            _NumberOfUrls = _urls.Count;
            var numberOfSessions = _urls.Count / 10;
            var count = 0;
            var session = "";
            do
            {
                do
                {
                    session = await CreateSessions();
                    if (session == "ERROR_WRONG_USER_KEY")
                    {
                        throw new Exception("ERROR_WRONG_USER_KEY");
                    }
                    if (session == "Bad Request" || session == "Service Unavailable" ||
                        session == "Session expired" || session == "WebException  error")
                    {
                        await Task.Delay(1000);
                        continue;
                    }
                    break;
                } while (true);
                if (count == numberOfSessions)
                {
                    break;
                }
                do
                {
                    var list = _urls.Take(10).ToList();
                    var IsSessionExpired = await ScrapeProfiles(session, list);
                    if (!IsSessionExpired)
                    {
                        _urls.RemoveRange(0, list.Count);
                        count++;
                        var json = JsonConvert.SerializeObject(_profiles);
                        File.WriteAllText("profiles.txt", json);
                        break;
                    }
                    else
                    {
                        break;
                    }

                } while (true);
            } while (true);

        }

        private async Task<bool> ScrapeProfiles(string session, List<string> urls)
        {
            var inputs = new List<Inputs>();
            for (int i = 0; i < urls.Count; i++)
            {
                inputs.Add(new Inputs { Url = urls[i], Cookie = session });
            }

            var profiles = new List<Profile>();
            for (int i = 0; i < inputs.Count; i++)
            {
                Inputs? input = inputs[i];
                var profile = await GetProfile(input, i);
                if (profile != null && profile.Name == "This url not found")
                {
                    Reporter.Error(profile.Name);
                    Reporter.Progress(_count, _NumberOfUrls, "Profile scraped");
                    _count++;
                    continue;
                }
                if (profile == null)
                {
                    Reporter.Progress(_count, _NumberOfUrls, "Profile scraped");
                    _count++;
                    continue;
                }
                profiles.Add(profile);
                Reporter.Progress(_count, _NumberOfUrls, "Profile scraped");
                _count++;
            }
            _profiles.AddRange(profiles);
            return false;
        }

        private async Task<string> CreateSessions()
        {
            var siteKey = await GetSiteKey();
            if (siteKey == "Session expired" || siteKey == "WebException  error")
            {
                return siteKey;
            }
            var session = await GetSession(siteKey);
            return session;
        }

        private async Task<string> GetSession(string siteKey)
        {

            var id = await GetRecaptchaId(siteKey);
            if (id == "Service Unavailable" || id == "ERROR_WRONG_USER_KEY")
            {
                return id;
            }

            var recpatchaResponse = await GetRecpatchaResponse(id);
            if (recpatchaResponse == "Service Unavailable")
            {
                return id;
            }
            var session = await GetCookies(recpatchaResponse);
            return session;
        }

        private async Task<string> GetRecpatchaResponse(string id)
        {
            var url = $"http://2captcha.com/res.php?action=get&key={KeyApI.Text}&id={id}&json=1";
            var objt = new JObject();
            var recpatchaResponse = "";
            var tries = 1;
            do
            {
                _json = await _caller.GetHtml(url);
                if (_json.Contains("502 Bad Gateway"))
                {
                    Reporter.Error("Service respond:\"502 Bad Gateway\" (GetSession func \"while getting the recaptcah reponse\")" + " ==> retried request(s): " + tries);
                    await Task.Delay(2000);
                    tries++;
                    continue;
                }
                if (_json.Contains("Service Unavailable"))
                {
                    Reporter.Error("Service Unavailable (GetRecpatchaResponse func)");
                    await Task.Delay(5000);
                    return "Service Unavailable";
                }
                try
                {
                    objt = JObject.Parse(_json);
                    recpatchaResponse = (string)objt.SelectToken("..request");
                    //503 Service Unavailable
                    if (recpatchaResponse == "CAPCHA_NOT_READY")
                    {
                        await Task.Delay(5000);
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Reporter.Error($"unexpected Error: {ex.ToString()} \r\n unexpected response from 2captcha service: {_json}");
                    return "Service Unavailable";
                }
                return recpatchaResponse;
            } while (true);
        }

        private async Task<string> GetRecaptchaId(string siteKey)
        {
            var url = $"http://2captcha.com/in.php?key={KeyApI.Text}&method=userrecaptcha&googlekey={siteKey}&pageurl=https://thatsthem.com/challenge?r=%2F&json=1";
            var objt = new JObject();
            var tries = 1;
            var id = "";
            do
            {
                _json = await _caller.GetHtml(url);
                if (_json.Contains("502 Bad Gateway"))
                {
                    Reporter.Error($"Service respond:\"502 Bad Gateway\" (GetSession func \"while getting Id of the captcha\") " + " ==> retried request(s): " + tries);
                    await Task.Delay(2000);
                    tries++;
                    continue;
                }
                if (_json.Contains("Service Unavailable"))
                {
                    Reporter.Error("Service Unavailable (GetIdRecaptcha func)");
                    await Task.Delay(5000);
                    return "Service Unavailable";
                }
                objt = JObject.Parse(_json);
                id = (string)objt.SelectToken("..request");
                return id;
            } while (true);
        }

        private async Task<string> GetCookies(string? recpatchaResponse)
        {
            var data = $"g-recaptcha-response={recpatchaResponse}&r=/";
            WebProxy proxy = new WebProxy("83.149.70.159:13082");
            var cookies = "";
            string HtmlResult = "";
            try
            {
                var wc = new WebClientCookieContainer();
                wc.Proxy = proxy;
                using (wc)
                {
                    //var random = new Random();
                    //int index = random.Next(_userAgents.Count);
                    wc.Headers.Set("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36 Edg/106.0.1370.34");
                    wc.Headers.Set("referer", "https://thatsthem.com/challenge?r=%2F");
                    wc.Headers.Set("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                    wc.Headers.Set("upgrade-insecure-requests", "1");
                    wc.Headers.Set("Referrer-Policy", "strict-origin-when-cross-origin");
                    wc.Headers.Set("sec-fetch-site", "same-origin");
                    wc.Headers.Set("pragma", "no-cache");
                    wc.Headers.Set("cache-control", "no-cache");
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    HtmlResult = wc.UploadString("https://thatsthem.com/challenge", data);

                    var doc = new HtmlAgilityPack.HtmlDocument();
                    var node = doc.DocumentNode?.SelectNodes("//div[@class='record']");
                    var lookedUpText = doc.DocumentNode?.SelectSingleNode("//i[@data-title='Lookups per day.']/../following-sibling::td")?.InnerText.Trim();
                    if (node == null && lookedUpText != null)
                    {
                        var array = lookedUpText.Split('/');
                        var lookedUp = int.Parse(array[0]);
                        if (lookedUp == 10)
                        {
                            return "session expired";
                        }
                    }
                }
                var cookiesBuilder = new StringBuilder();
                foreach (Cookie cookie in wc.cookies.GetAllCookies())
                {
                    cookiesBuilder.Append(cookie.Name + "=" + cookie.Value + ";");
                }
                cookiesBuilder.Length--;
                cookies = cookiesBuilder.ToString();
            }
            catch (WebException ex)
            {

                if (ex.ToString().Contains("(400) Bad Request"))
                {
                    return "Bad Request (GetCookies func)";
                }
                await Task.Delay(2000);
            }
            catch (Exception ex)
            {
                Reporter.Log("unexpected Error (GetCookies func) = " + ex.ToString());
                return "Bad Request";
            }
            Reporter.Log("Obtaining session succeeded");
            return cookies;
        }

        private async Task<string> GetSiteKey()
        {
            WebProxy proxy = new WebProxy("37.48.118.90:13082");
            var WebExceptionTries = 0;
            do
            {
                try
                {
                    var random = new Random();
                    //int index = random.Next(_userAgents.Count);
                    var webClient = new WebClient();
                    webClient.Headers.Set("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36 Edg/106.0.1370.34");
                    webClient.Proxy = proxy;
                    var html = webClient.DownloadString("https://thatsthem.com/");
                    var doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);
                    var sitKey = doc.DocumentNode?.SelectSingleNode("//div[@data-sitekey]")?.GetAttributeValue("data-sitekey", "");
                    if (sitKey == null)
                    {
                        //doc.Save("check La5ra chbih.html");
                        Reporter.Error("Session expired (get sitKey func)");
                        return "Session expired";
                    }
                    return sitKey;
                }
                catch (WebException ex)
                {
                    if (WebExceptionTries == 3)
                    {
                        return "WebException  error";
                    }
                    Reporter.Error($"WebException  error={ex.ToString()}");
                    WebExceptionTries++;
                    await Task.Delay(2000);
                    continue;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    continue;
                }
            } while (true);
        }

        private async Task<Profile> GetProfile(Inputs inputs, int i)
        {
            var profile = new Profile();

            var doc = new HtmlAgilityPack.HtmlDocument();

            try
            {
                //6c39444004f7f617e848452049be32e5
                var webClient = new WebClient();
                //WebProxy proxy = new WebProxy("83.149.70.159:13082");
                //webClient.Proxy = proxy;
                webClient.Headers.Set("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36 Edg/106.0.1370.34");
                webClient.Headers.Set("cookie", inputs.Cookie);
                //webClient.Proxy = proxy;
                //Reporter.Error($"request start at {DateTime.Now:hh:mn:ss}");
                //Reporter.Error($"waiting");
                var html = webClient.DownloadString(inputs.Url);
                //Reporter.Error($"get response at {DateTime.Now:hh:mn:ss}");
                doc.LoadHtml(html);
            }
            catch (WebException ex)
            {
                await Task.Delay(2000);
                if (ex.ToString().Contains("(404) Not Found"))
                {
                    return new Profile { Name = "This url not found = " + inputs.Url };
                }
                else
                {
                    Reporter.Error($"unexpected Error (GetProfile func): {ex.ToString()}");
                }
                //return new Profile { Name = "session expired" };
            }
            var node = doc.DocumentNode?.SelectNodes("//div[@class='record']")?.First();
            if (node == null && !doc.DocumentNode.OuterHtml.Contains("Create Account"))
            {
                return null;
            }
            if (node == null && doc.DocumentNode.OuterHtml.Contains("Create Account"))
            {
                return new Profile { Name = "session expired" };
            }

            var name = node.SelectSingleNode(".//div[@class='name']/a")?.InnerText.Trim() ?? "";
            profile.Name = name;
            var addsress = node.SelectSingleNode(".//span[@class='street']/..")?.InnerText.Replace("\r\n", " ").Trim() ?? "";
            if (addsress != "")
            {
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex("[ ]{2,}", options);
                addsress = regex.Replace(addsress, " ");
                profile.Adress = addsress;
            }
            var emails = node.SelectNodes(".//div[@class='email']/span");
            #region Emails
            if (emails != null)
            {
                if (emails.Count == 4)
                {
                    profile.Email_1 = emails[0].InnerText;
                    profile.Email_2 = emails[1].InnerText;
                    profile.Email_3 = emails[2].InnerText;
                    profile.Email_4 = emails[3].InnerText;
                }
                if (emails.Count == 3)
                {
                    profile.Email_1 = emails[0].InnerText;
                    profile.Email_2 = emails[1].InnerText;
                    profile.Email_3 = emails[2].InnerText;
                }
                if (emails.Count == 2)
                {
                    profile.Email_1 = emails[0].InnerText;
                    profile.Email_2 = emails[1].InnerText;
                }
                if (emails.Count == 1)
                {
                    profile.Email_1 = emails[0].InnerText;
                }
            }
            #endregion
            var age = node.SelectSingleNode(".//div[@class='age']/text()[1]")?.InnerText.Trim() ?? "";
            profile.Age = age;
            return profile;
        }

        private void openInputB_Click(object sender, EventArgs e)
        {
            try
            {
                var p = new Process();
                p.StartInfo = new ProcessStartInfo(inputI.Text)
                {
                    UseShellExecute = true
                };
                p.Start();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.ToString());
            }
        }

        private void loadInputB_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog { Filter = @"TXT|*.txt", InitialDirectory = _path };
            if (o.ShowDialog() == DialogResult.OK)
            {
                inputI.Text = o.FileName;
            }
        }

        private void openOutputB_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(outputI.Text);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.ToString());
            }
        }

        private void loadOutputB_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = @"csv file|*.csv",
                Title = @"Select the output location"
            };
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                outputI.Text = saveFileDialog1.FileName;
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }
        private void LoadConfig()
        {
            try
            {
                _config = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("config.txt"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
            InitControls(this);
        }
        void InitControls(Control parent)
        {
            try
            {
                foreach (Control x in parent.Controls)
                {
                    try
                    {
                        if (x.Name.EndsWith("I"))
                        {
                            switch (x)
                            {
                                case CheckBox _:
                                    ((CheckBox)x).Checked = bool.Parse(_config[((CheckBox)x).Name]);
                                    break;
                                case RadioButton radioButton:
                                    radioButton.Checked = bool.Parse(_config[radioButton.Name]);
                                    break;
                                case TextBox _:
                                case RichTextBox _:
                                    x.Text = _config[x.Name];
                                    break;
                                case NumericUpDown numericUpDown:
                                    numericUpDown.Value = int.Parse(_config[numericUpDown.Name]);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    InitControls(x);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
            if (_profiles.Count > 0 && _profiles.Count < _urls.Count)
            {
                _profiles.Save($"Profiles{DateTime.Now:dd_MM_yyyy_hh_mm_ss}.xlsx", "sheet1");
                File.WriteAllText("the line of the last url we scraped.txt", _profiles.Count + "");
            }
        }
    }
}