using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SMM
{
    public static class Utils
    {
        public static void deleteDirectory(string directory)
        {
            DirectoryInfo d = new DirectoryInfo(directory);

            foreach (FileInfo f in d.GetFiles())
                f.Delete();

            d.Delete();
        }

        public static void deleteDirectory(string directory, bool removeSobDirectories)
        {
            DirectoryInfo d = new DirectoryInfo(directory);

            foreach (FileInfo f in d.GetFiles())
                f.Delete();

            if (removeSobDirectories)
                foreach (DirectoryInfo subDir in d.GetDirectories())
                    deleteDirectory(subDir.FullName, true);

            d.Delete();
        }
    }
}
