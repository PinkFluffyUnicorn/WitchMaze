using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.MapStuff;
using WitchMaze.Items;

namespace WitchMaze.GameStates.InGameStates
{
    class SingleTime : InGameState
    {
        //basicly a test
        MapCreator mapCreator;
        Map map;
        public void initialize()
        {
            mapCreator = new MapCreator();
            map = mapCreator.generateMap();
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
            //throw new NotImplementedException();
            return EInGameState.SingleTime;
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice)
        {
            map.draw(gameTime, graphicsDevice);
        }
    }
}
