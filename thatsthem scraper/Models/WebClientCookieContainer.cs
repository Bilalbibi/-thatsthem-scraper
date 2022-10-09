using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace thatsthem_scraper.Models
{
    public class WebClientCookieContainer : WebClient
    {
        public CookieContainer cookies = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);

            if (request is HttpWebRequest)
                ((HttpWebRequest)request).CookieContainer = cookies;

            return request;
        }
    }
}
