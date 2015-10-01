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
    class Wall : Block
    {

        /// <summary>
        /// Constructor for Wall - Object
        /// </summary>
        /// <param name="_model">Modell</param>
        /// <param name="_position">Position Vector3, Weltkoordinaten</param>
        public Wall(Model _model, Vector3 _position, float _rotation, Texture2D _texture )
        {
            model = _model;
            position = _position;
            walkable = false;
            transportable = false;
            rotation = _rotation;
            name = MapCreator.tiles.wall;
            minimapIcon = new InterfaceObjects.Icon(new Vector2(0, 0), "Textures/MiniMapTextures/minimapWall");
            textur = _texture;
        }

        override public void draw(Matrix projection, Matrix camera)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {

                    //effect.EnableDefaultLighting();
                    effect.LightingEnabled = true;

                    effect.TextureEnabled = true;
                    effect.Texture = textur;

                    effect.AmbientLightColor = ambientColor;
                    effect.EmissiveColor = emissiveColor;

                    effect.DirectionalLight0.Enabled = true;
                    effect.DirectionalLight0.Direction = light0Direction;
                    effect.DirectionalLight0.DiffuseColor = light0Color;
                    
                    effect.View = camera;
                    effect.Projection = projection;
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY((float)rotation) * Matrix.CreateScale((float)0.5) * Matrix.CreateTranslation(position);
                }
                mesh.Draw();
            }

        }
    }
}
