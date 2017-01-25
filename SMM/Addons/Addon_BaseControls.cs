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
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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

        public string SMMVersion;

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
            f.Size = new System.Drawing.Size(800, 600);
            GetVersion();
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
                Size = new System.Drawing.Size(f.Width - 36, f.Height - 77),
                FixedPanel = FixedPanel.Panel1,
                SplitterDistance = 200
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
            panel = splitContainer.Panel2;
        }

        #endregion

        #region Orther

        void GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            SMMVersion = fvi.FileVersion;
        }

        #endregion
    }
}
