using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.ItemStuff.Items
{
    class Branch : Item
    {
         public Branch(Vector3 _position)
        {

            ambient = new Vector3(1f, 1f, 1f);
            emissive = new Vector3(0f, 0f, 0f);
            specularColor = new Vector3(1f, 1f, 1f);
            directionalDiffuse = new Vector3(0.466f,0.617f, 0.151f);
            directionalDirection = new Vector3(0f, 1f, 0f);
            directionalSpecular = new Vector3(0.5f, 0.5f, 0.5f);
            specularPower = 0f;


            itemIndex = EItemIndex.Branch;
            position = _position;
            model = Game1.getContent().Load<Model>("Models/Items/Branch");
            itemIcon = new InterfaceObjects.Icon(new Vector2(0, 0), "Textures/ItemIcons/Branch");
        }
    }
}
