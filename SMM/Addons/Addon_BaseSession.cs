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
