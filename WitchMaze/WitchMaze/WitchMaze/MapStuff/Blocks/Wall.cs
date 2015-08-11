using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WitchMaze.MapStuff.Blocks
{
    class Wall : Block
    {

        float rotation;
        /// <summary>
        /// Constructor for Wall - Object
        /// </summary>
        /// <param name="_model">Modell</param>
        /// <param name="_position">Position Vector3, Weltkoordinaten</param>
        public Wall(Model _model, Vector3 _position, float _rotation )
        {
            model = _model;
            position = _position;
            walkable = false;
            transportable = false;
            rotation = _rotation;
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
                    effect.DirectionalLight0.Direction = new Vector3(1, 0, 0);  // coming along the x-axis
                    effect.DirectionalLight0.SpecularColor = new Vector3(0, 1, 0); // with green highlights*

                    //effect.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f); // Add some overall ambient light.
                    //effect.EmissiveColor = new Vector3(1, 0, 0); // Sets some strange emmissive lighting.  This just looks weird. 
                    Console.WriteLine(rotation);
                    effect.World = mesh.ParentBone.Transform * Matrix.CreateRotationY(rotation) * Matrix.CreateTranslation(position);
                    effect.View = Player.Player.getCamera();
                    effect.Projection = Player.Player.getProjection();
                }

                mesh.Draw();
            }

        }

       

    }
}
