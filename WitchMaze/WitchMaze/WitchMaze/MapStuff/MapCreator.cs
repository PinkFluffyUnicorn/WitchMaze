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

        private int help4 = 100;

        //help for testing 
        int help1 = 100;

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
            // Mauer neu hinzufügen, dann gibt es beim labyrinth mit Ecken keine Probleme :P

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
                    else if (mapType[i, j] == 1)
                    {
                        float rotation = (float)rnd.Next(3);
                        while (help == rotation)//same rotation as before 
                        {
                            rotation = rotation + 1;
                            rotation = rotation % 4;
                        }
                        rotation = rotation % 4;
                        map.map[i, j] = new Wall(Game1.getContent().Load<Model>("cube"), new Vector3((float)(i * Settings.getBlockSizeX()), (float)(Settings.getBlockSizeY()), (float)(j * Settings.getBlockSizeZ())), rotation * 90);
                        help = rotation;
                    }
                    else //if (mapType[i,j] == 2)
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

//            Random rnd = new Random();
//            int choice = rnd.Next(9);
//            if (choice == 0)
//            {

//            }
//            if (choice == 1)
//            {

//            }
//            if (choice == 2)
//            {

//            }
//            if (choice == 3)
//            {

//            }
//            if (choice == 4)
//            {

//            }
//            if (choice == 5)
//            {

//            }
//            if (choice == 6)
//            {

//            }
//            if (choice == 7)
//            {

//            }
//            if (choice == 8)
//            {

//            }
//            else
//            {

