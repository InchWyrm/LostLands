using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LostLands
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        IntroScreen intro;
        PlayerCreationScreen PCS;
        StoryScreen SS;
        Player player;
        MainScreen MS;
        Screen activeScreen;
        KeyboardState keyboardState;
        SpriteFont font;

        bool fpsOn = false;
        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            //graphics.IsFullScreen = true;
            
            IsMouseVisible = true;

            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           
            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Services.AddService(typeof(SpriteBatch), spriteBatch);
            Services.AddService(typeof(ContentManager), Content);

            Content.Load<Texture2D>(@"Backgrounds\Back");
            font = Content.Load<SpriteFont>("MetalLord");

            StartGame();
            // TODO: use this.Content to load your game content here
        }

        public void StartGame()
        {
            player = new Player(this, Content.Load<Texture2D>("Character1"));
            intro = new IntroScreen(this);
            PCS = new PlayerCreationScreen(this, ref player);
            SS = new StoryScreen(this, ref player);
            MS = new MainScreen(this, ref player, ref graphics);

            
            activeScreen = intro;
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {

            // Allows the game to exit
            if (keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }

            if (intro.Options)
            {
                //player.inStory = false;
                //player.walking = false;

                //activeScreen = MS;
                //player.changeStats(20, 10, 18, 6, 6);
                //player.setVitalities();
                graphics.ToggleFullScreen();
                //graphics.IsFullScreen = !graphics.IsFullScreen;
                intro.Options = false;
            }
            else
                if (intro.active == false)
                {
                    if (PCS.active == true)
                    {
                        activeScreen = PCS;
                    }
                    else
                    {
                        if (activeScreen != SS && SS.active == true)
                        {
                            player = PCS.getPlayer();
                            player.X = 100;
                            player.Y = 450;
                            SS.setPlayer(ref player);
                            activeScreen = SS;
                        }

                        if (SS.active == false && activeScreen != MS)
                        {
                            player.X = 318;
                            player.Y = 260;
                            player.faceSouth();
                            activeScreen = MS;
                        }
                    }
                }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            
            activeScreen.Draw(gameTime);

            spriteBatch.Begin();

            if (activeScreen == MS && player.getCurentHealth() < 0)
            {
                spriteBatch.Draw(Content.Load<Texture2D>(@"backgrounds\Reaper"), new Rectangle(0, 0, 800, 600), Color.White);
            }

                #region fps
                frameCounter++;
                string fps = string.Format("fps: {0}", frameRate);
                this.Window.Title = "Lost Lands " + fps;
                if (Keyboard.GetState().IsKeyDown(Keys.P) && keyboardState.IsKeyUp(Keys.P))
                {
                    if (fpsOn)
                        fpsOn = false;
                    else
                        fpsOn = true;
                }
                if (fpsOn)
                    spriteBatch.DrawString(activeScreen.font2, fps, new Vector2(0, 0), Color.Brown);
                #endregion

            spriteBatch.End();

            keyboardState = Keyboard.GetState(); 
            //intro.draw();

            base.Draw(gameTime);
        }
    }
}