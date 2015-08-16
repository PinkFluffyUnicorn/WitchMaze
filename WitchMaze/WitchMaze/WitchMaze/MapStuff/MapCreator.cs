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

        /// <summary>
        /// float for rotating the blocks in different directions 
        /// </summary>
        private float help = 0;

        /// <summary>
        /// Constructor for MapCreator, initializes mapType and map
        /// </summary>
        /// 


        Random rnd = new Random();
        public MapCreator()
        {
            mapType = new int[Settings.getMapSizeX(), Settings.getMapSizeZ()];
            map = new Map();
        }

        /// <summary>
        /// mapType gets filled here, here is the Place for the fancy random Function ^^
        /// </summary>
        public void initialize()
        {
            
            for (int i = 0; i < Settings.getMapSizeX(); i++)
            {
                for (int j = 0; j < Settings.getMapSizeZ(); j++)
                {

                    //chess pattern inside
                   /* if ((i % 2 == 1 && j % 2 == 1) || (i % 2 == 0 && j % 2 == 0))
                    {
                        mapType[i, j] = 2;
                    }
                    if ((i % 2 == 1 && j % 2 == 0) || (i % 2 == 0 && j % 2 == 1))
                    {
                        mapType[i, j] = 1;
                    }*/


                    // Edge of Labyrinth 
                    if (i == 0 || j == 0 || i == Settings.getMapSizeX() - 1 || j == Settings.getMapSizeZ() - 1)
                    {
                            mapType[i, j] = 2;
                    }


                }
            }

            //createMaze();

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
                        map.map[i, j] = new Floor(new Vector3((float)(i * Settings.getBlockSizeX()), 0.0f, (float)(j * Settings.getBlockSizeZ())),Game1.getContent().Load<Model>("bottom"));
                    }
                    else if (mapType[i, j] == 2)
                    {
                        float rotation = (float)rnd.Next(3);
                        while(help == rotation)//same rotation as before 
                        {
                            rotation = rotation + 1;
                            rotation = rotation % 4;
                        }
                        rotation = rotation % 4;
                        map.map[i, j] = new Wall(Game1.getContent().Load<Model>("cube"), new Vector3((float)(i * Settings.getBlockSizeX()), (float)(Settings.getBlockSizeY()), (float)(j * Settings.getBlockSizeZ())), rotation * 90);
                        help = rotation;
                    }    
                    else //if (mapType[i,j] == 1)
                    {
                        map.map[i, j] = new BlackHole(new Vector3((float)(i * Settings.getBlockSizeX()) , 0.0f, (float)(j * Settings.getBlockSizeZ())), Game1.getContent().Load<Model>("bottom"));
                    }
                        

                }
            }
            return map;
        }


        /// <summary>
        /// function to create random maze, only works with the inner part, 
        /// </summary>
        public void createMaze()
        {
            int numCellsX = 0;
            int numCellsY = 0;

            
            
            //determine number of cells 
            if (Settings.getMapSizeX() % 2 == 1)
                numCellsX = (Settings.getMapSizeX() - 2) / 2;
            else
                numCellsX = (Settings.getMapSizeX() - 2) / 2;


            if (Settings.getMapSizeZ() % 2 == 1)
                numCellsY = (Settings.getMapSizeZ()-2) / 2;
            else
                numCellsY = (Settings.getMapSizeZ() - 2) / 2;


            // Array to store the Cells in 
            Cell[,] maze = new Cell[numCellsX, numCellsY];
 

            //index of cells in Map 
            int CellindexX = 3;
            int CellindexY = 3;


            // Create Cells with right number of Walls 
            for(int i = 0; i < numCellsX; i++)
            {
                for(int j = 0;j < numCellsY; j++)
                {
                    //Cse corner left down 
                    if (i == numCellsX - 1 && Settings.getMapSizeX() % 2 == 0 && j == numCellsY - 1 && Settings.getMapSizeZ() % 2 == 0)
                    {
                        maze[i, j] = new Cell(CellindexX, CellindexY, true, false, false, true, true, false, false, false);
                    }
                    //case: when number of Tiles in x Direction is straight and the last Cell misses the last third (upright, right, downright
                    if (i == numCellsX - 1 && Settings.getMapSizeX() % 2 == 0)
                    {
                        maze[i, j] = new Cell(CellindexX, CellindexY, true, true, false, true, true, false, true, false);
                    }
                    //same case for y - Direction, missing (bottom, bottomleft, bottomright)
                    else if (j == numCellsY - 1 && Settings.getMapSizeZ() % 2 == 0)
                    {
                        maze[i, j] = new Cell(CellindexX, CellindexY, true, false, true, true, true, true, false, false);
                    }
                    else
                    {
                        maze[i + 1, j+1] = new Cell(CellindexX, CellindexY, true, true, true, true, true, true, true, true);
                    }
                    // Corner needs an extra thing
                    CellindexY += 2;
                }
                CellindexY = 3;
                CellindexX += 2;
            }

            
            //find path through Cells 
            // start at random Cell
            // mit Cellindex arbeiten
            //schon besuchte numCellsX auf Stack ablegen
            Stack<Cell> stack = new Stack<Cell>();
            ownFunctions.ownRandom rnd = new ownFunctions.ownRandom();
            double nul = 1;
            double numCellsA = (double)numCellsX - 1;
            double numCellsB = (double)numCellsY - 1;
            double xStartHelp = rnd.ownRandomFunction(nul, numCellsA);
            double yStartHelp = rnd.ownRandomFunction(nul, numCellsB);



            int xStart = (int)xStartHelp;
            int yStart = (int)yStartHelp;

            stack.Push(maze[xStart, yStart]);
            maze[xStart, yStart].setVisited(true);

            while(stack.Count != 0)
            {
                Cell akt = stack.Peek();
                // choose next Cell to visit, if not possible, pop
                // if eine Nachbarzelle noch unbesucht, tue diese auf den Stack und mach die Wall dazwischn weg, hier auch ggf. Diagonale entfernen 
                int n = numOfNotVisited(akt, maze);
                if (n != 0)
                {
                    
                    double help1 = rnd.ownRandomFunction(1, n);
                    int help = (int)Math.Round(help1);
                        
                    while (getNext(help, akt, maze).getVisited() == true )
                    {
                        help++;
                        help = help % 4 + 1;
                    }
                    //neues Feld gefunden, neues auf Stack  und Wall updaten, iwo muss die Wall vom Nachbarn geupdatet werden 

                    Cell next = getNext(help, akt, maze);
                    stack.Push(next);
                    akt.setWall(help, false);
                   
                }
                else
                {
                    Cell help = stack.Pop();
                    //help.visited = true; wird eigtl nicht gebraucht
                }

                //updaten der richtigen Map

                updateMap(akt);

                // Hier noch das Nachfolgefeld updaten


            }

        }

        /// <summary>
        /// Method which returns the number of neighbour Cells which have not been visited yet
        /// </summary>
        /// <returns></returns>
        private int numOfNotVisited(Cell zelle, Cell[,] array)
        {
            int help = 0;
            if (zelle.getX() - 1 < array.GetLength(0))
            {
                if (array[zelle.getX() + 1, zelle.getY()].getVisited() == false)//exception !!
                {
                    help++;
                }
            }
            if (zelle.getY() + 1 < array.GetLength(1))
            {
                if (array[zelle.getX(), zelle.getY() + 1].getVisited() == false)
                {
                    help++;
                }
            }
            if (zelle.getY() - 1 < 0)
            {
                if (array[zelle.getX(), zelle.getY() - 1].getVisited() == false)
                {
                    help++;
                }
            }
            if (zelle.getX() - 1 < 0)
            {
                if (array[zelle.getX() - 1, zelle.getY()].getVisited() == false)
                {
                    help++;
                }
            }
            
            return help;
        }

        /// <summary>
        /// Method which returns the next Cell in a certain Direction 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="akt"></param>
        /// <param name="maze"></param>
        /// <returns></returns>
        Cell getNext(int i, Cell akt, Cell[,] maze)
        {
            if (i == 1) return maze[akt.getX(), akt.getY()+1];
            if (i == 2) return maze[akt.getX(), akt.getY()-1];
            if (i == 3) return maze[akt.getX()+1, akt.getY()];
            if (i == 4) return maze[akt.getX()-1, akt.getY()];
            if (i == 5) return maze[akt.getX()-1, akt.getY()+1];
            if (i == 6) return maze[akt.getX()+1, akt.getY()+1];
            if (i == 8) return maze[akt.getX()-1, akt.getY()-1];
            if (i == 7) return maze[akt.getX() + 1, akt.getY() - 1];
            else return null;
        }


        //Methode um Wände in der richtigen Map upzudaten
        void updateMap(Cell cell)
        {
            //linke obere Ecke berechnen
            int xFinale = cell.getX();
            int yFinale = cell.getY();
            // Zelle und alle zugehörigen updaten
          
            //mapType[xFinale + i, yFinale + j] = BUg, Bug, Bug
            //linksoben
            mapType[xFinale - 1 , yFinale - 1 ] = cell.getWall(5) == true? 1 : 0;
            //Mitte oben 
            mapType[xFinale -1, yFinale] = cell.getWall(1) == true ? 1 : 0;
            //rechts oben 
            mapType[xFinale -1, yFinale + 1] = cell.getWall(6) == true ? 1 : 0;//exception
            //linksMItte??
            mapType[xFinale, yFinale - 1] = cell.getWall(2) == true ? 1 : 0;
            //Mitte
            mapType[xFinale, yFinale] = 0;
            //Mitte rechts??
            mapType[xFinale , yFinale + 1] = cell.getWall(3) == true ? 1 : 0;
            //unten links
            mapType[xFinale + 1, yFinale - 1] = cell.getWall(7) == true ? 1 : 0;
            //unten MItte
            mapType[xFinale + 1, yFinale ] = cell.getWall(4) == true ? 1 : 0;
            //unten links
            mapType[xFinale + 1, yFinale + 1] = cell.getWall(8) == true ? 1 : 0;

        }

          

    }
}

