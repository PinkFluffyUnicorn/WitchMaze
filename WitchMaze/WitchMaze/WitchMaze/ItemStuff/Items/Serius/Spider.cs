using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.ItemStuff.Items
{
    class Spider : Item
    {
         public Spider(Vector3 _position)
        {
            ambient = new Vector3(1f, 1f, 1f);
            emissive = new Vector3(0.01f, 0.01f, 0.01f);
            specularColor = new Vector3(0.5f, 0.5f, 0.5f);
            directionalDiffuse = new Vector3(1f, 1f, 1f);
            directionalDirection = new Vector3(0f, 1f, 0f);
            directionalSpecular = new Vector3(0.5f, 0.5f, 0.5f);
            specularPower = 2f;

            itemIndex = EItemIndex.Spider;
            position = _position;
            model = Game1.getContent().Load<Model>("Models/Items/Spider");
            itemIcon = new InterfaceObjects.Icon(new Vector2(0, 0), "Textures/ItemIcons/Spider");
        }
    }
}
