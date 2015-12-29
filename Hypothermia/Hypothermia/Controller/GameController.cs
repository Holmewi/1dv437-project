using Hypothermia.Model;
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
        Paused,
        GameOver
    }

    public class GameController : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GameState CurrentGameState = GameState.MainMenu;

        private View.Camera camera;
        private PlayerController playerController;
        private Model.EnemySimulation enemySimulation;
        private View.Menu.MenuView menuView;    
        private View.GameView gameView;
        private View.GFXRenderer GFX;

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

            this.camera = new View.Camera(GraphicsDevice, 64);

            this.playerController = new PlayerController(this.camera);
            this.enemySimulation = new Model.EnemySimulation(this.camera);

            this.gameView = new View.GameView(this.camera);
            this.menuView = new View.Menu.MenuView(graphics, this.camera);
            this.GFX = new View.GFXRenderer(this.camera);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.playerController.LoadContent(Content);
            this.menuView.LoadContent(Content);
            
            //  TODO: Make a solution to change levels
            this.gameView.LoadContent(Content, 1);
            this.GFX.LoadContent(Content, 1);
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
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            MouseState mouse = Mouse.GetState();

            if (CurrentGameState == GameState.MainMenu)
            {
                this.IsMouseVisible = true;
                if (this.menuView.PlayButton.IsClicked == true)
                {
                    this.gameView.LoadContent(Content, 1);
                    this.playerController.Start();
                    CurrentGameState = GameState.Playing;
                }
                if (this.menuView.OptionButton.IsClicked == true)
                    Debug.WriteLine("Option menu");
                if (this.menuView.QuitButton.IsClicked == true)
                    Exit();
                this.GFX.UpdatePregame(elapsedTime);
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
                    this.playerController.Start();
                    this.gameView.LoadContent(Content, 1);
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
                this.IsMouseVisible = false;
                this.menuView.PlayButton.IsClicked = false;
                this.menuView.ResumeButton.IsClicked = false;
                this.menuView.NewButton.IsClicked = false;
                
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    CurrentGameState = GameState.Paused;
                if (this.playerController.Player.PlayerState == PlayerState.Dead)
                    CurrentGameState = GameState.GameOver;

                this.playerController.Update(elapsedTime, this.gameView.Tiles);
                this.gameView.Update(elapsedTime);
                this.GFX.UpdateIngame(elapsedTime, this.playerController.Player.Velocity, this.playerController.Player.Position);
                for (int i = 0; i < this.gameView.Enemies.Count; i++)
                {
                    this.enemySimulation.Update(elapsedTime, this.gameView.Enemies[i], this.gameView.Tiles);
                    this.playerController.Player.Combat(this.gameView.Enemies[i]);

                    if (this.gameView.Enemies[i].Position.Y > camera.MapHeight + this.gameView.Enemies[i].Rect.Height)
                        this.gameView.Enemies.RemoveAt(i);
                }
            }
            else if (CurrentGameState == GameState.Options)
            {
                this.IsMouseVisible = true;
            }
            else if (CurrentGameState == GameState.GameOver)
            {
                //  TODO: Create a gameover screen
                this.playerController.Start();
                this.gameView.LoadContent(Content, 1);
                CurrentGameState = GameState.Playing;
            }

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
            
            if (CurrentGameState == GameState.Playing)
            {
                this.GFX.DrawBackground(spriteBatch, CurrentGameState);
                this.gameView.Draw(spriteBatch);
                this.playerController.Draw(spriteBatch);
                this.GFX.DrawForeground(spriteBatch, CurrentGameState);
            }
            else if (CurrentGameState == GameState.Paused)
            {
                this.GFX.DrawBackground(spriteBatch, CurrentGameState);
                this.gameView.Draw(spriteBatch);
                this.playerController.Draw(spriteBatch);
                this.GFX.DrawForeground(spriteBatch, CurrentGameState);
                this.menuView.Draw(GraphicsDevice, spriteBatch, CurrentGameState);

            }
            else if (CurrentGameState == GameState.Options)
            {

            }
            else if (CurrentGameState == GameState.MainMenu)
            {
                this.GFX.DrawForeground(spriteBatch, CurrentGameState);
                this.menuView.Draw(GraphicsDevice, spriteBatch, CurrentGameState);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
