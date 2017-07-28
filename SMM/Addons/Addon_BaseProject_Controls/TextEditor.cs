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
using System.Windows.Forms;

namespace SMM.Addons.Addon_BaseProject_Controls
{
    public partial class TextEditor : UserControl, IDockableControl
    {
        string FILENAME { get; set; }
        bool IsSaved = true;
        Stack<string> undoList = new Stack<string>();

        public TextEditor(string filename)
        {
            InitializeComponent();
            FILENAME = filename;

            richTextBox1.LoadFile(FILENAME, RichTextBoxStreamType.PlainText);
            IsSaved = true;
        }

        private void reloadFromDiscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsSaved)
            {
                if (MessageBox.Show("Do you want to reload file? All changes will be lost", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    richTextBox1.LoadFile(FILENAME, RichTextBoxStreamType.PlainText);
                    IsSaved = true;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsSaved)
            {
                if (MessageBox.Show(string.Format("Do you want to overwrite file {0}{1}{0}?", '"', FILENAME), "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    richTextBox1.SaveFile(FILENAME, RichTextBoxStreamType.PlainText);
                    IsSaved = true;
                }
            }
        }

        public void Close()
        {
            if (!IsSaved)
                if (MessageBox.Show(string.Format("Do you want to save file {0}{1}{0}?", '"', FILENAME), "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    richTextBox1.SaveFile(FILENAME, RichTextBoxStreamType.PlainText);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undoList.Count > 0)
                richTextBox1.Text = undoList.Pop();

            if (undoList.Count == 0)
                undoToolStripMenuItem.Enabled = false;
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            IsSaved = false;

            if (e.KeyCode == Keys.Space)
            {
                undoList.Push(richTextBox1.Text);
                undoToolStripMenuItem.Enabled = true;
            }
        }
    }
}
