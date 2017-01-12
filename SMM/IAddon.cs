using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMM
{
    public interface IAddon
    {
        AddonInfo Info { get; }

        void Initialize();

        void Shutdown();
    }

    public class AddonInfo
    {
        public string Name;
        public string Publisher;
        public string Version;
        public string Description;
    }
}
