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

namespace WitchMaze.MapStuff.Blocks
{
    abstract class Block
    {
        public Vector3 position { get; protected set; }
       
        public Block()
        {

        }

        public Block(Vector3 _position)
        {
            _position = position;
        }

        public abstract void draw(GameTime gameTime);


       



    }
    
}
