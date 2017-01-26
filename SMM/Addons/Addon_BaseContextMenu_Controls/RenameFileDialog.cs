using System;
using System.IO;
using System.Windows.Forms;

namespace SMM.Addons.Addon_BaseContextMenu_Controls
{
    public partial class RenameFileDialog : Form
    {
        FileInfo F;
        DirectoryInfo D;
        TreeNode N;

        public RenameFileDialog(FileInfo f, TreeNode n)
        {
            InitializeComponent();
            F = f;
            N = n;

            textBox1.Text = f.Name;
        }

        public RenameFileDialog(DirectoryInfo d, TreeNode n)
        {
            InitializeComponent();
            D = d;
            N = n;

            textBox1.Text = d.Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (F == null)
            {
                string newPath = Path.Combine(D.Parent.FullName, textBox1.Text);
                Directory.Move(D.FullName, newPath);
            }
            else
            {
                string newPath = Path.Combine(F.Directory.FullName, textBox1.Text);
                File.Move(F.FullName, newPath);
            }
            
            N.Name = textBox1.Text;
            N.Text = textBox1.Text;
            Close();
        }
    }
}
