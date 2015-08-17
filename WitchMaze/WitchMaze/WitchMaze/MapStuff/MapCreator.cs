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
        enum tiles { floor, wall, blackhole };

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
            Console.WriteLine();
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
            insertBlackHoles();
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
                        Vector3 position = new Vector3((float)(i * Settings.getBlockSizeX()), 0.0f, (float)(j * Settings.getBlockSizeZ()));
                        map.map[i, j] = new BlackHole(position, Game1.getContent().Load<Model>("bottom"), findTransportPoint(position));
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

            //Stack<Cell> stack = new Stack<Cell>();
            Queue<Cell> queue = new Queue<Cell>();

            //Random Anfang finden, kann auch noch mehr in die Mitte verlegt werden 
            int thirdX = numCellsX / 3;
            int thirdY = numCellsY / 3;
            int StartX = rnd.Next(thirdX) + thirdX;
            int StartY = rnd.Next(thirdY) + thirdY;

            Cell akt = maze[StartX, StartY];
            akt.setVisited(true);
            //stack.Push(akt);
            queue.Enqueue(akt);

            while(queue.Count != 0)//stack.Count != 0)
            {
                akt = queue.Peek();
                //akt = stack.Peek();
                int n = numOfNotVisited(maze, akt);
                if(n == 0)
                {
                    updateMap(akt);
                    //stack.Pop();
                    queue.Dequeue();
                }
                else
                {
                    
                    // find next neighbour
                    int[] index = notVisited(maze, akt, n);
                    //int random = rnd.Next(n - 1);
                    //int finaleIndex = index[random];

                    for( int i = 0; i < n; i++)
                    {
                        int finaleIndex = index[i];
                        Cell next = getNext(maze, akt, finaleIndex);
                        next.setVisited(true);

                        // Wand von eigener & next updaten
                        updateWall(ref akt, ref next, finaleIndex);

                        updateMap(akt);
                        //stack.Push(next);
                        queue.Enqueue(next);
                    }
                }
            }


        }


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
                        //debug ausgabe 
                        if (i == (int)neighbour.up) Console.WriteLine("upper Cell not visited");
                        if (i == (int)neighbour.down) Console.WriteLine("lower Cell not visited");
                        if (i == (int)neighbour.left) Console.WriteLine("left Cell not visited");
                        if (i == (int)neighbour.right) Console.WriteLine("right Cell not visited");
                    }
                }
            }
            Console.WriteLine("Insgesamt wurden "+ result + " Nachbarn von "+cell.getCellIndexX()+" , "+cell.getCellindexY()+" noch nicht besucht");
            return result;
        }

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
                        if (i == (int)neighbour.up) Console.WriteLine("upper Cell in neighbourList");
                        if (i == (int)neighbour.down) Console.WriteLine("lower Cell in neighbourList");
                        if (i == (int)neighbour.left) Console.WriteLine("left Cell in neighbourList");
                        if (i == (int)neighbour.right) Console.WriteLine("right Cell in neighbourList ");
                    }
                }
            }
            Console.Write("Ergebnis aus notVisited() : ");
            for(int i = 0; i < length; i++)
            {
                Console.Write(result[i] + ", ");
            }
            Console.WriteLine();
            return result;
        }

        private Cell getNext(Cell[,] maze, Cell cell, int index) // hier Grenzen abfangen, und umrechnen, bei Index Fehlern ist hier eine Quelle ! 
        {
            int newX = 0;
            int newY = 0;
            if (index == (int)neighbour.up && cell.getCellIndexX() - 1 >= 0)
            {
                newX = cell.getCellIndexX() - 1;
                newY = cell.getCellindexY();
                Console.WriteLine("Die obere Zelle ("+newX+","+newY+") von "+cell.getCellIndexX()+ ", "+cell.getCellindexY()+"wird zurückgegeben.");
                return maze[newX, newY];
            }
            else if (index == (int)neighbour.down && cell.getCellIndexX() + 1 < maze.GetLength(0))
            {
                newX = cell.getCellIndexX() + 1;
                newY = cell.getCellindexY();
                Console.WriteLine("Die untere Zelle (" + newX + "," + newY + ") von " + cell.getCellIndexX() + ", " + cell.getCellindexY() + "wird zurückgegeben.");
                return maze[newX, newY];
            }
            else if (index == (int)neighbour.left && cell.getCellindexY() - 1 >= 0)
            {
                newX = cell.getCellIndexX();
                newY = cell.getCellindexY() - 1;
                Console.WriteLine("Die linke Zelle (" + newX + "," + newY + ") von " + cell.getCellIndexX() + ", " + cell.getCellindexY() + "wird zurückgegeben.");
                return maze[newX, newY];
            }
            else if (index == (int)neighbour.right && cell.getCellindexY() + 1 < maze.GetLength(1))
            {
                newX = cell.getCellIndexX();
                newY = cell.getCellindexY() + 1;
                Console.WriteLine("Die rechte Zelle (" + newX + "," + newY + ") von " + cell.getCellIndexX() + ", " + cell.getCellindexY() + "wird zurückgegeben.");
                return maze[newX, newY];
            }
            else if (index == (int)neighbour.upLeft && cell.getCellIndexX() - 1 >= 0 && cell.getCellindexY() - 1 >= 0)
            {
                newX = cell.getCellIndexX() - 1;
                newY = cell.getCellindexY() - 1;
                Console.WriteLine("Die linke obere Zelle (" + newX + "," + newY + ") von " + cell.getCellIndexX() + ", " + cell.getCellindexY() + "wird zurückgegeben.");
                return maze[newX, newY];
            }
            else if (index == (int)neighbour.upRight && cell.getCellIndexX() - 1 >= 0 && cell.getCellindexY() + 1 < maze.GetLength(1))
            {
                newX = cell.getCellIndexX() - 1;
                newY = cell.getCellindexY() + 1;
                Console.WriteLine("Die rechte obere Zelle (" + newX + "," + newY + ") von " + cell.getCellIndexX() + ", " + cell.getCellindexY() + "wird zurückgegeben.");
                return maze[newX, newY];
            }
            else if (index == (int)neighbour.downRight && cell.getCellIndexX() + 1 < maze.GetLength(0) && cell.getCellindexY() + 1 < maze.GetLength(1))
            {
                newX = cell.getCellIndexX() + 1;
                newY = cell.getCellindexY() + 1;
                Console.WriteLine("Die rechte untere Zelle (" + newX + "," + newY + ") von " + cell.getCellIndexX() + ", " + cell.getCellindexY() + "wird zurückgegeben.");
                return maze[newX, newY];
            }
            else if (index == (int)neighbour.downLeft && cell.getCellIndexX() + 1 < maze.GetLength(0) && cell.getCellindexY() - 1 >= 0)
            {
                newX = cell.getCellIndexX() + 1;
                newY = cell.getCellindexY() - 1;
                Console.WriteLine("Die linke untere Zelle (" + newX + "," + newY + ") von " + cell.getCellIndexX() + ", " + cell.getCellindexY() + "wird zurückgegeben.");
                return maze[newX, newY];
            }
            else
            {
                newX = cell.getCellIndexX() + 1;
                newY = cell.getCellindexY();
                Console.WriteLine("Nächste Zelle (" + newX + "," + newY + ") lag ausserhalb vom Feld, oder es wurde ein falscher Index"+index+" in die Funktion gegeben");
                return null;
            }
        }

        private void updateWall(ref Cell akt, ref Cell next, int index)//später noch auf Diagonale Zellen erweitern, bis jetzt noch keine Diagonalen
        {
            if (next == null)
            {
                Console.WriteLine(" Die nächste Zelle ist null.");
                if(akt == null)
                {
                    Console.WriteLine("auch die aktuelle Zelle ist null, somit wird nichts geupdatet");
                }
                else
                {
                    Console.WriteLine("Die aktuelle Zelle ist nicht null, somit wird nur sie geupdatet.");
                    akt.setWall(index, false);
                }
            }
            else if (akt == null)
            {
                Console.WriteLine(" Nur die aktuelle zelle ist Null, nicht die nächste. Nur die nächste wird geupdatet.");
                if (index == (int)neighbour.up)
                {
                    Console.WriteLine("Von der nächsten Zelle wurde die untere Wall auf false gesetzt");
                    next.setWall((int)neighbour.down, false);
                }
                else if (index == (int)neighbour.down)
                {
                    Console.WriteLine("Von der nächsten Zelle wurde die obere Wall auf false gesetzt");
                    next.setWall((int)neighbour.up, false);
                }
                else if (index == (int)neighbour.left)
                {
                    Console.WriteLine("Von der nächsten Zelle wurde die rechte Wall auf false gesetzt");
                    next.setWall((int)neighbour.right, false);
                }
                else if (index == (int)neighbour.right)
                {
                    Console.WriteLine("Von der nächsten Zelle wurde die linke Wall auf false gesetzt");
                    next.setWall((int)neighbour.left, false);
                }
                else Console.WriteLine("Mehr ist momentan noch nicht implementiert");
            }
            else
            {
                akt.setWall(index, false);
                if (index == (int)neighbour.up)
                {
                    Console.WriteLine("Von der aktuellen Zelle wurde die obere Wall auf false gesetzt");
                    next.setWall((int)neighbour.down, false);
                }
                else if (index == (int)neighbour.down)
                {
                    Console.WriteLine("Von der aktuellen Zelle wurde die untere Wall auf false gesetzt");
                    next.setWall((int)neighbour.up, false);
                }
                else if (index == (int)neighbour.left)
                {
                    Console.WriteLine("Von der aktuellen Zelle wurde die linke Wall auf false gesetzt");
                    next.setWall((int)neighbour.right, false);
                }
                else if (index == (int)neighbour.right)
                {
                    Console.WriteLine("Von der aktuellen Zelle wurde die rechte Wall auf false gesetzt");
                    next.setWall((int)neighbour.left, false);
                }
                else Console.WriteLine("Mehr ist momentan noch nicht implementiert");
            }
        }

        private void updateMap(Cell cell)// hier ist auch eine Fehlerquelle für Index kram 
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

            for (int i = 0; i < Settings.getMapSizeX(); i++)
            {
                for(int j = 0; j < Settings.getMapSizeZ(); j++)
                {
                    Console.Write(mapType[i, j]);
                }
                Console.WriteLine();
            }
        }


        private void insertBlackHoles()
        {
            int anzahl = Settings.getMapSizeX() * Settings.getMapSizeZ() / 30;
            for ( int i = 0; i < anzahl; i++)
            {
                int x = 0;
                int y = 0;
                bool swap = true;
                while( mapType[x,y] == (int)tiles.wall || mapType[x,y] == (int)tiles.blackhole || x >= Settings.getMapSizeX() || y >= Settings.getMapSizeZ() || distanceToBlackHole(x,y))
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
                mapType[x, y] = (int)tiles.blackhole;
            }
        }

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
