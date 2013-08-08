using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shooter;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Win8ShooterGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        Player player;
        KeyboardState currentKeyboardState;
        KeyboardState previouskeyboardState;
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;
        MouseState currentMouseState;
        MouseState previousMouseState;
        float playerMoveSpeed;
        Texture2D mainBackground;
        Rectangle rectBackground;
        //float scale = 1f;
        ParalaxingBackground bgLayer1;
        ParalaxingBackground bgLayer2;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player();
            bgLayer1 = new ParalaxingBackground();

            bgLayer2 = new ParalaxingBackground();
            rectBackground = new Rectangle(0,0,GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            playerMoveSpeed = 8.0f;

           
           // enable freedrag for windows 8 movement
            
            TouchPanel.EnabledGestures = GestureType.FreeDrag;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // Load the player resources


          
            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>(@"Graphics\shipAnimation");
            playerAnimation.Initialize(playerTexture,Vector2.Zero ,115,69,8,30,Color.White,1f,true);
            bgLayer1.Initialize(Content, @"Graphics\bgLayer1", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -1);

            bgLayer2.Initialize(Content, @"Graphics\bgLayer2", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -2);



            mainBackground = Content.Load<Texture2D>(@"Graphics\mainbackground"); 
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
           
            player.Initialize(playerAnimation,playerPosition);
            
            //player.Initialize(Content.Load<Texture2D>("Graphics\\player"), playerPosition); 
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
        private void UpdatePlayer(GameTime gameTime)
        {


            player.Update(gameTime);
            //windows 8 gestures monogame
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();

                if (gesture.GestureType == GestureType.FreeDrag)
                {
                    player.Position += gesture.Delta;


                }
            }

            //mouse

            Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);


            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 posDelta = mousePosition - player.Position;
                posDelta.Normalize();
                posDelta = posDelta * playerMoveSpeed;
                player.Position = player.Position + posDelta;

            }
            //thumbstick
            player.Position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;
            player.Position.Y -= currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;
        //keybaord /dpad
            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                player.Position.X -= playerMoveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                player.Position.X += playerMoveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentGamePadState.DPad.Up == ButtonState.Pressed)
            {
                player.Position.Y -= playerMoveSpeed;


            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                player.Position.Y += playerMoveSpeed;
            }

           // player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);
           // player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height);


            player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - (player.PlayerAnimation.FrameWidth * player.Scale));

            player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - (player.PlayerAnimation.FrameHeight * player.Scale));



        
        }
        
        
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            previousGamePadState = currentGamePadState;
            previouskeyboardState = currentKeyboardState;
            previousMouseState = currentMouseState;

            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            bgLayer1.Update(gameTime);

            bgLayer2.Update(gameTime); 

            UpdatePlayer(gameTime);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Start drawing

            _spriteBatch.Begin();


            _spriteBatch.Draw(mainBackground, rectBackground, Color.White);
            bgLayer1.Draw(_spriteBatch);
            bgLayer2.Draw(_spriteBatch);


            // Draw the Player

            player.Draw(_spriteBatch);



            // Stop drawing

            _spriteBatch.End(); 
            base.Draw(gameTime);
        }
    }
}
