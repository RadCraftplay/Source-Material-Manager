using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SMM
{
    public partial class Form1 : Form
    {
        public static List<IAddon> a = new List<IAddon>();
        public static Form1 form;
        
        public static string getInstallDir()
        {
            return Directory.GetCurrentDirectory();
        }

        #region Base form

        public Form1()
        {
            InitializeComponent();
            FormClosed += Form1_FormClosed;
            form = this;

            a.Add(new Addons.Addon_BaseControls());
            a.Add(new Addons.Addon_BaseMenuStripControls());
            a.Add(new Addons.Addon_BaseProject());
            a.Add(new Addons.Addon_BaseSession());
            a.Add(new Addons.Addon_BaseContextMenu());
            a.Add(new Addons.Addon_BasePluginLoader());
            a.Add(new Addons.Addon_BaseAbout());

            foreach (IAddon addon in a)
                addon.Initialize();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (IAddon addon in a)
                addon.Shutdown();
        }

        #endregion
    }
}
