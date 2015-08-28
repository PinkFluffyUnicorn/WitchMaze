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
        //every Item is atleast 5 Blocks away(maximum distance) from every player (and other Item?) <------
        float distanceForItems = 5.0f;
        List<Item> itemsToSpawn;
        List<MapStuff.Blocks.Block> spawnBlocks;

        float timeToNewItem; //Variable, marks the time for a new Item
        float timeForNewItem = 5;//Constant, marks time you need for a new Item

        Random random;

        /// <summary>
        /// Initialli fills ItemMap
        /// </summary>
        /// <param name="itemMap">ItemMap to fill</param>
        public void initialSpawn(ItemMap itemMap, MapStuff.Map map)
        {
            spawnBlocks = new List<MapStuff.Blocks.Block>();
            for(int i = 0; i < Settings.getMapSizeX(); i++){
                for (int j = 0; j < Settings.getMapSizeZ(); j++)
                {
                    if (map.getBlockAt(i, j).name == MapStuff.MapCreator.tiles.floor)
                        spawnBlocks.Add(map.getBlockAt(i, j));
                }
            }

            random = new Random();
            itemsToSpawn = new List<Item>();
            timeToNewItem = 0;
            timeForNewItem *= 10000; //für Millisekunden
            //spawn 5 new Items here
            spawnItem();
            spawnItem();
            spawnItem();
            spawnItem();
            spawnItem();

            ///ToDo: do it right!

            //itemMap.insertItem(new WingOfABat(new Vector3(5,1,5)), 5, 5);
        }

        /// <summary>
        /// Updates Item Map if Changed
        /// </summary>
        /// <param name="itemMap">the ItemMap to update</param>
        public void update(ItemMap itemMap, GameTime gameTime, List<PlayerStuff.Player> playerList)
        {
            Console.WriteLine(timeToNewItem);
            Console.WriteLine(itemsToSpawn.Count());

            timeToNewItem += gameTime.TotalGameTime.Milliseconds;
            if (itemsToSpawn.Count == 0)
                timeToNewItem = 0;
            if (timeToNewItem >= timeForNewItem)
            {
                Item itemToSpawn = itemsToSpawn.First();
                //compute new SpawnPosition here
                bool possiblePosition = false;
                foreach (MapStuff.Blocks.Floor floor in spawnBlocks)
                {
                    foreach (PlayerStuff.Player p in playerList)
                    {
                        float distPlayerMapBlock = Math.Max(Math.Abs(floor.position.X - p.getPosition().X), Math.Abs(floor.position.Y - p.getPosition().Y));
                        if(distPlayerMapBlock >= distanceForItems)
                            possiblePosition = true;
                        else
                            possiblePosition = false;
                        //abbrechen wenn keine mögliche position
                        if(!possiblePosition)
                            break;
                    } 
                    //check if position is possible and if there is no other item on the oosition
                    if (possiblePosition && itemMap.isEmpty((int)floor.position.X, (int)floor.position.Z))
                    {
                        itemToSpawn.position = floor.position;
                        //insert Item in ItemMap
                        itemsToSpawn.RemoveAt(0);
                        itemMap.insertItem(itemToSpawn, (int)itemToSpawn.position.X, (int)itemToSpawn.position.Z);
                        timeToNewItem = 0;//but why?
                        //timeToNewItem = timeToNewItem % timeForNewItem;
                        return;
                    }
                        
                }
            }
            
        }

        public void spawnItem()
        {
            if (itemsToSpawn.Count() > 5)
                throw new NotImplementedException();
            //Random Funktion einfügen(vllt abhöngig oder immer abwechselnd oder so spawnen für besseres balancing
            Vector3 position = new Vector3(0, 0, 0);//position is addetet afterwarts
            //create random a new Item
            int index = random.Next(1, 11);
            Console.WriteLine(index);
            switch (index)
            {
                case 1:
                    itemsToSpawn.Add(new Branch(position));
                    break;
                case 2:
                    itemsToSpawn.Add(new Caterpillar(position));
                    break;
                case 3:
                    itemsToSpawn.Add(new Crystal(position));
                    break;
                case 4:
                    itemsToSpawn.Add(new Eye(position));
                    break;
                case 5:
                    itemsToSpawn.Add(new Frog(position));
                    break;
                case 6:
                    itemsToSpawn.Add(new Pig(position));
                    break;
                case 7:
                    itemsToSpawn.Add(new Slime(position));
                    break;
                case 8:
                    itemsToSpawn.Add(new Spider(position));
                    break;
                case 9:
                    itemsToSpawn.Add(new UnicornHorn(position));
                    break;
                case 10:
                    itemsToSpawn.Add(new WingOfABat(position));
                    break;
            }
            Console.Write("");
        }
    }
}
