using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace System_Uptime_Monitor
{
    public partial class FormOptions : Form
    {
        private const string RUN_LOCATION = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private const string APP_NAME = "System Uptime Monitor";
        private string APP_EXE_LOCATION = Application.ExecutablePath.ToString();

        public FormOptions()
        {
            InitializeComponent();
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {
            updateAutoStartup();
        }
        private void updateAutoStartup()
        {
            if (isAutoStartEnable() == true)
                cbAutoRun.Checked = true;
            else
                cbAutoRun.Checked = false;
        }
        private void syncAutoStartup()
        {
            if (cbAutoRun.Checked == true)
                setAutoStartup();
            else
                unsetAutoStartup();
        }


        private void cbAutoRun_CheckedChanged(object sender, EventArgs e)
        {
            syncAutoStartup();
        }

        private void setAutoStartup()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(RUN_LOCATION);
            key.SetValue(APP_NAME, APP_EXE_LOCATION);
        }
        private void unsetAutoStartup()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(RUN_LOCATION);
            key.DeleteValue(APP_NAME,false);
        }
        private bool isAutoStartEnable()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(RUN_LOCATION);

            if (key.GetValue(APP_NAME) == null)
                return false;
            else
                return true;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
