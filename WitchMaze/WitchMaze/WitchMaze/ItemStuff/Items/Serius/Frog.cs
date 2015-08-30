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
            ambient = new Vector3(1f, 1f, 1f);
            emissive = new Vector3(0f, 0f, 0f);
            specularColor = new Vector3(0f, 0.911f, 0f);
            directionalDiffuse = new Vector3(0f, 0.911f, 0f);
            directionalDirection = new Vector3(0f, 1f, 0f);
            directionalSpecular = new Vector3(0f, 0.4f, 0f);
            specularPower = 2f;

            itemIndex = EItemIndex.Frog;
            position = _position;
            model = Game1.getContent().Load<Model>("Models/Items/Frog");
            itemIcon = new InterfaceObjects.Icon(new Vector2(0, 0), "Textures/ItemIcons/Frog");
        }
    }
}
