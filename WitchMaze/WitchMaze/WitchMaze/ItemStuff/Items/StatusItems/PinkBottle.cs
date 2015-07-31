using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.ItemStuff.Items
{
    class PinkBottle : Item
    {
        public PinkBottle(Vector3 _position)
        {
            position = _position;
            model = Game1.getContent().Load<Model>("bottle");
        }
    }
}
