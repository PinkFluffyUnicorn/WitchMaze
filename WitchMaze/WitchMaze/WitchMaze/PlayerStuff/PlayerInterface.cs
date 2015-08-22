using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.InterfaceObjects;

namespace WitchMaze.PlayerStuff
{
    class PlayerInterface
    {//needs to be reworked
        Clock clock;
        List<InterfaceObject> playerInterface;
    
        public void addIcon(Icon i){
            playerInterface.Add(i);
        }

        /// <summary>
        /// adds a clock, automaticly updates it in the interface update, clock starts automaticly
        /// </summary>
        /// <param name="c"></param>
        public void addClock(Clock c)
        {
            clock = c;
            clock.start();
        }

        public void update(GameTime gameTime)
        {
            if (clock != null)
                clock.update(gameTime);
        }

    }
}
