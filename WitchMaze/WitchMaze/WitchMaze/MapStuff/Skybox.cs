using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.MapStuff
{
    class Skybox
    {
        Texture2D[] skyboxTextures;
        Model skyboxModel;

        public Skybox(Texture2D[] _skyboxTextures, Model _skyboxModel )
        {
            skyboxModel = _skyboxModel;
            skyboxTextures = _skyboxTextures;
        }

        /// <summary>
        /// Methode um ka muss mal ausprobiert werden, was das tut 
        /// </summary>
        public void initialize()
        {
            int i = 0;
            foreach (ModelMesh mesh in skyboxModel.Meshes)
                foreach (BasicEffect currentEffect in mesh.Effects)
                    skyboxTextures[i++] = currentEffect.Texture;

            //foreach (ModelMesh mesh in skyboxModel.Meshes)
            //    foreach (ModelMeshPart meshPart in mesh.MeshParts)
            //        meshPart.Effect = effect.Clone();
        }


        public void draw()
        {
            //SamplerState samplerState = new SamplerState();
            //samplerState.AddressU = TextureAddressMode.Clamp;
            //samplerState.AddressV = TextureAddressMode.Clamp;
            //device.SamplerStates[0] = samplerState;

            //DepthStencilState dss = new DepthStencilState();
            //dss.DepthBufferEnable = false;
            //device.DepthStencilState = dss;

            //Matrix[] skyboxTransforms = new Matrix[skyboxModel.Bones.Count];
            //skyboxModel.CopyAbsoluteBoneTransformsTo(skyboxTransforms);
            //int i = 0;
            //foreach (ModelMesh mesh in skyboxModel.Meshes)
            //{
            //    foreach (Effect currentEffect in mesh.Effects)
            //    {
            //        Matrix worldMatrix = skyboxTransforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(xwingPosition);
            //        currentEffect.CurrentTechnique = currentEffect.Techniques["Textured"];
            //        currentEffect.Parameters["xWorld"].SetValue(worldMatrix);
            //        currentEffect.Parameters["xView"].SetValue(viewMatrix);
            //        currentEffect.Parameters["xProjection"].SetValue(projectionMatrix);
            //        currentEffect.Parameters["xTexture"].SetValue(skyboxTextures[i++]);
            //    }
            //    mesh.Draw();
            //}

            //dss = new DepthStencilState();
            //dss.DepthBufferEnable = true;
            //device.DepthStencilState = dss;
        }



    }
}
