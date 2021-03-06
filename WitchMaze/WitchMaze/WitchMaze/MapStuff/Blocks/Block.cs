﻿using System;
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
using WitchMaze.InterfaceObjects;

namespace WitchMaze.MapStuff.Blocks
{
    abstract class Block
    {
        protected Vector3 ambientColor = new Vector3(0.75f, 0.75f, 0.75f);
        protected Vector3 emissiveColor = new Vector3(0, 0, 0);
        protected Vector3 light0Direction = new Vector3(1, 1, 1);
        protected Vector3 light0Color = new Vector3(0.75f, 0.75f, 0.75f);

        public Icon minimapIcon { get; protected set; }
        /// <summary>
        /// Position of a Block
        /// </summary>
        public Vector3 position { get; protected set; }

        /// <summary>
        /// Boolean whether Block is walkable(true) or not (false)
        /// </summary>
        public Boolean walkable { get; protected set; }

        /// <summary>
        /// Boolean whether Block can transport you somewhere(true) or not (false)
        /// </summary>
        public Boolean transportable { get; protected set; }

        /// <summary>
        /// Model for the Block
        /// </summary>
        public Model model { get; protected set; }

        public Texture2D textur { get; protected set; }

        public MapCreator.tiles name { get; protected set; }

        

        protected float rotation = 0;
        /// <summary>
        /// 1. Constructor for Block - Class
        /// </summary>
        public Block()
        {

        }

        /// <summary>
        /// 2. Constructor for Block - Class with a position
        /// </summary>
        /// <param name="_position"></param>
        public Block(Vector3 _position, float _rotation)
        {
            position = _position;
            rotation = _rotation; 
        }

        public abstract void draw(Matrix projection, Matrix camera);

        
    } 
}
