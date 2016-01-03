using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View
{
    public class Options
    {
        private Vector2 resolution;
        private bool fullScreen;

        private bool settingsChanged = true;

        public Vector2 Resolution { 
            get { return this.resolution; }
            set { this.resolution = value; }
        }

        public bool FullScreen
        {
            get { return this.fullScreen; }
            set { this.fullScreen = value; }
        }

        public bool SettingsChanged
        {
            get { return this.settingsChanged; }
            set { this.settingsChanged = value; }
        }
    }
}
