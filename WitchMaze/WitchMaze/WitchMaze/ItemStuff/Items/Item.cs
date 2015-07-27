using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.ItemStuff.Items
{
    abstract class Item
    {
        public Vector3 position { get; protected set; }
        public Model model { get; protected set; }


        public void draw()
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.LightingEnabled = true; // Turn on the lighting subsystem.

                    effect.DirectionalLight0.DiffuseColor = new Vector3(1f, 0.2f, 0.2f); // a reddish light
                    effect.DirectionalLight0.Direction = new Vector3(1, 0, 0);  // coming along the x-axis
                    effect.DirectionalLight0.SpecularColor = new Vector3(0, 1, 0); // with green highlights

                    /*effect.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f); // Add some overall ambient light.
                    effect.EmissiveColor = new Vector3(1, 0, 0); // Sets some strange emmissive lighting.  This just looks weird. */

                    effect.World = mesh.ParentBone.Transform * Matrix.CreateTranslation(position);
                    effect.View = Player.Player.getCamera();
                    effect.Projection = Player.Player.getProjection();
                }

                mesh.Draw();
            }
        }

    }
}
