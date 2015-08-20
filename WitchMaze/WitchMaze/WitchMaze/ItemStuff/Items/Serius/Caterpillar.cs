using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.ItemStuff.Items
{
    class Caterpillar : Item
    {
        public Caterpillar(Vector3 _position)
        {
            position = _position;
            model = Game1.getContent().Load<Model>("Models/Items/caterpillar");
            itemIcon = new InterfaceObjects.Icon(new Vector2(0, 0), "Textures/ItemIcons/Caterpillar");
        }
    }
}
