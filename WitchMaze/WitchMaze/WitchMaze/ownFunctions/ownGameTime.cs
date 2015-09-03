using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.ownFunctions
{
    class ownGameTime
    {

        /// <summary>
        /// returns the elapsedGameTime in milliseconds
        /// </summary>
        private float elapsedGameTime;
        /// <summary>
        /// returns the ElapsedGameTime in Miliseconds
        /// </summary>
        /// <returns></returns>
        public float getElapsedGameTime() 
        { 
            if (!isPaused) 
                return elapsedGameTime; 
            else
                return 0;
        }
        /// <summary>
        /// returns the totalTime in milliseconds
        /// </summary>
        public float totalTime { get; private set; }

        bool isPaused;
        float pauseStartTime;
        private float pauseTime;

        public ownGameTime()
        {
            isPaused = false;
            pauseTime = 0;
        }

        /// <summary>
        /// pauses the GameTime
        /// </summary>
        public void pause()
        {
            isPaused = true;
            pauseStartTime = totalTime - pauseTime;
        }

        /// <summary>
        /// resumes the Paused GameTime
        /// </summary>
        public void resume()
        {
            isPaused = false;
        }

        /// <summary>
        /// updates the GameTime
        /// </summary>
        public void update(GameTime gameTime)
        {
            if (!isPaused)
            {
                elapsedGameTime = gameTime.ElapsedGameTime.Milliseconds;
                totalTime = gameTime.TotalGameTime.Milliseconds - pauseTime; 
            }
            else
            {
                pauseTime = totalTime - pauseStartTime;
                float trashBin = gameTime.ElapsedGameTime.Milliseconds;//throws away the elapsed time so that no big number is reached
            }

        }
    }
}
