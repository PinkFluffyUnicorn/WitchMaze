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
using WitchMaze.PlayerStuff;

namespace WitchMaze.MapStuff.Blocks
{
 
    class BlackHole : Block
    {
        /// <summary>
        /// Position which the blackholetransports to 
        /// </summary>
        private Vector3 transportPosition;
        

        /// <summary>
        /// Constructor for BlackHoles
        /// </summary>
        /// <param name="_position">Position Vector3, Weltkoordinaten</param>
        /// <param name="_model">Modell</param>
        public BlackHole(Vector3 _position/*, Color color*/, Model _model, Vector3 _transportPosition)
        {
            model = _model;
            position = _position;
            transportable = true;
            walkable = true;
            transportPosition = _transportPosition;
            name = MapCreator.tiles.blackhole;
        }

        /// <summary>
        /// getter for the Position the Blackhole transports the Player to
        /// </summary>
        /// <returns>new Position of player as a Vector3</returns>
        public Vector3 getTransportPosition()
        {
            return transportPosition;
        }

        /// <summary>
        /// setter for the transportposition, is set in Mapcreator
        /// </summary>
        /// <param name="value">Vector3 with the position of the blackhole</param>
        public void setTransportPosition(Vector3 value)
        {
            transportPosition = value;
        }
    }
}
