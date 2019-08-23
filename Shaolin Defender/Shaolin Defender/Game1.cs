using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shaolin_Defender
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        // testing 
        // backgorund and creating objects
        Texture2D player; //arrow 1
        Texture2D coins;

        Vector2 playerPos;
        //List<Vector2> coinPos = new List<Vector2>();
        Vector2 coinPos;

        //collision
        Rectangle playerRectangle;
        Rectangle coinsRectangle;

        // adding fonts
        private SpriteFont scoreFont;
        // getting the width and height of the window
        private int widthWindow;
        private int heigthWindow;
        // MovementControls
        private int speed = 5;// speed;
        private float angle = 0;
        //bool for collision detection
        bool isHit = false;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            // testing 
            // playerPos = new Vector2(700, 700);// Controls the playerPosition

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // Window Size Controls
            widthWindow = GraphicsDevice.Viewport.Width;
            heigthWindow = GraphicsDevice.Viewport.Height;
               graphics.PreferredBackBufferWidth = 1500;
               graphics.PreferredBackBufferHeight = 1010;
                
               graphics.IsFullScreen = false;
           
            graphics.ApplyChanges();
           

            coinPos = new Vector2(50, 50);
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw players.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // testing 
            //Loading Texture
            player = this.Content.Load<Texture2D>("Coin");
            coins = this.Content.Load<Texture2D>("Coin");
            scoreFont = Content.Load<SpriteFont>("Title");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
        protected override void Update(GameTime gameTime)
        {
           
            playerRectangle = new Rectangle((int)(playerPos.X), (int)(playerPos.Y), player.Width, player.Height);
            //TODO : add a for loop to go thro all the vectors of coins;
            coinsRectangle = new Rectangle((int)(coinPos.X), (int)(coinPos.Y), coins.Width, coins.Height);
            // TODO: Add your update logic here
            // testing 
            isHit = false;
            // widthWindow = GraphicsDevice.Viewport.Width;
            //   heigthWindow = GraphicsDevice.Viewport.Height;
            
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //claming player movements
            playerPos.X = MathHelper.Clamp(playerPos.X,0, 1190-player.Width);
         //   playerPos.X = MathHelper.Clamp(0,playerPos.Y, Window.ClientBounds.Height - player.Height);

            //Keymovements

            if (Keyboard.GetState().IsKeyDown(Keys.W) && playerPos.Y>0 + player.Height/2)
                playerPos.Y -= speed;
            if (Keyboard.GetState().IsKeyDown(Keys.S) && playerPos.Y < 390)
                playerPos.Y += speed;
            if (Keyboard.GetState().IsKeyDown(Keys.A) && playerPos.X>0+player.Width/2)
                playerPos.X -= speed;
            if (Keyboard.GetState().IsKeyDown(Keys.D) && playerPos.X < 390)
                playerPos.X += speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                angle -= 0.1f;
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                angle += 0.1f;
            //angle += 1f;



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
            //testing 
            spriteBatch.Begin();
            Rectangle sourceRectangle = new Rectangle(0, 0, player.Width, player.Height);
            Vector2 origin = new Vector2(player.Width / 2, player.Height / 2);
            spriteBatch.Draw(player, playerPos * 2, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
            spriteBatch.Draw(coins, coinPos, Color.White);
            //  spriteBatch.Draw(player, position:playerPos);
            //text
            //spriteBatch.DrawString(scoreFont, "rectanglearrowright2" + arrow_collision_detection_2.Right, new Vector2(150, 150), Color.Black);// width
            //spriteBatch.DrawString(scoreFont, "rectanglearrowleft2" + arrow_collision_detection_2.Left, new Vector2(200, 200), Color.Black);// height
            //spriteBatch.DrawString(scoreFont, "rectanglearrowrbottom2 " + arrow_collision_detection_2.Bottom, new Vector2(250, 250), Color.Black);// width
            //spriteBatch.DrawString(scoreFont, "rectanglearrowrTop2" + arrow_collision_detection_2.Top, new Vector2(300, 300), Color.Black);// height
            //spriteBatch.DrawString(scoreFont, " rectanglearrowright1" + arrow_collision_detection_1.Right, new Vector2(550, 250), Color.Black);
            //spriteBatch.DrawString(scoreFont, " rectanglearrowleft1" + arrow_collision_detection_1.Left, new Vector2(550, 350), Color.Black);
            //spriteBatch.DrawString(scoreFont, " rectanglearrowrbottom1" + arrow_collision_detection_1.Bottom, new Vector2(550, 450), Color.Black);
            //spriteBatch.DrawString(scoreFont, " rectanglearrowrTop1" + arrow_collision_detection_1.Top, new Vector2(550, 550), Color.Black);
            //spriteBatch.DrawString(scoreFont, "arrow2 x" + playerPos_coins.X, new Vector2(600, 300), Color.Black);//height
            spriteBatch.DrawString(scoreFont, "x , y " + playerPos.X + " " + playerPos.Y, new Vector2(400, 400), Color.Black);
            spriteBatch.DrawString(scoreFont, "window(x , y )" + Window.ClientBounds.Width + " " + Window.ClientBounds.Height, new Vector2(500, 400), Color.Black);

            if (isHit == true)
            {
                spriteBatch.DrawString(scoreFont, "Collision detected", new Vector2(700, 70), Color.Black);
            }

            spriteBatch.End();
            //testing 
            base.Draw(gameTime);
        }
    }
}

