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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
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
            LoadSession();
        }

        public void Shutdown()
        {
            SaveSession();
        }

        #endregion

        #region Session

        void SaveSession()
        {
            try
            {
                TextWriter w = new StreamWriter(sessionFile);
                XmlSerializer s = new XmlSerializer(typeof(Session));
                Session p = BuildSession();
                s.Serialize(w, p);

                w.Flush();
                w.Close();
                w.Dispose();
            }
            catch (Exception ex) { }
        }

        void LoadSession()
        {
            try
            {
                TextReader r = new StreamReader(sessionFile);
                XmlSerializer s = new XmlSerializer(typeof(Session));
                Session se = (Session)s.Deserialize(r);
                r.Close();
                r.Dispose();
                Form1.form.WindowState = se.WindowState;
                Form1.form.Size = se.WindowSize;
                Addon_BaseControls.splitContainer.SplitterDistance = se.Panel1Width;
            }
            catch (Exception ex) { }
        }

        Session BuildSession()
        {
            Session s = new Session()
            {
                WindowState = Form1.form.WindowState,
                WindowSize = Form1.form.Size,
                Panel1Width = Addon_BaseControls.splitContainer.Panel1.Width
            };

            return s;
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

    public class Session
    {
        public FormWindowState WindowState;
        public Size WindowSize;
        public int Panel1Width;
    }
}
