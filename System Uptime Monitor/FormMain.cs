using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// gets perfirnance counter
using System.Diagnostics;

namespace System_Uptime_Monitor
{
    public partial class FormMain : Form
    {
        private TimeSpan sysUptime;
        bool close = false;
        
        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetSysTime();
            UpdateDisplay();

            this.Text = Properties.Settings.Default.AppName + " " + Properties.Settings.Default.Version;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetSysTime();
            UpdateDisplay();
        }

        private void GetSysTime()
        {
            // gets systime and set to varibles
            PerformanceCounter upTime = new PerformanceCounter("System", "System Up Time");
            upTime.NextValue();

            sysUptime = TimeSpan.FromSeconds(upTime.NextValue());
        }
        
        private void UpdateDisplay()
        { 
            //updates weeks...sec display
            
            // week converation
            int totalDays = sysUptime.Days;
            int weeks = totalDays / 7;
            int days = totalDays;

            //update GUI
            tbWeeks.Text = Convert.ToString(weeks);
            tbDays.Text = Convert.ToString(days);
            tbHours.Text = Convert.ToString(sysUptime.Hours);
            tbMins.Text = Convert.ToString(sysUptime.Minutes);
            tbSec.Text = Convert.ToString(sysUptime.Seconds);
        }

        private void btHide_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            //close = true;
            //this.Close();
            this.Visible = false;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            //
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true;
                //bring to front if behind other form
                this.Activate();
                WindowState = FormWindowState.Normal;
                
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            close = true;
            this.Close();
            //this.Visible = false;
        }

        private void optionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOptions fmOptions = new FormOptions();
            fmOptions.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout fmAbout = new FormAbout();
            fmAbout.ShowDialog();
        }
        
        
        //context menu

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            close = true;
            this.Close();
            
        }
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOptions fmOptions = new FormOptions();
            fmOptions.ShowDialog();
        }

        private void showUptimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
        }
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormAbout fmAbout = new FormAbout();
            fmAbout.ShowDialog();
        }


        //end of form

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //prevents closing 
            if (close != true)
            {
                e.Cancel=true;
                this.Visible = false;
            }
            
        


        }

       


        

        
    }
}
