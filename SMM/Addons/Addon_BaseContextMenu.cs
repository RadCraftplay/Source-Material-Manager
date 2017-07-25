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
using Ionic.Zip;
using SMM.Addons.Addon_BaseProject_Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMM.Addons
{
    public class Addon_BaseContextMenu : IAddon
    {
        #region Variables

        /// <summary>
        /// Instance of project tree view
        /// </summary>
        TreeView projectTreeView;
        /// <summary>
        /// Context menu strip for project tree view
        /// </summary>
        ContextMenuStrip projectTreeViewContextMenuStrip;

        ToolStripMenuItem addContextStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem importContextStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem renameContextStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem deleteContextStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem closeProjectContextStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem exportProjectContextStripMenuItem = new ToolStripMenuItem();

        TreeNode lastNode;

        #endregion

        #region Base Addon

        public AddonInfo Info
        {
            get
            {
                return new AddonInfo() { Name = "Base context menu", Publisher = "Distroir", Version = "beta 1" };
            }
        }

        public void Initialize()
        {
            projectTreeView = Addon_BaseControls.treeView;
            CreateContextMenuStrip();
        }

        public void Shutdown()
        {
            
        }

        #endregion

        #region TreeViewContextMenu

        /// <summary>
        /// Creates context menu strip
        /// </summary>
        void CreateContextMenuStrip()
        {
            projectTreeViewContextMenuStrip = new ContextMenuStrip();
            projectTreeView.ContextMenuStrip = projectTreeViewContextMenuStrip;
            projectTreeView.MouseClick += ProjectTreeView_MouseClick;
            projectTreeView.MouseDoubleClick += ProjectTreeView_MouseDoubleClick;
            CreateContextMenuStripItems();
        }

        /// <summary>
        /// This method executes every time when user double-clicks on project tree view
        /// </summary>
        private void ProjectTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Open file associated with selected node
            OpenFile();
        }

        /// <summary>
        /// This method executes every time when user clicks on project tree view
        /// </summary>
        private void ProjectTreeView_MouseClick(object sender, MouseEventArgs e)
        {
            //Select node on rmb click
            if (e.Button == MouseButtons.Right)
                projectTreeView.SelectedNode = projectTreeView.GetNodeAt(e.Location);

            //Manage context menu items depending on file extension
            if (IsHeadNode(projectTreeView.SelectedNode)) //Project menu items
            {
                addContextStripMenuItem.Visible = true;
                importContextStripMenuItem.Visible = true;
                renameContextStripMenuItem.Visible = false;
                deleteContextStripMenuItem.Visible = false;
                closeProjectContextStripMenuItem.Visible = true;
                exportProjectContextStripMenuItem.Visible = true;
            }
            else if (projectTreeView.SelectedNode.ImageIndex == 0) //Directory
            {
                addContextStripMenuItem.Visible = true;
                importContextStripMenuItem.Visible = true;
                renameContextStripMenuItem.Visible = true;
                deleteContextStripMenuItem.Visible = true;
                closeProjectContextStripMenuItem.Visible = false;
                exportProjectContextStripMenuItem.Visible = false;
            }
            else if (projectTreeView.SelectedNode.ImageIndex == 4) //VMT
            {
                addContextStripMenuItem.Visible = false;
                importContextStripMenuItem.Visible = false;
                renameContextStripMenuItem.Visible = true;
                deleteContextStripMenuItem.Visible = true;
                closeProjectContextStripMenuItem.Visible = false;
                exportProjectContextStripMenuItem.Visible = false;
            }
            else if (projectTreeView.SelectedNode.ImageIndex == 5) //VTF
            {
                addContextStripMenuItem.Visible = false;
                importContextStripMenuItem.Visible = false;
                renameContextStripMenuItem.Visible = true;
                deleteContextStripMenuItem.Visible = true;
                closeProjectContextStripMenuItem.Visible = false;
                exportProjectContextStripMenuItem.Visible = false;
            }
            else //Other files and directories
            {
                addContextStripMenuItem.Visible = false;
                importContextStripMenuItem.Visible = false;
                renameContextStripMenuItem.Visible = true;
                deleteContextStripMenuItem.Visible = true;
                closeProjectContextStripMenuItem.Visible = false;
                exportProjectContextStripMenuItem.Visible = false;
            }
        }

        /// <summary>
        /// This function creates context menu strip items
        /// </summary>
        void CreateContextMenuStripItems()
        {
            addContextStripMenuItem.Image = TreeViewIcons.document__plus;
            addContextStripMenuItem.Text = "New file";
            addContextStripMenuItem.Click += AddItem_Click;
            addContextStripMenuItem.Visible = false;
            projectTreeViewContextMenuStrip.Items.Add(addContextStripMenuItem);

            importContextStripMenuItem.Image = TreeViewIcons.document_import;
            importContextStripMenuItem.Text = "Import file";
            importContextStripMenuItem.Click += ImportItem_Click;
            importContextStripMenuItem.Visible = false;
            projectTreeViewContextMenuStrip.Items.Add(importContextStripMenuItem);

            renameContextStripMenuItem.Image = TreeViewIcons.pencil;
            renameContextStripMenuItem.Text = "Rename";
            renameContextStripMenuItem.Click += RenameItem_Click;
            renameContextStripMenuItem.Visible = false;
            projectTreeViewContextMenuStrip.Items.Add(renameContextStripMenuItem);

            deleteContextStripMenuItem.Image = TreeViewIcons.cross_script;
            deleteContextStripMenuItem.Text = "Delete";
            deleteContextStripMenuItem.Click += DeleteItem_Click;
            deleteContextStripMenuItem.Visible = false;
            projectTreeViewContextMenuStrip.Items.Add(deleteContextStripMenuItem);

            closeProjectContextStripMenuItem.Image = TreeViewIcons.cross_script;
            closeProjectContextStripMenuItem.Text = "Close project";
            closeProjectContextStripMenuItem.Click += CloseProjectContextStripMenuItem_Click;
            closeProjectContextStripMenuItem.Visible = false;
            projectTreeViewContextMenuStrip.Items.Add(closeProjectContextStripMenuItem);

            exportProjectContextStripMenuItem.Image = TreeViewIcons.folder_export;
            exportProjectContextStripMenuItem.Text = "Export project";
            exportProjectContextStripMenuItem.Click += ExportProjectContextStripMenuItem_Click;
            exportProjectContextStripMenuItem.Visible = false;
            projectTreeViewContextMenuStrip.Items.Add(exportProjectContextStripMenuItem);
        }

        private void RenameItem_Click(object sender, EventArgs e)
        {
            if (projectTreeView.SelectedNode.ImageIndex == 0)
            {
                //If selected node is directory rename directory
                var v = new Addon_BaseContextMenu_Controls.RenameFileDialog(new DirectoryInfo(projectTreeView.SelectedNode.FullPath), projectTreeView.SelectedNode);
                v.ShowDialog();
            }
            else
            {
                //Ortherwise rename file
                var v = new Addon_BaseContextMenu_Controls.RenameFileDialog(new FileInfo(projectTreeView.SelectedNode.FullPath), projectTreeView.SelectedNode);
                v.ShowDialog();
            }
        }

        private void ImportItem_Click(object sender, EventArgs e)
        {
            //Create open file dialog
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All supported files|*.txt;*.vmt;*.vtf";
            ofd.Multiselect = true;

            //Show dialog
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                TreeNode n = projectTreeView.SelectedNode;
                string destination = null;

                if (n.ImageIndex == 0) //If node is directory
                {
                    destination = n.FullPath;
                }
                else //If node is file
                {
                    FileInfo f = new FileInfo(n.FullPath);
                    destination = f.Directory.FullName;
                }

                foreach (string s in ofd.FileNames) //Import all selected files
                {
                    FileInfo i = new FileInfo(s);
                    File.Copy(i.FullName, Path.Combine(destination, i.Name), false);
                    AddNewTreeNode(i);
                }
            }
        }

        private void AddItem_Click(object sender, EventArgs e)
        {
            var v = new Addon_BaseContextMenu_Controls.AddFileDialog(projectTreeView.SelectedNode);
            v.ShowDialog();
        }

        private async void ExportProjectContextStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Zip file|*.zip";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //Export ZIP archive
                TreeNode n = projectTreeView.SelectedNode;
                await Task.Run(() => PakZip(sfd.FileName, n));
                //Inform user that exporting finished
                MessageBox.Show("Exporting finished!");
            }
        }

        private void CloseProjectContextStripMenuItem_Click(object sender, EventArgs e)
        {
            projectTreeView.SelectedNode.Remove();
        }

        private void DeleteItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to remove selected item(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (projectTreeView.SelectedNode.ImageIndex == 0 || projectTreeView.SelectedNode.ImageIndex == 1)
                    Utils.deleteDirectory(projectTreeView.SelectedNode.FullPath, true);
                else
                    File.Delete(projectTreeView.SelectedNode.FullPath);
                projectTreeView.SelectedNode.Remove();
            }
        }

        #endregion
        
        #region Opening Files

        /// <summary>
        /// Opens file associated with selected node
        /// </summary>
        async void OpenFile()
        {
            if (!(projectTreeView.SelectedNode.ImageIndex == 0))
            {
                //Close all controls in panel on right side of screen
                ClosePanelControls();
                //Set last node
                lastNode = projectTreeView.SelectedNode;

                //Check for known extensions
                if (projectTreeView.SelectedNode.Text.EndsWith(".txt"))
                {
                    //Open file and create control
                    var tb = new TextEditor(Addon_BaseControls.treeView.SelectedNode.FullPath);
                    Addon_BaseControls.panel.Controls.Add(tb);
                    tb.Dock = DockStyle.Fill;
                }
                else if (projectTreeView.SelectedNode.Text.EndsWith(".vmt"))
                {
                    //Open file and create control
                    var tb = new TextEditor(Addon_BaseControls.treeView.SelectedNode.FullPath);
                    Addon_BaseControls.panel.Controls.Add(tb);
                    tb.Dock = DockStyle.Fill;
                }
                else if (projectTreeView.SelectedNode.Text.EndsWith(".vtf"))
                {
                    //Load file
                    string path = Addon_BaseControls.treeView.SelectedNode.FullPath;
                    await Task.Run(() => LoadVTF(path));

                    //Create control
                    VTFViewer v = new VTFViewer();
                    Addon_BaseControls.panel.Controls.Add(v);
                    v.Dock = DockStyle.Fill;
                    v.pictureBox1.Image = vtfImg;
                }
                else { } //Does nothing
            }
        }

        Image vtfImg;

        unsafe void LoadVTF(string filename)
        {
            uint u = new uint();

            VtfLib.vlInitialize();
            VtfLib.vlCreateImage(&u);
            VtfLib.vlBindImage(u);
            VtfLib.vlImageLoad(filename, false);

            byte[] lpImageData = new byte[VtfLib.vlImageComputeImageSize(VtfLib.vlImageGetWidth(), VtfLib.vlImageGetHeight(), 1, 1, VtfLib.ImageFormat.ImageFormatRGBA8888)];
            fixed (byte* lpOutput = lpImageData)
            {
                if (!VtfLib.vlImageConvert(VtfLib.vlImageGetData(0, 0, 0, 0), lpOutput, VtfLib.vlImageGetWidth(), VtfLib.vlImageGetHeight(), VtfLib.vlImageGetFormat(), VtfLib.ImageFormat.ImageFormatRGBA8888))
                {
                    throw new FormatException(VtfLib.vlGetLastError());
                }
            }

            Bitmap b = new Bitmap((int)VtfLib.vlImageGetWidth(), (int)VtfLib.vlImageGetHeight());

            uint uiIndex = 0;
            for (int y = 0; y < b.Height; y++)
            {
                for (int x = 0; x < b.Width; x++)
                {
                    byte R = lpImageData[uiIndex++];
                    byte G = lpImageData[uiIndex++];
                    byte B = lpImageData[uiIndex++];
                    byte A = lpImageData[uiIndex++];

                    b.SetPixel(x, y, Color.FromArgb(A, R, G, B));
                }
            }

            VtfLib.vlShutdown();

            vtfImg = (Image)b;
        }

        #endregion

        #region Utils

        bool IsHeadNode(TreeNode node)
        {
            foreach (TreeNode n in projectTreeView.Nodes)
                if (n == node)
                    return true;
            return false;
        }

        void ClosePanelControls()
        {
            foreach (IDockableControl d in Addon_BaseControls.panel.Controls)
                d.Close();

            Addon_BaseControls.panel.Controls.Clear();
        }

        void PakZip(string filename, TreeNode n)
        {
            ZipFile f = new ZipFile(filename);
            //TreeNode n = projectTreeView.SelectedNode;
            List<FileEntry> e = new List<FileEntry>();

            GenerateList(e, n, n.FullPath);
            PakFiles(e, f);
            f.Save();

            e.Clear();
            f.Dispose();
            Utils.cleanupMemory();
        }

        void PakFiles(List<FileEntry> entries, ZipFile file)
        {
            foreach (FileEntry e in entries)
            {
                try
                {
                    switch (e.type)
                    {
                        case EntryType.Directory:
                            file.AddDirectory(e.filename, e.path);
                            break;
                        case EntryType.File:
                            file.AddFile(e.filename, e.path);
                            break;
                    }
                }
                catch { }
            }
        }

        void GenerateList(List<FileEntry> e, TreeNode node, string root)
        {
            foreach (TreeNode n in node.Nodes)
            {
                string path = n.FullPath.Remove(0, root.Length + 1);

                if (Directory.Exists(n.FullPath))
                {
                    try
                    {
                        e.Add(new FileEntry() { filename = n.FullPath, path = path, type = EntryType.Directory });
                        GenerateList(e, n, root);
                    }
                    catch { }
                }
                else if (File.Exists(n.FullPath))
                {
                    FileInfo i = new FileInfo(n.FullPath);
                    path = path.Remove(path.Length - i.Name.Length);
                    e.Add(new FileEntry() { filename = n.FullPath, path = path, type = EntryType.File });
                }
            }
        }

        void AddNewTreeNode(FileInfo f)
        {
            TreeNode n = projectTreeView.SelectedNode;

            if (f.Name.EndsWith(".txt"))
                n.Nodes.Add(f.Name, f.Name, 3, 3);
            else if (f.Name.EndsWith(".vtf"))
                n.Nodes.Add(f.Name, f.Name, 5, 5);
            else if (f.Name.EndsWith(".vmt"))
                n.Nodes.Add(f.Name, f.Name, 4, 4);
            else
                n.Nodes.Add(f.Name, f.Name, 2, 2);
        }

        #endregion
    }

    enum EntryType
    {
        File,
        Directory
    }

    class FileEntry
    {
        public string filename;
        public string path;
        public EntryType type;
    }
}
