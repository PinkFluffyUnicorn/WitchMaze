using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.Items
{
    class GreenBottle : Item
    {
        public GreenBottle(Vector3 _position)
        {
            position = _position;
            model = Game1.getContent().Load<Model>("bottle");
        }

        public override void draw()
        {
            model.Draw(Matrix.CreateTranslation(position), Player.Player.getCamera(), Player.Player.getProjection());
        }
    }
}
