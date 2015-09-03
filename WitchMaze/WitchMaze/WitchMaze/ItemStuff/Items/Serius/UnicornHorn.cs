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
            ambient = new Vector3(1f, 1f, 1f);
            emissive = new Vector3(0.1f, 0.1f, 0.1f);
            specularColor = new Vector3(1f, 1f, 1f);
            directionalDiffuse = new Vector3(0.2f, 0.2f, 0.19f);
            directionalDirection = new Vector3(0, 1f, 0);
            directionalSpecular = new Vector3(0.3f, 0.3f, 0.29f);
            specularPower = 2f;
            rotate = 0.785f;

            scale = Matrix.CreateScale((float)0.0013);

            itemIndex = EItemIndex.UnicornHorn;
            position = _position;
            model = Game1.getContent().Load<Model>("Models/Items/UnicornHorn");
            itemIcon = new InterfaceObjects.Icon(new Vector2(0, 0), "Textures/ItemIcons/UnicornHorn");
        }
    }
}
