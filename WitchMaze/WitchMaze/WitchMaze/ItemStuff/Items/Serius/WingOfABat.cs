using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.ItemStuff.Items
{
    class WingOfABat : Item
    {
         public WingOfABat(Vector3 _position)
        {
            ambient = new Vector3(1f, 1f, 1f);
            emissive = new Vector3(0f, 0f, 0f);
            specularColor = new Vector3(0.7f, 0.7f, 0.7f);
            directionalDiffuse = new Vector3(0.7f, 0.7f, 0.7f);
            directionalDirection = new Vector3(0f, -1f, 0f);
            directionalSpecular = new Vector3(0.7f, 0.7f, 0.7f);
            specularPower = 2f;
            scale = Matrix.CreateScale(0.0025f);

            itemIndex = EItemIndex.WingOfABat;
            position = _position;
            model = Game1.getContent().Load<Model>("Models/Items/BatWings");
            itemIcon = new InterfaceObjects.Icon(new Vector2(0, 0), "Textures/ItemIcons/BatWings");
        }
    }
}
