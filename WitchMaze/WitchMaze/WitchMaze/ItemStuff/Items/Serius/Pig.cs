using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.ItemStuff.Items
{
    class Pig : Item
    {
         public Pig(Vector3 _position)
        {
            ambient = new Vector3(1f, 1f, 1f);
            emissive = new Vector3(0.1f, 0.1f, 0.1f);
            specularColor = new Vector3(1f, 0.853f, 0.9f);
            directionalDiffuse = new Vector3(0.1f, 0.1f, 0.1f);
            directionalDirection = new Vector3(0f, 1f, 0f);
            directionalSpecular = new Vector3(0.88f, 0.093f, 0.591f);
            directional1Diffuse = new Vector3(0.1f, 0.1f, 0.1f);
            directional1Direction = new Vector3(0f, -1f, 0f);
            directional1Specular = new Vector3(0.88f, 0.093f, 0.591f);
            specularPower = 2f;

            scale = Matrix.CreateScale((float)0.0015);

            itemIndex = EItemIndex.Pig;
            position = _position;
            model = Game1.getContent().Load<Model>("Models/Items/Pig");
            itemIcon = new InterfaceObjects.Icon(new Vector2(0, 0), "Textures/ItemIcons/Pig");
        }
    }
}
