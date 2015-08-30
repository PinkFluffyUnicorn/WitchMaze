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

namespace WitchMaze.GameStates.InGameStates
{
    class NeedForIngrediance : InGameState
    {
         //Spielmodus in dem man durch ein Labyrinth rennen muss und Items einsammeln kann, siegbedinungen können später so hinzugefügt werden(mach ne eigenne klasse dafür)

        public NeedForIngrediance(List<Player> _playerList)
        {
            playerList = _playerList;
        }

        public override void loadContent()
        {

        }

        public override void unloadContent()
        {

        }

        public override EInGameState update(GameTime gameTime)
        {
            //Write your Update Code here:


            //update Playeer, Items ect...
            base.update(gameTime);
            //just in case...
            return EInGameState.SingleTime;
        }
      
    }
}
