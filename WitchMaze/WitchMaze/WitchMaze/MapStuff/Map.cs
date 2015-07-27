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
    class Map
    {
        /// <summary>
        /// Map
        /// </summary>
        public Block[,] map;


        /// <summary>
        /// Constructor for Map
        /// </summary>
        public Map()
        {
            map = new Block[Settings.mapSizeX, Settings.mapSizeZ];
        }

        /// <summary>
        /// returns if a tile at position p is walkable
        /// </summary>
        /// <param name="p">Vector2 position</param>
        /// <returns>bool</returns>
        public bool getTileWalkableAt(Vector2 p)
        {
            return map[(int)p.X, (int)p.Y].walkable;
        }

        /// <summary>
        /// draws the Map
        /// </summary>
        /// <param name="gameTime"></param>
        public void draw()
        {

            for (int i = 0; i < Settings.mapSizeX; i++)
            {
                for ( int j = 0; j < Settings.mapSizeZ; j++)
                {
                    map[i,j].draw();

                }
            }
        }



    }
}
