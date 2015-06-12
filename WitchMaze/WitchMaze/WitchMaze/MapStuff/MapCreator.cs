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
using WitchMaze.MapStuff.Blocks;


namespace WitchMaze.MapStuff
{
    class MapCreator
    {
        public int[,] mapType;
        public Map map;
        // Types(weil ich nicht weiß, ob hier ein enum shcon sinn macht ^^
        // 0: Floor, 1: Wall, 2: Blackhole

        public MapCreator()
        {
            mapType = new int[Settings.mapSizeX, Settings.mapSizeZ];
            map = new Map();
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

        public Map generateMap()
        {
            for (int i = 0; i < Settings.mapSizeX; i++)
            {
                for (int j = 0; j < Settings.mapSizeZ; j++)
                {
                    mapType[i, j] = i % 3;
                    // Console.WriteLine(mapType[i, j].ToString());
                    
                    if (mapType[i, j] == 0)
                        map.map[i, j] = new Floor(new Vector3((float)(i * Settings.blockSizeX), 0f, (float)(j * Settings.blockSizeZ)), Settings.floorColor);
                    if (mapType[i, j] == 2)
                        map.map[i, j] = new Wall(new Vector3((float)(i * Settings.blockSizeX), (float)(j * Settings.blockSizeZ * 0.0), (float)(j * Settings.blockSizeZ)), Game1.getContent().Load<Model>("cube"));
                    if (mapType[i,j] == 1)
                        map.map[i, j] = new BlackHole(new Vector3((float)(i * Settings.blockSizeX), 0f, (float)(j * Settings.blockSizeZ)), Settings.blackHoleColor);

                }
            }
            return map;
        }


    }
}
