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
            
            for (int i = 0; i < Settings.mapSizeX; i++)
            {
                for (int j = 0; j < Settings.mapSizeZ; j++)
                {
                    if (i == 0 || j == 0 || i == Settings.mapSizeX - 1 || j == Settings.mapSizeZ - 1)
                        mapType[i, j] = 2;
                    else
                        mapType[i, j] = 0;
                }
            }
            //Console.WriteLine("PlaceWallsRandom");
            //placeWallsRandom(6, 10, 6, 5);
            //placeWallsRandom(4, 6, 4, 8);

            // BlackHoles noch hinzufügen, nach gleichem Prinzip
        }

        public Map generateMap()
        {
            
            for (int i = 0; i < Settings.mapSizeX; i++)
            {
                for (int j = 0; j < Settings.mapSizeZ; j++)
                {
                    Console.Write(mapType[i, j]);
                    if (mapType[i, j] == 0)
                    {
                        // map.map[i, j] = new Floor(new Vector3((float)(i * Settings.blockSizeX), 0f, (float)(j * Settings.blockSizeZ)), Settings.floorColor);
                        map.map[i, j] = new Wall(new Vector3((float)(i * Settings.blockSizeX), 0.0f, (float)(j * Settings.blockSizeZ)), Game1.getContent().Load<Model>("bottom"), true, false);
                    }
                    if (mapType[i, j] == 2)
                    {
                        Console.Write("zeichnen");
                        map.map[i, j] = new Wall(new Vector3((float)(i * Settings.blockSizeX), (float)(Settings.blockSizeY / 2.0f), (float)(j * Settings.blockSizeZ)), Game1.getContent().Load<Model>("cube"), false, false);
                        //y- Position wird in der eigenen KLasse gesetzt ... ist einfacher
                    }    
                    if (mapType[i,j] == 1)
                    {
                        //map.map[i, j] = new BlackHole(new Vector3((float)(i * Settings.blockSizeX), 0f, (float)(j * Settings.blockSizeZ)), Settings.blackHoleColor);
                        map.map[i, j] = new Wall(new Vector3((float)(i * Settings.blockSizeX), 0.0f, (float)(j * Settings.blockSizeZ)), Game1.getContent().Load<Model>("bottom"), true, true);
                    }
                        

                }
                Console.WriteLine();
            }
            return map;
        }

        public void placeWallsRandom(int minlengthWall, int maxlengthWall, int granularity, int numWalls)
        {
            Random r = new Random();
            for (int i = 0; i < numWalls; i++)
            {
                // find startposition
                int x = granularity * (r.Next(0, (Settings.mapSizeX - 1) / granularity));
                int y = granularity * (r.Next(0, (Settings.mapSizeZ - 1) / granularity));

                int direction = r.Next(0, 3);

                int length = granularity * r.Next(minlengthWall, maxlengthWall) * 1;
                // draw wall  
                for(int j = 0; j < length; j++)
                {
                    //Randbehandlung
                    if (x < 0) x = 0;
                    if (x >= Settings.mapSizeX) x = Settings.mapSizeX - 1;
                    if (y < 0) y = 0;
                    if (y >= Settings.mapSizeZ) y = Settings.mapSizeZ - 1;
                    
                    // filling the Map with walls
                    mapType[x, y] = 2;
                    Console.WriteLine("getsFilledwithWall");

                    //moving to next Brick
                    if (direction == 0) x++;
                    if (direction == 1) y++;
                    if (direction == 2) x--;
                    if (direction == 3) y--;
                }
            }
        }

          

    }
}
