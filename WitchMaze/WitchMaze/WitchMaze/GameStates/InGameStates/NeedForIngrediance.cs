using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using WitchMaze.MapStuff;
using WitchMaze.ItemStuff.Items;
using WitchMaze.PlayerStuff;
using WitchMaze.ItemStuff;
using WitchMaze.InterfaceObjects;
using WitchMaze.ownFunctions;

namespace WitchMaze.GameStates.InGameStates
{
    class NeedForIngrediance : InGameState
    {
         //Spielmodus in dem man durch ein Labyrinth rennen muss und Items einsammeln kann, siegbedinungen können später so hinzugefügt werden(mach ne eigenne klasse dafür)
        Clock clock;
        ItemList[] itemsToCollect;
        int numberItems = 5;
        float scale = 1.3f;
        public NeedForIngrediance(List<Player> _playerList)
        {
            playerList = _playerList;
        }


        //man hat 60 sekunden um 10 Items zu sammeln, schafft man es gewinnt man, schafft man es nicht so verliert man

        public override void initialize()
        {
            base.initialize();

            //anzahl der eingesammelten Items
            itemsToCollect = new ItemList[playerList.Count];
            int i = 0;
            foreach (Player p in playerList)
            {
                itemsToCollect[i] = new ItemList(numberItems);
                itemsToCollect[i].spawnRandomItems();
                itemsToCollect[i].setIndividualScale(scale);
                itemsToCollect[i].setPosition(new Vector2(p.getViewport().X + p.getViewport().Width / 2 -  itemsToCollect[i].getWidth() / 2 , p.getViewport().Y + p.getViewport().Height - itemsToCollect[i].getHeight()));

                i++;
            }

            if (playerList.Count == 1)
            {//singleplayer ligic
                clock = new Clock(new Vector2(0, 0));
                clock.setIndividualScale(scale);
                clock.setPosition(new Vector2(playerList.First().getViewport().Width / 2 - clock.getWidth() / 2, playerList.First().getViewport().Height - clock.getHeight() - itemsToCollect[0].getHeight()));

                clock.start();
            }

            currentInGameState = EInGameState.NeedForIngrediance;
            
        }



        public override EInGameState update(ownGameTime gameTime)
        {
            //Write WinCondition Here
            int i = 0;
            foreach (Player p in playerList)
            {
                foreach (Item item in p.getItemsCollected())
                {
                    if (itemsToCollect[i].contains(item))
                        itemsToCollect[i].setCollected(item);
                }
                //HashSet sombodey won?
                if (itemsToCollect[i].allCollected())
                {
                    p.hasWon = true;
                    return EInGameState.Exit;
                }

                i++;
            }

            if (playerList.Count == 1)
            {
                clock.update(gameTime);
                //solo verloren?
                if (clock.getTotalMilliseconds() > 10 * 1000)
                {
                    
                    return EInGameState.Exit;
                }
                    
            }



            base.update(gameTime);
            return EInGameState.NeedForIngrediance;
        }

        //public Item[] fiveRandomItems(int numberOfItems)
        //{

        //}


        public override void Draw()
        {
            base.Draw();
            int i = 0;
            foreach (Player p in playerList)
            {

                //Console.WriteLine(itemsToCollect[i, j].itemIndex + " i:" + i + " j:" + j + " position:" + itemsToCollect[i, j].itemIcon.getPosition() );
                itemsToCollect[i].draw();
                i++;
            }

            if (playerList.Count == 1)
            {
                clock.draw();
                //Console.WriteLine(clock.getPosition());
            }
        }
    }
}
