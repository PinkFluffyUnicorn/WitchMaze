using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.MapStuff
{
    class Cell
    {
        enum neighbour { up, right, down, left, upLeft, upRight, downRight, downLeft};
        bool visited;

        bool up;
        bool right;
        bool down;
        bool left;
        bool upLeft;
        bool upRight;
        bool downRight;
        bool downLeft;

        /// <summary>
        /// Index for the cell middle in the whole Labyrnith map
        /// </summary>
        int MapIndexX;
        int MapIndexY;

        /// <summary>
        /// INdex for the Cell Array 
        /// </summary>
        int CellindexX;
        int CellindexY;


        public Cell(int _MapIndexX, int _MapIndexY, int _CellIndexX, int _CellIndexY, bool _up, bool _right, bool _down, bool _left, bool _upLeft,bool _upRight, bool _downRight, bool _downLeft)
        {
            up = _up;
            right = _right;
            down = _down;
            left = _left;
            upLeft = _upLeft;
            upRight = _upRight;
            downRight = _downRight;
            downLeft = _downLeft;

            MapIndexX = _MapIndexX;
            MapIndexY = _MapIndexY;

            CellindexX = _CellIndexX;
            CellindexY = _CellIndexY;

            visited = false;
        }

        public void setVisited(bool value)
        {
            visited = value;
        }

        public bool getVisited()
        {
            return visited;
        }

        public int getMapIndexX()
        {
            return MapIndexX;
        }

        public int getMapIndexY()
        {
            return MapIndexY;
        }

        public int getCellIndexX()
        {
            return CellindexX;
        }

        public int getCellindexY()
        {
            return CellindexY;
        }


        public bool getWall(int index)
        {
            if (index == (int)neighbour.up) return up;
            else if (index == (int)neighbour.right) return right;
            else if (index == (int)neighbour.down) return down;
            else if (index == (int)neighbour.left) return left;
            else if (index == (int)neighbour.upLeft) return upLeft;
            else if (index == (int)neighbour.upRight) return upRight;
            else if (index == (int)neighbour.downRight) return downRight;
            else if (index == (int)neighbour.downLeft) return downLeft;
            else
            {
                Console.WriteLine("Hier ist ein Fehler beim abrufen der Walls !!");
                return false; // hier könnte eine Fehlerquelle sein ! 
            }
        }

        public void setWall(int index, bool value)
        {
            if (index == (int)neighbour.up) up = value;
            else if (index == (int)neighbour.right) right = value;
            else if (index == (int)neighbour.down) down = value;
            else if (index == (int)neighbour.left) left = value;
            else if (index == (int)neighbour.upLeft) upLeft = value;
            else if (index == (int)neighbour.upRight) upRight = value;
            else if (index == (int)neighbour.downRight) downRight = value;
            else if (index == (int)neighbour.downLeft) downLeft = value;
            else Console.WriteLine("Fehler beim setzten der Wall !");
        }

    }
}
