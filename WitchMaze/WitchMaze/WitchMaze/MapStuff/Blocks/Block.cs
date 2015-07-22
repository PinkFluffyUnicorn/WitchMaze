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

namespace WitchMaze.MapStuff.Blocks
{
    abstract class Block
    {
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
        public Block(Vector3 _position)
        {
            _position = position;
        }
        /// <summary>
        /// abstract draw Class for every Blocktype implemented differently
        /// </summary>
        public abstract void draw();

        


       



    }
    
}
