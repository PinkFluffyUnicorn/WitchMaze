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
    class Wall : Block
    {

        /// <summary>
        /// Constructor for Wall - Object
        /// </summary>
        /// <param name="_model">Modell</param>
        /// <param name="_position">Position Vector3, Weltkoordinaten</param>
        public Wall(Model _model, Vector3 _position, float _rotation )
        {
            model = _model;
            position = _position;
            walkable = false;
            transportable = false;
            rotation = _rotation;
            name = MapCreator.tiles.wall;
        }
    }
}
