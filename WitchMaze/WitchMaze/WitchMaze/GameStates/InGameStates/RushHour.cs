﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.ItemStuff;
using WitchMaze.MapStuff;
using WitchMaze.PlayerStuff;
using WitchMaze.InterfaceObjects;
using WitchMaze.ownFunctions;

namespace WitchMaze.GameStates.InGameStates
{
    class RushHour : InGameState
    {
        //Spielmodus in dem man durch ein Labyrinth rennen muss und Items einsammeln kann, siegbedinungen können später so hinzugefügt werden(mach ne eigenne klasse dafür)
        Clock clock;
        Text[] numberItemsCollected;
        float scale = 1.3f;
        public RushHour(List<Player> _playerList)
        {
            playerList = _playerList;
        }


        //man hat 60 sekunden um 10 Items zu sammeln, schafft man es gewinnt man, schafft man es nicht so verliert man

        public override void initialize()
        {
            base.initialize();

            //anzahl der eingesammelten Items
            numberItemsCollected = new Text[playerList.Count];
            int i = 0;
            foreach (Player p in playerList)
            {
                numberItemsCollected[i] = new Text("Items Collected: 0/10", new Vector2(0, 0));
                numberItemsCollected[i].setIndividualScale(scale);//mehr geht nicht wegen minimap im multiplayer
                numberItemsCollected[i].setPosition(new Vector2(p.getViewport().X + p.getViewport().Width / 2 - numberItemsCollected[i].getWidth() / 2,p.getViewport().Y + p.getViewport().Height - numberItemsCollected[i].getHeight()));
                i++;
            }

            if (playerList.Count == 1)
            {//singleplayer ligic
                clock = new Clock(new Vector2(0, 0));
                clock.setIndividualScale(scale);
                clock.setPosition(new Vector2(playerList.First().getViewport().Width / 2 - clock.getWidth() / 2, playerList.First().getViewport().Height - clock.getHeight() - numberItemsCollected[0].getHeight()));

                clock.start();
            }

            currentInGameState = EInGameState.RushHour;
            
        }



        public override EInGameState update(ownGameTime gameTime)
        {
            //Write WinCondition Here
            int i = 0;
            foreach (Player p in playerList)
            {
                numberItemsCollected[i].updateText("Items Collected: " + p.getItemsCollected().Count + "/10");
                i++;
                //HashSet sombodey one?
                if (p.getItemsCollected().Count >= 10)
                {
                    p.hasWon = true;
                    return EInGameState.Exit;

                }

            }

            if (playerList.Count == 1)
            {
                clock.update(gameTime);
                //solo verloren?
                if (clock.getTotalMilliseconds() > 120 * 1000)
                    return EInGameState.Exit;
            }



            base.update(gameTime);
            return EInGameState.RushHour;
        }


        public override void Draw()
        {
            base.Draw();

            int i = 0;
            foreach (Player p in playerList)
            {
                numberItemsCollected[i].draw();
                i++;
            }

            if (playerList.Count == 1)
            {
                clock.draw();
            }


        }
    }
}
