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
        public SingleTime() { }

        Player.Player player1;
        public override void initialize()
        {
            mapCreator = new MapCreator();
            mapCreator.initialize();
            map = mapCreator.generateMap();

            itemMap = new ItemMap();
            itemSpawner = new ItemSpawner();
            itemSpawner.initialSpawn(itemMap);

            player1 = new Player.Player();
        }

        public override void loadContent()
        {
            //throw new NotImplementedException();
        }

        public override void unloadContent()
        {
            //throw new NotImplementedException();
        }

        public override EInGameState update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            player1.update(gameTime);
            itemSpawner.update(itemMap, gameTime);
            return EInGameState.SingleTime;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            player1.draw(gameTime);
            map.draw(gameTime);
            itemMap.draw();
        }
    }
}
