using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.ItemStuff;
using WitchMaze.ItemStuff.Items;

namespace WitchMaze.InterfaceObjects
{
    /// <summary>
    /// basicly an array that manages the ItemsToCollect
    /// </summary>
    class ItemList : InterfaceObject
    {
        Item[] itemList;
        bool[] itemCollected;
        Icon[] redCrossList;
        ItemSpawner itemSpawner;

        public ItemList(int count){
            redCrossList = new Icon[count];
            itemList = new Item[count];
            itemCollected = new bool[count];
            itemSpawner = new ItemSpawner();
        }

        /// <summary>
        /// randomly spawns different Items in the ItemList
        /// </summary>
        public void spawnRandomItems()
        {
            for (int i = 0; i < this.itemList.Count(); i++)
            {
                redCrossList[i] = new Icon(new Vector2(0, 0), "Textures/redCross");
                Item item = itemSpawner.generateRandomItem();
                itemList[i] = item;
                for(int j = 0; j < i; j++){
                    if (itemList[j].itemIndex == itemList[i].itemIndex)//itemList[i].itemIndex == itemList[j].itemIndex
                    {
                        i--;
                        break;
                    }
                }
                itemCollected[i] = false;
            }
        }

        /// <summary>
        /// checks if the Item item is contained in the List
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool contains(Item item)
        {
            for (int i = 0; i < this.itemList.Count(); i++)
            {
                if(itemList[i].itemIndex == item.itemIndex)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// sets a item to the "collected" state
        /// </summary>
        /// <param name="item"></param>
        public void setCollected(Item item){
            for (int i = 0; i < this.itemList.Count(); i++)
            {
                if (itemList[i].itemIndex == item.itemIndex)
                    itemCollected[i] = true;
            }
        }
        public bool allCollected()
        {
            for (int i = 0; i < this.itemList.Count(); i++)
            {
                if (!itemCollected[i])
                    return false;
            }
            return true;
        }

        public override float getHeight()
        {
            return itemList[0].itemIcon.getHeight();
        }
        public override float getWidth()
        {
            float width = 0;
            for (int i = 0; i < this.itemList.Count(); i++)
            {
                width += itemList[i].itemIcon.getWidth();
            }
            return width;
        }

        public override void setPosition(Vector2 p)
        {
            Vector2 nextPosition = p;
            for (int i = 0; i < this.itemList.Count(); i++)
            {
                redCrossList[i].setPosition(nextPosition);
                itemList[i].itemIcon.setPosition(nextPosition);
                nextPosition = new Vector2(nextPosition.X + itemList[i].itemIcon.getWidth(), nextPosition.Y);
            }
        }

        public override void setIndividualScale(float _individualScale)
        {
            for (int i = 0; i < this.itemList.Count(); i++)
            {
                redCrossList[i].setIndividualScale(_individualScale);
                itemList[i].itemIcon.setIndividualScale(_individualScale);
            }
        }

        /// <summary>
        /// draws all the non collected Items in the ItemList
        /// </summary>
        public override void draw()
        {
            for (int i = 0; i < this.itemList.Count(); i++)
            {
                itemList[i].itemIcon.draw();
                if (itemCollected[i])
                    redCrossList[i].draw();
            }
        }
    }
}
