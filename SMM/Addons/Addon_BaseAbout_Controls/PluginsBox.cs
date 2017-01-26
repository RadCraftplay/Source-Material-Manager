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
using System.Drawing;
using System.Windows.Forms;

namespace SMM.Addons.Addon_BaseAbout_Controls
{
    public partial class PluginsBox : Form
    {
        public PluginsBox()
        {
            InitializeComponent();
            IntPtr Hicon = MenuStripIcons.puzzle.GetHicon();
            Icon = Icon.FromHandle(Hicon);

            LoadPluginList();
            PrepareListView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        void PrepareListView()
        {
            listView1.SuspendLayout();
            listView1.Columns.RemoveAt(3);
            listView1.ResumeLayout();
        }


        void LoadPluginList()
        {
            foreach (IAddon a in Addon_BasePluginLoader.Plugins)
                AddItem(a);
        }

        void AddItem(IAddon a)
        {
            ListViewItem i = new ListViewItem();
            AddonInfo info = a.Info;

            i.Text = info.Name;
            i.SubItems.Add(info.Publisher);
            i.SubItems.Add(info.Version);
            i.SubItems.Add(info.Description);

            i.Tag = info;

            listView1.Items.Add(i);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
            {
                
            }
        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            versionToolStripMenuItem.Checked = !versionToolStripMenuItem.Checked;

            if (!versionToolStripMenuItem.Checked)
                listView1.Columns.RemoveAt(2);
            else
                listView1.Columns.Insert(2, versionColumn);
            */
        }

        private void descriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {

            /*
            descriptionToolStripMenuItem.Checked = !descriptionToolStripMenuItem.Checked;

            if (!descriptionToolStripMenuItem.Checked)
                listView1.Columns.RemoveAt(listView1.Columns.Count - 1);
            else
                listView1.Columns.Insert(listView1.Columns.Count, descriptionColumn);
            */
        }
    }
}
