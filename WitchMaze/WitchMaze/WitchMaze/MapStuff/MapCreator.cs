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
        /// <summary>
        /// double Array to store which Type the Tile is going to be 
        /// </summary>
        public int[,] mapType;

        /// <summary>
        /// stores the Map
        /// </summary>
        public Map map;

        // Types(weil ich nicht weiß, ob hier ein enum shcon sinn macht ^^
        // 0: Floor, 1: Wall, 2: Blackhole

        enum neighbour { up, right, down, left, upLeft, upRight, downRight, downLeft };


        /// <summary>
        /// float for rotating the blocks in different directions 
        /// </summary>
        private float rotate = 0;

        Random rnd = new Random();

        /// <summary>
        /// Constructor for MapCreator, initializes mapType and map
        /// </summary>
        public MapCreator()
        {
            mapType = new int[Settings.getMapSizeX(), Settings.getMapSizeZ()];
            map = new Map();
        }

        /// <summary>
        /// mapType gets filled here with a fancy random function
        /// </summary>
        public void initialize()
        {

            createMaze();

            for (int i = 0; i < Settings.getMapSizeX(); i++)
            {
                for (int j = 0; j < Settings.getMapSizeZ(); j++)
                {
                    // Edge of Labyrinth 
                    if (i == 0 || j == 0 || i == Settings.getMapSizeX() - 1 || j == Settings.getMapSizeZ() - 1)
                    {
                        mapType[i, j] = 1;
                    }


                }
            }

            // BlackHoles noch hinzufügen, nach gleichem Prinzip

        }

        /// <summary>
        /// Method for generating the Map from MapType, Map gets filled here
        /// </summary>
        /// <returns>Map</returns>
        public Map generateMap()
        {
            for (int i = 0; i < Settings.getMapSizeX(); i++)
            {
                for (int j = 0; j < Settings.getMapSizeZ(); j++)
                {
                    Console.Write(mapType[i, j]);
                    if (mapType[i, j] == 0)
                    {
                        map.map[i, j] = new Floor(new Vector3((float)(i * Settings.getBlockSizeX()), 0.0f, (float)(j * Settings.getBlockSizeZ())), Game1.getContent().Load<Model>("bottom"));
                    }
                    if (mapType[i, j] == 1)
                    {
                        float rotation = (float)rnd.Next(3);
                        while (rotate == rotation)//same rotation as before 
                        {
                            rotation = rotation + 1;
                            rotation = rotation % 4;
                        }
                        rotation = rotation % 4;
                        map.map[i, j] = new Wall(Game1.getContent().Load<Model>("cube"), new Vector3((float)(i * Settings.getBlockSizeX()), (float)(Settings.getBlockSizeY()), (float)(j * Settings.getBlockSizeZ())), rotation * 90);
                        rotate = rotation;
                    }
                    if (mapType[i, j] == 2)
                    {
                        map.map[i, j] = new BlackHole(new Vector3((float)(i * Settings.getBlockSizeX()), 0.0f, (float)(j * Settings.getBlockSizeZ())), Game1.getContent().Load<Model>("bottom"));
                    }


                }
                Console.WriteLine();
            }
            return map;
        }


        /// <summary>
        /// function to create random maze, only works with the inner part, 
        /// </summary>
        private void createMaze()
        {
            int numCellsX = 0;
            int numCellsY = 0;

            //determine number of cells depending on even or odd size of maze
            if (Settings.getMapSizeX() % 2 == 1)
                numCellsX = (Settings.getMapSizeX() - 1) / 2;
            else
                numCellsX = (Settings.getMapSizeX() - 2) / 2;

            if (Settings.getMapSizeZ() % 2 == 1)
                numCellsY = (Settings.getMapSizeZ() - 1) / 2;
            else
                numCellsY = (Settings.getMapSizeZ() - 2) / 2;

            // Array to store the Cells in 
            Cell[,] maze = new Cell[numCellsX, numCellsY];


            int MapIndexX = 1;
            int MapIndexY = 1;

            //überalle Cells laufen und initialisieren 
            for (int i = 0; i < numCellsX; i++)
            {
                for (int j = 0; j < numCellsY; j++)
                {
                    maze[i, j] = new Cell(MapIndexX, MapIndexY, i, j, true, true, true, true, true, true, true, true);
                    MapIndexY += 2;

                }
                MapIndexY = 1;
                MapIndexX += 2;
            }

            Stack<Cell> stack = new Stack<Cell>();

            //Random Anfang finden
            int StartX = rnd.Next(numCellsX - 1);
            int StartY = rnd.Next(numCellsY - 1);

            Cell akt = maze[StartX, StartY];
            akt.setVisited(true);
            stack.Push(akt);

            while(stack.Count != null)
            {
                int n = numOfNotVisited(akt);
                if(n == 0)
                {
                    updateMap(akt, maze);
                    stack.Pop();
                }
                else
                {
                    // find next neighbour
                    int[] index = notVisited(akt, n);
                    int random = rnd.Next(n - 1);

                    int finaleIndex = index[random];

                    Cell next = getNext(finaleIndex);
                    next.setVisited(true);

                    // Wand von eigener & next updaten
                    updateWall(ref akt, ref next, finaleIndex);

                    updateMap(akt, maze);
                    stack.Push(next);
                }
            }


        }


        private int numOfNotVisited(Cell cell)
        {
            return 0;
        }

        private int[] notVisited(Cell cell, int length)
        {
            int[] result= new int[length];

            return result;
        }

        private Cell getNext(int index)
        {
            return null;
        }

        private void updateWall(ref Cell akt, ref Cell next, int index)
        {

        }

        private void updateMap(Cell cell, )
    }
}

       
