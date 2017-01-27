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
