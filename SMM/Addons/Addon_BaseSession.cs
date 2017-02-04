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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SMM.Addons
{
    public class Addon_BaseSession : IAddon
    {
        string sessionFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Distroir", "Source Material Manager", "session.xml");

        #region Base Addon

        public AddonInfo Info
        {
            get
            {
                return new AddonInfo() { Name = "Base session", Publisher = "Distroir", Version = "beta 1" };
            }
        }

        public void Initialize()
        {
            CheckDirectories();
            Config.Load();
            LoadSession();
        }

        public void Shutdown()
        {
            SaveSession();
            Config.Save();
        }

        #endregion

        #region Session

        void SaveSession()
        {
            try
            {
                Config.AddVariable("Session_FormWindowState", (Int32)Form1.form.WindowState);
                Config.AddVariable("Session_FormWindowWidth", Form1.form.Size.Width);
                Config.AddVariable("Session_FormWindowHeight", Form1.form.Size.Height);
                Config.AddVariable("Session_SplitterDistance", Addon_BaseControls.splitContainer.SplitterDistance);
            }
            catch { }
        }

        void LoadSession()
        {
            try
            {
                Form1.form.WindowState = (FormWindowState)Config.ReadInt("Session_FormWindowState");
                Form1.form.Size = new Size(Config.ReadInt("Session_FormWindowWidth"), Config.ReadInt("Session_FormWindowHeight"));
                Addon_BaseControls.splitContainer.SplitterDistance = Config.ReadInt("Session_SplitterDistance");
            }
            catch { }
        }

        #endregion

        #region Session_Directories


        void CheckDirectories()
        {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Distroir", "Source Material Manager")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Distroir", "Source Material Manager"));
        }


        #endregion
    }
}
