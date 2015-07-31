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
        static float formatX = 16;
        static float formatY = 9;

        static int resolutionX = 1920; public static int getResolutionX() { return resolutionX; }
        static int resolutionY = 1080; public static int getResolutionY() { return resolutionY; }
        static float interfaceScale = 1f; public static float getInterfaceScale() { return interfaceScale; }

        
        public static void setResolutionX(int x)
        {   interfaceScale = (float)x / (float)resolutionX;
            resolutionX = x;
            resolutionY = (int)(x / formatX * formatY); 
        }
        public static void setResolutionY(int y) 
        {
            interfaceScale = (float)resolutionY / (float)y;
            resolutionX = (int)((y / formatY) * formatX); 
            resolutionY = y;
        }

        static bool isFullScreen = false; public static bool isFullscreen() { return isFullScreen; }

        public static float blockSizeX = 1f;
        public static float blockSizeY = 1f;
        public static float blockSizeZ = 1f;
        public static Color floorColor = Color.DeepPink;
        public static Color blackHoleColor = Color.Black;
        public static Color WallColor = Color.Gold;

        public static int mapSizeX = 11;
        public static int mapSizeZ = 21;

        

// if changing GraphicsDeviceManager properties outside 
// your game constructor also call:
// graphics.ApplyChanges();

    }
}
