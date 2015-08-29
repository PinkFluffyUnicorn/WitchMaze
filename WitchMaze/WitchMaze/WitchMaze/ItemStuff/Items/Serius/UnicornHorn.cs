using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.ItemStuff.Items
{
    class UnicornHorn : Item
    {
         public UnicornHorn(Vector3 _position)
        {
            itemIndex = EItemIndex.UnicornHorn;
            position = _position;
            model = Game1.getContent().Load<Model>("Models/Items/UnicornHorn");
            itemIcon = new InterfaceObjects.Icon(new Vector2(0, 0), "Textures/ItemIcons/UnicornHorn");
        }
    }
}
