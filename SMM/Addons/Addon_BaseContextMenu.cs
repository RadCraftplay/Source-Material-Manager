using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SMM.Addons.Addon_BaseProject_Controls;

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
                    MessageBox.Show("Texture file", "Replace me, please");
                }
                else
                {
                    MessageBox.Show("Orther file", "Replace me, please");
                }
            }
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
