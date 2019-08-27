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
        int i = 0;
        // Textures for background & objects
        Texture2D mCircle; // circle (main platform)
        Texture2D startPlat; // start platform
        Texture2D endPlat; // end platform
        Texture2D player;
        Texture2D coins;
        Texture2D fireStick;
        Texture2D backGround;
        
        // Vectors
        Vector2 mCirclePos;
        Vector2 startPos;
        Vector2 endPos;
        Vector2 playerPos;
        List<Vector2> coinPos= new List<Vector2>(5); // list to store the location of the coins
        List<Vector2> fireStickPos = new List<Vector2>(5);//list to store the fire stick locations


        //Temp
       

       // Vector2 coinPos;

        // Collision
        Rectangle circleRectangle;
        Rectangle startRectangle;
        Rectangle endRectangle;
        Rectangle playerRectangle;
        Rectangle coinsRectangle;
        Rectangle mainFrame;
        //
       
        List<Rectangle> fireStickRectangleList = new List<Rectangle>(5);
    
        // Fonts
        private SpriteFont scoreFont;
        
        string countDown = "";

        // Gets width and height of the window
        private int widthWindow;
        private int heigthWindow;

        // MovementControls
        private int speed = 2;// speed;
        private float angle = 0; // angle rotation
        private float angle1 = 0; // angle for rotation of circle
        private float angle2 = 0; // rotation for wall type 1
        private float angle3 = 0; // rotation for wall type 2

        bool allCoins = false;

        // Bool for collision detection
        bool isInside = false; // main circle
        bool isOutside = false;
        bool isInsideS = false; // start platform
        bool isInsideE = false; // end platform
        bool isHit = false;
        //Game over 
        bool isGameOver = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            // Object positions (x, y)

            //mCirclePos = new Vector2(765, 500);
            //startPos = new Vector2(195, 500);
            //endPos = new Vector2(1320, 500);

            playerPos = new Vector2(164, 490); // Controls the playerPosition
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
            player = this.Content.Load<Texture2D>("Coin");
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
            isInsideS = false;
            isInsideE = false;
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
            playerRectangle = new Rectangle((int)(playerPos.X), (int)(playerPos.Y), player.Width, player.Height);

            //Checking if the player hit the coins
            for (int i = 0; i < coinPos.Count; i++)
            {
                coinsRectangle = new Rectangle((int)(coinPos[i].X), (int)(coinPos[i].Y), coins.Width, coins.Height);

                if (playerRectangle.Intersects(coinsRectangle))
                {
                    isHit = true;
                    gameController.increaseScore();

                    // Check if all coins are collected
                    if (gameController.getScore() == 4) // # of coins
                        allCoins = true;

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
                if((fireStickPos[i] - playerPos).Length() < 65)
                {
                    isGameOver = true;
                }
            }
            if (gameController.timer <= 0)
                isGameOver = true;

            // Checking if the player is outside all platforms
            if (isInside == false && isOutside == true && isInsideE == false && isInsideS == false)
            {
                isGameOver = true;
            }
               
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //claming player movements
            // playerPos.X = MathHelper.Clamp(playerPos.X,0, 1190-player.Width);
            // playerPos.X = MathHelper.Clamp(0,playerPos.Y, Window.ClientBounds.Height - player.Height);

          //  startPos.X = startPos.X * (float)Math.Cos(angle1) - startPos.Y * (float)Math.Sin(angle1);
           // startPos.Y = startPos.X * (float)Math.Sin(angle1) + startPos.Y * (float)Math.Cos(angle1);

            // Keymovements
            if (Keyboard.GetState().IsKeyDown(Keys.W) )//&& playerPos.Y>0 + player.Height/2)
                playerPos.Y -= speed;
            if (Keyboard.GetState().IsKeyDown(Keys.S) )//&& playerPos.Y < 390)
                playerPos.Y += speed;
            if (Keyboard.GetState().IsKeyDown(Keys.A)) //&& playerPos.X>0+player.Width/2)
                playerPos.X -= speed;
            if (Keyboard.GetState().IsKeyDown(Keys.D))// && playerPos.X < 390)
                playerPos.X += speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                angle -= 0.1f;
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                angle += 0.1f;
            // angle += 1f;

            // Resets the game!
            if (isGameOver == true)
            {
                gameController.reset();
                isGameOver = false;
                playerPos = new Vector2(164, 490);
                //remove the remaining coins and respwan them again
                coinPos.Clear();
                //re-add all the coins again
                coinPos.Add(new Vector2(454, 252));
                coinPos.Add(new Vector2(698, 184));
                coinPos.Add(new Vector2(968, 474));
                coinPos.Add(new Vector2(478, 780));
            }
            
            // Is player on circle platform?
            if ((playerPos- mCirclePos).Length() < 350)
            {
                isInside = true;
            }
            // Is player on starting platform?
            else if ((playerPos - startPos).Length() < 350)
            {
                isInsideS = true;
            }
            // Is player on ending platform?
            else if ((playerPos - endPos).Length() < 350)
            {
                isInsideE = true;
            }
            else
            {
                //restart
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
            Rectangle sourceRectangle = new Rectangle(0, 0, player.Width, player.Height);
            Vector2 originMain = new Vector2(mCircle.Width / 2, mCircle.Height / 2);
            Vector2 originStart = new Vector2(startPlat.Width / 2, startPlat.Height / 2);
            Vector2 originEnd = new Vector2(endPlat.Width / 2, endPlat.Height / 2);
            Vector2 origin = new Vector2(player.Width / 2, player.Height / 2);
            
            //Vector2 originForPeople = new Vector2();
            

            Vector2 fireStickOrigin = new Vector2(fireStick.Width / 2, fireStick.Height / 2);

            spriteBatch.Draw(mCircle, mCirclePos, null, Color.White, angle1, originMain, 3, SpriteEffects.None, 1);
            spriteBatch.Draw(startPlat, startPos, null, Color.White, 0, originStart, 1.7f, SpriteEffects.None, 1);
            spriteBatch.Draw(endPlat, endPos, null, Color.White, 0, originEnd, 1.15f, SpriteEffects.None, 1);
            spriteBatch.Draw(player, playerPos, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
            
            for (i = 0; i < coinPos.Count; i++)
            {
                spriteBatch.Draw(coins, coinPos[i], Color.White);
            }
            for (i = 0; i < fireStickPos.Count; i++)
            {
                spriteBatch.Draw(fireStick, fireStickPos[i], null, Color.White, angle1, fireStickOrigin, 1.0f, SpriteEffects.None, 1);
                

            }
            //  spriteBatch.Draw(player, position:playerPos);

            // Score & Timer

            spriteBatch.DrawString(scoreFont, "Time: " + countDown, new Vector2(35, 10), Color.Black); // timer
            spriteBatch.DrawString(scoreFont, "Score: " + gameController.getScore() + "/4", new Vector2(1250, 10), Color.Black); // score
            //spriteBatch.DrawString(scoreFont, "x , y " + playerPos.X + " " + playerPos.Y, new Vector2(400, 400), Color.Black);
            ////spriteBatch.DrawString(scoreFont, "window(x , y )" + Window.ClientBounds.Width + " " + Window.ClientBounds.Height, new Vector2(500, 400), Color.Black);
            //spriteBatch.DrawString(scoreFont, "bar size (x , y )" + fireStickRectangle.X, new Vector2(500, 500), Color.Black);
            //spriteBatch.DrawString(scoreFont, "bar size (x , y )" + fireStickRectangle.Y, new Vector2(600, 600), Color.Black);
            if (isInside == true)
            {
                spriteBatch.DrawString(scoreFont, "the player is inside the circle ", new Vector2(400, 400), Color.Black);
                //spriteBatch.Draw(player, playerPos, null, Color.White, angle1, new Vector2(640, 500), 1.0f, SpriteEffects.None, 0);
            }
            else if (isInsideS == true)
            {
                spriteBatch.DrawString(scoreFont, "the player is inside the start plat ", new Vector2(400, 400), Color.Black);
                //spriteBatch.Draw(player, playerPos, null, Color.White, angle1, new Vector2(640, 500), 1.0f, SpriteEffects.None, 0);
            }
            else if (isInsideE == true)
            {
                spriteBatch.DrawString(scoreFont, "the player is inside the end plat ", new Vector2(400, 400), Color.Black);
                //spriteBatch.Draw(player, playerPos, null, Color.White, angle1, new Vector2(640, 500), 1.0f, SpriteEffects.None, 0);
            }
            else
            {
                //spriteBatch.Draw(player, playerPos, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);

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


