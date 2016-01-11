using Hypothermia.Controller;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hypothermia.View.Menu
{
    public class MenuView : Options
    {
        private GraphicsDeviceManager graphics;
        private Camera camera;

        private SpriteFont font;
        private Button playButton;
        private Button optionButton;
        private Button quitButton;
        private Button newButton;
        private Button resumeButton;

        private bool playerWon = false;
        protected float loadTimer = 2.0f;

        public MenuView(GraphicsDeviceManager graphics, Camera camera)
        {
            this.graphics = graphics;
            this.camera = camera;

            base.Resolution = new Vector2(1600, 900);
            base.FullScreen = false;
            base.AmbientVolume = 50;
        }

        public void LoadContent(ContentManager content)
        {
            this.font = content.Load<SpriteFont>("Fonts/SpriteFont1");
            this.playButton = new Button(this.graphics.GraphicsDevice, content.Load<Texture2D>("playButton"));
            this.optionButton = new Button(this.graphics.GraphicsDevice, content.Load<Texture2D>("optionButton"));
            this.quitButton = new Button(this.graphics.GraphicsDevice, content.Load<Texture2D>("quitButton"));
            this.newButton = new Button(this.graphics.GraphicsDevice, content.Load<Texture2D>("newButton"));
            this.resumeButton = new Button(this.graphics.GraphicsDevice, content.Load<Texture2D>("resumeButton"));
        }

        public void LoadSettings(GraphicsDevice device)
        {
            this.graphics.PreferredBackBufferWidth = (int)base.Resolution.X;
            this.graphics.PreferredBackBufferHeight = (int)base.Resolution.Y;
            this.graphics.IsFullScreen = base.FullScreen;
            this.graphics.ApplyChanges();

            this.camera.DeviceHeight = device.Viewport.Height;
            this.camera.DeviceWidth = device.Viewport.Width;

            MediaPlayer.Volume = base.AmbientVolume;

            base.SettingsChanged = false;
        }

        public void Update(float elapsedTime, GraphicsDevice device, MouseState mouse, GameState state)
        {
            Vector2 logicMouse = this.camera.GetDeviceCoordinates(device.Viewport.Width / 2, device.Viewport.Height / 2);

            this.playButton.Position = this.camera.GetDeviceCoordinates(this.playButton.Texture.Width / 2, 140);
            this.resumeButton.Position = this.camera.GetDeviceCoordinates(this.resumeButton.Texture.Width / 2, 200);
            this.newButton.Position = this.camera.GetDeviceCoordinates(this.newButton.Texture.Width / 2, 140);
            this.optionButton.Position = this.camera.GetDeviceCoordinates(this.optionButton.Texture.Width / 2, 80);
            this.quitButton.Position = this.camera.GetDeviceCoordinates(this.quitButton.Texture.Width / 2, 20);

            if (state == GameState.MainMenu)
            {
                this.playButton.Update(mouse, logicMouse);
                this.optionButton.Update(mouse, logicMouse);
                this.quitButton.Update(mouse, logicMouse);
            }
            else if (state == GameState.Paused)
            {
                this.resumeButton.Update(mouse, logicMouse);
                this.newButton.Update(mouse, logicMouse);
                this.optionButton.Update(mouse, logicMouse);
                this.quitButton.Update(mouse, logicMouse);
            }
            else if (state == GameState.GameOver)
            {
                this.loadTimer -= elapsedTime;
            }

            Matrix transform = this.camera.Transform;
        }

        public void Draw(GraphicsDevice device, SpriteBatch sb, GameState state)
        {
            if (state == GameState.MainMenu)
            {
                device.Clear(Color.Lerp(Color.White, Color.LightSlateGray, 0.5f));

                this.loadTimer = 2.0f;
                this.playButton.Draw(sb);
                this.optionButton.Draw(sb);
                this.quitButton.Draw(sb);

                sb.End();
            }
            else if (state == GameState.Paused)
            {
                this.resumeButton.Draw(sb);
                this.newButton.Draw(sb);
                this.optionButton.Draw(sb);
                this.quitButton.Draw(sb);

                sb.End();
            }
            else if (state == GameState.GameOver)
            {
                if (playerWon)
                {
                    sb.DrawString(this.font, "Congratulations, you completed the game!", this.camera.GetDeviceCoordinates(0, 0), Color.Black);
                    if (this.loadTimer < 0)
                        sb.DrawString(this.font, "Press Enter to go back to the main menu.", this.camera.GetDeviceCoordinates(0, -20), Color.Black);
                }
                else
                {
                    sb.DrawString(this.font, "I'm sorry, you suck...", this.camera.GetDeviceCoordinates(0, 0), Color.Black);
                    if(this.loadTimer < 0)
                        sb.DrawString(this.font, "Press Enter to go back to the main menu.", this.camera.GetDeviceCoordinates(0, -20), Color.Black);
                }
                sb.End();
            }
        }

        public Button PlayButton { get { return this.playButton; } }

        public Button OptionButton { get { return this.optionButton; } }

        public Button QuitButton { get { return this.quitButton; } }

        public Button ResumeButton { get { return this.resumeButton; } }

        public Button NewButton { get { return this.newButton; } }

        public bool PlayerWon { set { this.playerWon = value; } }

        public float LoadTimer { get { return this.loadTimer; } }
    }
}