/// <summary>
/// class for Labyrinthgeneration
/// </summary>
public class Cell
{
    /// <summary>
    /// Boolean whether the Cell has a top wall 
    /// </summary>
    Boolean wand_oben; //1
    /// <summary>
    /// Boolean whether Cell has a bottom Wall
    /// </summary>
    Boolean wand_unten;//2
    /// <summary>
    /// Boolean whether Cell has a right Wall 
    /// </summary>
    Boolean wand_rechts;//3
    /// <summary>
    /// Boolean whether Cell has left Wall
    /// </summary>
    Boolean wand_links;//4
    /// <summary>
    /// Boolean whether cell has left top Wall
    /// </summary>
    Boolean wand_links_oben;//5
    /// <summary>
    /// Boolean whether cell has right rop Wall
    /// </summary>
    Boolean wand_rechts_oben;//6
    /// <summary>
    /// Boolean whether cell has left down Wall
    /// </summary>
    Boolean wand_links_unten;//7
    /// <summary>
    /// Boolean whether cell hat right down Wall
    /// </summary>
    Boolean wand_rechts_unten;//8

    /// <summary>
    /// Position of Cell in x- Direction
    /// </summary>
    private int X;
    /// <summary>
    /// Position of Cell in y-Direction
    /// </summary>
    private int Y;

