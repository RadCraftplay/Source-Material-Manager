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
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace SMM.Addons
{
    public class Addon_BaseControls : IAddon
    {
        #region Variables

        /// <summary>
        /// Instance of main window
        /// </summary>
        public Form f;

        /// <summary>
        /// Split container containing treeView and panel
        /// </summary>
        public static SplitContainer splitContainer;
        /// <summary>
        /// Main menu strip of application
        /// </summary>
        public static MenuStrip menuStrip;

        /// <summary>
        /// Tree view for project files
        /// </summary>
        public static TreeView treeView;
        /// <summary>
        /// Image list for treeView
        /// </summary>
        public static ImageList treeViewImageList;
        /// <summary>
        /// Panel at right part of the window
        /// </summary>
        public static Panel panel;

        /// <summary>
        /// Version of Source Material Manager
        /// </summary>
        public static string SMMVersion;

        #endregion

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

        /// <summary>
        /// Creates base controls
        /// </summary>
        public void CreateControls()
        {
            CreateMenuStrip();
            CreateSplitContainer();
            CreateTreeView();
            CreateImageList();
            CreatePanel();
        }

        /// <summary>
        /// Creates main menu strip
        /// </summary>
        public void CreateMenuStrip()
        {
            //Create menu strip
            menuStrip = new MenuStrip()
            {
                Location = new System.Drawing.Point(0, 0),
                Size = new System.Drawing.Size(24, 24),
                Name = "menuStrip",
                Text = "menuStrip1"
            };
            //Add it to form
            f.Controls.Add(menuStrip);

            //Set DockStyle to Top
            menuStrip.Dock = DockStyle.Top;
        }

        /// <summary>
        /// Creates menu strip
        /// </summary>
        public void CreateSplitContainer()
        {
            //Create split container
            splitContainer = new SplitContainer()
            {
                Location = new System.Drawing.Point(12, 27),
                Size = new System.Drawing.Size(f.Width - 36, f.Height - 77),
                FixedPanel = FixedPanel.Panel1,
                SplitterDistance = 200
            };
            //Add it to form
            f.Controls.Add(splitContainer);

            //Anchor container to all sides of control
            splitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        /// <summary>
        /// Creates tree view of projects
        /// </summary>
        public void CreateTreeView()
        {
            //Create tree view
            treeView = new TreeView()
            {
                Location = new System.Drawing.Point(0, 0),
                Size = new System.Drawing.Size(10, 10)
            };
            //Add tree view to left side of split container
            splitContainer.Panel1.Controls.Add(treeView);

            //Set it's dock to Fill
            //It will fill entire left panel
            treeView.Dock = DockStyle.Fill;
            //Add event
            //Mouse button down
            //Select node when user clicks on control
            //Fix for winforms (or improvement)
            treeView.MouseDown += (sender, args) => treeView.SelectedNode = treeView.GetNodeAt(args.X, args.Y);
        }

        /// <summary>
        /// Creates image list and adds images to it
        /// </summary>
        public void CreateImageList()
        {
            //Create image list
            treeViewImageList = new ImageList();

            //Add images to it
            treeViewImageList.Images.Add(TreeViewIcons.folder_horizontal);
            treeViewImageList.Images.Add(TreeViewIcons.folder_horizontal_open);
            treeViewImageList.Images.Add(TreeViewIcons.document);
            treeViewImageList.Images.Add(TreeViewIcons.document_text);
            treeViewImageList.Images.Add(TreeViewIcons.document_code);
            treeViewImageList.Images.Add(TreeViewIcons.image);

            //Use this image list in tree view
            treeView.ImageList = treeViewImageList;
        }

        /// <summary>
        /// Creates panel al right part of the screen
        /// </summary>
        public void CreatePanel()
        {
            //Enable autoscroll in panel
            //If control inside of panel will be to small, scroll bars will appear
            splitContainer.Panel2.AutoScroll = true;

            //Set background color of panel
            splitContainer.Panel2.BackColor = System.Drawing.Color.White;
            //Use panel in split container
            panel = splitContainer.Panel2;
        }

        #endregion

        #region Orther

        /// <summary>
        /// Get version of the application
        /// </summary>
        void GetVersion()
        {
            //Get version of assembly
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            SMMVersion = fvi.FileVersion;
        }

        #endregion
    }
}
