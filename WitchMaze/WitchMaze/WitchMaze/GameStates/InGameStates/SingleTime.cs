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
using WitchMaze.Items;
using WitchMaze.Player;

namespace WitchMaze.GameStates.InGameStates
{
    class SingleTime : InGameState
    {
        //basicly a test
        MapCreator mapCreator;
        Map map;

        Player.Player player1;
        public void initialize()
        {
            mapCreator = new MapCreator();
            map = mapCreator.generateMap();

            player1 = new Player.Player();
        }

        public void loadContent()
        {
            //throw new NotImplementedException();
        }

        public void unloadContent()
        {
            //throw new NotImplementedException();
        }

        public EInGameState update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            player1.update(gameTime);
            return EInGameState.SingleTime;
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            player1.draw(gameTime);
            map.draw(gameTime);
        }
    }
}
