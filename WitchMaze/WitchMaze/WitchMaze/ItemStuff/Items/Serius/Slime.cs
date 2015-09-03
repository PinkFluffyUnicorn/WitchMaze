using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.ItemStuff.Items
{
    class Slime : Item
    {
         public Slime(Vector3 _position)
        {
            ambient = new Vector3(0.6f, 0.6f, 0.6f);
            emissive = new Vector3(0f, 0f, 0f);
            specularColor = new Vector3(0.769f, 1f, 0.329f);

            directionalDiffuse = new Vector3(1f, 1f, 1f);
            directionalDirection = new Vector3(0f, -1f, 0f);
            directionalSpecular = new Vector3(1f, 1f, 1f);
            directional1Diffuse = new Vector3(1f, 1f, 1f);
            directional1Direction = new Vector3(0f, -1f, 0f);
            directional1Specular = new Vector3(1f, 1f, 1f);
            specularPower = 20f;
            positionY = 0.05f;

            scale = Matrix.CreateScale(0.0015f);
            rotationOK = false;

            itemIndex = EItemIndex.Slime;
            position = _position;
            model = Game1.getContent().Load<Model>("Models/Items/GreenSlime");
            itemIcon = new InterfaceObjects.Icon(new Vector2(0, 0), "Textures/ItemIcons/GreenSlime");
        }
    }
}
