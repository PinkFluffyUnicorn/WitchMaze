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
            Game1.getGraphics().PreferredBackBufferHeight = Settings.getResolutionY();
            Game1.getGraphics().PreferredBackBufferWidth = Settings.getResolutionX();
            Game1.getGraphics().ApplyChanges();
        }
        public static void setResolutionY(int y) 
        {
            interfaceScale = (float)resolutionY / (float)y;
            resolutionX = (int)((y / formatY) * formatX); 
            resolutionY = y;
            Game1.getGraphics().PreferredBackBufferHeight = Settings.getResolutionY();
            Game1.getGraphics().PreferredBackBufferWidth = Settings.getResolutionX();
            Game1.getGraphics().ApplyChanges();
        }

        static bool isFullScreen = false; public static bool isFullscreen() { return isFullScreen; }
        
        /// <summary>
        /// float how big a block(Wall, Floor, Blackhole) in x- Direction is
        /// </summary>
        static float blockSizeX = 1f; 
       /// <summary>
       /// getter for Blocksize in x- Direction
       /// </summary>
       /// <returns>blocksize in float</returns>
        public static float getBlockSizeX() { return blockSizeX; }
        /// <summary>
        /// float how big a block(Wall, Floor, Blackhole) in y- Direction is
        /// </summary>
        static float blockSizeY = 1f; 
        /// <summary>
        /// getter for Blocksize in y- Direction 
        /// </summary>
        /// <returns>blocksize in float</returns>
        public static float getBlockSizeY() { return blockSizeY; }
        /// <summary>
        /// float how big a block(Wall, Floor, Blackhole) in z- Direction is
        /// </summary>
        static float blockSizeZ = 1f; 
        /// <summary>
        /// getter for Blocksize in z- Direction 
        /// </summary>
        /// <returns>blocksize in float</returns>
        public static float getBlockSizeZ() { return blockSizeZ; }


        

        /// <summary>
        /// Size of Map in x-Direction integer
        /// </summary>
        static int mapSizeX = 20; //64:36 laggt, 50:50 laggt, 40:40 ist spielmar(ohne items und so...) , 48:27 auch spielbar(kleine map für 4...) 
        /// <summary>
        /// getter for MapSize in x-Direction
        /// </summary>
        /// <returns>mapSize in int</returns>
        public static int getMapSizeX() { return mapSizeX; }
        /// <summary>
        /// Size of Map in z-Direction integer
        /// </summary>
        static int mapSizeZ = 20; 
        /// <summary>
        /// getter for MapSize in z- Direction
        /// </summary>
        /// <returns>MapSize in int </returns>
        public static int getMapSizeZ() { return mapSizeZ; }

        public static Color acaOrange = new Color(255, 144, 1);


        

        

// if changing GraphicsDeviceManager properties outside 
// your game constructor also call:
// graphics.ApplyChanges();

    }
}
