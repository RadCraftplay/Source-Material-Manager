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
using System.Windows.Forms;

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
