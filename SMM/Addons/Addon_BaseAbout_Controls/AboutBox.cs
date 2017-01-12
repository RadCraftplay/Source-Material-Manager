using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace SMM.Addons.Addon_BaseAbout_Controls
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            IntPtr Hicon = MenuStripIcons.information.GetHicon();
            Icon = Icon.FromHandle(Hicon);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://p.yusukekamiyamane.com/");
        }
    }
}
