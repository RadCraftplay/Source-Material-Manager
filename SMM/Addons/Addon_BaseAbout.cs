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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMM.Addons
{
    class Addon_BaseAbout : IAddon
    {
        #region Base Addon

        public AddonInfo Info
        {
            get
            {
                return new AddonInfo() { Name = "Base about application", Publisher = "Distroir", Version = "beta 1" };
            }
        }

        public void Initialize()
        {
            CreateMenuItems();
        }

        public void Shutdown()
        {
            
        }

        #endregion

        #region Menu Items

        void CreateMenuItems()
        {
            CreatePluginsMenuItem();
            CreateAboutMenuItem();
        }

        void CreateAboutMenuItem()
        {
            ToolStripItem i = new ToolStripMenuItem();
            i.Text = "About";
            i.Image = MenuStripIcons.information;
            i.Click += AboutToolStripMenuItem_Click;

            Addon_BaseMenuStripControls.optionsMenuStripItem.DropDownItems.Add(i);
        }

        void CreatePluginsMenuItem()
        {
            ToolStripItem i = new ToolStripMenuItem();
            i.Text = "Plugins";
            i.Image = MenuStripIcons.puzzle;
            i.Click += PluginsToolStripMenuItem_Click;

            Addon_BaseMenuStripControls.optionsMenuStripItem.DropDownItems.Add(i);
        }

        private void PluginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var v = new Addon_BaseAbout_Controls.PluginsBox();
            v.ShowDialog();

        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var v = new Addon_BaseAbout_Controls.AboutBox();
            v.ShowDialog();
        }

        #endregion
    }
}
