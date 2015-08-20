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
using WitchMaze.InterfaceObjects;

namespace WitchMaze.MapStuff.Blocks
{
    abstract class Block
    {
        public Icon minimapIcon { get; protected set; }
        /// <summary>
        /// Position of a Block
        /// </summary>
        public Vector3 position { get; protected set; }

        /// <summary>
        /// Boolean whether Block is walkable(true) or not (false)
        /// </summary>
        public Boolean walkable { get; protected set; }

        /// <summary>
        /// Boolean whether Block can transport you somewhere(true) or not (false)
        /// </summary>
        public Boolean transportable { get; protected set; }

        /// <summary>
        /// Model for the Block
        /// </summary>
        public Model model { get; protected set; }

        public MapCreator.tiles name { get; protected set; }

        protected float rotation = 0;
        /// <summary>
        /// 1. Constructor for Block - Class
        /// </summary>
        public Block()
        {

        }

        /// <summary>
        /// 2. Constructor for Block - Class with a position
        /// </summary>
        /// <param name="_position"></param>
        public Block(Vector3 _position, float _rotation)
        {
            position = _position;
            rotation = _rotation; 
        }
        /// <summary>
        /// draw Class same for everx blocktype
        /// </summary>
        public void draw(Matrix projection, Matrix camera)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.View = camera;
                    effect.Projection = projection;
                    effect.World = transforms[mesh.ParentBone.Index] *Matrix.CreateRotationY((float)rotation) * Matrix.CreateTranslation(position);
                }
                mesh.Draw();
            }
            
        }
    } 
}
