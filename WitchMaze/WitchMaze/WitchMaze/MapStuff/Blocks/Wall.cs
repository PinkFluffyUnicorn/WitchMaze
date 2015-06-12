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
        Model model;
        Matrix[] transforms;
   



        public Wall(Vector3 _position, Model _model)
        {
            model = _model;
            position = _position;
            transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

        }
        
        public override void draw(GameTime gameTime)
        {
            //model.Draw(Player.Player.world, Player.Player.camera, Player.Player.projection);
            /*foreach (ModelMesh mesh in model.Meshes)
            {
                // This is where the mesh orientation is set, as well 
                // as our camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = Player.Player.world;
                    effect.View = Player.Player.camera;
                    effect.Projection = Player.Player.projection;
                }
                // Draw the mesh, using the effects set above.
                mesh.Draw();*/
            //}
        }

       

    }
}
