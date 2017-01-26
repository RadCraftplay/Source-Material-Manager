using System;
using System.IO;
using System.Windows.Forms;

namespace SMM.Addons.Addon_BaseContextMenu_Controls
{
    public partial class AddFileDialog : Form
    {
        TreeNode NODE;

        public AddFileDialog(TreeNode node)
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            NODE = node;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filename = Path.Combine(NODE.FullPath, textBox1.Text);

            if (filename.EndsWith(".vtf"))
            {
                MessageBox.Show("Sorry, we don't support creating VTF files yet", "SMM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            AddNewTreeNode(filename, comboBox1.SelectedIndex);
            Close();
        }

        void AddNewTreeNode(string name, int type)
        {
            TreeNode n = NODE;

            switch (type)
            {
                case 0:
                    File.Create(name).Close();
                    FileInfo f = new FileInfo(name);

                    if (f.Name.EndsWith(".txt"))
                        n.Nodes.Add(f.Name, f.Name, 3, 3);
                    else if (f.Name.EndsWith(".vtf"))
                        n.Nodes.Add(f.Name, f.Name, 5, 5);
                    else if (f.Name.EndsWith(".vmt"))
                        n.Nodes.Add(f.Name, f.Name, 4, 4);
                    else
                        n.Nodes.Add(f.Name, f.Name, 2, 2);
                    break;
                case 1:
                    Directory.CreateDirectory(name);
                    DirectoryInfo d = new DirectoryInfo(name);
                    n.Nodes.Add(d.Name, d.Name, 0, 1);
                    break;
            }
        }
    }
}
