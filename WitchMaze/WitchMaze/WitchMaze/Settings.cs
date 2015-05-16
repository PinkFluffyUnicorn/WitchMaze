using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WitchMaze
{
    class Settings
    {
        public static int resolutionX = 340;
        public static int resolutionY = 480;

        bool isFullScreen = false;

        public static float blockSizeX = 1f;
        public static float blockSizeY = 2f;
        public static float blockSizeZ = 1f;
        public static Color floorColor = Color.DeepPink;

        public static int mapSizeX = 2;
        public static int mapSizeZ = 2;

// if changing GraphicsDeviceManager properties outside 
// your game constructor also call:
// graphics.ApplyChanges();

    }
}
