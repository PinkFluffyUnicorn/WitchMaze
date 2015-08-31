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

        /// <summary>
        /// Constructor for Floor
        /// </summary>
        /// <param name="_position">Position Vector3, Weltkoordinaten</param>
        /// <param name="_model">Modell</param>
        public Floor(Vector3 _position, /*Color color,*/ Model _model, float _rotation, Texture2D _texture)
        {
            model = _model;
            position = _position;
            walkable = true;
            transportable = false;
            name = MapCreator.tiles.floor;
            rotation = _rotation;
            minimapIcon = new InterfaceObjects.Icon(new Vector2(0, 0), "Textures/MiniMapTextures/minimapFloor");
            textur = _texture;
        }

        override public void draw(Matrix projection, Matrix camera)
        {

            SamplerState samplerState = new SamplerState();
            samplerState.AddressU = TextureAddressMode.Clamp;
            samplerState.AddressV = TextureAddressMode.Clamp;
            Game1.getGraphics().GraphicsDevice.SamplerStates[0] = samplerState;


            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.LightingEnabled = true;

                    effect.AmbientLightColor = new Vector3(1f, 1f, 1f);
                    //effect.EmissiveColor = new Vector3(1, 1, 1);
                    //effect.DirectionalLight0.Enabled = true;
                    effect.DirectionalLight0.Direction = new Vector3(0, 1, 0);
                    effect.DirectionalLight0.DiffuseColor = new Vector3(1, 0, 0);
                    //effect.DirectionalLight1.Direction = new Vector3(1, 1, 0);
                    //effect.DirectionalLight1.DiffuseColor = new Vector3(0, 1, 0);
                    effect.TextureEnabled = true;
                    effect.Texture = textur;
                    effect.View = camera;
                    effect.Projection = projection;
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY((float)rotation) * Matrix.CreateScale((float)0.5) * Matrix.CreateTranslation(position);
                }
                mesh.Draw();
            }
        }
    }
}
