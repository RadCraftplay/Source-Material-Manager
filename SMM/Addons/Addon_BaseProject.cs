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
using System.Xml.Serialization;
using System.IO;

namespace SMM.Addons
{
    public class Addon_BaseProject : IAddon
    {
        TreeView projectTreeView;
        string projectListFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Distroir", "Source Material Manager", "projects.xml");

        FolderBrowserDialog projectBrowserDialog;

        #region BaseAddon

        public AddonInfo Info
        {
            get
            {
                return new AddonInfo() { Name = "Base project", Publisher = "Distroir", Version = "beta 1" };
            }
        }

        public void Initialize()
        {
            projectTreeView = Addon_BaseControls.treeView;
            projectBrowserDialog = new FolderBrowserDialog();

            CheckDirectories();
            LoadProjects();
            CreateMenuStripItems();
            
        }

        public void Shutdown()
        {
            CheckDirectories();
            SaveProjects();
        }

        #endregion

        #region Session

        public void SaveProjects()
        {
            TextWriter w = new StreamWriter(projectListFile);
            XmlSerializer s = new XmlSerializer(typeof(string[]));
            string[] p = GetProjects();
            s.Serialize(w, p);

            w.Flush();
            w.Close();
            w.Dispose();
        }

        public string[] GetProjects()
        {
            string[] projects = new string[projectTreeView.Nodes.Count];

            for (int i = 0; i < projectTreeView.Nodes.Count; i++)
                projects[i] = projectTreeView.Nodes[i].Text;

            return projects;
        }

        public void LoadProjects()
        {
            try
            {
                TextReader r = new StreamReader(projectListFile);
                XmlSerializer s = new XmlSerializer(typeof(string[]));
                string[] projects = (string[])s.Deserialize(r);
                foreach (string p in projects)
                    OpenDirectory(p);

                r.Close();
                r.Dispose();
            }
            catch (Exception ex) { }
        }

        #endregion

        #region Session_Directories


        void CheckDirectories()
        {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Distroir", "Source Material Manager")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Distroir", "Source Material Manager"));
        }


        #endregion

        #region Menu Strip items

        void CreateMenuStripItems()
        {
            CreateOpenMenuStripItem();
        }

        void CreateOpenMenuStripItem()
        {
            ToolStripItem i = new ToolStripMenuItem();
            i.Text = "Open directory";
            i.Image = MenuStripIcons.folder_open;
            i.Click += FolderOpenToolStripItem_Click;

            Addon_BaseMenuStripControls.fileMenuStripItem.DropDownItems.Add(i);
        }

        private void FolderOpenToolStripItem_Click(object sender, EventArgs e)
        {
            if (projectBrowserDialog.ShowDialog() == DialogResult.OK)
                OpenDirectory(projectBrowserDialog.SelectedPath);
        }

        #endregion

        #region Projects

        void LoadDir(TreeNode n, string directory)
        {
            DirectoryInfo i = new DirectoryInfo(directory);

            foreach (DirectoryInfo d in i.GetDirectories())
            {
                TreeNode tn = n.Nodes.Add(d.Name, d.Name, 0, 1);
                LoadDir(tn, d.FullName);
            }

            foreach (FileInfo f in i.GetFiles())
            {
                if (f.Name.EndsWith(".txt"))
                    n.Nodes.Add(f.Name, f.Name, 3, 3);
                else if (f.Name.EndsWith(".vtf"))
                    n.Nodes.Add(f.Name, f.Name, 5, 5);
                else if (f.Name.EndsWith(".vmt"))
                    n.Nodes.Add(f.Name, f.Name, 4, 4);
                else
                    n.Nodes.Add(f.Name, f.Name, 2, 2);
            }
        }

        #endregion

        #region IO

        void OpenDirectory(string directory)
        {
            TreeNode n = projectTreeView.Nodes.Add(directory);
            n.ImageIndex = 0;
            n.SelectedImageIndex = 1;
            LoadDir(n, directory);
        }

        #endregion
    }

    public class Project
    {
        public string folder;
    }
}
