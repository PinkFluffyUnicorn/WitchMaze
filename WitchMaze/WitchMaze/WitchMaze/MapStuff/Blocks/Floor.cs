using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using WitchMaze.PlayerStuff;

namespace WitchMaze.MapStuff.Blocks
{
    class Floor : Block
    {
        public VertexPositionColor[] plane;

        /// <summary>
        /// Constructor for Floor
        /// </summary>
        /// <param name="_position">Position Vector3, Weltkoordinaten</param>
        /// <param name="_model">Modell</param>
        public Floor(Vector3 _position, /*Color color,*/ Model _model )
        {
            model = _model;
            position = _position;
            walkable = true;
            transportable = false;

        }


        /// <summary>
        /// Own Draw Method
        /// Calculates world, view and Projection Matrix and sets the Lighting 
        /// </summary>
        public override void draw()
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.LightingEnabled = true; // Turn on the lighting subsystem.

                     effect.DirectionalLight0.DiffuseColor = new Vector3(1f, 0.2f, 0.2f); // a reddish light
                     effect.DirectionalLight0.Direction = new Vector3(0, 1, 0);  // coming along the x-axis
                     effect.DirectionalLight0.SpecularColor = new Vector3(0, 1, 0); // with green highlights

                      

                     /*effect.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f); // Add some overall ambient light.
                     effect.EmissiveColor = new Vector3(1, 0, 0); // Sets some strange emmissive lighting.  This just looks weird. */

                    effect.World = mesh.ParentBone.Transform * Matrix.CreateTranslation(position);
                     effect.View = Player.getCamera();
                     effect.Projection = Player.getProjection();
                }

                mesh.Draw();
            }

            //model.Draw(Matrix.CreateTranslation(position), Player.Player.getCamera(), Player.Player.getProjection());

        }
    }
}
