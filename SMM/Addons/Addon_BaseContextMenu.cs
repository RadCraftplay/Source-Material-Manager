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
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SMM.Addons.Addon_BaseProject_Controls;
using System.Drawing;

namespace SMM.Addons
{
    class Addon_BaseContextMenu : IAddon
    {
        TreeView projectTreeView;
        ContextMenuStrip projectTreeViewContextMenuStrip;

        ToolStripMenuItem deleteContextStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem closeProjectContextStripMenuItem = new ToolStripMenuItem();

        TreeNode lastNode;

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

        void CreateContextMenuStrip()
        {
            projectTreeViewContextMenuStrip = new ContextMenuStrip();
            projectTreeView.ContextMenuStrip = projectTreeViewContextMenuStrip;
            projectTreeView.MouseClick += ProjectTreeView_MouseClick;
            projectTreeView.MouseDoubleClick += ProjectTreeView_MouseDoubleClick;
            CreateContextMenuStripItems();
        }

        private void ProjectTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFile();
        }

        private void ProjectTreeView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                projectTreeView.SelectedNode = projectTreeView.GetNodeAt(e.Location);

            if (IsHeadNode(projectTreeView.SelectedNode))
            {
                deleteContextStripMenuItem.Visible = false;
                closeProjectContextStripMenuItem.Visible = true;
            }
            else
            {
                deleteContextStripMenuItem.Visible = true;
                closeProjectContextStripMenuItem.Visible = false;
            }
        }

        void CreateContextMenuStripItems()
        {
            deleteContextStripMenuItem.Image = TreeViewIcons.cross_script;
            deleteContextStripMenuItem.Text = "Delete";
            deleteContextStripMenuItem.Click += DeleteItem_Click; //((sender, e) => { MessageBox.Show("Replace this with something else"); });
            deleteContextStripMenuItem.Visible = false;
            projectTreeViewContextMenuStrip.Items.Add(deleteContextStripMenuItem);

            closeProjectContextStripMenuItem.Image = TreeViewIcons.cross_script;
            closeProjectContextStripMenuItem.Text = "Close project";
            closeProjectContextStripMenuItem.Click += CloseProjectContextStripMenuItem_Click; //((sender, e) => { MessageBox.Show("Replace this with something else"); });
            closeProjectContextStripMenuItem.Visible = false;
            projectTreeViewContextMenuStrip.Items.Add(closeProjectContextStripMenuItem);
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

        void OpenFile()
        {
            if (!(projectTreeView.SelectedNode.ImageIndex == 0))
            {
                ClosePanelControls();
                lastNode = projectTreeView.SelectedNode;

                if (projectTreeView.SelectedNode.Text.EndsWith(".txt"))
                {
                    var tb = new Addon_BaseProject_Controls.TextEditor(Addon_BaseControls.treeView.SelectedNode.FullPath);
                    Addon_BaseControls.panel.Controls.Add(tb);
                    tb.Dock = DockStyle.Fill;
                }
                else if (projectTreeView.SelectedNode.Text.EndsWith(".vmt"))
                {
                    var tb = new Addon_BaseProject_Controls.TextEditor(Addon_BaseControls.treeView.SelectedNode.FullPath);
                    Addon_BaseControls.panel.Controls.Add(tb);
                    tb.Dock = DockStyle.Fill;
                }
                else if (projectTreeView.SelectedNode.Text.EndsWith(".vtf"))
                {
                    LoadVtf l = new LoadVtf(LoadVTF);
                    l.Invoke(Addon_BaseControls.treeView.SelectedNode.FullPath);

                    //MessageBox.Show("Texture file", "Replace me, please");
                }
                else
                {
                    //Does nothing
                    //MessageBox.Show("Orther file", "Replace me, please");
                }
            }
        }

        delegate void LoadVtf(string filename);

        unsafe void LoadVTF(string filename)
        {
            uint u = new uint();

            VtfLib.vlInitialize();
            VtfLib.vlCreateImage(&u);
            VtfLib.vlBindImage(u);
            VtfLib.vlImageLoad(Addon_BaseControls.treeView.SelectedNode.FullPath, false);

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

            VTFViewer v = new VTFViewer();
            Addon_BaseControls.panel.Controls.Add(v);
            v.Dock = DockStyle.Fill;
            v.pictureBox1.Image = (Image)b;

            //PictureViewerControl con = new PictureViewerControl();
            //con.Dock = DockStyle.Fill;
            //con.pictureBox1.Image = (Image)b;
            //con.BASE_IMG = (Image)b;
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

        #endregion
    }
}
