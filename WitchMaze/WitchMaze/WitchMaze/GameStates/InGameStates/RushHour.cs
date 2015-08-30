using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.ItemStuff;
using WitchMaze.MapStuff;
using WitchMaze.PlayerStuff;
using WitchMaze.InterfaceObjects;

namespace WitchMaze.GameStates.InGameStates
{
    class RushHour : InGameState
    {
        //Spielmodus in dem man durch ein Labyrinth rennen muss und Items einsammeln kann, siegbedinungen können später so hinzugefügt werden(mach ne eigenne klasse dafür)
        Clock clock;
        Text[] numberItemsCollected;
        public RushHour(List<Player> _playerList)
        {
            playerList = _playerList;
        }


        //man hat 60 sekunden um 10 Items zu sammeln, schafft man es gewinnt man, schafft man es nicht so verliert man
        float timer;

        public override void initialize()
        {
            //anzahl der eingesammelten Items
            numberItemsCollected = new Text[playerList.Count];
            int i = 0;
            foreach (Player p in playerList)
            {
                numberItemsCollected[i] = new Text("Items Collected: 0", new Vector2(0, 0));
                numberItemsCollected[i].setIndividualScale(1.3f);//mehr geht nicht wegen minimap im multiplayer
                numberItemsCollected[i].setPosition(new Vector2(p.getViewport().X + p.getViewport().Width / 2 - numberItemsCollected[i].getWidth() / 2,p.getViewport().Y + p.getViewport().Height - numberItemsCollected[i].getHeight()));
                i++;
            }

            if (playerList.Count == 1)
            {//singleplayer ligic
                clock = new Clock(new Vector2(0, 0));
                clock.setIndividualScale(1.3f);
                clock.setPosition(new Vector2(playerList.First().getViewport().Width / 2 - clock.getWidth() / 2, playerList.First().getViewport().Height - clock.getHeight() - numberItemsCollected[0].getHeight()));

                clock.start();
            }
            else
            {//multiplayer logic

            }

            currentInGameState = EInGameState.MazeRun;
            base.initialize();
        }

        

        //public override void loadContent()
        //{

        //}

        //public override void unloadContent()
        //{

        //}

        public override EInGameState update(GameTime gameTime)
        {
            //Write WinCondition Here
            int i = 0;
            foreach (Player p in playerList)
            {
                numberItemsCollected[i].updateText("Items Collected: " + p.getItemsCollected().Count);
                i++;
                //HashSet sombodey one?
                if (p.getItemsCollected().Count > 10)
                    return EInGameState.Exit;
            }

            if (playerList.Count == 1)
            {
                clock.update(gameTime);
                //solo verloren?
                if (clock.getTotalMilliseconds() > 100 * 1000)
                    return EInGameState.Exit;
            }



            base.update(gameTime);
            return EInGameState.MazeRun;
        }


        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            int i = 0;
            foreach (Player p in playerList)
            {
                numberItemsCollected[i].draw();
                i++;
            }

            if (playerList.Count == 1)
            {
                clock.draw();
                //Console.WriteLine(clock.getPosition());
            }
            else
            {

            };


        }
    }
}
