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

        Random randomItem;
        Random randomMap;

        /// <summary>
        /// Initialli fills ItemMap
        /// </summary>
        /// <param name="itemMap">ItemMap to fill</param>
        public void initialSpawn(ItemMap itemMap, MapStuff.Map map , List<PlayerStuff.Player> playerList)
        {
            spawnBlocks = new List<MapStuff.Blocks.Block>();
            for(int i = 0; i < Settings.getMapSizeX(); i++){
                for (int j = 0; j < Settings.getMapSizeZ(); j++)
                {
                    if (map.getBlockAt(i, j).name == MapStuff.MapCreator.tiles.floor)
                        spawnBlocks.Add(map.getBlockAt(i, j));
                }
            }

            randomMap = new Random();
            randomItem = new Random();
            itemsToSpawn = new List<Item>();
            timeToNewItem = 0;
            float h = timeForNewItem;//zwischenspeicher
            timeForNewItem = 0; //DAMIT ERSTE Items GLEICH GESPAWNT WERDEN
            //spawn 5 new Items here
            spawnItem(itemMap);
            spawnItem(itemMap);
            spawnItem(itemMap);
            spawnItem(itemMap);
            spawnItem(itemMap);

            ////spawn Items directly here
            //spawnItem(itemMap, playerList);
            //spawnItem(itemMap, playerList);
            //spawnItem(itemMap, playerList);
            //spawnItem(itemMap, playerList);
            //spawnItem(itemMap, playerList);

            timeForNewItem = h;
            timeForNewItem *= 10000;

            //itemMap.insertItem(new WingOfABat(new Vector3(5,1,5)), 5, 5);
        }

        /// <summary>
        /// Updates Item Map if Changed
        /// </summary>
        /// <param name="itemMap">the ItemMap to update</param>
        public void update(ItemMap itemMap, GameTime gameTime, List<PlayerStuff.Player> playerList)
        {
            //Console.WriteLine(timeToNewItem);
            //Console.WriteLine(itemsToSpawn.Count());

            timeToNewItem += gameTime.TotalGameTime.Milliseconds;
            if (itemsToSpawn.Count == 0)
                timeToNewItem = 0;
            if (timeToNewItem >= timeForNewItem)
            {
                spawnItem(itemMap, playerList);
            }
            
        }

        /// <summary>
        /// returns if an Item one of the items to spawn
        /// </summary>
        /// <param name="item">Item to check if its contained</param>
        /// <returns>if item is contained</returns>
        private bool contains(Item item)
        {
            foreach (Item i in itemsToSpawn)
            {
                if (i.itemIndex == item.itemIndex)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// spawns an Item direct in the ItemMap
        /// </summary>
        /// <param name="itemMap">ItemMap to spawn Items in</param>
        /// <param name="playerList">List of Players</param>
        private void spawnItem(ItemMap itemMap, List<PlayerStuff.Player> playerList)
        {
            Item itemToSpawn = itemsToSpawn.First();
            List<Vector3> spawnPositions = new List<Vector3>();//List of posible spawn position (to randomize)
            //compute new SpawnPosition here
            bool possiblePosition = false;
            foreach (MapStuff.Blocks.Floor floor in spawnBlocks)
            {
                foreach (PlayerStuff.Player p in playerList)
                {
                    float distPlayerMapBlock = Math.Max(Math.Abs(floor.position.X - p.getPosition().X), Math.Abs(floor.position.Y - p.getPosition().Y));
                    if (distPlayerMapBlock >= distanceForItems)
                        possiblePosition = true;
                    else
                        possiblePosition = false;
                    //abbrechen wenn keine mögliche position
                    if (!possiblePosition)
                        break;
                }
                //check if position is possible and if there is no other item on the oosition
                if (possiblePosition)
                    spawnPositions.Add(floor.position);

            }

            //if there is no possible position
            if (spawnPositions.Count == 0)
                return;

            //index errechnen
            int index = randomMap.Next(0, spawnPositions.Count());
            while (!itemMap.isEmpty((int)spawnPositions.ElementAt(index).X, (int)spawnPositions.ElementAt(index).Z))//could be a problem later... se how it performes ingame...
            {
                index = randomMap.Next(0, spawnPositions.Count());
            }
            itemToSpawn.position = spawnPositions.ElementAt(index);
            //insert Item in ItemMap
            itemsToSpawn.RemoveAt(0);
            itemMap.insertItem(itemToSpawn, (int)itemToSpawn.position.X, (int)itemToSpawn.position.Z);
            timeToNewItem = 0;//but why?
            //timeToNewItem = timeToNewItem % timeForNewItem;
        }

        /// <summary>
        /// wirites nw Item in the spawn waiting list
        /// </summary>
        /// <param name="itemMap">ItemMap to spawn the item in</param>
        public void spawnItem(ItemMap itemMap)
        {
            if (itemsToSpawn.Count() > 5)
                throw new NotImplementedException();
            //Random Funktion einfügen(vllt abhöngig oder immer abwechselnd oder so spawnen für besseres balancing
            Vector3 position = new Vector3(0, 0, 0);//position is addetet afterwarts
            //create random a new Item
            int index = randomItem.Next(1, 11);
            //if itemToSpawn already contains the item to spawn(also check the ItemMap!)
            if (index == 1 && ( itemMap.contains(new Branch(new Vector3(0, 0, 0))) || this.contains(new Branch(new Vector3(0, 0, 0))) ) ) // && itemsToSpawn.Contains(new Branch(new Vector3(0, 0, 0)))
                        index++;
            if (index == 2 && ( itemMap.contains(new Caterpillar(new Vector3(0, 0, 0))) || this.contains(new Caterpillar(new Vector3(0, 0, 0))) ) )
                        index++;
            if (index == 3 && ( itemMap.contains(new Crystal(new Vector3(0, 0, 0))) || this.contains(new Crystal(new Vector3(0, 0, 0))) ) )
                        index++;
            if (index == 4 && ( itemMap.contains(new Eye(new Vector3(0, 0, 0))) || this.contains(new Eye(new Vector3(0, 0, 0))) ) )
                        index++;
            if (index == 5 && ( itemMap.contains(new Frog(new Vector3(0, 0, 0))) || this.contains(new Frog(new Vector3(0, 0, 0))) ) )
                        index++;
            if (index == 6 && ( itemMap.contains(new Pig(new Vector3(0, 0, 0))) || this.contains(new Pig(new Vector3(0, 0, 0))) ) )
                        index++;
            if (index == 7 && ( itemMap.contains(new Slime(new Vector3(0, 0, 0))) || this.contains(new Slime(new Vector3(0, 0, 0))) ) )
                        index++;
            if (index == 8 && ( itemMap.contains(new Spider(new Vector3(0, 0, 0))) || this.contains(new Spider(new Vector3(0, 0, 0))) ) )
                        index++;
            if (index == 9 && ( itemMap.contains(new UnicornHorn(new Vector3(0, 0, 0))) || this.contains(new UnicornHorn(new Vector3(0, 0, 0))) ) )
                        index++;
            if (index == 10 && ( itemMap.contains(new WingOfABat(new Vector3(0, 0, 0))) || this.contains(new WingOfABat(new Vector3(0, 0, 0))) ) )
                        index = 1;


            //Item Spawnen
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
        }
    }
}
