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
using WitchMaze.Map.Blocks;


namespace WitchMaze.Map
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
        }


        public Map generateMap()
        {
            for (int i = 0; i < Settings.mapSizeX; i++)
            {
                for (int j = 0; i < Settings.mapSizeZ; j++)
                {
                    mapType[i, j] = 0;
                    if (mapType[i, j] == 0)
                        map.map[i, j] = new Floor(new Vector3((float)(i * Settings.blockSizeX), 0f, (float)(j * Settings.blockSizeZ)), Settings.floorColor);
                    if (mapType[i, j] == 1)
                        map.map[i, j] = new Wall();
                    else
                        map.map[i, j] = new BlackHole();

                }
            }
            return map;
        }


    }
}
