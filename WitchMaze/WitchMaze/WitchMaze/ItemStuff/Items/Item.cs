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
        public enum EItemIndex
        {
            Branch,
            Caterpillar,
            Crystal,
            Eye,
            Frog,
            Pig,
            Slime,
            Spider,
            UnicornHorn,
            WingOfABat,
        }
        public EItemIndex itemIndex { get; protected set; }

        public InterfaceObjects.Icon itemIcon { get; protected set; }
        public Vector3 position { get; set; }
        public Model model { get; protected set; }

        protected Matrix scale = Matrix.CreateScale((float) 0.001);

        private float rotation = 0;
        protected bool rotationOK = true;

        protected Vector3 ambient, emissive, specularColor, directionalDiffuse, directionalDirection, directionalSpecular, directional1Diffuse, directional1Direction, directional1Specular;
        protected float specularPower, rotate;
        protected float positionY = 0.2f;

        public void draw(Matrix projection, Matrix camera)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.LightingEnabled = true;

                    effect.AmbientLightColor = ambient;

                    effect.EmissiveColor = emissive;

                    effect.SpecularColor = specularColor;
                    effect.SpecularPower = specularPower;

                    effect.DirectionalLight0.Enabled = true;
                    effect.DirectionalLight0.DiffuseColor = directionalDiffuse; 
                    effect.DirectionalLight0.Direction = directionalDirection;  
                    effect.DirectionalLight0.SpecularColor = directionalSpecular;

                    effect.DirectionalLight1.Enabled = true;
                    effect.DirectionalLight1.DiffuseColor = directional1Diffuse;
                    effect.DirectionalLight1.Direction = directional1Direction;
                    effect.DirectionalLight1.SpecularColor = directional1Specular;


                    effect.World = mesh.ParentBone.Transform * scale *Matrix.CreateRotationZ(rotate)* Matrix.CreateRotationY(rotation)* Matrix.CreateTranslation(position.X, positionY, position.Z);
                    effect.View = camera;
                    effect.Projection = projection;
                }

                mesh.Draw();
            }
            if (rotationOK)
                rotation += (float)0.01;
        }

    }
}
