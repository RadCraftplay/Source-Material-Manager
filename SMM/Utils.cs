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
using System.IO;

namespace SMM
{
    public static class Utils
    {
        /// <summary>
        /// Removes directory
        /// </summary>
        /// <param name="directory">Directory to remove</param>
        public static void deleteDirectory(string directory)
        {
            DirectoryInfo d = new DirectoryInfo(directory);

            foreach (FileInfo f in d.GetFiles())
                f.Delete();

            d.Delete();
        }

        /// <summary>
        /// Removes directory with subfiles
        /// </summary>
        /// <param name="directory">Directory to remove</param>
        /// <param name="removeSubDirectories">If true, removes subdirectories</param>
        public static void deleteDirectory(string directory, bool removeSubDirectories)
        {
            DirectoryInfo d = new DirectoryInfo(directory);

            foreach (FileInfo f in d.GetFiles())
                f.Delete();

            if (removeSubDirectories)
                foreach (DirectoryInfo subDir in d.GetDirectories())
                    deleteDirectory(subDir.FullName, true);

            d.Delete();
        }

        /// <summary>
        /// Cleans memory
        /// </summary>
        public static void cleanupMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
