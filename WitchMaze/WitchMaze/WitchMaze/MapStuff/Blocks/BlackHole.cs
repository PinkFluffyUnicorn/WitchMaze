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
using WitchMaze.PlayerStuff;

namespace WitchMaze.MapStuff.Blocks
{
 
    class BlackHole : Block
    {
        private Vector3 transportPosition;

        /// <summary>
        /// Constructor for BlackHoles
        /// </summary>
        /// <param name="_position">Position Vector3, Weltkoordinaten</param>
        /// <param name="_model">Modell</param>
        public BlackHole(Vector3 _position/*, Color color*/, Model _model, Vector3 _transportPosition)
        {
            model = _model;
            position = _position;
            transportable = true;
            walkable = true;
            transportPosition = _transportPosition;
        }

        /// <summary>
        /// getter for the Position the Blackhole transports the Player to
        /// </summary>
        /// <returns>new Position of player as a Vector3</returns>
        public Vector3 getTransportPosition()
        {
            return transportPosition;
        }

        /// <summary>
        /// setter for the transportposition, is set in Mapcreator
        /// </summary>
        /// <param name="value">Vector3 with the position of the blackhole</param>
        public void setTransportPosition(Vector3 value)
        {
            transportPosition = value;
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

                    /* effect.DirectionalLight0.DiffuseColor = new Vector3(1f, 0.2f, 0.2f); // a reddish light
                     effect.DirectionalLight0.Direction = new Vector3(1, 0, 0);  // coming along the x-axis
                     effect.DirectionalLight0.SpecularColor = new Vector3(0, 1, 0); // with green highlights

                     effect.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f); // Add some overall ambient light.
                     effect.EmissiveColor = new Vector3(1, 0, 0); // Sets some strange emmissive lighting.  This just looks weird. */

                    effect.World =  mesh.ParentBone.Transform * Matrix.CreateTranslation(position);
                    effect.View = Player.getCamera();
                    effect.Projection = Player.getProjection();
                }

                mesh.Draw();
            }

        }
        
    }
}
