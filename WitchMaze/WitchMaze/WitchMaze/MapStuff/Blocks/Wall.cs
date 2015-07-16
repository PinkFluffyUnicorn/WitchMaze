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
        Model model;
          


        public Wall(Vector3 _position, Model _model, Boolean _walkable, Boolean _transportable)
        {

            model = _model;
            position = _position;
            walkable = _walkable;
            transportable = _transportable;
        }
        
        public override void draw(GameTime gameTime)
        {
           
            model.Draw(Matrix.CreateScale(0.05f)*Matrix.CreateScale(Settings.blockSizeX, Settings.blockSizeY, Settings.blockSizeZ) * Matrix.CreateTranslation(position), Player.Player.getCamera(), Player.Player.getProjection());
            Game1.effect.LightingEnabled = true;
            //Game1.getEffect().World = Matrix.Identity;
            //Game1.getEffect().CurrentTechnique.Passes[0].Apply();
            
        }

       

    }
}
