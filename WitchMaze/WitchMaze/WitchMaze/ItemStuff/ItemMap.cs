﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.ItemStuff.Items;

namespace WitchMaze.ItemStuff
{
    class ItemMap
    {
        Item[,] itemMap;
        Random random;


        /// <summary>
        /// Creates a ItemMap
        /// </summary>
        public ItemMap()
        {
            random = new Random();
            itemMap = new Item[Settings.mapSizeX, Settings.mapSizeZ];
        }

        /// <summary>
        /// returns if ItemMap is empty at (x,y)
        /// </summary>
        /// <param name="x">X Coordinate of point</param>
        /// <param name="y">Y Coordinate of point</param>
        /// <returns></returns>
        public bool isEmpty(int x, int y)
        {
            if (itemMap[x, y] != null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// returns the Item at position x,y if not empty (otherwise exeption)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Item getItem(int x, int y)
        {
            return itemMap[x, y];
        }
        /// <summary>
        /// inserts a Item in the ItemMap
        /// </summary>
        /// <param name="item">Item to Insert</param>
        /// <param name="x">X Coordinate of Item</param>
        /// <param name="y">Y Coordinate of Item</param>
        public void insertItem(Item item, int x, int y)
        {
            itemMap[x, y] = item;
        }
        /// <summary>
        /// delete a Item in the Item Map and autimagicly spawns a new one
        /// </summary>
        /// <param name="x">X Coordinate of Item</param>
        /// <param name="y">Y Coordinate of Item</param>
        public void deleteItem(int x, int y)
        {
            //delete old one
            itemMap[x, y] = null;
            //spawn new Item //atm at the position it initially spawned...
            GameStates.InGameState.getItemSpawner().spawnItem(x, y);
        }

        /// <summary>
        /// draw the itemMap
        /// </summary>
        public void draw()
        {
            
            for (int i = 0; i < Settings.mapSizeX; i++)
            {
                for (int j = 0; j < Settings.mapSizeZ; j++)
                {
                    if (itemMap[i, j] != null)
                    {
                        itemMap[i, j].draw();
                        //Console.WriteLine("Draw Item At: " + i + ","+j);
                    }
                        
                }
            }
        }

    }
}