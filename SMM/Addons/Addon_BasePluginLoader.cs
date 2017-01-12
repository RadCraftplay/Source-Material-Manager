using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace SMM.Addons
{
    class Addon_BasePluginLoader : IAddon
    {
        public static List<IAddon> Plugins = new List<IAddon>();
        string pluginsDir()
        {
            return Form1.getInstallDir() + "\\plugins";
        }

        #region Base Addon

        public AddonInfo Info
        {
            get
            {
                return new AddonInfo() { Name = "Base plugin loader", Publisher = "Distroir", Version = "beta 1" };
            }
        }

        public void Initialize()
        {
            LoadPlugins();

            foreach (IAddon a in Plugins)
                a.Initialize();
        }

        public void Shutdown()
        {
            foreach (IAddon a in Plugins)
                a.Shutdown();
        }

        #endregion

        private static object CreateInstance(string assemblyName, string className)
        {
            var assembly = Assembly.LoadFile(assemblyName);

            var type = assembly.GetTypes()
                .First(t => t.Name == className);

            return Activator.CreateInstance(type);
        }

        void LoadPlugins()
        {
            if (!Directory.Exists(pluginsDir()))
                Directory.CreateDirectory(pluginsDir());

            DirectoryInfo d = new DirectoryInfo(pluginsDir());

            foreach (FileInfo i in d.GetFiles())
            {
                object instance = CreateInstance(i.FullName, "Addon");
                IAddon a = (IAddon)instance;
                Plugins.Add(a);
            }
        }
    }
}
