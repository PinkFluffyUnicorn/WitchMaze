using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitchMaze.ItemStuff.Items;

namespace WitchMaze.PlayerStuff
{
    class Player
    {
        public enum EPlayerViewportPosition
        {
            fullscreen,

            left, //or up/down ist better?
            right,

            topLeft,
            topRight,
            botLeft,
            botRight,
        }

        public enum EPlayerControlls
        {
            none,

            Keyboard1,
            Keyboard2,

            Gamepad1,
            Gamepad2,
            Gamepad3,
            Gamepad4,
        }

        //for debugging purpose ->
        public enum EPlayerIndex
        {
            one,
            two,
            three,
            vour,
        }
        public EPlayerIndex eplayerIndex;
        //<- for debugging

        EPlayerControlls playerControlls;
        EPlayerViewportPosition playerViewportPosition;

        Viewport viewport;
        /// <summary>
        /// Returns the Viewport of the Player
        /// </summary>
        /// <returns>Viewport</returns>
        public Viewport getViewport(){return viewport; }
        public EPlayerViewportPosition getViewportPosition() { return playerViewportPosition; }
         
        Vector3 newPosition;
        Vector3 position;
        Vector3 lookAt;
        Vector3 upDirection;
        KeyboardState keyboard;
        float aspectRatio;
        float timeSinceLastMove;
        float timescale = 900;
        Vector3 direction; //für bewegung vorne hinten
        Vector3 ortoDirection; //für bewegung links rechts
        Model model;
        Vector2 playerMapPosition;
        List<Item> itemsCollected; 
        

        //Shader
        BasicEffect effect = Game1.getEffect();

        private static Matrix projection, camera, world ;

        /// <summary>
        /// Writes the Player Status in the Console
        /// </summary>
        private void reportStatus() {
            Console.Clear();
            Console.WriteLine("X: " + (int)position.X + " Z: " + (int)position.Z);
            //Console.WriteLine(position);
            Console.WriteLine(itemsCollected.Count);
        }

        public Matrix getProjection() { return projection; }
        public Matrix getCamera() { return camera; }
        public Matrix getWorld() { return world; }
        public Vector3 getPosition() { return this.position; }
        //public Vector2[,] getBoundingBox { return null;}
        public Model getModel() { return this.model; }

        //Write Constructors here
        //public Player()
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Creates a Player that already has the possible Controlls Set for him, he misses everything else, set that too!
        /// </summary>
        /// <param name="_playerControlls">sets the way of conrolling the Player</param>
        public Player(EPlayerControlls _playerControlls, EPlayerIndex pi){
            playerControlls = _playerControlls;
            eplayerIndex = pi;

        }

        public void setFinalPlayer(Vector3 spawnPosition, EPlayerViewportPosition viewportPosition){
            playerViewportPosition = viewportPosition;
            this.setViewport();

            itemsCollected = new List<Item>();
            keyboard = Keyboard.GetState();
            aspectRatio = Game1.getGraphics().GraphicsDevice.Viewport.AspectRatio;
            // params : position, forward,up, matrix out 

            //werte sollten später für jeden Spieler einzeln angepasst werden

            position = spawnPosition;
            //position = new Vector3(5, 1, 5);
            lookAt = new Vector3(0, 1, 1);//sollte neu berechnet werden //immer zur mitte der Map?
            upDirection = new Vector3(0, 1, 0);

            //draufsicht
            /* position = new Vector3(5, -40, 5);
             lookAt = new Vector3(0, 1, 1);
             upDirection = new Vector3(0, 0, 1);*/


            GamePadState currentState = GamePad.GetState(PlayerIndex.One); //do we need this and why? :O

            //camera = Matrix.CreateLookAt(new Vector3(position.X - Settings.getResolutionX() / 2, position.Y, position.Z), lookAt, upDirection);
            camera = Matrix.CreateWorld(position, lookAt, upDirection);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.5f, 1000.0f);
            world = Matrix.Identity;
            model = Game1.getContent().Load<Model>("Models/Player/player2");
            direction = lookAt - position;
            ortoDirection = Vector3.Cross(direction, upDirection);
            effect.LightingEnabled = true;
        }

