using System;
using System.Collections.Generic;
using System.Linq;
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
    class Floor : Block
    {

        /// <summary>
        /// Constructor for Floor
        /// </summary>
        /// <param name="_position">Position Vector3, Weltkoordinaten</param>
        /// <param name="_model">Modell</param>
        public Floor(Vector3 _position, /*Color color,*/ Model _model, float _rotation )
        {
            model = _model;
            position = _position;
            walkable = true;
            transportable = false;
            name = MapCreator.tiles.floor;
            rotation = _rotation;
            minimapIcon = new InterfaceObjects.Icon(new Vector2(0, 0), "Textures/MiniMapTextures/minimapFloor");
        }
    }
}
