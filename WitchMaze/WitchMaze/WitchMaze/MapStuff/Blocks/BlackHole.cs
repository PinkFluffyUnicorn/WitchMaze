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
        /// <summary>
        /// Position which the blackholetransports to 
        /// </summary>
        public Vector3 transportPosition{get; private set;}
        

        /// <summary>
        /// Constructor for BlackHoles
        /// </summary>
        /// <param name="_position">Position Vector3, Weltkoordinaten</param>
        /// <param name="_model">Modell</param>
        public BlackHole(Vector3 _position, Model _model, Vector3 _transportPosition, Texture2D _texture)
        {
            model = _model;
            position = _position;
            transportable = true;
            walkable = true;
            transportPosition = _transportPosition;
            name = MapCreator.tiles.blackhole;
            minimapIcon = new InterfaceObjects.Icon(new Vector2(0, 0), "Textures/MiniMapTextures/minimapBlackHole");
            textur = _texture;
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

        override public void draw(Matrix projection, Matrix camera)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.TextureEnabled = true;
                    effect.Texture = Game1.getContent().Load<Texture2D>("Models/MapStuff/BlackHoleTexture");
                    //effect.EnableDefaultLighting();
                    effect.LightingEnabled = true;

                    effect.AmbientLightColor = new Vector3(1f, 1f, 1f);
                    effect.EmissiveColor = new Vector3(1f, 1f, 1f);
                    effect.DirectionalLight0.Direction = new Vector3(0, 1, 0);
                    effect.DirectionalLight0.DiffuseColor = new Vector3(1, 0, 0);
                    
                    effect.View = camera;
                    effect.Projection = projection;
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY((float)rotation) * Matrix.CreateScale((float)0.5) * Matrix.CreateTranslation(position);
                }
                mesh.Draw();
            }

        }
    }
}
