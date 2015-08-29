using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.ItemStuff.Items
{
    class Frog : Item
    {
         public Frog(Vector3 _position)
        {
            itemIndex = EItemIndex.Frog;
            position = _position;
            model = Game1.getContent().Load<Model>("Models/Items/Frog");
            itemIcon = new InterfaceObjects.Icon(new Vector2(0, 0), "Textures/ItemIcons/Frog");
        }
    }
}
