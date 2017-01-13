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
    public partial class VTFViewer : UserControl, IDockableControl
    {
        public Image BASE_IMG;

        public VTFViewer()
        {
            InitializeComponent();

            pictureBox1.BackColor = Color.FromArgb(0, Color.White);
            pictureBox1.Paint += PictureBox1_Paint;
        }

        public void Close()
        {
            
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.Width = pictureBox1.Image.Width;
            pictureBox1.Height = pictureBox1.Image.Height;

            pictureBox1.Dock = DockStyle.None;
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }
    }
}
