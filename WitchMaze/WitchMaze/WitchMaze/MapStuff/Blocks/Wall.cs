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
    class Wall : Block
    {



        public Wall(Model _model, Vector3 _position)
        {

            model = _model;
            position = _position;
            walkable = false;
            transportable = false;
        }
        
        public override void draw(GameTime gameTime)
        {

            
            model.Draw(Matrix.CreateTranslation(position), Player.Player.getCamera(), Player.Player.getProjection());
            //Game1.getEffect().World = Matrix.Identity;
            //Game1.getEffect().CurrentTechnique.Passes[0].Apply();
            
        }

       

    }
}
