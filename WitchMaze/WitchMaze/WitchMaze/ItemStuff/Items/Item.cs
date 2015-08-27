using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.PlayerStuff;

namespace WitchMaze.ItemStuff.Items
{
    abstract class Item
    {
        public InterfaceObjects.Icon itemIcon { get; protected set; }
        public Vector3 position { get; protected set; }
        public Model model { get; protected set; }

        private Matrix scale = Matrix.CreateScale((float) 0.001);

        private float rotation = 0;

        public void draw(Matrix projection, Matrix camera)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.LightingEnabled = true;

                    effect.AmbientLightColor = new Vector3(1, 1, 1);

                    effect.DirectionalLight0.DiffuseColor = new Vector3(1f, 1f, 1f); 
                    effect.DirectionalLight0.Direction = new Vector3(0, 1, 0);  
                    effect.DirectionalLight0.SpecularColor = new Vector3(0, 1, 0); 

                    effect.World = mesh.ParentBone.Transform * scale * Matrix.CreateRotationY(rotation)* Matrix.CreateTranslation(position.X, 0.2f, position.Z);
                    effect.View = camera;
                    effect.Projection = projection;
                }

                mesh.Draw();
            }
            rotation += (float)0.01;
        }

    }
}
