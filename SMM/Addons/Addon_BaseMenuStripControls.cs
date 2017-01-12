using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SMM.Addons
{
    public class Addon_BaseMenuStripControls : IAddon
    {
        public MenuStrip menuStrip;

        public static ToolStripMenuItem fileMenuStripItem;
        public static ToolStripMenuItem editMenuStripItem;
        public static ToolStripMenuItem optionsMenuStripItem;

        #region Base addon

        public AddonInfo Info
        {
            get
            {
                return new AddonInfo() { Name = "Base menu strip controls", Publisher = "Distroir", Version = "beta 1" };
            }
        }

        public void Initialize()
        {
            menuStrip = Addon_BaseControls.menuStrip;
            AddToolStripItems();
        }

        public void Shutdown()
        {
            
        }

        #endregion

        #region BaseControls

        public void AddToolStripItems()
        {
            fileMenuStripItem = new ToolStripMenuItem()
            {
                Name = "fileMenuStripItem",
                Size = new System.Drawing.Size(37, 20),
                Text = "File"
            };
            editMenuStripItem = new ToolStripMenuItem()
            {
                Name = "editMenuStripItem",
                Size = new System.Drawing.Size(37, 20),
                Text = "Edit"
            };
            optionsMenuStripItem = new ToolStripMenuItem()
            {
                Name = "optionsMenuStripItem",
                Size = new System.Drawing.Size(37, 20),
                Text = "Options"
            };

            menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileMenuStripItem, editMenuStripItem, optionsMenuStripItem });
        }

        #endregion
    }
}
