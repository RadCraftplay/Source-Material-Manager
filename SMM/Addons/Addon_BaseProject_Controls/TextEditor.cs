using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMM.Addons.Addon_BaseProject_Controls
{
    public partial class TextEditor : UserControl, IDockedControl
    {
        string FILENAME { get; set; }
        bool IsSaved = true;
        bool FileWasLoaded = false;

        public TextEditor(string filename)
        {
            InitializeComponent();
            FILENAME = filename;

            richTextBox1.LoadFile(FILENAME, RichTextBoxStreamType.PlainText);
            IsSaved = true;
        }

        private void TextEditor_Load(object sender, EventArgs e)
        {
            
        }

        private void reloadFromDiscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to reload file? All changes will be lost", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                richTextBox1.LoadFile(FILENAME, RichTextBoxStreamType.PlainText);
                FileWasLoaded = true;
                IsSaved = true;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Do you want to overwrite file {0}{1}{0}?", '"', FILENAME), "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                richTextBox1.SaveFile(FILENAME, RichTextBoxStreamType.PlainText);
                FileWasLoaded = true;
                IsSaved = true;
            }
        }

        public void Close()
        {
            if (!IsSaved)
                if (MessageBox.Show(string.Format("Do you want to save file {0}{1}{0}?", '"', FILENAME), "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    richTextBox1.SaveFile(FILENAME, RichTextBoxStreamType.PlainText);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (FileWasLoaded)
                IsSaved = false;
            else
                FileWasLoaded = true;
        }
    }
}
