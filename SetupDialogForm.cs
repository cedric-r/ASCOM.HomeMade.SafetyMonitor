using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ASCOM.Utilities;
using ASCOM.HomeMade;

namespace ASCOM.HomeMade
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        public SetupDialogForm()
        {
            InitializeComponent();
            // Initialise current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here
            // Update the state variables with results from the dialogue
            try
            {
                textBoxClouds.Text = textBoxClouds.Text.Replace(",", CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
                textBoxClouds.Text = textBoxClouds.Text.Replace(".", CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
                textBoxMin.Text = textBoxMin.Text.Replace(",", CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
                textBoxMin.Text = textBoxMin.Text.Replace(".", CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
                textBoxMax.Text = textBoxMax.Text.Replace(",", CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
                textBoxMax.Text = textBoxMax.Text.Replace(".", CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
                textBoxTempOffset.Text = textBoxTempOffset.Text.Replace(",", CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
                textBoxTempOffset.Text = textBoxTempOffset.Text.Replace(".", CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
                textBoxMaxWind.Text = textBoxMaxWind.Text.Replace(".", CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
                textBoxMaxWind.Text = textBoxMaxWind.Text.Replace(",", CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
                textBoxMaxGusts.Text = textBoxMaxGusts.Text.Replace(".", CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
                textBoxMaxGusts.Text = textBoxMaxGusts.Text.Replace(",", CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);

                SafetyMonitor.comServer = (string)internetServer.Text;
                SafetyMonitor.soloServer = (string)soloServer.Text;
                SafetyMonitor.trace = chkTrace.Checked;
                SafetyMonitor.tempCloud = Convert.ToDouble(textBoxClouds.Text);
                SafetyMonitor.minTemp = Convert.ToDouble(textBoxMin.Text);
                SafetyMonitor.maxTemp = Convert.ToDouble(textBoxMax.Text);
                SafetyMonitor.tempOffset = Convert.ToDouble(textBoxTempOffset.Text);
                SafetyMonitor.limitTempHumid = checkBoxLimitTempHumid.Checked;
                SafetyMonitor.UPSURL = textBoxUPSURL.Text;
                SafetyMonitor.UPSSearch = textBoxUPSSearch.Text;
                SafetyMonitor.Internet = checkBoxInternet.Checked;
                SafetyMonitor.UPS = checkBoxUPS.Checked;
                SafetyMonitor.maxWind = Convert.ToDouble(textBoxMaxWind.Text);
                SafetyMonitor.maxGust = Convert.ToDouble(textBoxMaxGusts.Text);
                SafetyMonitor.rainSensor = Convert.ToInt32(textBoxRainSensor.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("http://ascom-standards.org/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void InitUI()
        {
            try
            { 
                chkTrace.Checked = SafetyMonitor.trace;
                textBoxClouds.Text = SafetyMonitor.tempCloud.ToString();
                textBoxMin.Text = SafetyMonitor.minTemp.ToString();
                textBoxMax.Text = SafetyMonitor.maxTemp.ToString();
                textBoxTempOffset.Text = SafetyMonitor.tempOffset.ToString();
                checkBoxLimitTempHumid.Checked = SafetyMonitor.limitTempHumid;
                checkBoxLuminosity.Checked = SafetyMonitor.limitLuminosity;
                
                textBoxUPSURL.Text = SafetyMonitor.UPSURL;
                textBoxUPSSearch.Text = SafetyMonitor.UPSSearch;
                checkBoxInternet.Checked = SafetyMonitor.Internet;
                checkBoxUPS.Checked = SafetyMonitor.UPS;
                textBoxMaxWind.Text = SafetyMonitor.maxWind.ToString();
                textBoxMaxGusts.Text = SafetyMonitor.maxGust.ToString();
                textBoxRainSensor.Text = SafetyMonitor.rainSensor.ToString();

                // set the list of com ports to those that are currently available
                if (String.IsNullOrEmpty(SafetyMonitor.comServer))
                {
                    internetServer.Text = SafetyMonitor.comServerDefault;
                }
                else
                {
                    internetServer.Text = SafetyMonitor.comServer;
                }
                if (String.IsNullOrEmpty(SafetyMonitor.comServer))
                {
                    soloServer.Text = SafetyMonitor.soloServerDefault;
                }
                else
                {
                    soloServer.Text = SafetyMonitor.soloServer;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SetupDialogForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}