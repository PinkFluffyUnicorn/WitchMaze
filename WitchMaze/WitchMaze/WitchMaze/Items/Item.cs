using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.Items
{
    abstract class Item
    {
        public Vector3 position { get; protected set; }
        public Model model { get; protected set; }

        public abstract void draw();

    }
}