//            }
//        }
//    }
//}

            int numCellsX = 0;
            int numCellsY = 0;

            
            
            //determine number of cells 
            if (Settings.getMapSizeX() % 2 == 1)
                numCellsX = (Settings.getMapSizeX() - 1) / 2;
            else
                numCellsX = (Settings.getMapSizeX() - 2) / 2;


            if (Settings.getMapSizeZ() % 2 == 1)
                numCellsY = (Settings.getMapSizeZ()- 1) / 2;
            else
                numCellsY = (Settings.getMapSizeZ() - 2) / 2;


            // Array to store the Cells in 
            Cell[,] maze = new Cell[numCellsX, numCellsY];
 

            //index of cells in Map 
            int CellindexX = 1;
            int CellindexY = 1;


            // Create Cells with right number of Walls 
            for(int i = 0; i < numCellsX; i++)
            {
                for(int j = 0;j < numCellsY; j++)
                {
                    maze[i, j] = new Cell(i, j, CellindexX, CellindexY, true, true, true, true, true, true, true, true);
                    CellindexY += 2;
                }
                CellindexY = 1;
                CellindexX += 2;
            }
            

            
            //find path through Cells 
            // start at random Cell
            // mit Cellindex arbeiten?? das arbeitet doch auf dem normalen Feld und nicht auf dem Zellen Feld  
            //schon besuchte numCellsX auf Stack ablegen
            Stack<Cell> stack = new Stack<Cell>();
            Random rnd = new Random();
            double numCellsA = (double)numCellsX - 1;
            double numCellsB = (double)numCellsY - 1;
            double xStartHelp = rnd.Next((int)numCellsA - 1);
            double yStartHelp = rnd.Next((int)numCellsB - 1);



            int xStart = (int)xStartHelp + 1;
            int yStart = (int)yStartHelp + 1;

            stack.Push(maze[xStart, yStart]);
            maze[xStart, yStart].setVisited(true);

            while(help4 > 0) 
            {
                Cell akt = stack.Peek();
                if (help1 > 0)
                {
                   
                }
                //akt.setVisited(true);
                // choose next Cell to visit, if not possible, pop
                // if eine Nachbarzelle noch unbesucht, tue diese auf den Stack und mach die Wall dazwischn weg, hier auch ggf. Diagonale entfernen 
                int n = numOfNotVisited(akt, maze);
                if (n == 0)
                {
                    Cell help = stack.Pop();
                    //help.setVisited(true);//wird eigtl nicht gebraucht
                }
                else
                {
                    // einmal von 1- 4 durchgehen und schauen,welche Nachfolgende Fliese noch nicht besetzt 
                    //zufälligen Startpunkt wählen 
                    int help = rnd.Next(3) + 1;

                    //speichert 
                    int help2 = 0;

                    for (int i = 0; i < 4; i++)
                    {
                        help2 = help + i;
                        help2 = help % 4;
                        if (getNext(help2, akt, maze) != null)
                        {
                            if (getNext(help2, akt, maze).getVisited() == false)
                            {
                                break;
                            }
                        }
                    }
                    Cell next = getNext(help2, akt, maze);
                    if (next == null)
                    {
                        stack.Pop();
                    }


                    else
                    {
                        next.setVisited(true);
                        // Nachfolgefeld updaten 
                        if (help2 == 1) next.setWall(2, false);
                        if (help2 == 2) next.setWall(1, false);
                        if (help2 == 3) next.setWall(4, false);
                        if (help2 == 4) next.setWall(5, false);
                        stack.Push(next);
                    }
                    akt.setWall(help2, false);
                }
                //updaten der richtigen Map
                updateMap(akt);
                for (int i = 0; i < Settings.getMapSizeX(); i++ )
                {
                    for(int j = 0; j < Settings.getMapSizeZ(); j++)
                    {
                        Console.Write(mapType[i, j]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                    help4--;
            }
 


        }

         ///<summary>
         ///Method which returns the number of neighbour Cells which have not been visited yet
         ///</summary>
         ///<returns></returns>
        private int numOfNotVisited(Cell zelle, Cell[,] array)
        {
            int help = 0;
            if (zelle.getXcell()  + 1 < array.GetLength(0)) // exception ! 
            {
                if (array[zelle.getXcell() + 1, zelle.getYcell()].getVisited() == false)//exception !!
                {
                    help++;
                }
            }
            if (zelle.getYcell() + 1 < array.GetLength(1))
            {
                if (array[zelle.getXcell(), zelle.getYcell() + 1].getVisited() == false)
                {
                    help++;
                }
            }
            if (zelle.getYcell() - 1 >= 0)
            {
                if (array[zelle.getXcell(), zelle.getYcell() - 1].getVisited() == false)
                {
                    help++;
                }
            }
            if (zelle.getXcell() - 1 >= 0)
            {
                if (array[zelle.getXcell() - 1, zelle.getYcell()].getVisited() == false)
                {
                    help++;
                }
            }

            if(help1 > 0)
            {
                help1--;
            }
           
            return help;
        }

         ///<summary>
         ///Method which returns the next Cell in a certain Direction 
         ///</summary>
         ///<param name="i">direction in which the next Cells lies</param>
         ///<param name="akt">current cell</param>
         ///<param name="maze"></param>
         ///<returns></returns>
        Cell getNext(int i, Cell akt, Cell[,] maze) // granzen abfangen 
        {
            //oben
            if (i == 1 && akt.getYcell() + 1 < maze.GetLength(1)) return maze[akt.getXcell(), akt.getYcell()+1];//exception
            //unten
            if (i == 2 && akt.getYcell() - 1 >= 0) return maze[akt.getXcell(), akt.getYcell()-1];
            //rechts
            if (i == 3 && akt.getXcell() + 1 < maze.GetLength(0)) return maze[akt.getXcell()+1, akt.getYcell()];
            //links
            if (i == 4 && akt.getXcell() - 1 >= 0) return maze[akt.getXcell() - 1, akt.getYcell()];
            //links oben
            if (i == 5 && akt.getYcell() + 1 < maze.GetLength(1) && akt.getXcell() - 1 >= 0 ) return maze[akt.getXcell() - 1, akt.getYcell() + 1];
            //rechts oben 
            if (i == 6 && akt.getYcell() + 1 < maze.GetLength(1) && akt.getXcell() + 1 < maze.GetLength(0)) return maze[akt.getXcell() + 1, akt.getYcell() + 1];
            //links unten
            if (i == 8 && akt.getYcell() - 1 >= 0 && akt.getXcell() - 1 >= 0 ) return maze[akt.getXcell() - 1, akt.getYcell() - 1];
            //rechts unten 
            if (i == 7 && akt.getYcell() - 1 >= 0 && akt.getXcell() + 1 < maze.GetLength(0)) return maze[akt.getXcell() + 1, akt.getYcell() - 1];
            else return null;
        }


        //Methode um Wände in der richtigen Map upzudaten
        void updateMap(Cell cell)
        {
            //mittepunkt der Zelle speichern 
            int xFinale = cell.getXmap();
            int yFinale = cell.getYmap();
            // Zelle und alle zugehörigen updaten
          

            //1: oben y+, 2:unten y-, 3:rechtsx+, 4:linksx-, 5: links obeny+x-, 6: rechts obeny+x+, 7: links unten, 8: rechts unten 
            //mapType[xFinale + i, yFinale + j] = BUg, Bug, Bug, 0: Boden, 1: Wall
            //linksoben
            mapType[xFinale + 1 , yFinale + 1 ] = cell.getWall(5) == true? 1 : 0;
            //Mitte oben 
            mapType[xFinale, yFinale + 1] = cell.getWall(1) == true ? 1 : 0;
            //rechts oben 
            mapType[xFinale - 1, yFinale + 1] = cell.getWall(6) == true ? 1 : 0;
            //links
            mapType[xFinale + 1, yFinale] = cell.getWall(3) == true ? 1 : 0;
            //Mitte
            mapType[xFinale, yFinale] = 0;
            //Mitte rechts
            mapType[xFinale - 1, yFinale] = cell.getWall(4) == true ? 1 : 0;
            //unten links
            mapType[xFinale + 1, yFinale - 1] = cell.getWall(7) == true ? 1 : 0;
            //unten MItte
            mapType[xFinale , yFinale - 1] = cell.getWall(2) == true ? 1 : 0;
            //unten links
            mapType[xFinale - 1, yFinale - 1] = cell.getWall(8) == true ? 1 : 0;

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
    /// Position of Cell in x- Direction in the maze
    /// </summary>
    private int Xmap;
    /// <summary>
    /// Position of Cell in y-Direction in the maze
    /// </summary>
    private int Ymap;


    /// <summary>
    /// Position of Cell in x-Direction in the Cell Map
    /// </summary>
    private int XCell;
    /// <summary>
    /// Position of Cell in y-Direction in the Cell Map
    /// </summary>
    private int YCell;

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

    public int getXmap()
    {
        return Xmap;
    }

    public int getYmap()
    {
        return Ymap;
    }

    public int getXcell()
    {
        return XCell;
    }

    public int getYcell()
    {
        return YCell;
    }

    

    public Boolean getWall(int j)
    {
        if (j == 1) return wand_unten;
        if (j == 2) return wand_oben;
        if (j == 3) return wand_links;
        if (j == 4) return wand_rechts;
        if (j == 5) return wand_rechts_unten;
        if (j == 6) return wand_links_unten;
        if (j == 8) return wand_links_oben;
        if (j == 7) return wand_rechts_oben;
        else return false;
    }

    public void setWall(int j, Boolean set)
    {
        if (j == 1) wand_unten = set;
        if (j == 2) wand_oben = set;
        if (j == 3) wand_links = set;
        if (j == 4) wand_rechts = set;
        if (j == 5) wand_rechts_unten = set;
        if (j == 6) wand_links_unten = set;
        if (j == 8) wand_links_oben = set;
        if (j == 7) wand_rechts_oben = set;
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
    public Cell (int _XCell, int _YCell, int _Xmap, int _Ymap, Boolean oben, Boolean unten, Boolean rechts, Boolean links, Boolean linksOben, Boolean rechtsOben, Boolean linksunten, Boolean rechtsunten)
    {

        XCell = _XCell;
        YCell = _YCell;

        Xmap = _Xmap;
        Ymap = _Ymap;

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