        /// <summary>
        /// this method sets the viewport of the player screen dependent on the ViewportPosition, aswell as the projection and Camera for the player
        /// </summary>
        private void setViewport()
        {
            Viewport defaultViewport = Game1.getGraphics().GraphicsDevice.Viewport;
            switch (playerViewportPosition)
            {
                case EPlayerViewportPosition.fullscreen:
                    viewport = defaultViewport;
                    projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.5f, 1000.0f);
                    break;
                case EPlayerViewportPosition.left:
                    viewport = defaultViewport;
                    viewport.Width = viewport.Width / 2;
                    projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio / 2, 0.5f, 1000.0f);
                    break;
                case EPlayerViewportPosition.right:
                    viewport = defaultViewport;
                    viewport.Width = viewport.Width / 2;
                    viewport.X = defaultViewport.Width / 2;
                    projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio / 2, 0.5f, 1000.0f);
                    break;
                case EPlayerViewportPosition.topLeft:
                    viewport = defaultViewport;
                    viewport.Width = viewport.Width / 2;
                    viewport.Height = viewport.Height / 2;
                    projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio / 4, 0.5f, 1000.0f);
                    break;
                case EPlayerViewportPosition.botLeft:
                    viewport = defaultViewport;
                    viewport.Width = viewport.Width / 2;
                    viewport.Height = viewport.Height / 2;
                    viewport.Y = defaultViewport.Height / 2;
                    projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio / 4, 0.5f, 1000.0f);
                    break;
                case EPlayerViewportPosition.topRight:
                    viewport = defaultViewport;
                    viewport.Width = viewport.Width / 2;
                    viewport.Height = viewport.Height / 2;
                    viewport.X = defaultViewport.Width / 2;
                    projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio / 4, 0.5f, 1000.0f);
                    break;
                case EPlayerViewportPosition.botRight:
                    viewport = defaultViewport;
                    viewport.Width = viewport.Width / 2;
                    viewport.Height = viewport.Height / 2;
                    viewport.X = defaultViewport.Width / 2;
                    viewport.Y = defaultViewport.Height / 2;
                    projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio / 4, 0.5f, 1000.0f);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }


        public void update(GameTime gameTime)
        {
            //this.reportStatus();

            this.move(gameTime);

            this.itemCollision();
           
        }

        private void move(GameTime gameTime)
        {
            //ToDo kollieion dierekt hier einbauen...
            switch (playerControlls)
            {
                case EPlayerControlls.Keyboard1:
                    this.moveK(gameTime, Keys.W, Keys.S, Keys.A, Keys.D, Keys.Q, Keys.E);
                    break;
                case EPlayerControlls.Keyboard2:
                    this.moveK(gameTime, Keys.NumPad5, Keys.NumPad2, Keys.NumPad1, Keys.NumPad3, Keys.NumPad4, Keys.NumPad6);
                    break;
                case EPlayerControlls.Gamepad1:
                    moveG(gameTime, PlayerIndex.One);
                    break;
                case EPlayerControlls.Gamepad2:
                    moveG(gameTime, PlayerIndex.Two);
                    break;
                case EPlayerControlls.Gamepad3:
                    moveG(gameTime, PlayerIndex.Three);
                    break;
                case EPlayerControlls.Gamepad4:
                    moveG(gameTime, PlayerIndex.Four);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Keyboard Movement
        /// </summary>
        /// <param name="gameTime">GameTime for correct movement dependent on Time not FPS</param>
        private void moveK(GameTime gameTime, Keys moveUp, Keys moveDown, Keys moveLeft, Keys moveRight, Keys lookLeft, Keys lookRight)
        {
            timeSinceLastMove = gameTime.ElapsedGameTime.Milliseconds;
            keyboard = Keyboard.GetState();
            direction = lookAt - position;
            ortoDirection = Vector3.Cross(direction, upDirection);

            ///kack steuerung auf mehr hatte ich grad keine lust
            if (keyboard.IsKeyDown(moveUp) && !keyboard.IsKeyDown(moveDown))
            {//forward
                //position.Z = position.Z * (timeSinceLastMove / timescale);
                newPosition = position + direction * (timeSinceLastMove / timescale);
                if (!this.mapCollision(newPosition))
                {
                    position = newPosition;
                    lookAt = position + direction;
                }
            }
            if (keyboard.IsKeyDown(moveDown) && !keyboard.IsKeyDown(moveUp))
            {//backward
                newPosition = position - direction * (timeSinceLastMove / timescale);
                if (!this.mapCollision(newPosition))
                {
                    position = newPosition;
                    lookAt = position + direction;
                }
            }
            if (keyboard.IsKeyDown(moveRight) && !keyboard.IsKeyDown(moveLeft))
            {//right
                newPosition = position + ortoDirection * (timeSinceLastMove / timescale);
                if (!this.mapCollision(newPosition))
                {
                    position = newPosition;
                    lookAt = position + direction;
                }
            }
            if (keyboard.IsKeyDown(moveLeft) && !keyboard.IsKeyDown(moveRight))
            {//left
                newPosition = position - ortoDirection * (timeSinceLastMove / timescale);
                if (!this.mapCollision(newPosition))
                {
                    position = newPosition;
                    lookAt = position + direction;
                }
            }
            if (keyboard.IsKeyDown(lookLeft) && !keyboard.IsKeyDown(lookRight) /*|| keyboard.IsKeyDown(Keys.Left) && !keyboard.IsKeyDown(Keys.Right)*/)
            {//rotate left / look left
                lookAt = position + (Vector3.Transform((lookAt - position), Matrix.CreateRotationY(timeSinceLastMove /  timescale)));
            }
            if (keyboard.IsKeyDown(lookRight) && !keyboard.IsKeyDown(lookLeft) /*|| keyboard.IsKeyDown(Keys.Right) && !keyboard.IsKeyDown(Keys.Left)*/)
            {//rotate rigth / look right
                lookAt = position + (Vector3.Transform((lookAt - position), Matrix.CreateRotationY(-timeSinceLastMove /  timescale)));
            }

            //ToDo:
            //Springen
            //Hoch schaun
            //up direction verschieben...
            //if (keyboard.IsKeyDown(Keys.Up) && !keyboard.IsKeyDown(Keys.Down))
            //{
            //    upDirection = upDirection + Matrix.CreateRotationX((timeSinceLastMove/timescale));
            //}
            //Runter schaun

            //fliegen
            if (!keyboard.IsKeyDown(Keys.LeftControl) && keyboard.IsKeyDown(Keys.LeftShift))
            {
                position += new Vector3(0,timeSinceLastMove / timescale,0);
                lookAt += new Vector3(0, timeSinceLastMove / timescale, 0);
            }
            if (keyboard.IsKeyDown(Keys.LeftControl) && !keyboard.IsKeyDown(Keys.LeftShift))
            {
                position -= new Vector3(0, timeSinceLastMove / timescale, 0);
                lookAt -= new Vector3(0, timeSinceLastMove / timescale, 0);
            }

        }

        //GamePad
        private void moveG(GameTime gameTime, PlayerIndex playerIndex)
        {

                // Get the current gamepad state.
                GamePadState currentState = GamePad.GetState(playerIndex);


                if (currentState.ThumbSticks.Left.Y > 0.0f)
                {// Player one has pressed the left thumbstick up.

                    position = position + direction * (currentState.ThumbSticks.Left.Y / 20);
                    lookAt = position + direction;
                }

                if (currentState.ThumbSticks.Left.Y < 0.0f)
                {// gamepadState one has pressed the left thumbstick down.

                    position = position + direction * (currentState.ThumbSticks.Left.Y / 20);
                    lookAt = position + direction;
                }

                if (currentState.ThumbSticks.Left.X > 0.0f)
                {// Player one has pressed the left thumbstick right.

                    position = position + ortoDirection * (currentState.ThumbSticks.Left.X / 20);
                    lookAt = position + direction;
                }

                if (currentState.ThumbSticks.Left.X < 0.0f)
                {// Player one has pressed the left thumbstick left.

                    position = position + ortoDirection * (currentState.ThumbSticks.Left.X / 20);
                    lookAt = position + direction;
                }

                if (currentState.ThumbSticks.Right.X > 0.0f)
                {// Player one has pressed the right thumbstick right.

                    lookAt = position + (Vector3.Transform((direction), Matrix.CreateRotationY(-currentState.ThumbSticks.Right.X / 50)));
                }

                if (currentState.ThumbSticks.Right.X < 0.0f)
                {// Player one has pressed the right thumbstick left.

                    lookAt = position + (Vector3.Transform((direction), Matrix.CreateRotationY(-currentState.ThumbSticks.Right.X / 50)));
                }

        }

        /// <summary>
        /// handles the collision for the player by checking if the maptiles near him are walkable
        /// </summary>
        /// <returns>Returns if Player will Collide if moved to p</returns>
        public bool mapCollision(Vector3 p)
        {
            //return false; // Das hier rausnehmen um Kollision wieder drin zu haben 
            //maping Player position to MapTile position
            playerMapPosition = new Vector2(p.X , p.Z ); //prototyp, später muss genau ermittelt werden auf welchen tiles der Player genau steht
            //PlayerMapCollision
            if (WitchMaze.GameStates.InGameState.getMap().getTileWalkableAt(playerMapPosition))
                return false;
            else
                return true;
        }
        /// <summary>
        /// handles player item collision
        /// </summary>
        private void itemCollision()
        {
            if (!GameStates.InGameState.getItemMap().isEmpty((int)position.X, (int)position.Z))
            {
                itemsCollected.Add(GameStates.InGameState.getItemMap().getItem((int)position.X, (int)position.Z));
                GameStates.InGameState.getItemMap().deleteItem((int)position.X, (int)position.Z);
            }
        }



        /// <summary>
        /// dunno what it does but without it it does not work
        /// </summary>
        /// <param name="gametime"></param>
        public void doStuff()
        {
            //kann der ganze update kram in update?


        }

        /// <summary>
        /// draws the player
        /// </summary>
        public void draw()
        {
            camera = Matrix.CreateLookAt(position, lookAt, upDirection);
            effect.VertexColorEnabled = true;
            effect.View = camera;
            effect.Projection = projection;
            effect.World = world;
            effect.CurrentTechnique.Passes[0].Apply();

            //model.Draw(Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(position), camera, projection); //player model (temporary)
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect _effect in mesh.Effects)
                {
                    _effect.EnableDefaultLighting();
                    _effect.LightingEnabled = true; // Turn on the lighting subsystem.

                    _effect.DirectionalLight0.DiffuseColor = new Vector3(1f, 0.2f, 0.2f); // a reddish light
                    _effect.DirectionalLight0.Direction = new Vector3(1, 0, 0);  // coming along the x-axis
                    _effect.DirectionalLight0.SpecularColor = new Vector3(0, 1, 0); // with green highlights

                    /*effect.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f); // Add some overall ambient light.
                    effect.EmissiveColor = new Vector3(1, 0, 0); // Sets some strange emmissive lighting.  This just looks weird. */

                    _effect.World = mesh.ParentBone.Transform * Matrix.CreateTranslation(position);
                    _effect.View = camera;
                    _effect.Projection = projection;
                }

                mesh.Draw();
            }


            Game1.getEffect().World = Matrix.Identity;
            Game1.getEffect().CurrentTechnique.Passes[0].Apply();
        }

    }
}
