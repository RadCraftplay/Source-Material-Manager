using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Net;
using System.IO;
using System.Diagnostics;
using SMM;

namespace SMM_Updater
{
    public class Addon_SMMUpdater : IAddon
    {
        string FILE_URL = "https://www.dropbox.com/s/6xm3mvg85cmepww/version.xml?dl=1";
        string FILE_PATH = Path.Combine(Path.GetTempPath(), "SMM_latestVersion.xml");
        string SMM_VERSION;

        #region Base addon

        public AddonInfo Info
        {
            get
            {
                return new AddonInfo()
                {
                    Name = "SMM Updater",
                    Publisher = "Distroir",
                    Version = "1.0.0.0"
                };
            }
        }

        public void Initialize()
        {
            GetVersion();
            StartDownload();
        }

        public void Shutdown()
        {

        }

        #endregion

        #region Methods

        void StartDownload()
        {
            WebClient c = new WebClient();
            c.DownloadFileCompleted += C_DownloadFileCompleted;

            c.DownloadFileAsync(new Uri(FILE_URL), FILE_PATH);
        }

        void GetVersion()
        {
            SMM_VERSION = SMM.Addons.Addon_BaseControls.SMMVersion;
        }

        #endregion

        #region Events

        private void C_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                TextReader r = new StreamReader(FILE_PATH);
                XmlSerializer s = new XmlSerializer(typeof(VersionInfo));

                VersionInfo v = (VersionInfo)s.Deserialize(r);
                r.Close();
                r.Dispose();

                if (SMM_VERSION != v.Version)
                {
                    if (MessageBox.Show(string.Format("Do you want to update SMM?{0}Latest version: {1}", Environment.NewLine, v.Version), "New version available!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Process.Start(v.URL);
                    }
                }
            }
            catch { }
        }

        #endregion
    }

    public class VersionInfo
    {
        public string Version;
        public string URL;
    }
}
