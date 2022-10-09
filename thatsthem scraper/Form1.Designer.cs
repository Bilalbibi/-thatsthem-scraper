namespace thatsthem_scraper
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.KeyApI = new System.Windows.Forms.TextBox();
            this.SaveScrapedData = new System.Windows.Forms.CheckBox();
            this.OpenFile = new System.Windows.Forms.CheckBox();
            this.openOutputB = new System.Windows.Forms.Button();
            this.openInputB = new System.Windows.Forms.Button();
            this.startB = new System.Windows.Forms.Button();
            this.outputI = new System.Windows.Forms.TextBox();
            this.loadOutputB = new System.Windows.Forms.Button();
            this.inputI = new System.Windows.Forms.TextBox();
            this.loadInputB = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DebugT = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ProgressB = new System.Windows.Forms.ProgressBar();
            this.displayT = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(816, 378);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.KeyApI);
            this.tabPage1.Controls.Add(this.SaveScrapedData);
            this.tabPage1.Controls.Add(this.OpenFile);
            this.tabPage1.Controls.Add(this.openOutputB);
            this.tabPage1.Controls.Add(this.openInputB);
            this.tabPage1.Controls.Add(this.startB);
            this.tabPage1.Controls.Add(this.outputI);
            this.tabPage1.Controls.Add(this.loadOutputB);
            this.tabPage1.Controls.Add(this.inputI);
            this.tabPage1.Controls.Add(this.loadInputB);
            this.tabPage1.ForeColor = System.Drawing.Color.Black;
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(808, 350);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Option";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(165, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "API Key :";
            // 
            // KeyApI
            // 
            this.KeyApI.Location = new System.Drawing.Point(235, 97);
            this.KeyApI.Name = "KeyApI";
            this.KeyApI.PasswordChar = '*';
            this.KeyApI.Size = new System.Drawing.Size(168, 23);
            this.KeyApI.TabIndex = 14;
            // 
            // SaveScrapedData
            // 
            this.SaveScrapedData.AutoSize = true;
            this.SaveScrapedData.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.SaveScrapedData.Location = new System.Drawing.Point(461, 323);
            this.SaveScrapedData.Name = "SaveScrapedData";
            this.SaveScrapedData.Size = new System.Drawing.Size(344, 21);
            this.SaveScrapedData.TabIndex = 13;
            this.SaveScrapedData.Text = "Records The scraped profiles Before the unexpected error";
            this.SaveScrapedData.UseVisualStyleBackColor = true;
            // 
            // OpenFile
            // 
            this.OpenFile.AutoSize = true;
            this.OpenFile.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.OpenFile.Location = new System.Drawing.Point(461, 296);
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(250, 21);
            this.OpenFile.TabIndex = 12;
            this.OpenFile.Text = "Open the output file  as soon as it ready";
            this.OpenFile.UseVisualStyleBackColor = true;
            // 
            // openOutputB
            // 
            this.openOutputB.ForeColor = System.Drawing.Color.SteelBlue;
            this.openOutputB.Location = new System.Drawing.Point(635, 198);
            this.openOutputB.Name = "openOutputB";
            this.openOutputB.Size = new System.Drawing.Size(89, 23);
            this.openOutputB.TabIndex = 11;
            this.openOutputB.Text = "Open Output";
            this.openOutputB.UseVisualStyleBackColor = true;
            this.openOutputB.Visible = false;
            this.openOutputB.Click += new System.EventHandler(this.openOutputB_Click);
            // 
            // openInputB
            // 
            this.openInputB.ForeColor = System.Drawing.Color.SteelBlue;
            this.openInputB.Location = new System.Drawing.Point(635, 145);
            this.openInputB.Name = "openInputB";
            this.openInputB.Size = new System.Drawing.Size(89, 23);
            this.openInputB.TabIndex = 10;
            this.openInputB.Text = "Open Input";
            this.openInputB.UseVisualStyleBackColor = true;
            this.openInputB.Click += new System.EventHandler(this.openInputB_Click);
            // 
            // startB
            // 
            this.startB.ForeColor = System.Drawing.Color.SteelBlue;
            this.startB.Location = new System.Drawing.Point(258, 307);
            this.startB.Name = "startB";
            this.startB.Size = new System.Drawing.Size(96, 23);
            this.startB.TabIndex = 9;
            this.startB.Text = "Start";
            this.startB.UseVisualStyleBackColor = true;
            this.startB.Click += new System.EventHandler(this.Start);
            // 
            // outputI
            // 
            this.outputI.Location = new System.Drawing.Point(38, 198);
            this.outputI.Name = "outputI";
            this.outputI.Size = new System.Drawing.Size(458, 23);
            this.outputI.TabIndex = 8;
            this.outputI.Visible = false;
            // 
            // loadOutputB
            // 
            this.loadOutputB.ForeColor = System.Drawing.Color.SteelBlue;
            this.loadOutputB.Location = new System.Drawing.Point(530, 198);
            this.loadOutputB.Name = "loadOutputB";
            this.loadOutputB.Size = new System.Drawing.Size(82, 23);
            this.loadOutputB.TabIndex = 7;
            this.loadOutputB.Text = "Output file";
            this.loadOutputB.UseVisualStyleBackColor = true;
            this.loadOutputB.Visible = false;
            this.loadOutputB.Click += new System.EventHandler(this.loadOutputB_Click);
            // 
            // inputI
            // 
            this.inputI.Location = new System.Drawing.Point(38, 146);
            this.inputI.Name = "inputI";
            this.inputI.Size = new System.Drawing.Size(458, 23);
            this.inputI.TabIndex = 6;
            // 
            // loadInputB
            // 
            this.loadInputB.ForeColor = System.Drawing.Color.SteelBlue;
            this.loadInputB.Location = new System.Drawing.Point(530, 145);
            this.loadInputB.Name = "loadInputB";
            this.loadInputB.Size = new System.Drawing.Size(82, 23);
            this.loadInputB.TabIndex = 5;
            this.loadInputB.Text = "Input file";
            this.loadInputB.UseVisualStyleBackColor = true;
            this.loadInputB.Click += new System.EventHandler(this.loadInputB_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DebugT);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(808, 350);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DebugT
            // 
            this.DebugT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DebugT.Location = new System.Drawing.Point(3, 3);
            this.DebugT.Name = "DebugT";
            this.DebugT.Size = new System.Drawing.Size(802, 344);
            this.DebugT.TabIndex = 0;
            this.DebugT.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ProgressB);
            this.panel1.Location = new System.Drawing.Point(16, 414);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(812, 30);
            this.panel1.TabIndex = 1;
            // 
            // ProgressB
            // 
            this.ProgressB.Location = new System.Drawing.Point(3, 5);
            this.ProgressB.Name = "ProgressB";
            this.ProgressB.Size = new System.Drawing.Size(805, 23);
            this.ProgressB.TabIndex = 0;
            // 
            // displayT
            // 
            this.displayT.AutoSize = true;
            this.displayT.ForeColor = System.Drawing.Color.SteelBlue;
            this.displayT.Location = new System.Drawing.Point(21, 389);
            this.displayT.Name = "displayT";
            this.displayT.Size = new System.Drawing.Size(52, 15);
            this.displayT.TabIndex = 2;
            this.displayT.Text = "Progress";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(840, 450);
            this.Controls.Add(this.displayT);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ForeColor = System.Drawing.Color.Teal;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "  thatsthem.com scraper 1.03";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Panel panel1;
        private ProgressBar ProgressB;
        private Button startB;
        private TextBox outputI;
        private Button loadOutputB;
        private TextBox inputI;
        private Button loadInputB;
        private Label displayT;
        private RichTextBox DebugT;
        private Button openOutputB;
        private Button openInputB;
        private CheckBox OpenFile;
        private CheckBox SaveScrapedData;
        private TextBox KeyApI;
        private Label label1;
    }
}