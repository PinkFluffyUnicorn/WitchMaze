using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WitchMaze.ownFunctions;

namespace WitchMaze
{
    class Settings
    {
        static float formatX = 16;
        static float formatY = 9;

        static int resolutionX = 1920; public static int getResolutionX() { return resolutionX; }
        static int resolutionY = 1080; public static int getResolutionY() { return resolutionY; }
        static float interfaceScale = 1f; public static float getInterfaceScale() { return interfaceScale; }

        static float soundVolume = 1;
        public static float getSoundVolume() { return soundVolume; }
        private static void setSoundVolume(float volume)
        {
            //if (volume < 0 || volume > 1)
            //    throw new IndexOutOfRangeException();

            soundVolume = volume;
            Game1.sounds.setVolume(volume);
        }
        private static void setResolutionX(int x)
        {
            interfaceScale = (float)x / 1920;//(float)resolutionX; //problem ist hier
        Console.WriteLine(interfaceScale);
            resolutionX = x;
            resolutionY = (int)(x / formatX * formatY);
            Game1.getGraphics().PreferredBackBufferHeight = Settings.getResolutionY();
            Game1.getGraphics().PreferredBackBufferWidth = Settings.getResolutionX();
            Game1.getGraphics().ApplyChanges();
        }
        private static void setResolutionY(int y) 
        {
            interfaceScale = 1080 / (float)y;
            resolutionX = (int)((y / formatY) * formatX); 
            resolutionY = y;
            Game1.getGraphics().PreferredBackBufferHeight = Settings.getResolutionY();
            Game1.getGraphics().PreferredBackBufferWidth = Settings.getResolutionX();
            Game1.getGraphics().ApplyChanges();
        }

        static bool isFullScreen = false;
        private static void setFullscreen(bool fullscreen)
        {
            isFullScreen = fullscreen;
            Game1.getGraphics().IsFullScreen = fullscreen;
            Game1.getGraphics().ApplyChanges();
        }
        public static bool isFullscreen() { return isFullScreen; }
        
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


        static Document docunemt = new Document("Settings.txt");
        public static void writeSettings(bool f, int res, float v)
        {
            setSoundVolume(v);
            setResolutionX(res);
            setFullscreen(f);
            docunemt.write(f + "" + res + "" + v+"          ");
        }

        public static void readSettings()
        {
            //kein dokument
            //nicht lesar
            //lesen


            //String h = docunemt.read();
            //Console.WriteLine(h);

            //if (h.Substring(0, 4).Equals("true"))
            //    isFullScreen = true;
            //if (h.Substring(0, 5).Equals("false"))
            //    isFullScreen = false;

            //if (h.Substring(5, 4).Equals("1920"))
            //    resolutionX = 1920;
            //if (h.Substring(5, 4).Equals("1366"))
            //    resolutionX = 1366;
            //if (h.Substring(5, 4).Equals("1280"))
            //    resolutionX = 1280;

            //if (h.Substring(9, 1).Equals("1"))
            //    soundVolume = 100;
            //if (h.Substring(9, 3).Equals("0.9"))
            //    soundVolume = 90;
            //if (h.Substring(9, 3).Equals("0.8"))
            //    soundVolume = 80;
            //if (h.Substring(9, 3).Equals("0.7"))
            //    soundVolume = 70;
            //if (h.Substring(9, 3).Equals("0.6"))
            //    soundVolume = 60;
            //if (h.Substring(9, 3).Equals("0.5"))
            //    soundVolume = 50;
            //if (h.Substring(9, 3).Equals("0.4"))
            //    soundVolume = 40;
            //if (h.Substring(9, 3).Equals("0.3"))
            //    soundVolume = 30;
            //if (h.Substring(9, 3).Equals("0.2"))
            //    soundVolume = 20;
            //if (h.Substring(9, 3).Equals("0.1"))
            //    soundVolume = 10;
            //if (h.Substring(9, 3).Equals("0"))
            //    soundVolume = 0;
        }

    }
}
