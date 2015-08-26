using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.MapStuff;
using WitchMaze.ItemStuff;
using WitchMaze.PlayerStuff;

namespace WitchMaze.InterfaceObjects
{
    class Minimap : InterfaceObject
    {
        Icon[,] miniMap;
        List<Icon> itemList;
        List<Icon> playerList;

        /// <summary>
        /// creates a minimap at _position
        /// </summary>
        /// <param name="_position">top left position of the minimap</param>
        /// <param name="map">the map the minimap ist created from</param>
        public Minimap(Vector2 _position, Map map)
        {
            position = _position;
            individualScale = .5f;
            itemList = new List<Icon>();
            playerList = new List<Icon>();
            miniMap = new Icon[Settings.getMapSizeX(), Settings.getMapSizeZ()];
            for (int x = 0; x < Settings.getMapSizeX(); x++)
            {
                for (int z = 0; z < Settings.getMapSizeZ(); z++)
                {
                    miniMap[x, z] = map.getBlockAt(x, z).minimapIcon;
                    miniMap[x, z].setIndividualScale(individualScale);
                    miniMap[x, z].setPosition(new Vector2(position.X + miniMap[0, 0].getWidth() * x, position.Y + miniMap[0, 0].getHeight() * z));
                }
            }
        }

        /// <summary>
        /// updates the Minimap and adds Items to the minimap
        /// </summary>
        /// <param name="itemMap"></param>
        /// <param name="player"></param>
        public void update(ItemMap itemMap, List<Player> player)
        {
            itemList.Clear();
            for(int i = 0; i < Settings.getMapSizeX(); i++){
                for (int j = 0; j < Settings.getMapSizeZ(); j++ )
                {
                    if (!itemMap.isEmpty(i, j))
                    {
                        Console.WriteLine(i + "," + j);
                        Icon h = itemMap.getItem(i, j).itemIcon;
                        h.setPosition(new Vector2(miniMap[0, 0].getWidth() * i + position.X, miniMap[0, 0].getHeight() * j + position.Y));
                        h.setIndividualScale(individualScale);
                        itemList.Add(h);
                    }

                }
            }

            playerList.Clear();
            foreach (Player p in player)
            {
                Icon h = p.playerIcon;
                h.setPosition(new Vector2(miniMap[0, 0].getWidth() * p.getPosition().X + position.X , miniMap[0, 0].getHeight() * p.getPosition().Z + position.Y ));
                h.setIndividualScale(individualScale);
                playerList.Add(h);
            }
        }


        /// <summary>
        /// sets the position of the ItemMap
        /// </summary>
        /// <param name="p">the new  top left position</param>
        public override void setPosition(Vector2 p)
        {
            position = p;
            for (int x = 0; x < Settings.getMapSizeX(); x++)
            {
                for (int z = 0; z < Settings.getMapSizeZ(); z++)
                {
                    miniMap[x, z].setPosition(new Vector2(p.X + miniMap[0, 0].getWidth() * x, p.Y + miniMap[0, 0].getHeight() * z));
                }
            }
        }

        public override float getHeight()
        {
            float height = 0;
            for (int x = 0; x < Settings.getMapSizeX(); x++)
                    height += miniMap[0, x].getHeight();
            return height;
        }


        public override float getWidth()
        {
            float width = 0;
            for (int x = 0; x < Settings.getMapSizeX(); x++)
                    width += miniMap[x, 0].getWidth();
            return width;
        }

        /// <summary>
        /// draw the minimap
        /// </summary>
        public override void draw()
        {
            for (int x = 0; x < Settings.getMapSizeX(); x++)
                for (int z = 0; z < Settings.getMapSizeZ(); z++)
                    miniMap[x, z].draw();
            foreach (Icon item in itemList)
            {
                item.draw();
                
            }
            foreach (Icon p in playerList)
            {
                p.draw();
            }
        }
    }
}
