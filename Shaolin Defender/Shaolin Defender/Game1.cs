using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
namespace Shaolin_Defender
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameController gameController = new GameController();
        Player player = new Player();
        int i = 0;
        // Textures for background & objects
        Texture2D mCircle; // circle (main platform)
        Texture2D startPlat; // start platform
        Texture2D endPlat; // end platform
        Texture2D playerTexture;
        Texture2D coins;
        Texture2D fireStick;
        Texture2D backGround;
        
        // Vectors
        Vector2 mCirclePos;
        Vector2 startPos;
        Vector2 endPos;
        //Vector2 Player.playerPos;
        List<Vector2> coinPos= new List<Vector2>(5); // list to store the location of the coins
        List<Vector2> fireStickPos = new List<Vector2>(5);//list to store the fire stick locations


        //Temp
       

       // Vector2 coinPos;

        // Collision
        Rectangle circleRectangle;
        Rectangle startRectangle;
        Rectangle endRectangle;
       // Rectangle playerRectangle;
        Rectangle coinsRectangle;
       // Rectangle mainFrame;
        //
       
        List<Rectangle> fireStickRectangleList = new List<Rectangle>(5);
        //

        // Fonts
        private SpriteFont scoreFont;
        
        
        string countDown = "";

        // Gets width and height of the window
        private int widthWindow;
        private int heigthWindow;

        // MovementControls
        
        private float angle = 0; // angle rotation
        private float angle1 = 0; // angle for rotation of circle
        private float angle2 = 0; // rotation for wall type 1
        private float angle3 = 0; // rotation for wall type 2

        // Bool for collision detection
        bool isInside = false;
        bool isOutside = false;
        bool isHit;
        //Game over 
        bool isGameOver = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            // Object positions (x, y)


            player.playerPos = new Vector2(164, 490);// Controls the Player.playerPosition
            mCirclePos = new Vector2(665, 500);
            startPos = new Vector2(197, 500);
            endPos = new Vector2(1090, 500);
            
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
            // Winsdow Size Controls
            widthWindow = GraphicsDevice.Viewport.Width;
            heigthWindow = GraphicsDevice.Viewport.Height;

           graphics.PreferredBackBufferWidth = 1500;
           graphics.PreferredBackBufferHeight = 1010;
                
            graphics.IsFullScreen = false;           
            graphics.ApplyChanges();



          
            //Coin locations

            
            coinPos.Add(new Vector2(454,252));
            coinPos.Add(new Vector2(698, 184));
            coinPos.Add(new Vector2(968, 474));
            coinPos.Add(new Vector2(478, 780));

            // Fire Stick Locations

            fireStickPos.Add(new Vector2(518,466));
            fireStickPos.Add(new Vector2(730,304));
            fireStickPos.Add(new Vector2(812,594));
            fireStickPos.Add(new Vector2(412,632));

            
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
           
            // Loading Texture
            mCircle = this.Content.Load<Texture2D>("Platform");
            startPlat = this.Content.Load<Texture2D>("Start_Line");
            endPlat = this.Content.Load<Texture2D>("Finish_Line");
            scoreFont = Content.Load<SpriteFont>("Title");
            playerTexture = this.Content.Load<Texture2D>("Coin");
            coins = this.Content.Load<Texture2D>("coin_1");
            fireStick = this.Content.Load<Texture2D>("Fire_Ball_1");
            backGround = this.Content.Load<Texture2D>("SpikePit");
                
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
            isHit = false;
            isInside = false;
            //make is only true after testing 
           
            // Roation rates
            angle1 += .01f; // main platform
            angle2 -= .012f; // type1 firewall
            angle3 += .012f; // type2 firewall

            gameController.timer -= (float)gameTime.ElapsedGameTime.TotalSeconds; // timer
            countDown = gameController.timer.ToString("0.0");

            circleRectangle = new Rectangle((int)(mCirclePos.X), (int)(mCirclePos.Y), mCircle.Width, mCircle.Height);
            startRectangle = new Rectangle((int)(startPos.X), (int)(startPos.Y), startPlat.Width, startPlat.Height);
            endRectangle = new Rectangle((int)(endPos.X), (int)(endPos.Y), endPlat.Width, endPlat.Height);
            player.playerRectangle = new Rectangle((int)(player.playerPos.X), (int)(player.playerPos.Y), playerTexture.Width, playerTexture.Height);

            //Checking if the player hit the coins
            for (int i =0;i < coinPos.Count; i++)
            {
                coinsRectangle = new Rectangle((int)(coinPos[i].X), (int)(coinPos[i].Y), coins.Width, coins.Height);

                if (player.playerRectangle.Intersects(coinsRectangle))
                {
                    isHit = true;
                    gameController.increaseScore();
                    coinPos.RemoveAt(i);
                }
            }
            //// checking if the player hit the firestick 
            //tempx = (float)(fireStick.Width * Math.Cos(angle1) + fireStickPos[1].X);
            //tempy = (float)(fireStick.Width * Math.Sin(angle1) + fireStickPos[1].Y);
            //fireStickRectangle = new Rectangle((int)(tempx), (int)(tempy), fireStick.Width, fireStick.Height);
            for (int i = 0; i < fireStickPos.Count; i++)
            {
                //fireStickRectangle = new Rectangle((int)(fireStickPos[i].X)-60, (int)(fireStickPos[i].Y),60,60);
                ////fireStickRectangle = new Rectangle();
                if((fireStickPos[i] - player.playerPos).Length() < 65)
                {
                    isGameOver = true;
                }
            }
            if (gameController.timer <= 0)
                isGameOver = true;
            //Checking if the player is outside

            if (isInside = false && isOutside == true)
            {
                isGameOver = true;
            }
               
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //claming player movements
            // Player.playerPos.X = MathHelper.Clamp(Player.playerPos.X,0, 1190-player.Width);
            //   Player.playerPos.X = MathHelper.Clamp(0,Player.playerPos.Y, Window.ClientBounds.Height - player.Height);

            // Keymovements
                player.playerMovement();
            // player.playerMovementWithRotation();

            if ((player.playerPos - mCirclePos).Length() < 350)
            {
                isInside = true;
            }
            else
                isInside = false;
            if (isInside == true)
            {
                if (player.playerPos.X < 672)//`&& player.playerPos.Y > 490)
                {
                    player.playerRotation();


                }
            }



            //angle += 1f;

            // Resets the game!
            if (isGameOver == true)
            {
                gameController.reset();
                isGameOver = false;
                player.playerPos = new Vector2(164, 490);
                //remove the remaining coins and respwan them again
                coinPos.Clear();
                //readd all the coins again
                coinPos.Add(new Vector2(454, 252));
                coinPos.Add(new Vector2(698, 184));
                coinPos.Add(new Vector2(968, 474));
                coinPos.Add(new Vector2(478, 780));


            }
          
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
            Rectangle sourceRectangle = new Rectangle(0, 0, playerTexture.Width, playerTexture.Height);
            Vector2 originMain = new Vector2(mCircle.Width / 2, mCircle.Height / 2);
            Vector2 originStart = new Vector2(startPlat.Width / 2, startPlat.Height / 2);
            Vector2 originEnd = new Vector2(endPlat.Width / 2, endPlat.Height / 2);
            Vector2 origin = new Vector2(playerTexture.Width / 2, playerTexture.Height / 2);
            
            //Vector2 originForPeople = new Vector2();
            

            Vector2 fireStickOrigin = new Vector2(fireStick.Width / 2, fireStick.Height / 2);

            spriteBatch.Draw(mCircle, mCirclePos, null, Color.White, angle1, originMain, 3, SpriteEffects.None, 1);
            spriteBatch.Draw(startPlat, startPos, null, Color.White, 0, originStart, 1.7f, SpriteEffects.None, 1);
            spriteBatch.Draw(endPlat, endPos, null, Color.White, 0, originEnd, 1.15f, SpriteEffects.None, 1);
            spriteBatch.Draw(playerTexture, player.playerPos, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
            
            //
            for (i = 0; i < coinPos.Count; i++)
            {
                spriteBatch.Draw(coins, coinPos[i], Color.White);
            }
            for (i = 0; i < fireStickPos.Count; i++)
            {
                spriteBatch.Draw(fireStick, fireStickPos[i], null, Color.White, angle1, fireStickOrigin, 1.0f, SpriteEffects.None, 1);
                

            }
            //  spriteBatch.Draw(player, position:Player.playerPos);

            // Score & Timer

            spriteBatch.DrawString(scoreFont, "Time: " + countDown, new Vector2(35, 10), Color.Black); // timer
            spriteBatch.DrawString(scoreFont, "Score: " + gameController.getScore(), new Vector2(1270, 10), Color.Black); // score
            spriteBatch.DrawString(scoreFont, "x , y " + player.playerPos.X + " " + player.playerPos.Y, new Vector2(400, 400), Color.Black);
            ////spriteBatch.DrawString(scoreFont, "window(x , y )" + Window.ClientBounds.Width + " " + Window.ClientBounds.Height, new Vector2(500, 400), Color.Black);
            //spriteBatch.DrawString(scoreFont, "bar size (x , y )" + fireStickRectangle.X, new Vector2(500, 500), Color.Black);
            //spriteBatch.DrawString(scoreFont, "bar size (x , y )" + fireStickRectangle.Y, new Vector2(600, 600), Color.Black);
            if (isInside == true)
            {
                //spriteBatch.DrawString(scoreFont, "the player is inside the circle ", new Vector2(400, 400), Color.Black);
                //spriteBatch.Draw(player, Player.playerPos, null, Color.White, angle1, new Vector2(640, 500), 1.0f, SpriteEffects.None, 0);
               

            }
            else
            {
                //spriteBatch.Draw(player, Player.playerPos, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);

                isOutside = true;
            }
            //if (isOutside == true)
            //{
            //    spriteBatch.DrawString(scoreFont, "Collision detected", new Vector2(500,500 ), Color.Black);
            //}

            spriteBatch.End();
            //testing 
            base.Draw(gameTime);
        }
    }
}


