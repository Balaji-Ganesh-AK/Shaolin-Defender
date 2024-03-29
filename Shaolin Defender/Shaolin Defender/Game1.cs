﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;


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
        List<SoundEffect> soundEffects = new List<SoundEffect>(4);
        GameController gameController = new GameController();
        //Coin Parent
        Coins newCoin = new Coins();
        //Fire  Ball 
        FireBall newFireBall = new FireBall();
        //Player Object
        Player player = new Player();

       // Animation myPlayerAnimation;
        int i = 0;
        Animation myPlayerAnimation, idlePlayerAnimation, coinAnimation;
        
        // Textures for background & objects
        Texture2D background; // spike background
        Texture2D endPlat; // endPlatform
        Texture2D startPlat; //startPlatform
        Texture2D mCircle; // circle (main platform)
        Texture2D mCircle_2; // circle(2nd platform)
        Texture2D mCircle_3; // cirlce (3rd platform)
        Texture2D coins;
        Texture2D fireStick;
        Texture2D safeZonePlat;
        Texture2D winZonePlat;
        Texture2D blur;
        
        
        //Animation texture
        Texture2D playerTexture, playerIdle;
        

        // Vectors
        Vector2 safeZonePos;
        Vector2 winZonePos;
        Vector2  backgroundPos5, backgroundPos6;
        Vector2 startPlatPos;
        Vector2 endPlatPos, endPlatPos2, endPlatPos3, endPlatPos4;
        
        public Vector2 mCirclePos;
        public Vector2 mCirclePos_2;
        public Vector2 mCirclePos_3;
        Vector2 blurPos;
        
        
        //get the circle
        Vector2 dir = new Vector2();
        // Collision
        Rectangle circleRectangle;
        Rectangle coinsRectangle;

        // Safe zones
        Rectangle safeZone;
        Rectangle winZone;
        Rectangle safeZoneStart;
        Rectangle winLine;
        public  Rectangle safeZoneCirlce1To2;
        public  Rectangle safeZoneCirlce2To3;
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

        // MovementControls
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

        int countTimerMax = 6;
        float speed =0;


       


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            // Object positions (x, y)
            backgroundPos5 = new Vector2(-600, -450);
            backgroundPos6 = new Vector2(3785, -450);
           
            startPlatPos = new Vector2(10, 220);
            endPlatPos = new Vector2(3150,620);
            endPlatPos2 = new Vector2(3785, 620);
            endPlatPos3 = new Vector2(4060, 620);
            endPlatPos4 = new Vector2(4695, 620);
            player.playerPos = new Vector2(54, 390); // Controls the Player.playerPosition
            mCirclePos = new Vector2(700, 450);
            mCirclePos_2 = new Vector2(1450,800);
            mCirclePos_3 = new Vector2(2350,750);
            blurPos = new Vector2(0, 0);
            safeZonePos = new Vector2(2960, 565);
            winZonePos = new Vector2(2820, 565);
            
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
            newCoin.addCoins();

            // Fire Locations & Adding
            newFireBall.addFireBall();

            // Win and safe zone 
            safeZone = new Rectangle(2860, 565, 600, 400);
            //winZone = new Rectangle(2900, 565, 600, 600);
            safeZoneStart = new Rectangle(10, 230, 330, 400);
            safeZoneCirlce1To2 = new Rectangle(1000,450,180,300);
            safeZoneCirlce2To3 = new Rectangle(1680,550,200,400);
            winLine = new Rectangle(3500,350,200,600);
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
           
            background = this.Content.Load<Texture2D>("Newbackground");
            // bgPlat = this.Content.Load<Texture2D>("background");
            startPlat = this.Content.Load<Texture2D>("Start_platform");
            endPlat = this.Content.Load<Texture2D>("End_platform");
            mCircle_2 = this.Content.Load<Texture2D>("platform2");
            mCircle = this.Content.Load<Texture2D>("NewPlatform1");
            mCircle_3 = this.Content.Load<Texture2D>("platform3");
            safeZonePlat = new Texture2D(GraphicsDevice, 500, 400);
            winZonePlat = new Texture2D(GraphicsDevice, 500, 400);
            //Idle animation
            playerIdle = this.Content.Load<Texture2D>("Idle_Player");
           

            //playerTexture = this.Content.Load<Texture2D>("Cowboy_man");
            playerTexture = this.Content.Load<Texture2D>("playerRun");
            //playerTextureLeft = this.Content.Load<Texture2D>("playerRunLeft");
            //Coin texture
            coins = this.Content.Load<Texture2D>("coinAnimation");

            fireStick = this.Content.Load<Texture2D>("fireball");
            blur = this.Content.Load<Texture2D>("blur");
            
            // playerRun = this.Content.Load<Texture2D>("playerRun");
            myPlayerAnimation = new Animation(Content, playerTexture, 100f, 4, true);

            idlePlayerAnimation = new Animation(Content, playerIdle, 200f,4,true);
            //Coin Animation
            coinAnimation = new Animation(Content,coins,100f,8,true);
            //Dab Animation
            //dabAnimation = new Animation(Content,playerDab, 100f,true);
            scoreFont = Content.Load<SpriteFont>("Title");
            gameOverFont = Content.Load<SpriteFont>("GameOver");


            // Background Music
            backgroundMusic = Content.Load<Song>("new/castle_music");
            MediaPlayer.Volume = .5f;
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
            //test
            whiteRectangle = new Texture2D(GraphicsDevice,1,1);
            whiteRectangle.SetData(new[] { Color.White});
            
            soundEffects.Add(Content.Load<SoundEffect>("new/coin_01"));
            soundEffects.Add(Content.Load<SoundEffect>("new/coin_02"));
            soundEffects.Add(Content.Load<SoundEffect>("new/splosh"));
            soundEffects.Add(Content.Load<SoundEffect>("new/hit_fire"));

            //sploshMusic = Content.Load<Song>("new/walking");

            var instance = soundEffects[0].CreateInstance();
            var instance1 = soundEffects[1].CreateInstance();
            var instance2 = soundEffects[2].CreateInstance();
            var instance3 = soundEffects[3].CreateInstance();
            instance.IsLooped = true;
            instance1.IsLooped = true;
            instance2.IsLooped = true;
            instance3.IsLooped = true;
            //instance.IsLooped = true;
            // instance.Play();
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
            player.isIdeal = true;
            
            // Roation rates
            angle1 += .01f; // main platform
            angle2 -= .012f; // type1 firewall
            angle3 += .042f; // type2 firewall
            
            circleRectangle = new Rectangle((int)(mCirclePos.X), (int)(mCirclePos.Y), mCircle.Width, mCircle.Height);
            player.playerRectangle = new Rectangle((int)(player.playerPos.X)-20, (int)(player.playerPos.Y)-20, playerTexture.Width/4, playerTexture.Height);

            //Coin Animation
            coinAnimation.Play(gameTime);
            //Checking if the player hit the coins
            for (int i = 0; i < newCoin.coinPos.Count; i++)
            {
                coinsRectangle = new Rectangle((int)(newCoin.coinPos[i].X) - 30, (int)(newCoin.coinPos[i].Y - 30), coins.Width / 12, coins.Height / 2);

                if (player.playerRectangle.Intersects(coinsRectangle))
                {
                    Random rnd = new Random();
                    int r = rnd.Next(1, 4);
                    if (r == 1 || r == 3)
                    {
                        SoundEffect.MasterVolume = .2f;
                        soundEffects[0].CreateInstance().Play();
                    }
                    else if (r == 2 || r == 4)
                    {
                        SoundEffect.MasterVolume = .2f;
                        soundEffects[1].CreateInstance().Play();
                    }
                    isHit = true;
                    gameController.increaseScore();
                    newCoin.coinPos.RemoveAt(i);
                }
            }
            // *********************************FireBall  movement Controller***********************************************  
            if (gameController.timer % 8 < 4)
                isChange = true;
            else
                isChange = false;
            for (int i = 0; i < newFireBall.fireStickPos.Count; i++)
            {

                if ((newFireBall.fireStickPos[i] - player.playerPos).Length() < 65)
                {
                    SoundEffect.MasterVolume = .25f;
                    soundEffects[3].CreateInstance().Play();
                    isGameOver = true;
                }
                
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // Keymovements
            if (isWinState == false && isPause == false)
            {
                player.playerMovement(gameTime);
                //myPlayerAnimation.Play(gameTime);
                gameController.timer -= (float)gameTime.ElapsedGameTime.TotalSeconds; // timer
                countDown = gameController.timer.ToString("0.0");
            }
            
            if ((player.playerPos - mCirclePos).Length() < 420 || (player.playerPos - mCirclePos_2).Length() < 410 ||
                (player.playerPos - mCirclePos_3).Length() < 490 || player.playerRectangle.Intersects(safeZoneCirlce1To2) == true ||
                player.playerRectangle.Intersects(safeZoneCirlce2To3) == true  )
            {
                isInside = true;
                test = true;
            }

            //Player ideal Animation;
            if (player.isIdeal == true)
            {
                idlePlayerAnimation.Play(gameTime);
            }
            else
                myPlayerAnimation.Play(gameTime);
          
            // Controlls player Rotation inside the circle  
            if (isInside == true)
            {
                // get the circle
                if ((player.playerPos - mCirclePos).Length() < 420)
                {
                    dir = mCirclePos - player.playerPos;
                }
                if ((player.playerPos - mCirclePos_2).Length() < 410)
                {
                    speed = 2f;
                    countTimerMax = 7;
                    dir = mCirclePos_2 - player.playerPos;
                    dir *= -1;
                }
                if ((player.playerPos - mCirclePos_3).Length() < 490)
                {
                    dir = mCirclePos_3 - player.playerPos;
                    dir *= -1;
                }

                player.playerRotation(dir);
            }

          

            for (int i = 0; i < newFireBall.fireStickPosMoving.Count; i++)
            {
               
                if (i % 2 == 0)
                {
                   if (isChange == false /*&& (fireStickPosMoving[i]- dir).Length()<420*/)
                   {
                        newFireBall.fireStickPosMoving[i] = new Vector2(newFireBall.fireStickPosMoving[i].X, newFireBall.fireStickPosMoving[i].Y - speed);
                   }
                   else
                   {
                        newFireBall.fireStickPosMoving[i] = new Vector2(newFireBall.fireStickPosMoving[i].X, newFireBall.fireStickPosMoving[i].Y + speed);
                   }
                    
                }
                else
                {
                    if (isChange == false /*&& (fireStickPosMoving[i] - dir).Length() < 420*/)
                    {
                        newFireBall.fireStickPosMoving[i] = new Vector2(newFireBall.fireStickPosMoving[i].X, newFireBall.fireStickPosMoving[i].Y + speed);
                    }
                    else
                    {
                        newFireBall.fireStickPosMoving[i] = new Vector2(newFireBall.fireStickPosMoving[i].X, newFireBall.fireStickPosMoving[i].Y - speed);
                    }

                }

                if ((newFireBall.fireStickPosMoving[i] - player.playerPos).Length() < 65)
                {
                    isGameOver = true;
                }

            }
           
            // Resets the game!
            if (isGameOver == true)
            {
                gameController.reset();
                test = false;
                isGameOver = false;
                player.playerPos = new Vector2(164, 490);

                speed = 0;
                // Remove the remaining coins and respwan them again
                newCoin.coinPos.Clear();

                // Re-add all the coins
                newCoin.addCoins();

                newFireBall.fireStickPos.Clear();
                newFireBall.fireStickPosMoving.Clear();
                // Fire Locations & Adding
                newFireBall.addFireBall();
            }

            //// Game over state
            if (test == true && isInside == false)
            {
                if (player.playerRectangle.Intersects(safeZone) != true)
                {
                    SoundEffect.MasterVolume = .5f;
                    soundEffects[2].CreateInstance().Play();
                    isGameOver = true;
                }
                
                    
            }
         


            // Checking if all the coins are collected!!
            if (gameController.checkWinState(newCoin.coinPos.Count) == 1)
            {
                isCoinDone = true;
            }
            else
            {
                isCoinDone = false;
            }
            
            if (isCoinDone == true && player.playerRectangle.Intersects(winLine) == true)
            {
                isWinState = true;
                isCoinDone = false;
                
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    //gameController.resetTimer();
                    isGameOver = true;
                    isWinState = false;
                    isPause = false;
                    isTimerUp = false;
                }
            }

            if (gameController.timer <= 0 )
            {
                isTimerUp = true;
                isPause = true;
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
                (graphics.PreferredBackBufferWidth/10 - player.playerPos.X + 235),
                (graphics.PreferredBackBufferHeight/2 - player.playerPos.Y - 50), 0));

            Rectangle sourceRectangle = new Rectangle(0, 0, playerTexture.Width, playerTexture.Height);

            Vector2 backgroundOrigin = new Vector2(0, 0);
            Vector2 originCircle = new Vector2(mCircle.Width / 2, mCircle.Height / 2);
            Vector2 originCircle_2 = new Vector2(mCircle_2.Width / 2, mCircle_2.Height / 2);
            Vector2 originCircle_3 = new Vector2(mCircle_3.Width / 2, mCircle_3.Height / 2);
            Vector2 origin = new Vector2(playerTexture.Width / 2, playerTexture.Height / 2);
            Vector2 originSafe = new Vector2(safeZone.Width / 2, safeZone.Height / 2);


            //Vector2 originForPeople = new Vector2();
            Vector2 fireStickOrigin = new Vector2(fireStick.Width / 2, fireStick.Height / 2);
            Vector2 coinOrigin = new Vector2(coins.Width / 2, coins.Height / 2);

            // Spikes
            spriteBatch.Draw(background, backgroundPos5, null, Color.White, 0, backgroundOrigin, 2, SpriteEffects.None, 0);
            spriteBatch.Draw(background, backgroundPos6, null, Color.White, 0, backgroundOrigin, 2, SpriteEffects.FlipHorizontally, 0);
            //start_platform
            spriteBatch.Draw(startPlat, startPlatPos, null, Color.White, 0, originCircle, 1.5f, SpriteEffects.None, 0);
            //end_platform
            spriteBatch.Draw(endPlat, endPlatPos, null, Color.White, 0, originCircle, 1.5f, SpriteEffects.None, 0);
            spriteBatch.Draw(endPlat, endPlatPos3, null, Color.White, 0, originCircle, 1.5f, SpriteEffects.None, 0);
            spriteBatch.Draw(endPlat, endPlatPos2, null, Color.White, 0, originCircle, 1.5f, SpriteEffects.FlipHorizontally, 0);
            spriteBatch.Draw(endPlat, endPlatPos4, null, Color.White, 0, originCircle, 1.5f, SpriteEffects.FlipHorizontally, 0);

            //cirlce 1
            spriteBatch.Draw(mCircle, mCirclePos, null, Color.White, angle1, originCircle, 1.7f, SpriteEffects.None, 0);
            //cirlce 2 
            spriteBatch.Draw(mCircle_2, mCirclePos_2, null, Color.White, angle1*-1.5f, originCircle_2, 1.5f, SpriteEffects.None, 0);
            //cirlce 3 
             spriteBatch.Draw(mCircle_2, mCirclePos_3, null, Color.White, angle1*-1.5f, originCircle_2, 1.9f, SpriteEffects.None, 0);
           


            spriteBatch.Draw(safeZonePlat, safeZonePos, null, Color.Red, 0, originCircle_2, 1f, SpriteEffects.None, 1);
            spriteBatch.Draw(winZonePlat, winZonePos, null, Color.Red, 0, originCircle_2, 1f, SpriteEffects.None, 1);

            //debug print
            //spriteBatch.DrawString(scoreFont, "Pos: " + player.isIdeal/* player.playerPos.X + "," + player.playerPos.Y*/, new Vector2(500, 350),Color.Black);

            //  spriteBatch.DrawString(scoreFont, "POs"+player.playerPos.X +" Y: "+ player.playerPos.Y , player.playerPos, Color.LightYellow); // score
           // spriteBatch.DrawString(scoreFont, "POs" + newCoin.coinPos.Count, player.playerPos, Color.LightYellow);

            //spriteBatch.DrawString(scoreFont, "Pos: " + gameController.timer%8/* player.playerPos.X + "," + player.playerPos.Y*/, new Vector2(500, 350),Color.Black);

            //spriteBatch.DrawString(scoreFont, "Pos: " + isChange/* player.playerPos.X + "," + player.playerPos.Y*/, new Vector2(500, 450), Color.LightYellow); // score

            //            player.playerRectangle.Width = player.playerRectangle.Width / 4;
            //spriteBatch.Draw(whiteRectangle, coinsRectangle, Color.Red);
            //spriteBatch.Draw(whiteRectangle, winLine, Color.Red);
            // spriteBatch.Draw(whiteRectangle, safeZone, null, Color.Red, an, fireStickOrigin, 1.0f, SpriteEffects.None, 0);
            //if (isInside ==true)
            //{
            //   spriteBatch.DrawString(scoreFont, "x , y " + player.playerPos.X +","+ player.playerPos.Y, new Vector2(1000,700), Color.Black);

            //debug print
            //spriteBatch.Draw(whiteRectangle, winLine, Color.Red);
            //if (isInside == true)
            //{
            //   spriteBatch.DrawString(scoreFont, "x , y " + player.playerPos.X +","+ player.playerPos.Y, new Vector2(1000,700), Color.Black);


            //}

            // Coins
            for (i = 0; i < newCoin.coinPos.Count; i++)
            {
                coinsRectangle = new Rectangle((int)(newCoin.coinPos[i].X)-30, (int)(newCoin.coinPos[i].Y-30), coins.Width/12, coins.Height/2);
                
                
                coinAnimation.Draw(spriteBatch, coinsRectangle, newCoin.coinPos[i],angle3, coinOrigin, 0.5f );
            }

            // Fireballs
            for (i = 0; i < newFireBall.fireStickPos.Count; i++)
            {
               spriteBatch.Draw(fireStick, newFireBall.fireStickPos[i], null, Color.White, angle1, fireStickOrigin, 1.0f, SpriteEffects.None, 0);
            }
            //Moving Fireball
            for (i = 0; i < newFireBall.fireStickPosMoving.Count; i++)
            {
                spriteBatch.Draw(fireStick, newFireBall.fireStickPosMoving[i], null, Color.White, angle1, fireStickOrigin, 1.0f, SpriteEffects.None, 0);
            }

            // Score & Timer
            spriteBatch.DrawString(scoreFont, "Time: " + countDown, new Vector2(player.playerPos.X - 390, player.playerPos.Y + 520), Color.LightYellow); // timer
            spriteBatch.DrawString(scoreFont, "Score: " + gameController.getScore() +"/11", new Vector2(player.playerPos.X + 1160, player.playerPos.Y + 520), Color.LightYellow); // score

            // All coins collected text
            if (isCoinDone == true)
            {
                spriteBatch.DrawString(scoreFont, "Hurry to the end!!!", new Vector2(player.playerPos.X + 160, player.playerPos.Y - 470), Color.Orange);
            }

            // GameOver State
            if (isTimerUp == true)
            {
                spriteBatch.Draw(blur, new Vector2(player.playerPos.X - 190, player.playerPos.Y - 320), null, Color.LightGray, 0, originCircle, 2.5f, SpriteEffects.None, 0);
                spriteBatch.DrawString(gameOverFont, "GAME OVER!", new Vector2(player.playerPos.X - 90, player.playerPos.Y - 100), Color.Black); // end state text
                spriteBatch.DrawString(scoreFont, "(Press ", new Vector2(player.playerPos.X + 60, player.playerPos.Y + 50), Color.Black);
                spriteBatch.DrawString(scoreFont, "Enter", new Vector2(player.playerPos.X + 295, player.playerPos.Y + 50), Color.DarkRed);
                spriteBatch.DrawString(scoreFont, " to restart)", new Vector2(player.playerPos.X + 500, player.playerPos.Y + 50), Color.Black);
                spriteBatch.DrawString(scoreFont, "Time: " + countDown, new Vector2(player.playerPos.X - 390, player.playerPos.Y + 520), Color.LightYellow); // timer
                spriteBatch.DrawString(scoreFont, "Score: " + gameController.getScore() + "/5", new Vector2(player.playerPos.X + 1160, player.playerPos.Y + 520), Color.LightYellow); // score
            }

            // Win State
            if (isWinState == true)
            {
                spriteBatch.Draw(blur, new Vector2(player.playerPos.X - 190, player.playerPos.Y - 320), null, Color.LightGray, 0, originCircle, 2.5f, SpriteEffects.None, 0);
                spriteBatch.DrawString(gameOverFont, "YOU WON!!", new Vector2(player.playerPos.X + 10, player.playerPos.Y - 100), Color.DarkOliveGreen);
                spriteBatch.DrawString(scoreFont, "(Press ", new Vector2(player.playerPos.X + 110, player.playerPos.Y + 50), Color.Black);
                spriteBatch.DrawString(scoreFont, "Enter", new Vector2(player.playerPos.X + 345, player.playerPos.Y + 50), Color.DarkRed);
                spriteBatch.DrawString(scoreFont, " to restart)", new Vector2(player.playerPos.X + 550, player.playerPos.Y + 50), Color.Black);
                spriteBatch.DrawString(scoreFont, "Time: " + countDown, new Vector2(player.playerPos.X - 390, player.playerPos.Y + 520), Color.LightYellow); // timer
                spriteBatch.DrawString(scoreFont, "Score: " + gameController.getScore() + "/5", new Vector2(player.playerPos.X + 1160, player.playerPos.Y + 520), Color.LightYellow); // score
            }


           // idlePlayerAnimation.Draw(spriteBatch, player.playerRectangle, player.playerPos, player.turnAngle, origin);

            if (player.isIdeal == true)
            {
                //play idle Animation
                idlePlayerAnimation.Draw(spriteBatch, player.playerRectangle, player.playerPos, player.turnAngle, origin, 1.6f);
            }
            else
                //play Player Run Animation
                myPlayerAnimation.Draw(spriteBatch, player.playerRectangle, player.playerPos, player.turnAngle, origin, 1.6f);

            //Coin Animation
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}


