using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Hypothermia.Controller
{
    public enum GameState
    {
        MainMenu,
        Options,
        Playing,
        Paused
    }

    public class GameController : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GameState CurrentGameState;

        private View.Camera camera;
        private View.MenuView menuView;    
        private View.GameView gameView;
        private Model.Player player;

        public GameController()
        {
            graphics = new GraphicsDeviceManager(this);
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
            CurrentGameState = GameState.MainMenu;

            if (this.player == null)
                this.player = new Model.Player();

            this.camera = new View.Camera(GraphicsDevice);
            this.gameView = new View.GameView(this.camera, this.player);
            this.menuView = new View.MenuView(graphics, this.camera);

            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.menuView.LoadContent(Content);
            this.gameView.LoadContent(Content);
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
            this.camera.MapWidth = this.gameView.MapWidth;
            this.camera.MapHeight = this.gameView.MapHeight;

            Debug.WriteLine(this.camera.GetVisualCoordinates(0, 100));
           

            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            MouseState mouse = Mouse.GetState();

            this.menuView.Update(GraphicsDevice, mouse, CurrentGameState);

            if (CurrentGameState == GameState.MainMenu)
            {
                this.player.Start();
                this.IsMouseVisible = true;
                if (this.menuView.PlayButton.IsClicked == true)
                    CurrentGameState = GameState.Playing;
                if (this.menuView.OptionButton.IsClicked == true)
                    Debug.WriteLine("Option menu");
                if (this.menuView.QuitButton.IsClicked == true)
                    Exit();
                this.menuView.Update(GraphicsDevice, mouse, CurrentGameState);
            }
            else if (CurrentGameState == GameState.Paused)
            {
                this.IsMouseVisible = true;
                this.menuView.PlayButton.IsClicked = false;
                if (this.menuView.ResumeButton.IsClicked == true)
                    CurrentGameState = GameState.Playing;
                if (this.menuView.NewButton.IsClicked == true)
                {
                    this.player.Start();
                    CurrentGameState = GameState.Playing;
                }
                if (this.menuView.OptionButton.IsClicked == true)
                    Debug.WriteLine("Option menu");
                if (this.menuView.QuitButton.IsClicked == true)
                    Exit();
                this.menuView.Update(GraphicsDevice, mouse, CurrentGameState);
            }
            else if (CurrentGameState == GameState.Playing)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    CurrentGameState = GameState.Paused;
    
                this.IsMouseVisible = false;
                this.menuView.PlayButton.IsClicked = false;
                this.menuView.ResumeButton.IsClicked = false;
                this.menuView.NewButton.IsClicked = false;
                this.camera.FocusOnPlayer(elapsedTime, this.player.Position, this.player.Velocity, this.gameView.MapWidth, this.gameView.MapHeight);
                this.player.MapCollision(this.gameView.MapWidth, this.gameView.MapHeight);
                this.player.Update(elapsedTime, this.gameView.Boxes);
            }
            else if (CurrentGameState == GameState.Options)
            {
                this.IsMouseVisible = true;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            
            if (CurrentGameState == GameState.Playing)
            {
                this.gameView.Draw(spriteBatch);
            }
            else if (CurrentGameState == GameState.Paused)
            {
                this.gameView.Draw(spriteBatch);
                this.menuView.Draw(GraphicsDevice, spriteBatch, CurrentGameState);

            }
            else if (CurrentGameState == GameState.Options)
            {

            }
            else if (CurrentGameState == GameState.MainMenu)
            {
                this.menuView.Draw(GraphicsDevice, spriteBatch, CurrentGameState);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
