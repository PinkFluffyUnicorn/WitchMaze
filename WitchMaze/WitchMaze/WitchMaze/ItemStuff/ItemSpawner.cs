using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.ItemStuff.Items;

namespace WitchMaze.ItemStuff
{
    class ItemSpawner
    {
        List<Item> itemsToSpawn;
        float timeToNewItem; //Variable, marks the time for a new Item
        float timeForNewItem = 5;//Constant, marks time you need for a new Item

        /// <summary>
        /// Initialli fills ItemMap
        /// </summary>
        /// <param name="itemMap">ItemMap to fill</param>
        public void initialSpawn(ItemMap itemMap)
        {
            ///ToDo: do it right!
            itemsToSpawn = new List<Item>();
            timeToNewItem = 0;
            timeForNewItem *= 50000; //für Millisekunden
            itemMap.insertItem(new GreenBottle(new Vector3(5,0,5)), 5, 5);
        }

        /// <summary>
        /// Updates Item Map if Changed
        /// </summary>
        /// <param name="itemMap">the ItemMap to update</param>
        public void update(ItemMap itemMap, GameTime gameTime)
        {
            timeToNewItem += gameTime.TotalGameTime.Milliseconds;
            if (itemsToSpawn.Count == 0)
                timeToNewItem = 0;
            if (timeToNewItem >= timeForNewItem)
            {
                Item itemToSpawn = itemsToSpawn.First();
                itemMap.insertItem(itemToSpawn, (int)itemToSpawn.position.X, (int)itemToSpawn.position.Z);
                timeToNewItem = 0;
            }
            //timeToNewItem = timeToNewItem % timeForNewItem;
        }

        public void spawnItem(int x, int y)
        {
            //Random Funktion einfügen(vllt abhöngig oder immer abwechselnd oder so spawnen für besseres balancing
            Vector3 position = new Vector3(x, 0, y);
            itemsToSpawn.Add(new GreenBottle(position));
        }
    }
}
