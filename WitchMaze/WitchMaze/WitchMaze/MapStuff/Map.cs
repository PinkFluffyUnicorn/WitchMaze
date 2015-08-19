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
        Block[,] map;

        public void setMap(Block _map, int x, int y)
        {
            map[x,y] = _map;
        }

        public Block[,] getMap(int x, int y)
        {
            return map;
        }

        

        /// <summary>
        /// Constructor for Map
        /// </summary>
        public Map()
        {
            map = new Block[Settings.getMapSizeX(), Settings.getMapSizeZ()];
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
     /// function to draw the map
     /// </summary>
     /// <param name="projection">ProjectionMatrix</param>
     /// <param name="camera">CameraMatrix</param>
        public void draw(Matrix projection, Matrix camera)
        {

            for (int i = 0; i < Settings.getMapSizeX(); i++)
            {
                for ( int j = 0; j < Settings.getMapSizeZ(); j++)
                {
                    map[i,j].draw(projection, camera);
                }
            }
        }



    }
}
