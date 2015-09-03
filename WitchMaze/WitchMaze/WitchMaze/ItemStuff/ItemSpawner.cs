using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.ItemStuff.Items;
using WitchMaze.ownFunctions;

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
        public ItemSpawner()
        {
            randomMap = new Random();
            randomItem = new Random();
            itemsToSpawn = new List<Item>();
            timeToNewItem = 0;
            timeForNewItem *= 1000;
        }
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

            //spawn 5 new Items here
            spawnItem(itemMap);
            spawnItem(itemMap);
            spawnItem(itemMap);
            spawnItem(itemMap);
            spawnItem(itemMap);

            //itemMap.insertItem(new WingOfABat(new Vector3(5,1,5)), 5, 5);
        }

        /// <summary>
        /// Updates Item Map if Changed
        /// </summary>
        /// <param name="itemMap">the ItemMap to update</param>
        public void update(ItemMap itemMap, ownGameTime gameTime, List<PlayerStuff.Player> playerList)
        {
            //Console.WriteLine(timeToNewItem);
            //Console.WriteLine(itemsToSpawn.Count());

            timeToNewItem += gameTime.getElapsedGameTime();
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

        Branch branch = new Branch(new Vector3(0, 0, 0));
        Caterpillar caterpillar =  new Caterpillar(new Vector3(0, 0, 0));
        Crystal crystal = new Crystal(new Vector3(0, 0, 0));
        Eye eye = new Eye(new Vector3(0, 0, 0));
        Frog frog = new Frog(new Vector3(0, 0, 0));
        Pig pig = new Pig(new Vector3(0, 0, 0));
        Slime slime = new Slime(new Vector3(0, 0, 0));
        Spider spider = new Spider(new Vector3(0, 0, 0));
        UnicornHorn unicornHorn = new UnicornHorn(new Vector3(0, 0, 0));
        WingOfABat wingOfABat = new WingOfABat(new Vector3(0, 0, 0));
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
            Item item = generateRandomItem();
            //if itemToSpawn already contains the item to spawn(also check the ItemMap!)
            if (item.itemIndex == branch.itemIndex && (itemMap.contains(item) || this.contains(item))) // && itemsToSpawn.Contains(new Branch(new Vector3(0, 0, 0)))
                item = caterpillar;
            if (item.itemIndex == caterpillar.itemIndex && (itemMap.contains(caterpillar) || this.contains(caterpillar) ) )
                item = crystal;
            if (item.itemIndex == crystal.itemIndex && (itemMap.contains(crystal) || this.contains(crystal)))
                item = eye;
            if (item.itemIndex == eye.itemIndex && (itemMap.contains(eye) || this.contains(eye)))
                item = frog;
            if (item.itemIndex == frog.itemIndex && (itemMap.contains(frog) || this.contains(frog)))
                item = pig;
            if (item.itemIndex == pig.itemIndex && (itemMap.contains(pig) || this.contains(pig)))
                item = slime;
            if (item.itemIndex == slime.itemIndex && (itemMap.contains(slime) || this.contains(slime)))
                item = spider;
            if (item.itemIndex == spider.itemIndex && (itemMap.contains(spider) || this.contains(spider)))
                item = unicornHorn;
            if (item.itemIndex == unicornHorn.itemIndex && (itemMap.contains(unicornHorn) || this.contains(unicornHorn)))
                item = wingOfABat;
            if (item.itemIndex == wingOfABat.itemIndex && (itemMap.contains(wingOfABat) || this.contains(wingOfABat)))
                item = branch;

            itemsToSpawn.Add(item);

            //Item Spawnen
            //switch (index)
            //{
            //    case 1:
            //        itemsToSpawn.Add(new Branch(position));
            //        break;
            //    case 2:
            //        itemsToSpawn.Add(new Caterpillar(position));
            //        break;
            //    case 3:
            //        itemsToSpawn.Add(new Crystal(position));
            //        break;
            //    case 4:
            //        itemsToSpawn.Add(new Eye(position));
            //        break;
            //    case 5:
            //        itemsToSpawn.Add(new Frog(position));
            //        break;
            //    case 6:
            //        itemsToSpawn.Add(new Pig(position));
            //        break;
            //    case 7:
            //        itemsToSpawn.Add(new Slime(position));
            //        break;
            //    case 8:
            //        itemsToSpawn.Add(new Spider(position));
            //        break;
            //    case 9:
            //        itemsToSpawn.Add(new UnicornHorn(position));
            //        break;
            //    case 10:
            //        itemsToSpawn.Add(new WingOfABat(position));
            //        break;
            //}
        }

        public Item generateRandomItem()
        {
            int index = randomItem.Next(1, 11);
            Item item;
            switch (index)
            {
                case 1:
                    item =  new Branch(new Vector3(0,0,0));
                    break;
                case 2:
                    item = new Caterpillar(new Vector3(0, 0, 0));
                    break;
                case 3:
                    item = new Crystal(new Vector3(0, 0, 0));
                    break;
                case 4:
                    item = new Eye(new Vector3(0, 0, 0));
                    break;
                case 5:
                    item = new Frog(new Vector3(0, 0, 0));
                    break;
                case 6:
                    item = new Pig(new Vector3(0, 0, 0));
                    break;
                case 7:
                    item = new Slime(new Vector3(0, 0, 0));
                    break;
                case 8:
                    item = new Spider(new Vector3(0, 0, 0));
                    break;
                case 9:
                    item = new UnicornHorn(new Vector3(0, 0, 0));
                    break;
                case 10:
                    item = new WingOfABat(new Vector3(0, 0, 0));
                    break;
                default :
                    throw new NotImplementedException();
            }
            return item;
        }
    }
}
