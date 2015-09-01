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
        public enum tiles { floor, wall, blackhole };

        enum neighbour { up, right, down, left, upLeft, upRight, downRight, downLeft };


        /// <summary>
        /// float for rotating the walls in different directions 
        /// </summary>
        private float rotateWall = 0;

        /// <summary>
        /// float for rotating the floors in different directions 
        /// </summary>
        private float rotateFloor = 0;

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
            insertWall();
            insertBlackHoles(7);
            insertRandomFloorTiles(15);
            insertWall();
            emptyStartPositions();
        }

        /// <summary>
        /// function which inserts the wall of the maze
        /// </summary>
        private void insertWall()
        {
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
                    
                    if (mapType[i, j] == (int)tiles.floor)
                    {
                        float rotation = (float)rnd.Next(3);
                        while (rotateFloor == rotation)//same rotation as before 
                        {
                            rotation = rotation + 1;
                            rotation = rotation % 4;
                        }
                        rotation = rotation % 4;
                        Vector3 position = new Vector3((float)(i * Settings.getBlockSizeX()), 0.0f, (float)(j * Settings.getBlockSizeZ()));
                        Floor floor = new Floor(position, Game1.getContent().Load<Model>("bottom"), rotation * (float)1.57, Game1.getContent().Load<Texture2D>("bottomTexture"));
                        map.setMap(floor, i, j);
                        rotateFloor = rotation;
                    }
                    else if (mapType[i, j] == (int)tiles.wall)
                    {
                        float rotation = (float)rnd.Next(3);
                        while (rotateWall == rotation)//same rotation as before 
                        {
                            rotation = rotation + 1;
                            rotation = rotation % 4;
                        }
                        rotation = rotation % 4;
                        Wall wall = new Wall(Game1.getContent().Load<Model>("cube"), new Vector3((float)(i * Settings.getBlockSizeX()), (float)(Settings.getBlockSizeY()), (float)(j * Settings.getBlockSizeZ())), rotation * (float)1.57, Game1.getContent().Load<Texture2D>("Textures/WallTextures"));
                        map.setMap(wall, i, j);
                        rotateWall = rotation;
                    }
                    else
                    {
                        Vector3 position = new Vector3((float)(i * Settings.getBlockSizeX()), 0.0f, (float)(j * Settings.getBlockSizeZ()));
                        BlackHole blackhole = new BlackHole(position, Game1.getContent().Load<Model>("Models/MapStuff/BlackHole"), findTransportPoint(position), Game1.getContent().Load<Texture2D>("Models/MapStuff/BlackHoleTexture"));
                        map.setMap(blackhole, i, j);
                    }
                }
            }
            return map;
        }


        /// <summary>
        /// function which calculates the startpositions of the Players
        /// </summary>
        /// <param name="anzahl">number of Players</param>
        /// <returns>List with startPositions</returns>
        public List<Vector3> startPositions(int anzahl)
        {
            int xMax = Settings.getMapSizeX() - 3;
            int yMax = Settings.getMapSizeZ() - 3;

            List<Vector3> liste = new List<Vector3>();

            if (anzahl >= 1) //links unten
            {
                liste.Add(new Vector3(xMax, (float)0.22, 2));
            }
            if (anzahl >= 2) //rechts oben 
            {
                liste.Add(new Vector3(2, (float)0.22, yMax));
            }
            if (anzahl >= 3) //links oben 
            {
                liste.Add(new Vector3(2, (float)0.22, 2));
            }
            if (anzahl >= 4) //rechts unten 
            {
                liste.Add(new Vector3(xMax, (float)0.22, yMax));
            }
            return liste;
        }

        private void emptyStartPositions()
        {
            List<Vector3> liste = startPositions(4);
            for (int i = 0; i < liste.Count; i++)//Liste mit Startpositionen durchgehen 
            {
                int x = (int)liste.ElementAt<Vector3>(i).X;
                int y = (int)liste.ElementAt<Vector3>(i).Z;
                //for (int j = -1; j < 2; j++)
                //{
                //    for (int k = -1; k < 2; k++)
                //    {
                //        mapType[x + j, y + k] = (int)tiles.floor;
                //    }
                //}
                mapType[x, y] = (int)tiles.floor;
                if(mapType[x+1, y] ==(int)tiles.wall)mapType[x + 1, y] = (int)tiles.floor;

            }

            
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

            //initialize all Cells
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

            //find random begin in the middleof the map 
            int thirdX = numCellsX / 3;
            int thirdY = numCellsY / 3;
            int StartX = rnd.Next(thirdX) + thirdX;
            int StartY = rnd.Next(thirdY) + thirdY;

            Cell akt = maze[StartX, StartY];
            akt.setVisited(true);
 
            Stack<Cell> stack = new Stack<Cell>();
            stack.Push(akt);
            int lastTurn = 0;

            while (stack.Count != 0)//stack.Count != 0)
            {
                akt = stack.Peek();
                int n = numOfNotVisited(maze, akt);
                if (n == 0)
                {
                    updateMap(akt);
                    stack.Pop();
                }
                else if(n == 1)
                {
                    int[] index = notVisited(maze, akt, n);
                    int finaleIndex = index[0];
                    lastTurn = finaleIndex;
                    Cell next = getNext(maze, akt, finaleIndex);
                    next.setVisited(true);
                    updateWall(ref akt, ref next, finaleIndex);
                    updateMap(akt);
                    stack.Push(next);
                }
                else
                {
                    // find next neighbour
                    int[] index = notVisited(maze, akt, n);
                    int random = rnd.Next(n - 1);
                    int finaleIndex = index[random];

                    while(finaleIndex == lastTurn)
                    {
                        finaleIndex = (index[random + 1 % n]);
                    }
                        
                    Cell next = getNext(maze, akt, finaleIndex);
                    next.setVisited(true);

                    // Wand von eigener & next updaten
                    updateWall(ref akt, ref next, finaleIndex);

                    updateMap(akt);
                    stack.Push(next);
                    
                }
            }
            


        }

        /// <summary>
        /// function to insert more floortiles, to make level less schlauchig
        /// </summary>
        /// <param name="AnzExtraFloor">how many extra floortiles to insert</param>
        private void insertRandomFloorTiles(int AnzExtraFloor)
        {
            // insert random floor tiles, to make it more interesting
            int[] arrayX = new int[AnzExtraFloor];
            int[] arrayY = new int[AnzExtraFloor];
            for (int i = 0; i < AnzExtraFloor; i++)
            {
                int counter = 0;
                int x = rnd.Next(Settings.getMapSizeX() - 1);
                int y = rnd.Next(Settings.getMapSizeZ() - 1);
                bool swap = true;
                while ((mapType[x, y] == (int)tiles.floor || mapType[x, y] == (int)tiles.blackhole || distance(arrayX, x, arrayY, y, i) == true || neighboorhoud(x, y) == false) & counter < 400)
                {
                    if (swap)
                    {
                        x = rnd.Next(Settings.getMapSizeX() - 1);
                        swap = false;
                    }
                    else
                    {
                        y = rnd.Next(Settings.getMapSizeZ() - 1);
                        swap = true;
                    }
                    counter++;
                }
                mapType[x, y] = (int)tiles.floor;
                arrayX[i] = x;
                arrayY[i] = y;

            }
        }

        /// <summary>
        /// function to check if the randomly inserted floor is connected to another floor or blackhole
        /// </summary>
        /// <param name="x">position of randomly inserted tile in x direction</param>
        /// <param name="y">position of randomly inserted tile in y direction</param>
        /// <returns>true if there is another floor or blackhole in direct neighboorhoud, false if there is not </returns>
        private bool neighboorhoud(int x, int y)
        {
            bool result = false;
            if(x - 1 > 0)
            {
                if (mapType[x - 1, y] == (int)tiles.floor ||mapType[x-1, y] == (int)tiles.blackhole) result = true;
            }
            if (x + 1 < Settings.getMapSizeX())
            {
                if (mapType[x + 1, y] == (int)tiles.floor || mapType[x + 1, y] == (int)tiles.blackhole) result = true;
            }
            if (y - 1 > 0)
            {
                if (mapType[x, y - 1] == (int)tiles.floor || mapType[x, y - 1] == (int)tiles.blackhole) result = true;
            }
            if (y + 1 < Settings.getMapSizeZ())
            {
                if (mapType[x, y + 1] == (int)tiles.floor || mapType[x, y + 1] == (int)tiles.blackhole) result = true;
            }
            
            return result;
        }

        /// <summary>
        /// function to calculate the distance between an already inserted random floortile an dthe new random floortile
        /// </summary>
        /// <param name="arrayX">already used x parameters</param>
        /// <param name="valueX">x value to check</param>
        /// <param name="arrayY">already used y parameters</param>
        /// <param name="valueY">y value to check</param>
        /// <param name="index">index how many cells have been created</param>
        /// <returns></returns>
        private bool distance(int[] arrayX, int valueX, int[]arrayY, int valueY, int index)
        {
            if (index == 0) return false;
            bool distance = false;
            for( int i = 0; i < index ; i++)
            {
                if(Math.Abs(arrayX[i] - valueX) < 2 && Math.Abs(arrayY[i] - valueY) < 2)
                {
                    distance = true;
                    break;
                }
            }
            return distance;
        }

        /// <summary>
        /// function which returns number of not visited neighbour cells
        /// </summary>
        /// <param name="maze">Double Array with all Cells in it </param>
        /// <param name="cell">current Cell</param>
        /// <returns>integer</returns>
        private int numOfNotVisited(Cell[,] maze, Cell cell)
        {
            int result = 0;
            for(int i = 0; i < 4; i++)
            {
                if(getNext(maze, cell, i) != null)
                {
                    if(getNext(maze, cell, i).getVisited() == false)
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// function which returns an array with the indices of all neighbour cells which have not been visited yet 
        /// </summary>
        /// <param name="maze">double array with all cells </param>
        /// <param name="cell">current cell</param>
        /// <param name="length">number of neighbour cells which have not been visited yet </param>
        /// <returns>array of integer</returns>
        private int[] notVisited(Cell[,] maze, Cell cell, int length)
        {
            int[] result= new int[length];
            int index = 0;
            for (int i = 0; i < 4; i++ )
            {
                if(getNext(maze, cell, i) != null)
                {
                    if(getNext(maze, cell,i).getVisited() == false)
                    {
                        result[index] = i;
                        index++;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// function which returns the next cell in a given direction
        /// </summary>
        /// <param name="maze">double array with all cells</param>
        /// <param name="cell">current cell</param>
        /// <param name="index">index in which directio next Cell lies </param>
        /// <returns>neighbour Cell in wanted direction</returns>
        private Cell getNext(Cell[,] maze, Cell cell, int index) 
        {
            int newX = 0;
            int newY = 0;
            if (index == (int)neighbour.up && cell.getCellIndexX() - 1 >= 0)
            {
                newX = cell.getCellIndexX() - 1;
                newY = cell.getCellindexY();
                return maze[newX, newY];
            }
            else if (index == (int)neighbour.down && cell.getCellIndexX() + 1 < maze.GetLength(0))
            {
                newX = cell.getCellIndexX() + 1;
                newY = cell.getCellindexY();
                return maze[newX, newY];
            }
            else if (index == (int)neighbour.left && cell.getCellindexY() - 1 >= 0)
            {
                newX = cell.getCellIndexX();
                newY = cell.getCellindexY() - 1;
                return maze[newX, newY];
            }
            else if (index == (int)neighbour.right && cell.getCellindexY() + 1 < maze.GetLength(1))
            {
                newX = cell.getCellIndexX();
                newY = cell.getCellindexY() + 1;
                return maze[newX, newY];
            }
            else if (index == (int)neighbour.upLeft && cell.getCellIndexX() - 1 >= 0 && cell.getCellindexY() - 1 >= 0)
            {
                newX = cell.getCellIndexX() - 1;
                newY = cell.getCellindexY() - 1;
                return maze[newX, newY];
            }
            else if (index == (int)neighbour.upRight && cell.getCellIndexX() - 1 >= 0 && cell.getCellindexY() + 1 < maze.GetLength(1))
            {
                newX = cell.getCellIndexX() - 1;
                newY = cell.getCellindexY() + 1;
                return maze[newX, newY];
            }
            else if (index == (int)neighbour.downRight && cell.getCellIndexX() + 1 < maze.GetLength(0) && cell.getCellindexY() + 1 < maze.GetLength(1))
            {
                newX = cell.getCellIndexX() + 1;
                newY = cell.getCellindexY() + 1;
                return maze[newX, newY];
            }
            else if (index == (int)neighbour.downLeft && cell.getCellIndexX() + 1 < maze.GetLength(0) && cell.getCellindexY() - 1 >= 0)
            {
                newX = cell.getCellIndexX() + 1;
                newY = cell.getCellindexY() - 1;
                return maze[newX, newY];
            }
            else
            {
                newX = cell.getCellIndexX() + 1;
                newY = cell.getCellindexY();
                return null;
            }
        }

        /// <summary>
        /// function to update the Walls of two neighbour Cells 
        /// </summary>
        /// <param name="akt">reference of current cell</param>
        /// <param name="next">reference of next cell</param>
        /// <param name="index">index which cell to update</param>
        private void updateWall(ref Cell akt, ref Cell next, int index)//später noch auf Diagonale Zellen erweitern, bis jetzt noch keine Diagonalen
        {
            if (next == null)
            {
                if(akt != null)
                {
                    akt.setWall(index, false);
                }
            }
            else if (akt == null)
            {
                if (index == (int)neighbour.up)
                {
                    next.setWall((int)neighbour.down, false);
                }
                else if (index == (int)neighbour.down)
                {
                    next.setWall((int)neighbour.up, false);
                }
                else if (index == (int)neighbour.left)
                {
                    next.setWall((int)neighbour.right, false);
                }
                else if (index == (int)neighbour.right)
                {
                    next.setWall((int)neighbour.left, false);
                }
            }
            else
            {
                akt.setWall(index, false);
                if (index == (int)neighbour.up)
                {
                    next.setWall((int)neighbour.down, false);
                }
                else if (index == (int)neighbour.down)
                {
                    next.setWall((int)neighbour.up, false);
                }
                else if (index == (int)neighbour.left)
                {
                    next.setWall((int)neighbour.right, false);
                }
                else if (index == (int)neighbour.right)
                {
                    next.setWall((int)neighbour.left, false);
                }
            }
        }

        /// <summary>
        /// function to update the map with the Walls of the Cell
        /// </summary>
        /// <param name="cell">cell with which map is updated</param>
        private void updateMap(Cell cell)
        {
            int x = cell.getMapIndexX();
            int y = cell.getMapIndexY();

            mapType[x, y] = (int)tiles.floor;

            mapType[x - 1, y] = cell.getWall((int)neighbour.up) == true ? 1 : 0;
            mapType[x + 1, y] = cell.getWall((int)neighbour.down) == true ? 1 : 0;
            mapType[x, y - 1] = cell.getWall((int)neighbour.left) == true ? 1 : 0;
            mapType[x, y + 1] = cell.getWall((int)neighbour.right) == true ? 1 : 0;
            mapType[x - 1, y - 1] = cell.getWall((int)neighbour.upLeft) == true ? 1 : 0;
            mapType[x - 1, y + 1] = cell.getWall((int)neighbour.upRight) == true ? 1 : 0;
            mapType[x + 1, y + 1] = cell.getWall((int)neighbour.downRight) == true ? 1 : 0;
            mapType[x + 1, y - 1] = cell.getWall((int)neighbour.downLeft) == true ? 1 : 0;
        }

        /// <summary>
        /// function to insert blackholes into the map
        /// </summary>
        private void insertBlackHoles(int anzahl)
        {
            for ( int i = 0; i < anzahl; i++)
            {
                int x = 1;
                int y = 1;
                bool swap = true;
                int counter = 0;
                while( (mapType[x,y] == (int)tiles.floor || mapType[x,y] == (int)tiles.blackhole  || x >= Settings.getMapSizeX() - 1 || y >= Settings.getMapSizeZ() - 1|| distanceToBlackHole(x,y) || EndOfWall(x,y)== false) & counter < 400)
                {
                    if (swap)
                    {
                        x = rnd.Next(Settings.getMapSizeX() - 2)+1;
                        swap = false;
                    }
                    else
                    {
                        y = rnd.Next(Settings.getMapSizeZ() - 2) +1;
                        swap = true;
                    }
                    counter++;
                }
                mapType[x, y] = (int)tiles.blackhole;
            }
        }

        /// <summary>
        /// function which returns if the new blackhole is to near to an old blackhole
        /// </summary>
        /// <param name="x">x parameter of new blackhole</param>
        /// <param name="y">y parameter of new blackhole</param>
        /// <returns></returns>
        private bool distanceToBlackHole(int x, int y)
        {
            bool result = false;
            for (int i = -3; i < 4; i++ )
            {
                for(int j = -3; j < 4; j++)
                {
                    if(x + i >= 0 && y + j >= 0 && x+i <Settings.getMapSizeX() && y+j < Settings.getMapSizeZ()) 
                    {
                        if(mapType[x+i, y+j] == (int)tiles.blackhole) 
                        {
                            result = true;
                            break;
                        }
                    }

                }
            }
            return result;
        }

        private bool EndOfWall(int x, int y)
        {
            int walls = 0;

            if (x - 1 > 0)
            {
                if (mapType[x - 1, y] == (int)tiles.wall) walls++;
            }
            if (x + 1 < Settings.getMapSizeX())
            {
                if (mapType[x + 1, y] == (int)tiles.wall) walls++; ;
            }
            if (y - 1 > 0)
            {
                if (mapType[x, y - 1] == (int)tiles.wall) walls++;
            }
            if (y + 1 < Settings.getMapSizeZ())
            {
                if (mapType[x, y + 1] == (int)tiles.wall) walls++;
            }
            if (walls > 1) return false;
            return true;

        }


        /// <summary>
        /// function to find the transportpoints for the blackholes
        /// </summary>
        /// <param name="position">position of Blackhole, Vector3</param>
        /// <returns>Vector3, transportpoint </returns>
        private Vector3 findTransportPoint(Vector3 position)
        {
            Vector3  result = new Vector3();
            result.Y = 0;
            int x = 0;
            int y = 0;
            bool swap = true;
            while (mapType[x, y] == (int)tiles.wall || mapType[x, y] == (int)tiles.blackhole || x >= Settings.getMapSizeX() || y >= Settings.getMapSizeZ() || Math.Abs(x - position.X) < 8 || Math.Abs(y - position.Z) < 8)
            {
                if (swap)
                {
                    x = rnd.Next(Settings.getMapSizeX());
                    swap = false;
                }
                else
                {
                    y = rnd.Next(Settings.getMapSizeZ());
                    swap = true;
                }
            }
            result.X = x;
            result.Z = y;
            return result; 

        }
    }
}
