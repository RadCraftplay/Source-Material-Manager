using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SMM.Addons
{
    class Addon_BaseContextMenu : IAddon
    {
        TreeView projectTreeView;
        ContextMenuStrip projectTreeViewContextMenuStrip;

        ToolStripMenuItem deleteContextStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem closeProjectContextStripMenuItem = new ToolStripMenuItem();

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
                if (projectTreeView.SelectedNode.Text.EndsWith(".txt"))
                    MessageBox.Show("Text file", "Replace me, please");
                else if (projectTreeView.SelectedNode.Text.EndsWith(".vmt"))
                    MessageBox.Show("Material file", "Replace me, please");
                else if (projectTreeView.SelectedNode.Text.EndsWith(".vtf"))
                    MessageBox.Show("Texture file", "Replace me, please");
                else
                    MessageBox.Show("Orther file", "Replace me, please");
            }
        }

        #endregion
    }
}
