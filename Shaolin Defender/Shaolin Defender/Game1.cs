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
    
        // Textures
        Texture2D arrow_1; //arrow 1
        Texture2D arrow_2;
        Texture2D mCircle; // circle (main platform)
        Texture2D startPlat; // start platform
        Texture2D endPlat; // end platform
    
        // Vectors
        Vector2 pos;
        Vector2 pos_arrow_2;
        Vector2 pos_mCircle;
        Vector2 pos_start;
        Vector2 pos_end;

        // Collision
        Rectangle arrow_collision_detection_1;
        Rectangle arrow_collision_detection_2;
        Rectangle circle_collision_detection;
        Rectangle start_collision_detection;
        Rectangle end_collision_detection;

        // adding fonts
        private SpriteFont scoreFont;

        // getting the width and height of the window
        private int widthWindow;
        private int heigthWindow;

        // MovementControls
        private int speed = 5; // speed
        private float angle = 0; // angle for rotation
        private float angle2 = 0;
        private float angle3 = 0;
    
        // bool for collision detection
        bool isHit = false;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
    
            Content.RootDirectory = "Content";
      
            // Object positions (x, y)
            pos = new Vector2(0, 450); 
            pos_arrow_2 = new Vector2(50, 50);
            pos_mCircle = new Vector2(665, 500);
            pos_start = new Vector2(165, 500);
            pos_end = new Vector2(1150, 500);
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
            // testing 
            //arrow_1 = new Texture2D(this.GraphicsDevice, 100 , 100);
            //Color[] colorData = new Color[100*100];
            //for (int i = 0; i < 10000; i++)
            //    colorData[i] = Color.Black;
            //arrow_1.SetData<Color>(colorData); // color of the cube ; 

            // testing 
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw arrow_1s.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // testing 
            // Load texture images
            arrow_1 = this.Content.Load<Texture2D>("arrow");
            arrow_2 = this.Content.Load<Texture2D>("arrow");
            mCircle = this.Content.Load<Texture2D>("Ycircle");
            startPlat = this.Content.Load<Texture2D>("start");
            endPlat = this.Content.Load<Texture2D>("finish");
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
            // Roation rates
            angle += .01f; // main platform
            angle2 -= .012f; // type1 firewall
            angle3 += .012f; // type2 firewall

            arrow_collision_detection_1 = new Rectangle((int)(pos.X), (int)(pos.Y), arrow_1.Width, arrow_1.Height);
            arrow_collision_detection_2 = new Rectangle((int)(pos_arrow_2.X), (int)(pos_arrow_2.Y), arrow_2.Width, arrow_2.Height);
            circle_collision_detection = new Rectangle((int)(pos_mCircle.X), (int)(pos_mCircle.Y), mCircle.Width, mCircle.Height);
            start_collision_detection = new Rectangle((int)(pos_start.X), (int)(pos_start.Y), startPlat.Width, startPlat.Height);
            end_collision_detection = new Rectangle((int)(pos_end.X), (int)(pos_end.Y), endPlat.Width, endPlat.Height);
            // TODO: Add your update logic here
            // testing 
            isHit = false;
            widthWindow = GraphicsDevice.Viewport.Width;
            heigthWindow = GraphicsDevice.Viewport.Height;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
        
            //Keymovements
            if (Keyboard.GetState().IsKeyDown(Keys.W) && pos.Y >= 0)
                pos.Y -= speed;
            if (Keyboard.GetState().IsKeyDown(Keys.S) && pos.Y <= heigthWindow - 50)
                pos.Y += speed;
            if (Keyboard.GetState().IsKeyDown(Keys.A) && pos.X >= 25)
                pos.X -= speed;
            if (Keyboard.GetState().IsKeyDown(Keys.D) && pos.X <= widthWindow - 20)
                pos.X += speed;

            //if (Keyboard.GetState().IsKeyDown(Keys.Q))
            //    angle -= 0.1f;
            //if (Keyboard.GetState().IsKeyDown(Keys.E))
            //    angle += 0.1f;
            //angle += 1f;
            //simple collision detection

            //right
            //left
            if (arrow_collision_detection_1.Right - 10 == arrow_collision_detection_2.Left && arrow_collision_detection_1.Top <= arrow_collision_detection_2.Bottom)
            //&& arrow_collision_detection_1.Bottom <= arrow_collision_detection_2.Top)
            {
                isHit = true;
            }

            //up
            //down

            //testing 
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
            Rectangle sourceRectangle = new Rectangle(0, 0, arrow_1.Width, arrow_1.Height);
            Vector2 origin = new Vector2(arrow_1.Width / 2, arrow_1.Height / 2);
            Vector2 originMain = new Vector2(mCircle.Width / 2, mCircle.Height / 2);
            Vector2 originStart = new Vector2(startPlat.Width / 2, startPlat.Height / 2);
            Vector2 originEnd = new Vector2(endPlat.Width / 2, endPlat.Height / 2);

            spriteBatch.Draw(arrow_1, pos * 2, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.FlipHorizontally, 1);
            spriteBatch.Draw(arrow_2, pos_arrow_2, Color.White);
            spriteBatch.Draw(mCircle, pos_mCircle, null, Color.Yellow, angle, originMain, 3, SpriteEffects.None, 0f);
            spriteBatch.Draw(startPlat, pos_start, null, Color.Green, 0, originStart, 1.7f, SpriteEffects.None, 0f);
            spriteBatch.Draw(endPlat, pos_end, null, Color.White, 0, originEnd, 1.15f, SpriteEffects.None, 0f);

            // Score & Timer
            spriteBatch.DrawString(scoreFont, "Time: " + "0.0", new Vector2(10, 10), Color.Black); // timer
            spriteBatch.DrawString(scoreFont, "Score: " + "0", new Vector2(1250, 10), Color.Black); // score

            // text, etc.
            spriteBatch.DrawString(scoreFont, "rectanglearrowright2" + arrow_collision_detection_2.Right, new Vector2(150, 150), Color.Black); // width
            spriteBatch.DrawString(scoreFont, "rectanglearrowleft2" + arrow_collision_detection_2.Left, new Vector2(200, 200), Color.Black); // height
            spriteBatch.DrawString(scoreFont, "rectanglearrowrbottom2 " + arrow_collision_detection_2.Bottom, new Vector2(250, 250), Color.Black); // width
            spriteBatch.DrawString(scoreFont, "rectanglearrowrTop2" + arrow_collision_detection_2.Top, new Vector2(300, 300), Color.Black); // height
            spriteBatch.DrawString(scoreFont, " rectanglearrowright1" + arrow_collision_detection_1.Right, new Vector2(550, 250), Color.Black);
            spriteBatch.DrawString(scoreFont, " rectanglearrowleft1" + arrow_collision_detection_1.Left, new Vector2(550, 350), Color.Black);
            spriteBatch.DrawString(scoreFont, " rectanglearrowrbottom1" + arrow_collision_detection_1.Bottom, new Vector2(550, 450), Color.Black);
            spriteBatch.DrawString(scoreFont, " rectanglearrowrTop1" + arrow_collision_detection_1.Top, new Vector2(550, 550), Color.Black);   
            spriteBatch.DrawString(scoreFont, "arrow2 x" + pos_arrow_2.X, new Vector2(600, 300), Color.Black); //height
            spriteBatch.DrawString(scoreFont, "width" + widthWindow, new Vector2(100, 300), Color.Black); // width of window
            spriteBatch.DrawString(scoreFont, "height" + heigthWindow, new Vector2(200, 300), Color.Black); // height of window
            spriteBatch.DrawString(scoreFont, " rectanglearrowrTop1" + arrow_collision_detection_1.Top, new Vector2(550, 550), Color.Black);
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
