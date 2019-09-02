using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

// Music by Kevin MacLeod: https://incompetech.filmmusic.io/song/5027-rising-tide/

namespace Shaolin_Defender
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Song backgroundMusic;
        GameController gameController = new GameController();
        Player player = new Player();
        int i = 0;

        // Textures for background & objects
        Texture2D background; // spike background
        Texture2D bgPlat; // non moving platform
        Texture2D mCircle; // circle (main platform)
        Texture2D mCircle_2; // circle(2nd platform)
        Texture2D mCircle_3; // cirlce (3rd platform)
        Texture2D playerTexture;
        Texture2D coins;
        Texture2D fireStick;
        Texture2D blur;

        // Vectors
        Vector2 backgroundPos1, backgroundPos2, backgroundPos3, 
            backgroundPos4, backgroundPos5, backgroundPos6, 
            backgroundPos7, backgroundPos8, backgroundPos9,
            backgroundPos10, backgroundPos11, backgroundPos12,
            backgroundPos13, backgroundPos14, backgroundPos15;
        Vector2 bgPlatPos;
        Vector2 mCirclePos;
        Vector2 mCirclePos_2;
        Vector2 mCirclePos_3;
        Vector2 blurPos;
        List<Vector2> coinPos = new List<Vector2>(5); // List to store the location of the coins
        List<Vector2> fireStickPos = new List<Vector2>(12); // List to store the fire stick locations

        // Collision
        Rectangle circleRectangle;
        Rectangle coinsRectangle;

        // Safe zones
        Rectangle safeZone;
        Rectangle winZone;
        Rectangle safeZoneStart;
        Rectangle safeZoneCirlce1To2;
        //delete
        Texture2D whiteRectangle;
        List<Rectangle> fireStickRectangleList = new List<Rectangle>(5);

        // Fonts
        private SpriteFont scoreFont;
        private SpriteFont gameOverFont;
        
        string countDown = "";

        // Gets width and height of the window
        private int widthWindow;
        private int heigthWindow;
        
        // Radius
        float radius;

        // MovementControls
        private float angle = 0; // angle rotation
        private float angle1 = 0; // angle for rotation of circle
        private float angle2 = 0; // rotation for wall type 1
        private float angle3 = 0; // rotation for wall type 2

        // Bool for collision detection
        bool isInside = false;
        bool isOutside = false;
        bool isHit;

        // GameOver & States
        bool isGameOver = false;
        bool isCoinDone = false;
        bool isWinState = false;
        bool isTimerUp = false;

        // Pause 
        bool isPause = false;

        //test
        bool test = false;
        bool isChange = false;

        //FireBall movement Controls;
        int countTimer = 0;
        int countTimerMax = 5;
       


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            // Object positions (x, y)
            backgroundPos1 = new Vector2(-1024, 1024);
            backgroundPos2 = new Vector2(-1024, 0);
            backgroundPos3 = new Vector2(-1024, -1024);
            backgroundPos4 = new Vector2(0, -1024);
            backgroundPos5 = new Vector2(0, 0);
            backgroundPos6 = new Vector2(0, 1024);
            backgroundPos7 = new Vector2(1024, 1024);
            backgroundPos8 = new Vector2(1024, 0);
            backgroundPos9 = new Vector2(1024, -1024);
            backgroundPos10 = new Vector2(2048, 1024);
            backgroundPos11 = new Vector2(2048, 0);
            backgroundPos12 = new Vector2(2048, -1024);
            backgroundPos13 = new Vector2(3072, 1024);
            backgroundPos14 = new Vector2(3072, 0);
            backgroundPos15 = new Vector2(3072, -1024);

            bgPlatPos = new Vector2(255, 160);
            player.playerPos = new Vector2(154, 465); // Controls the Player.playerPosition
            mCirclePos = new Vector2(750, 500);
            mCirclePos_2 = new Vector2(1600,500);
            mCirclePos_3 = new Vector2(2430,500);
            blurPos = new Vector2(0, 0);
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

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;

            graphics.IsFullScreen = false ;           
            graphics.ApplyChanges();

            // Coin locations & Adding
            coinPos.Add(new Vector2(554, 252));
            coinPos.Add(new Vector2(798, 184));
            coinPos.Add(new Vector2(1068, 474));
            coinPos.Add(new Vector2(578, 780));
            coinPos.Add(new Vector2(800, 500));

            // Fire Locations & Adding
            fireStickPos.Add(new Vector2(558, 500));
            fireStickPos.Add(new Vector2(470, 500));
            fireStickPos.Add(new Vector2(830, 500));
            //fireStickPos.Add(new Vector2(800, 370));
            //fireStickPos.Add(new Vector2(770, 440));
            //fireStickPos.Add(new Vector2(412, 632));
            //fireStickPos.Add(new Vector2(652, 200));
            //fireStickPos.Add(new Vector2(672, 700));
            //fireStickPos.Add(new Vector2(752, 700));
            //fireStickPos.Add(new Vector2(832, 700));
            //fireStickPos.Add(new Vector2(740, 510));
            //fireStickPos.Add(new Vector2(940, 480));
            //fireStickPos.Add(new Vector2(1070, 370));
            //fireStickPos.Add(new Vector2(1070, 610));

            // Win and safe zone 
            safeZone = new Rectangle(1150, 315, 400, 400);
            winZone = new Rectangle(1288, 300, 400, 600);
            safeZoneStart = new Rectangle(10, 230, 330, 400);
            safeZoneCirlce1To2 = new Rectangle(1120,315,100,400);
            
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
            background = this.Content.Load<Texture2D>("SpikePit");
            bgPlat = this.Content.Load<Texture2D>("background"); 
            mCircle = this.Content.Load<Texture2D>("Platform1");
            mCircle_2 = this.Content.Load< Texture2D>("Platform1");
            mCircle_3 = this.Content.Load<Texture2D>("Platform1");
            scoreFont = Content.Load<SpriteFont>("Title");
            gameOverFont = Content.Load<SpriteFont>("GameOver");
            //playerTexture = this.Content.Load<Texture2D>("Cowboy_man");
            playerTexture = this.Content.Load<Texture2D>("CowBoyFrame");
            coins = this.Content.Load<Texture2D>("coin_1");
            fireStick = this.Content.Load<Texture2D>("fireball");
            blur = this.Content.Load<Texture2D>("blur");
            
            // Background Music
            backgroundMusic = Content.Load<Song>("Rising_Tide");
            MediaPlayer.Play(backgroundMusic);
            //test
            whiteRectangle = new Texture2D(GraphicsDevice,1,1);
            whiteRectangle.SetData(new[] { Color.White});

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

            
            // Roation rates
            angle1 += .01f; // main platform
            angle2 -= .012f; // type1 firewall
            angle3 += .042f; // type2 firewall
            
            circleRectangle = new Rectangle((int)(mCirclePos.X), (int)(mCirclePos.Y), mCircle.Width, mCircle.Height);
            player.playerRectangle = new Rectangle((int)(player.playerPos.X), (int)(player.playerPos.Y), playerTexture.Width, playerTexture.Height);

            //Checking if the player hit the coins
            for (int i = 0; i < coinPos.Count; i++)
            {
                coinsRectangle = new Rectangle((int)(coinPos[i].X), (int)(coinPos[i].Y), coins.Width, coins.Height);

                if (player.playerRectangle.Intersects(coinsRectangle))
                {
                    isHit = true;
                    gameController.increaseScore();
                    coinPos.RemoveAt(i);
                }
            }
            // player movement***********************************************  
            if (gameController.timer % 8 < 4)
                isChange = true;
            else
                isChange = false;
            for (int i = 0; i < fireStickPos.Count; i++)
            {
                //fireStickRectangle = new Rectangle((int)(fireStickPos[i].X)-60, (int)(fireStickPos[i].Y),60,60);
                ////fireStickRectangle = new Rectangle();

                if (isChange == false)
                {
                    fireStickPos[i] = new Vector2(fireStickPos[i].X, fireStickPos[i].Y + 1f);
                    

                }
                else
                {
                    fireStickPos[i] = new Vector2(fireStickPos[i].X, fireStickPos[i].Y - 1f);
                }

                if ((fireStickPos[i] - player.playerPos).Length() < 65)
                {
                    isGameOver = true;
                }
                
            }
               
            if(fireStickPos[1].Y > 10)
            {
                
                  //  fireStickPos[1] = new Vector2((int)(fireStickPos[i].X) - 1, fireStickPos[i].Y);
                    
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // Keymovements
            if (isWinState == false && isPause == false)
            {
                player.playerMovement();
                gameController.timer -= (float)gameTime.ElapsedGameTime.TotalSeconds; // timer
                countDown = gameController.timer.ToString("0.0");
            }

            if ((player.playerPos - mCirclePos).Length() < 420 || (player.playerPos - mCirclePos_2).Length() < 420 || (player.playerPos - mCirclePos_3).Length() < 420)
            {
                isInside = true;
                test = true;
            }

           

            // Controlls player Rotation inside the circle  
            if (isInside == true)
            {
                Vector2 dir = new Vector2();
              
                // get the circle
                if ((player.playerPos - mCirclePos).Length() < 420)
                {
                    dir = mCirclePos - player.playerPos ;
                }
                if ((player.playerPos - mCirclePos_2).Length() < 420)
                {
                    dir = mCirclePos_2 - player.playerPos;
                    dir *= -1;
                }
                if ((player.playerPos - mCirclePos_3).Length() < 420)
                {
                    dir = mCirclePos_3 - player.playerPos;
                }

                player.playerRotation(dir);
            }

            // Fireball vertical movement
            //fireStickPos. += speed;
            //fireStickPos[1].Y += 

            //angle += 1f;

            // Resets the game!
            if (isGameOver == true)
            {
                gameController.reset();
                test = false;
                isGameOver = false;
                player.playerPos = new Vector2(164, 490);

                // Remove the remaining coins and respwan them again
                coinPos.Clear();

                // Re-add all the coins
                coinPos.Add(new Vector2(554, 252));
                coinPos.Add(new Vector2(798, 184));
                coinPos.Add(new Vector2(1068, 474));
                coinPos.Add(new Vector2(578, 780));
                coinPos.Add(new Vector2(800, 500));
            }

            //// Game over state
            if (test == true && isInside == false)
            {

                if (player.playerRectangle.Intersects(safeZoneCirlce1To2) != true)
                    isGameOver = true;
            
                
               
            }

            // Checking if all the coins are collected!!
            if (gameController.checkWinState() == 1)
            {
                isCoinDone = true;
            }
            else
            {
                isCoinDone = false;
            }
            
            if (isCoinDone == true && player.playerRectangle.Intersects(winZone) == true)
            {
                isWinState = true;
                isCoinDone = false;
                
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    gameController.resetTimer();
                    isGameOver = true;
                    isWinState = false;
                    isPause = false;
                    isTimerUp = false;
                }
            }

            if (gameController.timer <= 0)
            {
                isTimerUp = true;
                isPause = true;
                isCoinDone = false;
                player.playerPos = new Vector2(154, 465);
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                     gameController.resetTimer();
                     isGameOver = true;
                     isWinState = false;
                     isPause = false;
                     isTimerUp = false;
                }
            }

            if (test == false)
            {
                player.playerPos.Y = MathHelper.Clamp(player.playerPos.Y, safeZoneStart.Top, safeZoneStart.Bottom - 50);
            }
  
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.OrangeRed);

            // TODO: Add your drawing code here

            // This controls the camera
            spriteBatch.Begin(SpriteSortMode.Texture, null, null, null, null, null, Matrix.CreateTranslation(
                (graphics.PreferredBackBufferWidth/10 - player.playerPos.X + 400),
                (graphics.PreferredBackBufferHeight/2 - player.playerPos.Y - 50), 0));

            Rectangle sourceRectangle = new Rectangle(0, 0, playerTexture.Width, playerTexture.Height);

            Vector2 backgroundOrigin = new Vector2(0, 0);
            Vector2 originCircle = new Vector2(mCircle.Width / 2, mCircle.Height / 2);
            Vector2 originCircle_2 = new Vector2(mCircle_2.Width / 2, mCircle_2.Height / 2);
            Vector2 origin = new Vector2(playerTexture.Width / 2, playerTexture.Height / 2);
            
            //Vector2 originForPeople = new Vector2();
            Vector2 fireStickOrigin = new Vector2(fireStick.Width / 2, fireStick.Height / 2);
            Vector2 coinOrigin = new Vector2(coins.Width / 2, coins.Height / 2);

            // Spikes
            spriteBatch.Draw(background, backgroundPos1, null, Color.OrangeRed, 0, backgroundOrigin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(background, backgroundPos2, null, Color.OrangeRed, 0, backgroundOrigin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(background, backgroundPos3, null, Color.OrangeRed, 0, backgroundOrigin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(background, backgroundPos4, null, Color.OrangeRed, 0, backgroundOrigin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(background, backgroundPos5, null, Color.OrangeRed, 0, backgroundOrigin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(background, backgroundPos6, null, Color.OrangeRed, 0, backgroundOrigin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(background, backgroundPos7, null, Color.OrangeRed, 0, backgroundOrigin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(background, backgroundPos8, null, Color.OrangeRed, 0, backgroundOrigin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(background, backgroundPos9, null, Color.OrangeRed, 0, backgroundOrigin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(background, backgroundPos10, null, Color.OrangeRed, 0, backgroundOrigin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(background, backgroundPos11, null, Color.OrangeRed, 0, backgroundOrigin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(background, backgroundPos12, null, Color.OrangeRed, 0, backgroundOrigin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(background, backgroundPos13, null, Color.OrangeRed, 0, backgroundOrigin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(background, backgroundPos14, null, Color.OrangeRed, 0, backgroundOrigin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(background, backgroundPos15, null, Color.OrangeRed, 0, backgroundOrigin, 1f, SpriteEffects.None, 0);

            spriteBatch.Draw(bgPlat, bgPlatPos, null, Color.White, 0, originCircle, 1.71f, SpriteEffects.None, 0);
            //cirlce 1
            spriteBatch.Draw(mCircle, mCirclePos, null, Color.White, angle1, originCircle, 2.9f, SpriteEffects.None, 0);
            //cirlce 2 
            spriteBatch.Draw(mCircle_2, mCirclePos_2, null, Color.White, angle1*-1.5f, originCircle_2, 2.9f, SpriteEffects.None, 1);
            //cirlce 3 
            spriteBatch.Draw(mCircle_3, mCirclePos_3, null, Color.White, angle1*.5f, originCircle_2, 2.9f, SpriteEffects.None, 0);
            //player
            spriteBatch.Draw(playerTexture, player.playerPos, sourceRectangle, Color.White, player.turnAngle, origin, 1.0f, SpriteEffects.None, 0);


            //spriteBatch.DrawString(scoreFont, "Pos: " + gameController.timer%8/* player.playerPos.X + "," + player.playerPos.Y*/, new Vector2(500, 350),Color.Black);
            //spriteBatch.DrawString(scoreFont, "Pos: " + isChange/* player.playerPos.X + "," + player.playerPos.Y*/, new Vector2(500, 450), Color.LightYellow); // score
            //debug print
            //spriteBatch.Draw(whiteRectangle,safeZoneCirlce1To2,Color.Red);
            if (isInside ==true)
            {
               spriteBatch.DrawString(scoreFont, "x , y " + player.playerPos.X +","+ player.playerPos.Y, new Vector2(1000,700), Color.Black);

            }

            // Coins
            for (i = 0; i < coinPos.Count; i++)
            {
                spriteBatch.Draw(coins, coinPos[i], null, Color.White, angle3, coinOrigin, 1.0f, SpriteEffects.None, 0);
            }

            // Fireballs
            for (i = 0; i < fireStickPos.Count; i++)
            {
               spriteBatch.Draw(fireStick, fireStickPos[i], null, Color.White, angle1, fireStickOrigin, 1.0f, SpriteEffects.None, 0);
            }

            // Score & Timer
            spriteBatch.DrawString(scoreFont, "Time: " + countDown, new Vector2(35, 950), Color.LightYellow); // timer
            spriteBatch.DrawString(scoreFont, "Score: " + gameController.getScore() + "/5", new Vector2(880, 750), Color.LightYellow); // score

            // All coins collected text
            if (isCoinDone == true)
            {
                spriteBatch.DrawString(scoreFont, "Hurry to the end!!!", new Vector2(430, 20), Color.Orange);
            }

            // GameOver State
            if (isTimerUp == true)
            {
                spriteBatch.Draw(blur, blurPos, null, Color.White, 0, originCircle, 2.5f, SpriteEffects.None, 1);
                spriteBatch.DrawString(gameOverFont, "GAME OVER!", new Vector2(160, 400), Color.Black); // end state text
                spriteBatch.DrawString(scoreFont, "(Press ", new Vector2(350, 550), Color.Black);
                spriteBatch.DrawString(scoreFont, "Enter", new Vector2(570, 550), Color.DarkRed);
                spriteBatch.DrawString(scoreFont, " to restart)", new Vector2(770, 550), Color.Black);
                spriteBatch.DrawString(scoreFont, "Time: " + countDown, new Vector2(35, 950), Color.Black); // timer
                spriteBatch.DrawString(scoreFont, "Score: " + gameController.getScore() + "/5", new Vector2(1180, 950), Color.Black); // score
            }

            // Win State
            if (isWinState == true)
            {
                spriteBatch.Draw(blur, blurPos, null, Color.White, 0, originCircle, 2.5f, SpriteEffects.None, 1);
                spriteBatch.DrawString(gameOverFont, "YOU WON!!", new Vector2(230, 400), Color.DarkOliveGreen);
                spriteBatch.DrawString(scoreFont, "(Press ", new Vector2(350, 550), Color.Black);
                spriteBatch.DrawString(scoreFont, "Enter", new Vector2(570, 550), Color.DarkRed);
                spriteBatch.DrawString(scoreFont, " to restart)", new Vector2(770,550), Color.Black);
                spriteBatch.DrawString(scoreFont, "Time: " + countDown, new Vector2(35, 950), Color.Black); // timer
                spriteBatch.DrawString(scoreFont, "Score: " + gameController.getScore() + "/5", new Vector2(1180, 950), Color.Black); // score
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}


