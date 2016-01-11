using Hypothermia.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Diagnostics;

namespace Hypothermia.Controller
{
    public enum GameState
    {
        MainMenu,
        Options,
        Playing,
        Paused,
        GameOver
    }

    public class MasterController : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GameState CurrentGameState = GameState.MainMenu;

        private View.Camera camera;
        private View.Menu.MenuView menuView;
        private View.SoundHandler soundHandler;
        private GameController gameController;

        private int tileSize = 64;

        public MasterController()
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
            this.camera = new View.Camera(GraphicsDevice, this.tileSize);
            this.menuView = new View.Menu.MenuView(graphics, this.camera);
            this.soundHandler = new View.SoundHandler();
            this.gameController = new GameController(this.camera);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.soundHandler.AddAmbient(Content.Load<Song>("Sounds/Ambient/L01_ambient_A"));

            this.menuView.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void DoMainMenu(float elapsedTime, MouseState mouse)
        {
            this.IsMouseVisible = true;
            if (this.menuView.PlayButton.IsClicked == true)
            {
                this.gameController.StartNewGame(Content);
                this.gameController.LoadLevel(Content);
                CurrentGameState = GameState.Playing;
            }
            if (this.menuView.OptionButton.IsClicked == true)
                Debug.WriteLine("Option menu");
            if (this.menuView.QuitButton.IsClicked == true)
                Exit();
            this.menuView.Update(elapsedTime, GraphicsDevice, mouse, CurrentGameState);
        }

        private void DoPauseMenu(float elapsedTime, MouseState mouse)
        {
            this.IsMouseVisible = true;
            this.menuView.PlayButton.IsClicked = false;
            if (this.menuView.ResumeButton.IsClicked == true)
                CurrentGameState = GameState.Playing;
            if (this.menuView.NewButton.IsClicked == true)
            {
                this.gameController.StartNewGame(Content);
                this.gameController.LoadLevel(Content);
                CurrentGameState = GameState.Playing;
            }
            if (this.menuView.OptionButton.IsClicked == true)
                Debug.WriteLine("Option menu");
            if (this.menuView.QuitButton.IsClicked == true)
                Exit();
            this.menuView.Update(elapsedTime, GraphicsDevice, mouse, CurrentGameState);
        }

        private void DoPlayCheck()
        {
            this.IsMouseVisible = false;
            this.menuView.PlayButton.IsClicked = false;
            this.menuView.ResumeButton.IsClicked = false;
            this.menuView.NewButton.IsClicked = false;

            if (this.gameController.Level.Count != this.gameController.CurrentLevel)
                this.gameController.LoadLevel(Content);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                CurrentGameState = GameState.Paused;
            if (this.gameController.GameOver)
                CurrentGameState = GameState.GameOver;
            if (this.gameController.Player.Position.Y > this.camera.MapHeight + this.camera.TileSize * 2)
            {
                this.gameController.Player.Lives -= 1;
                this.gameController.LoadLevel(Content);
            }
        }

        private void DoGameOverCheck(float elapsedTime, MouseState mouse)
        {
            if (this.gameController.GameWon)
                this.menuView.PlayerWon = true;
            else
                this.menuView.PlayerWon = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && this.menuView.LoadTimer < 0)
            {
                this.menuView.PlayerWon = false;
                CurrentGameState = GameState.MainMenu;
            }
            this.menuView.Update(elapsedTime, GraphicsDevice, mouse, CurrentGameState); 
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            MouseState mouse = Mouse.GetState();

            if(this.menuView.SettingsChanged)
                this.menuView.LoadSettings(GraphicsDevice);

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    this.DoMainMenu(elapsedTime, mouse);
                    break;
                case GameState.Paused:
                    this.DoPauseMenu(elapsedTime, mouse);
                    break;
                case GameState.Options:
                    Debug.WriteLine("Options not implemented");
                    break;
                case GameState.Playing:
                    this.DoPlayCheck();
                    this.gameController.Update(elapsedTime);
                    break;
                case GameState.GameOver:
                    this.DoGameOverCheck(elapsedTime, mouse);
                    break;
            }

            this.soundHandler.Update(CurrentGameState);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Lerp(Color.White, Color.DeepSkyBlue, 0.5f));

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

            switch (CurrentGameState)
            {
                case GameState.Playing:
                    this.gameController.Draw(spriteBatch, CurrentGameState);
                    break;
                case GameState.Paused:
                    this.gameController.Draw(spriteBatch, CurrentGameState);
                    this.menuView.Draw(GraphicsDevice, spriteBatch, CurrentGameState);
                    break;
                case GameState.GameOver:
                    this.menuView.Draw(GraphicsDevice, spriteBatch, CurrentGameState);
                    break;
                case GameState.Options:
                    break;
                case GameState.MainMenu:
                    this.menuView.Draw(GraphicsDevice, spriteBatch, CurrentGameState);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