    /// <summary>
    /// Boolean whether Cell has already been visited
    /// </summary>
    Boolean visited;

    public Boolean getVisited()
    {
        return visited;
    }

    public void setVisited(Boolean set)
    {
        visited = set;
    }

    public int getX()
    {
        return X;
    }

    public int getY()
    {
        return Y;
    }

    

    public Boolean getWall(int j)
    {
        if (j == 1) return wand_oben;
        if (j == 2) return wand_unten;
        if (j == 3) return wand_rechts;
        if (j == 4) return wand_links;
        if (j == 5) return wand_links_oben;
        if (j == 6) return wand_rechts_oben;
        if (j == 8) return wand_rechts_unten;
        if (j == 7) return wand_links_unten;
        else return false;
    }

    public void setWall(int j, Boolean set)
    {
        if (j == 1) wand_oben = set;
        if (j == 2) wand_unten = set;
        if (j == 3) wand_rechts = set;
        if (j == 4) wand_links = set;
        if (j == 5) wand_links_oben = set;
        if (j == 6) wand_rechts_oben = set;
        if (j == 8) wand_rechts_unten = set;
        if (j == 7) wand_links_unten = set;
    }

    /// <summary>
    /// Constructor for Cell 
    /// </summary>
    /// <param name="zeile">x-Direction</param>
    /// <param name="spalte">y-Direction</param>
    /// <param name="oben">top Wall</param>
    /// <param name="unten">bottom Wall</param>
    /// <param name="rechts">right Wall</param>
    /// <param name="links">left Wall</param>
    public Cell (int _X, int _Y, Boolean oben, Boolean unten, Boolean rechts, Boolean links, Boolean linksOben, Boolean rechtsOben, Boolean linksunten, Boolean rechtsunten)
    {
        X = _X;
        Y = _Y;

        visited = false;

        //wegen Ecken und so 
        wand_oben = oben;
        wand_unten = unten;
        wand_rechts = rechts;
        wand_links = links;
        wand_links_oben = linksOben;
        wand_rechts_oben = rechtsOben;
        wand_links_unten = linksunten;
        wand_rechts_unten = rechtsunten;

    }



}
