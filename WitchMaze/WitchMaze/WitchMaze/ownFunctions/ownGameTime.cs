using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.ownFunctions
{
    class ownGameTime
    {
        GameTime gameTime;

        /// <summary>
        /// returns the elapsedGameTime in milliseconds
        /// </summary>
        public float elapsedGameTime { get; private set; }
        /// <summary>
        /// returns the totalTime in milliseconds
        /// </summary>
        public float totalTime { get; private set; }

        bool pause;
        private float pauseTime;

        public ownGameTime(GameTime gameTime)
        {
            pause = false;
            pauseTime = 0;
            this.gameTime = gameTime;
        }

        /// <summary>
        /// updates the GameTime
        /// </summary>
        public void update()
        {
            if (!pause)
            {
                elapsedGameTime = gameTime.ElapsedGameTime.Milliseconds;
                totalTime = gameTime.TotalGameTime.Milliseconds; 
            }

        }
    }
}
