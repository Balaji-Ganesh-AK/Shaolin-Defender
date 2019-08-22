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
    Texture2D arrow_1; //arrow 1
    Texture2D arrow_2;
    Vector2 pos;
    Vector2 pos_arrow_2;

    //collision
    Rectangle arrow_collision_detection_1;
    Rectangle arrow_collision_detection_2;
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
        pos = new Vector2(0, 0); // Controls the position
        pos_arrow_2 = new Vector2(50, 50);
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

        //testing 
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
        arrow_1 = this.Content.Load<Texture2D>("arrow");
        arrow_2 = this.Content.Load<Texture2D>("arrow");
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
        arrow_collision_detection_1 = new Rectangle((int)(pos.X), (int)(pos.Y), arrow_1.Width, arrow_1.Height);
        arrow_collision_detection_2 = new Rectangle((int)(pos_arrow_2.X), (int)(pos_arrow_2.Y), arrow_2.Width, arrow_2.Height);
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
        if (Keyboard.GetState().IsKeyDown(Keys.S) && pos.Y <= heigthWindow - 20)
            pos.Y += speed;
        if (Keyboard.GetState().IsKeyDown(Keys.A) && pos.X >= 0)
            pos.X -= speed;
        if (Keyboard.GetState().IsKeyDown(Keys.D) && pos.X <= widthWindow - 20)
            pos.X += speed;
            //if (Keyboard.GetState().IsKeyDown(Keys.Q))
            //    angle -= 0.1f;
            //if (Keyboard.GetState().IsKeyDown(Keys.E))
            //    angle += 0.1f;
            angle += 1f;


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
        spriteBatch.Draw(arrow_1, pos * 2, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.FlipHorizontally, 1);
        spriteBatch.Draw(arrow_2, pos_arrow_2, Color.White);

        //text
        spriteBatch.DrawString(scoreFont, "rectanglearrowright2" + arrow_collision_detection_2.Right, new Vector2(150, 150), Color.Black);// width
        spriteBatch.DrawString(scoreFont, "rectanglearrowleft2" + arrow_collision_detection_2.Left, new Vector2(200, 200), Color.Black);// height
        spriteBatch.DrawString(scoreFont, "rectanglearrowrbottom2 " + arrow_collision_detection_2.Bottom, new Vector2(250, 250), Color.Black);// width
        spriteBatch.DrawString(scoreFont, "rectanglearrowrTop2" + arrow_collision_detection_2.Top, new Vector2(300, 300), Color.Black);// height
        spriteBatch.DrawString(scoreFont, " rectanglearrowright1" + arrow_collision_detection_1.Right, new Vector2(550, 250), Color.Black);
        spriteBatch.DrawString(scoreFont, " rectanglearrowleft1" + arrow_collision_detection_1.Left, new Vector2(550, 350), Color.Black);
        spriteBatch.DrawString(scoreFont, " rectanglearrowrbottom1" + arrow_collision_detection_1.Bottom, new Vector2(550, 450), Color.Black);
        spriteBatch.DrawString(scoreFont, " rectanglearrowrTop1" + arrow_collision_detection_1.Top, new Vector2(550, 550), Color.Black);
        spriteBatch.DrawString(scoreFont, "arrow2 x" + pos_arrow_2.X, new Vector2(600, 300), Color.Black);//height
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
