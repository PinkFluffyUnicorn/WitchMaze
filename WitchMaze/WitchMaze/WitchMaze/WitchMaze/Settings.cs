using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace WitchMaze
{
    class Settings
    {
        public static int resolutionX = 340;
        public static int resolutionY = 480;

        bool isFullScreen = false;

// if changing GraphicsDeviceManager properties outside 
// your game constructor also call:
// graphics.ApplyChanges();

    }
}
