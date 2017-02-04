/*
Source Material Manager
Copyright (C) 2016-2017 Distroir
Email: radcraftplay2@gmail.com

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

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

        void CheckDirectories()
        {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Distroir", "Source Material Manager")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Distroir", "Source Material Manager"));
        }

        #region Base form

        public Form1()
        {
            InitializeComponent();
            FormClosed += Form1_FormClosed;
            form = this;

            CheckDirectories();
            Config.Load();

            a.Add(new Addons.Addon_BaseControls());
            a.Add(new Addons.Addon_BaseMenuStripControls());
            a.Add(new Addons.Addon_BaseProject());
            a.Add(new Addons.Addon_BaseSession());
            a.Add(new Addons.Addon_BaseContextMenu());
            a.Add(new Addons.Addon_BasePluginLoader());
            a.Add(new Addons.Addon_BaseAbout());
            a.Add(new SMM_Updater.Addon_SMMUpdater());

            foreach (IAddon addon in a)
                addon.Initialize();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (IAddon addon in a)
                addon.Shutdown();

            Config.Save();
        }

        #endregion
    }
}
