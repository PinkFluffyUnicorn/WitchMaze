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
    class MazeRun : InGameState
    {
        //Spielmodus in dem man durch ein Labyrinth rennen muss und Items einsammeln kann, siegbedinungen können später so hinzugefügt werden(mach ne eigenne klasse dafür)

        public MazeRun(List<Player> _playerList)
        {
            playerList = _playerList;
        }


        //man hat 60 sekunden um 10 Items zu sammeln, schafft man es gewinnt man, schafft man es nicht so verliert man
        float timer;

        public override void initialize()
        {
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
            base.update(gameTime);
            return EInGameState.MazeRun;
        }


        //public override void Draw(GameTime gameTime)
        //{
        //    //kann man das outsourcen?


        //}
    }
}
