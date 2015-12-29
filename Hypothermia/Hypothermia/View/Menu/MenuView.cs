using Hypothermia.Controller;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        private Button playButton;
        private Button optionButton;
        private Button quitButton;
        private Button newButton;
        private Button resumeButton;

        public MenuView(GraphicsDeviceManager graphics, Camera camera)
        {
            this.graphics = graphics;
            this.camera = camera;
            base.Resolution = new Vector2(800, 600);
            base.FullScreen = false;
        }

        public void LoadContent(ContentManager content)
        {
            this.graphics.PreferredBackBufferWidth = (int)base.Resolution.X;
            this.graphics.PreferredBackBufferHeight = (int)base.Resolution.Y;
            this.graphics.IsFullScreen = base.FullScreen;
            this.graphics.ApplyChanges();

            this.playButton = new Button(this.graphics.GraphicsDevice, content.Load<Texture2D>("playButton"));
            this.optionButton = new Button(this.graphics.GraphicsDevice, content.Load<Texture2D>("optionButton"));
            this.quitButton = new Button(this.graphics.GraphicsDevice, content.Load<Texture2D>("quitButton"));
            this.newButton = new Button(this.graphics.GraphicsDevice, content.Load<Texture2D>("newButton"));
            this.resumeButton = new Button(this.graphics.GraphicsDevice, content.Load<Texture2D>("resumeButton"));
        }

        public void Update(GraphicsDevice device, MouseState mouse, GameState state)
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

            Matrix transform = this.camera.Transform;
        }

        public void Draw(GraphicsDevice device, SpriteBatch sb, GameState state)
        {
            if (state == GameState.MainMenu)
            {
                device.Clear(Color.Lerp(Color.White, Color.LightSlateGray, 0.5f));

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
        }

        public Button PlayButton { get { return this.playButton; } }

        public Button OptionButton { get { return this.optionButton; } }

        public Button QuitButton { get { return this.quitButton; } }

        public Button ResumeButton { get { return this.resumeButton; } }

        public Button NewButton { get { return this.newButton; } }
    }
}
