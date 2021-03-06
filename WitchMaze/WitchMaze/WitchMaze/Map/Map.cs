﻿using System;
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

        public Map()
        {
            map = new Block[Settings.mapSizeX, Settings.mapSizeZ];
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
