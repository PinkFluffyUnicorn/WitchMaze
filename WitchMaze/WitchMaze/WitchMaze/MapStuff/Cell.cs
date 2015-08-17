using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.MapStuff
{
    class Cell
    {
        /// <summary>
        /// enum for the neighbour cells
        /// </summary>
        enum neighbour { up, right, down, left, upLeft, upRight, downRight, downLeft};

        /// <summary>
        /// Bool visited is true, when the Cell is already on the stack or has been there
        /// </summary>
        bool visited;

        /// <summary>
        /// Bool up is true, when the Cell has an upper wall
        /// </summary>
        bool up;
        /// <summary>
        /// Bool right is true, when the Cell has a right wall
        /// </summary>
        bool right;
        /// <summary>
        /// Bool down is true, when the Cell has a lower wall
        /// </summary>
        bool down;
        /// <summary>
        /// Bool left is true, when the Cell has a left wall
        /// </summary>
        bool left;
        /// <summary>
        /// Bool upLeft is true, when the Cell has a upLeft Wall
        /// </summary>
        bool upLeft;
        /// <summary>
        /// Bool upRight is true, when the Cell has a upRight Wall
        /// </summary>
        bool upRight;
        /// <summary>
        /// Bool downright is true, when the Cell has a downRight Wall
        /// </summary>
        bool downRight;
        /// <summary>
        /// Bool downLeft is true, when the Cell has a downLeft Wall
        /// </summary>
        bool downLeft;

        /// <summary>
        /// Index for the cell middle in the whole Labyrnith map
        /// </summary>
        int MapIndexX;
        int MapIndexY;

        /// <summary>
        /// Index for the Cell Array 
        /// </summary>
        int CellindexX;
        int CellindexY;


        /// <summary>
        /// Constructor for Cell
        /// </summary>
        /// <param name="_MapIndexX">MapIndex of the middle ofthe Cell in x- direction </param>
        /// <param name="_MapIndexY">MapIndex of the middle ofthe Cell in x- direction</param>
        /// <param name="_CellIndexX">CellIndex of the Cell in x- direction</param>
        /// <param name="_CellIndexY">CellIndex of the Cell in x- direction</param>
        /// <param name="_up">bool if cell has up Wall</param>
        /// <param name="_right">bool if cell has right Wall</param>
        /// <param name="_down">bool if cell has down Wall</param>
        /// <param name="_left">bool if cell has left Wall</param>
        /// <param name="_upLeft">bool if cell has upLeft Wall</param>
        /// <param name="_upRight">bool if cell has upRight Wall</param>
        /// <param name="_downRight">bool if cell has downRight Wall</param>
        /// <param name="_downLeft">bool if cell has downLeft Wall</param>
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

        /// <summary>
        /// setter for visited
        /// </summary>
        /// <param name="value">new value of visited</param>
        public void setVisited(bool value)
        {
            visited = value;
        }

        /// <summary>
        /// getter for visited
        /// </summary>
        /// <returns>value of visited</returns>
        public bool getVisited()
        {
            return visited;
        }

        /// <summary>
        /// getter for MapIndex in x direction
        /// </summary>
        /// <returns>MapIndex in x-direction</returns>
        public int getMapIndexX()
        {
            return MapIndexX;
        }


        /// <summary>
        /// getter for MapIndex in y direction
        /// </summary>
        /// <returns>MapIndex in y-direction</returns>
        public int getMapIndexY()
        {
            return MapIndexY;
        }

        /// <summary>
        /// getter for CellIndex in x direction
        /// </summary>
        /// <returns>cell index in x direction</returns>
        public int getCellIndexX()
        {
            return CellindexX;
        }

        /// <summary>
        /// getter for Cellindex in y direction
        /// </summary>
        /// <returns>CellIndex in y direction </returns>
        public int getCellindexY()
        {
            return CellindexY;
        }


        /// <summary>
        /// getter for Wall, calculated over neighbour enum 
        /// </summary>
        /// <param name="index">index which wall to get</param>
        /// <returns></returns>
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
                return false; 
            }
        }

        /// <summary>
        /// setter for Walls
        /// </summary>
        /// <param name="index">which wall to set</param>
        /// <param name="value">what value the wall should have; true: there is a Wall; false: there is no wall</param>
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
