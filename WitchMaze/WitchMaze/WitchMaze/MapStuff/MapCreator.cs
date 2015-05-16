using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace WitchMaze.MapStuff
{
    class MapCreator
    {
        public int[,] mapType;
        // Types(weil ich nicht weiß, ob hier ein enum shcon sinn macht ^^
        // 0: Floor, 1: Wall, 2: Blackhole

        public MapCreator()
        {
            mapType = new int[Settings.mapSizeX, Settings.mapSizeZ];
        }

        public void initialize()
        {
            //some funny Random stuff which fills the array with useful numbers 
            for (int i = 0; i < Settings.mapSizeX; i++)
            {
                for (int j = 0; i < Settings.mapSizeZ; j++)
                {
                    mapType[i, j] = 0;
                }
            }
        }


    }
}
