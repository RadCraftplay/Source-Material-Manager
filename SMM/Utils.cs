﻿/*
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

        public static void cleanupMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
