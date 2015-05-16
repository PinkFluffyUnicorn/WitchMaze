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
    class Map
    {
        public Block[,] map;
        public int[,] mapType;

        public Map(int[,] _mapType)
        {
            map = new Block[Settings.mapSizeX, Settings.mapSizeZ];
            mapType = _mapType;
        }

        public void initialize()
        {

            for (int i = 0; i < Settings.mapSizeX; i++)
            {
                for (int j = 0; i < Settings.mapSizeZ; j++)
                {
                    if(mapType[i,j] == 0 )
                        map[i, j] = new Floor(new Vector3((float)(i * Settings.blockSizeX), 0f, (float)(j * Settings.blockSizeZ)), Settings.floorColor);
                    if (mapType[i, j] == 1)
                        map[i, j] = new Wall();
                    else
                        map[i, j] = new BlackHole();

                }
            }

        }

        public void draw(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
        
            for (int i = 0; i < Settings.mapSizeX; i++)
            {
                for ( int j = 0; i < Settings.mapSizeZ; j++)
                {
                    map[i,j].draw(gameTime, graphicsDevice);
                }
            }
        }
    }
}
