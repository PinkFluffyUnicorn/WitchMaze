using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.PlayerStuff
{
    class Skybox
    {
        Texture2D skyboxTextures;
        Model skyboxModel;

        public Skybox(Texture2D _skyboxTextures, Model _skyboxModel)
        {
            skyboxModel = _skyboxModel;
            skyboxTextures = _skyboxTextures;
        }

        /// <summary>
        /// Methode um ka muss mal ausprobiert werden, was das tut 
        /// </summary>
        public void initialize()
        {

            foreach (ModelMesh mesh in skyboxModel.Meshes)
                foreach (BasicEffect currentEffect in mesh.Effects)
                    skyboxTextures = currentEffect.Texture;

            foreach (ModelMesh mesh in skyboxModel.Meshes)
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                    meshPart.Effect = Game1.effect.Clone();
        }


        public void draw(Matrix view, Matrix projection, Vector3 playerPosition)
        {
            SamplerState samplerState = new SamplerState();
            samplerState.AddressU = TextureAddressMode.Clamp;
            samplerState.AddressV = TextureAddressMode.Clamp;
            Game1.getGraphics().GraphicsDevice.SamplerStates[0] = samplerState;

            DepthStencilState dss = new DepthStencilState();
            dss.DepthBufferEnable = false;
            Game1.getGraphics().GraphicsDevice.DepthStencilState = dss;


            Matrix[] skyboxTransforms = new Matrix[skyboxModel.Bones.Count];
            skyboxModel.CopyAbsoluteBoneTransformsTo(skyboxTransforms);
            foreach (ModelMesh mesh in skyboxModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    Matrix worldMatrix = skyboxTransforms[mesh.ParentBone.Index] * /*Matrix.CreateScale((float)500)*/  Matrix.CreateTranslation(playerPosition);
                    //effect.CurrentTechnique = effect.Techniques["Textured"];
                    effect.TextureEnabled = true;
                    effect.Texture = skyboxTextures;
                    effect.World = worldMatrix;
                    effect.View = view;
                    effect.Projection = projection;
                    effect.Texture = skyboxTextures;

                }
                mesh.Draw();
            }
            dss = new DepthStencilState();
            dss.DepthBufferEnable = true;
            Game1.getGraphics().GraphicsDevice.DepthStencilState = dss;
        }
    }
}
