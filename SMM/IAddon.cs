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
namespace SMM
{
    public interface IAddon
    {
        /// <summary>
        /// Info about addon
        /// </summary>
        AddonInfo Info { get; }

        /// <summary>
        /// Initializes plugin
        /// </summary>
        void Initialize();

        /// <summary>
        /// This method is being executed when SMM is shutting down
        /// </summary>
        void Shutdown();
    }

    public class AddonInfo
    {
        public string Name;
        public string Publisher;
        public string Version;
        public string Description;
    }
}
