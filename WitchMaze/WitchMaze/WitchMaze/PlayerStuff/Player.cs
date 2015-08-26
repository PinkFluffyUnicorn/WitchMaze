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
using WitchMaze.InterfaceObjects;

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
            Keyboard3,
            KeyboardNumPad,

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

        //Player Variablen
        EPlayerControlls playerControlls;

        //Zeichnen
        EPlayerViewportPosition playerViewportPosition;
        Viewport viewport;
        float timeSinceLastMove;
        float timescale = 900;
        Vector3 direction; //für bewegung vorne hinten
        Vector3 ortoDirection; //für bewegung links rechts
        Vector2 playerMapPosition;
        Vector3 newPosition;
        Vector3 position;
        Vector3 lookAt;
        Vector3 upDirection;
        KeyboardState keyboard;
        float aspectRatio;
        

        //Move
        //Move movePlayer;
        //Keys up;
        //Keys down;
        //Keys left;
        //Keys right;
        //Keys lookLeft;
        //Keys kookRight;

        //Shader
        BasicEffect effect = Game1.getEffect();
        private static Matrix projection, camera, world;
        
        //to Draw
        Model model;
        Skybox skybox;
        private Matrix scale = Matrix.CreateScale((float)0.002);

        //other
        List<Item> itemsCollected;
        public Icon playerIcon { get; protected set; }
        

        //get methods
        public Viewport getViewport() { return viewport; }
        public EPlayerViewportPosition getViewportPosition() { return playerViewportPosition; }
        public Skybox getSkybox(){return skybox;}
        public Matrix getProjection() { return projection; }
        public Matrix getCamera() { return camera; }
        public Matrix getWorld() { return world; }
        public Vector3 getPosition() { return this.position; }
        //public Vector2[,] getBoundingBox { return null;}
        public Model getModel() { return this.model; }


        //set
        /// <summary>
        /// Creates a Player that already has the possible Controlls Set for him, he misses everything else, set that too!
        /// </summary>
        /// <param name="_playerControlls">sets the way of conrolling the Player</param>
        public Player(EPlayerControlls _playerControlls, EPlayerIndex pi)
        {
            playerControlls = _playerControlls;
            eplayerIndex = pi;

        }
        public void setFinalPlayer(Vector3 spawnPosition, EPlayerViewportPosition viewportPosition){
            playerViewportPosition = viewportPosition;
            this.setViewport();

            itemsCollected = new List<Item>();
            keyboard = Keyboard.GetState();
            aspectRatio = viewport.AspectRatio;
            // params : position, forward,up, matrix out 

            //werte sollten später für jeden Spieler einzeln angepasst werden

            playerIcon = new Icon(new Vector2(0, 0), "Textures/playerIcon");

            position = spawnPosition;
            //position = new Vector3(5, 1, 5);
            lookAt = new Vector3(0, (float)0.2, 1);//sollte neu berechnet werden //immer zur mitte der Map?
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
            

            skybox = new Skybox(Game1.getContent().Load<Texture2D>("Models/SkyboxTexture"), Game1.getContent().Load<Model>("cube"));

            skybox.initialize();
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

        //update
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
                    this.moveK(gameTime, Keys.T, Keys.F, Keys.G, Keys.H, Keys.R, Keys.Z);
                    break;
                case EPlayerControlls.Keyboard3:
                    this.moveK(gameTime, Keys.I, Keys.J, Keys.K, Keys.L, Keys.U, Keys.O);
                    break;
                case EPlayerControlls.KeyboardNumPad:
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

                    lookAt = position + (Vector3.Transform((direction), Matrix.CreateRotationY(-currentState.ThumbSticks.Right.X )));
                }

                if (currentState.ThumbSticks.Right.X < 0.0f)
                {// Player one has pressed the right thumbstick left.

                    lookAt = position + (Vector3.Transform((direction), Matrix.CreateRotationY(-currentState.ThumbSticks.Right.X )));
                }

        }

        /// <summary>
        /// handles the collision for the player by checking if the maptiles near him are walkable
        /// </summary>
        /// <param name="p">the position to check</param>
        /// <returns>Returns if Player will Collide if moved to p</returns>
        public bool mapCollision(Vector3 p)
        {//Problem: mittelpunkt in der mitte... daher zurückrechnen...
            playerMapPosition = new Vector2((int)Math.Round(p.X), (int)Math.Round(p.Z));
            Console.WriteLine(playerMapPosition);
            Console.WriteLine(position +"position");
            ////"bonding box" player
            float radius = 0.5f;
            

            Vector2 toCheck; //the next tile to ckeck
            //Vector2 topLeft; //the top left point of the tie to check

            //top left tile
            toCheck = new Vector2(playerMapPosition.X - 1, playerMapPosition.Y + 1);
            if (checkBlockAt(toCheck, p))
                if (!WitchMaze.GameStates.InGameState.getMap().getTileWalkableAt(toCheck))
                    return true;

            //top middle tile
            toCheck = new Vector2(playerMapPosition.X, playerMapPosition.Y + 1);
            if(checkBlockAt(toCheck, p))
                if (!WitchMaze.GameStates.InGameState.getMap().getTileWalkableAt(toCheck))
                    return true;

            //top right tile
            toCheck = new Vector2(playerMapPosition.X + 1, playerMapPosition.Y + 1);
            if (checkBlockAt(toCheck, p))
                if (!WitchMaze.GameStates.InGameState.getMap().getTileWalkableAt(toCheck))
                    return true;

            //middel left tile
            toCheck = new Vector2(playerMapPosition.X - 1, playerMapPosition.Y);
            if (checkBlockAt(toCheck, p))
                if (!WitchMaze.GameStates.InGameState.getMap().getTileWalkableAt(toCheck))
                    return true;

            //middle middle tile //Tile i am standing on
            if (!WitchMaze.GameStates.InGameState.getMap().getTileWalkableAt(playerMapPosition))
                return true;
            //else
            //    return true;
            //toCheck = new Vector2(playerMapPosition.X, playerMapPosition.Y);
            //topLeft = new Vector2(GameStates.InGameState.getMap().getBlockAt((int)(toCheck.X - Settings.getBlockSizeX() / 2), (int)(toCheck.Y + Settings.getBlockSizeZ() / 2)).position.X,
            //                        GameStates.InGameState.getMap().getBlockAt((int)(toCheck.X - Settings.getBlockSizeX() / 2), (int)(toCheck.Y + Settings.getBlockSizeZ() / 2)).position.Z);
            //if (circleRectangleCollision(toCheck, radius, topLeft, Settings.getBlockSizeX(), Settings.getBlockSizeZ()))
            //    return !WitchMaze.GameStates.InGameState.getMap().getTileWalkableAt(playerMapPosition);


            //middle right tile
            toCheck = new Vector2(playerMapPosition.X + 1, playerMapPosition.Y);
            if (checkBlockAt(toCheck, p))
                if (!WitchMaze.GameStates.InGameState.getMap().getTileWalkableAt(toCheck))
                    return true;

            //bot left tile
            toCheck = new Vector2(playerMapPosition.X - 1, playerMapPosition.Y - 1);
            if (checkBlockAt(toCheck, p))
                if (!WitchMaze.GameStates.InGameState.getMap().getTileWalkableAt(toCheck))
                    return true;

            //bot middle tile
            toCheck = new Vector2(playerMapPosition.X, playerMapPosition.Y - 1);
            if (checkBlockAt(toCheck, p))
                if (!WitchMaze.GameStates.InGameState.getMap().getTileWalkableAt(toCheck))
                    return true;

            //bot right tile
            toCheck = new Vector2(playerMapPosition.X + 1, playerMapPosition.Y - 1);
            if (checkBlockAt(toCheck, p))
                if (!WitchMaze.GameStates.InGameState.getMap().getTileWalkableAt(toCheck))
                    return true;

            return false;
        }

        /// <summary>
        /// returns if the block is colliding
        /// </summary>
        /// <param name="toCheck"></param>
        /// <returns></returns>
        private bool checkBlockAt(Vector2 toCheck, Vector3 p)
        {
            float radius = 0.35f;
            Vector2 topLeft = new Vector2(GameStates.InGameState.getMap().getBlockAt((int)toCheck.X, (int)toCheck.Y).position.X - Settings.getBlockSizeX() / 2,
                        GameStates.InGameState.getMap().getBlockAt((int)toCheck.X, (int)toCheck.Y).position.Z + Settings.getBlockSizeZ() / 2);

            if (circleRectangleCollision(new Vector2(p.X, p.Z), radius, topLeft, Settings.getBlockSizeX(), Settings.getBlockSizeZ()))
                if (!WitchMaze.GameStates.InGameState.getMap().getTileWalkableAt(toCheck))
                    return true;
            return false;
        }

        /// <summary>
        /// returns if a circle is colliding with a rectangle
        /// </summary>
        /// <param name="circleCenter">the center point of the circle</param>
        /// <param name="r">radius of the circle</param>
        /// <param name="topLeftRectangle">the top left point of the rectangle</param>
        /// <param name="w">the width of the rectangle</param>
        /// <param name="h">the height of the rectangle</param>
        /// <returns></returns>
        private bool circleRectangleCollision(Vector2 circleCenter, float r, Vector2 topLeftRectangle, float w, float h)
        {
            ////Fall 1: Kreismittelpunkt liegt im rechteck //fall passiert in unserem beispiel quasi immer...
            //if (circleCenter.X < topLeftRectangle.X //linker rand
            //    && circleCenter.X > topLeftRectangle.X + w //rechter rand
            //    && circleCenter.Y > topLeftRectangle.Y //unterer rand
            //    && circleCenter.Y < topLeftRectangle.Y + h)//oberer rand
            //    return true;

            //Fall2: ein außenpunkt des rechteckes liegt im kreis
            Vector2 tl = topLeftRectangle;
            Vector2 tr = new Vector2(tl.X + w, tl.Y);
            Vector2 bl = new Vector2(tl.X, tl.Y - h);
            Vector2 br = new Vector2(tl.X + w, tl.Y - h);
            float bTL = (float)Math.Sqrt((circleCenter.X - tl.X) * (circleCenter.X - tl.X) + (circleCenter.Y - tl.Y) * (circleCenter.Y - tl.Y));
            float bTR = (float)Math.Sqrt((circleCenter.X - tr.X) * (circleCenter.X - tr.X) + (circleCenter.Y - tr.Y) * (circleCenter.Y - tr.Y));
            float bBL = (float)Math.Sqrt((circleCenter.X - bl.X) * (circleCenter.X - bl.X) + (circleCenter.Y - bl.Y) * (circleCenter.Y - bl.Y));
            float bBR = (float)Math.Sqrt((circleCenter.X - br.X) * (circleCenter.X - br.X) + (circleCenter.Y - br.Y) * (circleCenter.Y - br.Y));
            if (bTL < r || bTR < r || bBL < r || bBR < r)
                return true;

            //Fall 3: kreis überschneidet eine kante des rechtecks, ohne einen eckpunkt zu treffen
            //obere kante
            //untere kante
            if (circleCenter.X > topLeftRectangle.X && circleCenter.X < topLeftRectangle.X + w)//prüfen ob es im richtigen x bereich liegt
            {
                //oben
                float dy = Math.Abs(topLeftRectangle.Y - circleCenter.Y);
                if (dy < r)
                    return true;
                //unten
                dy = Math.Abs(topLeftRectangle.Y - h - circleCenter.Y);
                if (dy < r)
                    return true;
            }
            //rechte kante
            //linke kante
            if (circleCenter.Y < topLeftRectangle.Y && circleCenter.Y > topLeftRectangle.Y - h)//prüfen ob es im richtigen x bereich liegt
            {
                //links
                float dx = Math.Abs(topLeftRectangle.X - circleCenter.X);
                Console.WriteLine((dx < r) + "left");
                if (dx < r)
                    return true;
                //rechts
                dx = Math.Abs(topLeftRectangle.X + w - circleCenter.X);//- oder +, das st hier die frage

                //Console.WriteLine(topLeftRectangle.X + ", " + circleCenter.X);
                //Console.WriteLine(dx);
                Console.WriteLine((dx < r) + "right");
                if (dx < r)
                    return true;
            }

            return false;
        }


        /// <summary>
        /// handles player item collision
        /// </summary>
        private void itemCollision()
        {//umrechnen da mittelpunkt in der mitte, führt bein castern zu int zu fehlern
            if (!GameStates.InGameState.getItemMap().isEmpty((int)(position.X - Settings.getBlockSizeX() / 2), (int)(position.Z - Settings.getBlockSizeZ() / 2)))
            {
                itemsCollected.Add(GameStates.InGameState.getItemMap().getItem((int)position.X, (int)position.Z));
                GameStates.InGameState.getItemMap().deleteItem((int)position.X, (int)position.Z);
            }
        }


        public void updateCamera()
        {
            camera = Matrix.CreateLookAt(position, lookAt, upDirection);
        }

        /// <summary>
        /// draws the player
        /// </summary>
        public void draw()
        {
            

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

                    _effect.World = mesh.ParentBone.Transform * scale * Matrix.CreateTranslation(position);
                    _effect.View = camera;
                    _effect.Projection = projection;
                }
                
                mesh.Draw();
            }
        }

        //public void drawInterface()
        //{
        //    foreach(InterfaceObject i in playerInterface)
        //    {
        //        i.draw();
        //    }
        //}

    }
}
