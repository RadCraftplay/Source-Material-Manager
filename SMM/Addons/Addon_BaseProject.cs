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
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SMM.Addons
{
    public class Addon_BaseProject : IAddon
    {
        #region Variables

        /// <summary>
        /// Instance of project tree view
        /// </summary>
        TreeView projectTreeView;
        /// <summary>
        /// Name of file containing list of projects
        /// </summary>
        string projectListFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Distroir", "Source Material Manager", "projects.xml");

        /// <summary>
        /// Folder browser dialog
        /// </summary>
        FolderBrowserDialog projectBrowserDialog;

        #endregion Variables

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

        /// <summary>
        /// Saves list of projects
        /// </summary>
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

        /// <summary>
        /// Gets list of projects
        /// </summary>
        /// <returns>List of projects</returns>
        public string[] GetProjects()
        {
            string[] projects = new string[projectTreeView.Nodes.Count];

            for (int i = 0; i < projectTreeView.Nodes.Count; i++)
                projects[i] = projectTreeView.Nodes[i].Text;

            return projects;
        }

        /// <summary>
        /// Loads list of projects from hard drive
        /// </summary>
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
            catch { }
        }

        #endregion

        #region Session_Directories

        /// <summary>
        /// Checks if directories to store data exist
        /// </summary>
        void CheckDirectories()
        {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Distroir", "Source Material Manager")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Distroir", "Source Material Manager"));
        }

        #endregion

        #region Menu Strip items

        /// <summary>
        /// Creates menu strip items
        /// </summary>
        void CreateMenuStripItems()
        {
            CreateOpenMenuStripItem();
        }

        /// <summary>
        /// Creates and adds "Open" menu strip item
        /// </summary>
        void CreateOpenMenuStripItem()
        {
            //Create item
            ToolStripItem i = new ToolStripMenuItem();
            i.Text = "Open directory";
            i.Image = MenuStripIcons.folder_open;
            i.Click += FolderOpenToolStripItem_Click;

            //Add item
            Addon_BaseMenuStripControls.fileMenuStripItem.DropDownItems.Add(i);
        }

        /// <summary>
        /// This method is being executed when user clicks on FolderOpenToolStripItem
        /// </summary>
        private void FolderOpenToolStripItem_Click(object sender, EventArgs e)
        {
            if (projectBrowserDialog.ShowDialog() == DialogResult.OK)
                OpenDirectory(projectBrowserDialog.SelectedPath);
        }

        #endregion

        #region Projects

        /// <summary>
        /// Builds tree of files in specified directory
        /// </summary>
        /// <param name="n">Parent node</param>
        /// <param name="directory">Directory to look for subdirectories and files</param>
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

        /// <summary>
        /// Starts building tree for specified directory
        /// </summary>
        /// <param name="directory">Directory to open</param>
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
