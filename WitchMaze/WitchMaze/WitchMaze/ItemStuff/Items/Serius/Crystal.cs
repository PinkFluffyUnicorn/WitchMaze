using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.ItemStuff.Items
{
    class Crystal : Item
    {


        
        public Crystal(Vector3 _position)
        {
            ambient = new Vector3(0.7f, 0.7f, 0.7f);
            emissive = new Vector3(0f, 0f, 0f);
            specularColor = new Vector3(0.4f, 0.6f, 0.6f);
            directional1Diffuse = new Vector3(1f, 1f, 1f);
            directional1Direction = new Vector3(0f, 1f, 0f);
            directional1Specular = new Vector3(1f, 1f, 1f);
            specularPower = 0f;


            itemIndex = EItemIndex.Crystal;
            position = _position;
            model = Game1.getContent().Load<Model>("Models/Items/BlueCrystal");
            itemIcon = new InterfaceObjects.Icon(new Vector2(0, 0), "Textures/ItemIcons/blueCrystal");
        }
    }
}
