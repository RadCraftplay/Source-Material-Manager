using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

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
