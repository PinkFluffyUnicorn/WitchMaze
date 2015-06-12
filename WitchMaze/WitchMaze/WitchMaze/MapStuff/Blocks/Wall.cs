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


        public Wall(Vector3 _position, Model _model)
        {
            model = _model;
            position = _position;

        }
        
        public override void draw(GameTime gameTime)
        {
            model.Draw(Player.Player.world, Player.Player.camera, Player.Player.projection);
        }

       

    }
}
