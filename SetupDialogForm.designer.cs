namespace ASCOM.HomeMade
{
    partial class SetupDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.internetServer = new System.Windows.Forms.TextBox();
            this.soloServer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxClouds = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxMax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxTempOffset = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBoxLimitTempHumid = new System.Windows.Forms.CheckBox();
            this.checkBoxLuminosity = new System.Windows.Forms.CheckBox();
            this.textBoxMaxWind = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxMaxGusts = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBoxUPS = new System.Windows.Forms.CheckBox();
            this.checkBoxInternet = new System.Windows.Forms.CheckBox();
            this.textBoxUPSURL = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxUPSSearch = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxRainSensor = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(285, 430);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(59, 24);
            this.cmdOK.TabIndex = 9;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(285, 461);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(59, 25);
            this.cmdCancel.TabIndex = 10;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "SafetyManager properties";
            // 
            // picASCOM
            // 
            this.picASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picASCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picASCOM.Image = global::ASCOM.HomeMade.Properties.Resources.ASCOM;
            this.picASCOM.Location = new System.Drawing.Point(296, 9);
            this.picASCOM.Name = "picASCOM";
            this.picASCOM.Size = new System.Drawing.Size(48, 56);
            this.picASCOM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picASCOM.TabIndex = 3;
            this.picASCOM.TabStop = false;
            this.picASCOM.Click += new System.EventHandler(this.BrowseToAscom);
            this.picASCOM.DoubleClick += new System.EventHandler(this.BrowseToAscom);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Internet Server";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // chkTrace
            // 
            this.chkTrace.AutoSize = true;
            this.chkTrace.Location = new System.Drawing.Point(109, 456);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(69, 17);
            this.chkTrace.TabIndex = 9;
            this.chkTrace.Text = "Trace on";
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // internetServer
            // 
            this.internetServer.Location = new System.Drawing.Point(109, 85);
            this.internetServer.Name = "internetServer";
            this.internetServer.Size = new System.Drawing.Size(100, 20);
            this.internetServer.TabIndex = 2;
            this.internetServer.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // soloServer
            // 
            this.soloServer.Location = new System.Drawing.Point(109, 59);
            this.soloServer.Name = "soloServer";
            this.soloServer.Size = new System.Drawing.Size(100, 20);
            this.soloServer.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "SOLO";
            // 
            // textBoxClouds
            // 
            this.textBoxClouds.Location = new System.Drawing.Point(109, 166);
            this.textBoxClouds.Name = "textBoxClouds";
            this.textBoxClouds.Size = new System.Drawing.Size(43, 20);
            this.textBoxClouds.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Max temp clouds";
            // 
            // textBoxMin
            // 
            this.textBoxMin.Location = new System.Drawing.Point(109, 218);
            this.textBoxMin.Name = "textBoxMin";
            this.textBoxMin.Size = new System.Drawing.Size(43, 20);
            this.textBoxMin.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Min temp";
            // 
            // textBoxMax
            // 
            this.textBoxMax.Location = new System.Drawing.Point(109, 192);
            this.textBoxMax.Name = "textBoxMax";
            this.textBoxMax.Size = new System.Drawing.Size(43, 20);
            this.textBoxMax.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Max temp";
            // 
            // textBoxTempOffset
            // 
            this.textBoxTempOffset.Location = new System.Drawing.Point(109, 267);
            this.textBoxTempOffset.Name = "textBoxTempOffset";
            this.textBoxTempOffset.Size = new System.Drawing.Size(43, 20);
            this.textBoxTempOffset.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 270);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Temp limit offfset";
            // 
            // checkBoxLimitTempHumid
            // 
            this.checkBoxLimitTempHumid.AutoSize = true;
            this.checkBoxLimitTempHumid.Location = new System.Drawing.Point(109, 244);
            this.checkBoxLimitTempHumid.Name = "checkBoxLimitTempHumid";
            this.checkBoxLimitTempHumid.Size = new System.Drawing.Size(106, 17);
            this.checkBoxLimitTempHumid.TabIndex = 18;
            this.checkBoxLimitTempHumid.Text = "Limit temp/humid";
            this.checkBoxLimitTempHumid.UseVisualStyleBackColor = true;
            // 
            // checkBoxLuminosity
            // 
            this.checkBoxLuminosity.AutoSize = true;
            this.checkBoxLuminosity.Location = new System.Drawing.Point(109, 398);
            this.checkBoxLuminosity.Name = "checkBoxLuminosity";
            this.checkBoxLuminosity.Size = new System.Drawing.Size(95, 17);
            this.checkBoxLuminosity.TabIndex = 19;
            this.checkBoxLuminosity.Text = "Luminosity limit";
            this.checkBoxLuminosity.UseVisualStyleBackColor = true;
            // 
            // textBoxMaxWind
            // 
            this.textBoxMaxWind.Location = new System.Drawing.Point(109, 294);
            this.textBoxMaxWind.Name = "textBoxMaxWind";
            this.textBoxMaxWind.Size = new System.Drawing.Size(43, 20);
            this.textBoxMaxWind.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 297);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Max wind";
            // 
            // textBoxMaxGusts
            // 
            this.textBoxMaxGusts.Location = new System.Drawing.Point(109, 320);
            this.textBoxMaxGusts.Name = "textBoxMaxGusts";
            this.textBoxMaxGusts.Size = new System.Drawing.Size(43, 20);
            this.textBoxMaxGusts.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 323);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Max gusts";
            // 
            // checkBoxUPS
            // 
            this.checkBoxUPS.AutoSize = true;
            this.checkBoxUPS.Location = new System.Drawing.Point(109, 375);
            this.checkBoxUPS.Name = "checkBoxUPS";
            this.checkBoxUPS.Size = new System.Drawing.Size(48, 17);
            this.checkBoxUPS.TabIndex = 24;
            this.checkBoxUPS.Text = "UPS";
            this.checkBoxUPS.UseVisualStyleBackColor = true;
            // 
            // checkBoxInternet
            // 
            this.checkBoxInternet.AutoSize = true;
            this.checkBoxInternet.Location = new System.Drawing.Point(109, 421);
            this.checkBoxInternet.Name = "checkBoxInternet";
            this.checkBoxInternet.Size = new System.Drawing.Size(62, 17);
            this.checkBoxInternet.TabIndex = 25;
            this.checkBoxInternet.Text = "Internet";
            this.checkBoxInternet.UseVisualStyleBackColor = true;
            // 
            // textBoxUPSURL
            // 
            this.textBoxUPSURL.Location = new System.Drawing.Point(109, 111);
            this.textBoxUPSURL.Name = "textBoxUPSURL";
            this.textBoxUPSURL.Size = new System.Drawing.Size(100, 20);
            this.textBoxUPSURL.TabIndex = 26;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "UPS URL";
            // 
            // textBoxUPSSearch
            // 
            this.textBoxUPSSearch.Location = new System.Drawing.Point(109, 137);
            this.textBoxUPSSearch.Name = "textBoxUPSSearch";
            this.textBoxUPSSearch.Size = new System.Drawing.Size(100, 20);
            this.textBoxUPSSearch.TabIndex = 28;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 140);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 29;
            this.label11.Text = "UPS Search";
            // 
            // textBoxRainSensor
            // 
            this.textBoxRainSensor.Location = new System.Drawing.Point(109, 349);
            this.textBoxRainSensor.Name = "textBoxRainSensor";
            this.textBoxRainSensor.Size = new System.Drawing.Size(43, 20);
            this.textBoxRainSensor.TabIndex = 30;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 352);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 13);
            this.label12.TabIndex = 31;
            this.label12.Text = "Rain sensor min";
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 494);
            this.Controls.Add(this.textBoxRainSensor);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBoxUPSSearch);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxUPSURL);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.checkBoxInternet);
            this.Controls.Add(this.checkBoxUPS);
            this.Controls.Add(this.textBoxMaxGusts);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxMaxWind);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.checkBoxLuminosity);
            this.Controls.Add(this.checkBoxLimitTempHumid);
            this.Controls.Add(this.textBoxTempOffset);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxMax);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxMin);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxClouds);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.soloServer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.internetServer);
            this.Controls.Add(this.chkTrace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picASCOM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASCOM.HomeMade.SafetyManager Setup";
            this.Load += new System.EventHandler(this.SetupDialogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.TextBox internetServer;
        private System.Windows.Forms.TextBox soloServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxClouds;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxMax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxTempOffset;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBoxLimitTempHumid;
        private System.Windows.Forms.CheckBox checkBoxLuminosity;
        private System.Windows.Forms.TextBox textBoxMaxWind;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxMaxGusts;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBoxUPS;
        private System.Windows.Forms.CheckBox checkBoxInternet;
        private System.Windows.Forms.TextBox textBoxUPSURL;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxUPSSearch;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxRainSensor;
        private System.Windows.Forms.Label label12;
    }
}