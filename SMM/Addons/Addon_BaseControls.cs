using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMM.Addons
{
    public class Addon_BaseControls : IAddon
    {
        public Form f;

        public static SplitContainer splitContainer;
        public static MenuStrip menuStrip;

        public static TreeView treeView;
        public static ImageList treeViewImageList;
        public static Panel panel;

        #region BaseAddon

        AddonInfo IAddon.Info
        {
            get
            {
                return new AddonInfo() { Name = "Base controls", Publisher = "Distroir", Version = "beta 1" };
            }
        }

        public void Initialize()
        {
            f = Form1.form;
            CreateControls();
        }

        public void Shutdown()
        {
            //Do nothing
        }

        #endregion

        #region BaseControls

        public void CreateControls()
        {
            CreateMenuStrip();
            CreateSplitContainer();
            CreateTreeView();
            CreateImageList();
            CreatePanel();
        }

        public void CreateMenuStrip()
        {
            menuStrip = new MenuStrip()
            {
                Location = new System.Drawing.Point(0, 0),
                Size = new System.Drawing.Size(24, 24),
                Name = "menuStrip",
                Text = "menuStrip1"
            };
            f.Controls.Add(menuStrip);
            menuStrip.Dock = DockStyle.Top;
        }

        public void CreateSplitContainer()
        {
            splitContainer = new SplitContainer()
            {
                Location = new System.Drawing.Point(12, 27),
                Size = new System.Drawing.Size(f.Width - 24, f.Height - 77),
                FixedPanel = FixedPanel.Panel1
            };
            f.Controls.Add(splitContainer);
            splitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        public void CreateTreeView()
        {
            treeView = new TreeView()
            {
                Location = new System.Drawing.Point(0, 0),
                Size = new System.Drawing.Size(10, 10)
            };
            splitContainer.Panel1.Controls.Add(treeView);
            treeView.Dock = DockStyle.Fill;
            treeView.MouseDown += (sender, args) => treeView.SelectedNode = treeView.GetNodeAt(args.X, args.Y);
        }

        public void CreateImageList()
        {
            treeViewImageList = new ImageList();

            treeViewImageList.Images.Add(TreeViewIcons.folder_horizontal);
            treeViewImageList.Images.Add(TreeViewIcons.folder_horizontal_open);
            treeViewImageList.Images.Add(TreeViewIcons.document);
            treeViewImageList.Images.Add(TreeViewIcons.document_text);
            treeViewImageList.Images.Add(TreeViewIcons.document_code);
            treeViewImageList.Images.Add(TreeViewIcons.image);

            treeView.ImageList = treeViewImageList;
        }

        public void CreatePanel()
        {
            splitContainer.Panel2.AutoScroll = true;
            splitContainer.Panel2.BackColor = System.Drawing.Color.White;
        }

        #endregion
    }
}
