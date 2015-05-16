using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.MapStuff;
using WitchMaze.Items;

namespace WitchMaze.GameStates.InGameStates
{
    class SinlgeTime : InGameState
    {
        MapCreator mapCreator;
        MapStuff.Map map;
        public void initialize()
        {
            mapCreator = new MapCreator();
            //map = new Map();
            throw new NotImplementedException();
        }

        public void loadContent()
        {
            throw new NotImplementedException();
        }

        public void unloadContent()
        {
            throw new NotImplementedException();
        }

        public EInGameState update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.GraphicsDeviceManager graphics)
        {
            throw new NotImplementedException();
        }
    }
}
