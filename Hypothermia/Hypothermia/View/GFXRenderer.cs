using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View
{
    public class GFXRenderer
    {
        private Camera camera;
        private GFX.SnowSimulation snowSimulation;
        private GFX.DepthSimulation depthSimulation;

        public GFXRenderer(Camera camera)
        {
            this.camera = camera;
            this.snowSimulation = new GFX.SnowSimulation(this.camera);
            this.depthSimulation = new GFX.DepthSimulation(this.camera);
        }

        public void LoadContent(ContentManager content, int level) 
        {
            switch (level)
            {
                case 1:
                    this.snowSimulation.LoadContent(content);
                    this.depthSimulation.Level1(content);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

        public void UpdatePregame(float elapsedTime) 
        {
            if (this.snowSimulation != null)
                this.snowSimulation.Update(elapsedTime);
        }

        public void UpdateIngame(float elapsedTime, Vector2 playerVelocity, Vector2 playerPosition)
        {
            if (this.snowSimulation != null)
                this.snowSimulation.Update(elapsedTime);

            if (this.depthSimulation.BackgroundDepth4 != null)
                foreach (GFX.Plane plane in this.depthSimulation.BackgroundDepth4)
                    plane.Update(playerVelocity, playerPosition, this.camera.MapWidth);

            if (this.depthSimulation.BackgroundDepth6 != null)
                foreach (GFX.Plane plane in this.depthSimulation.BackgroundDepth6)
                    plane.Update(playerVelocity, playerPosition, this.camera.MapWidth);
        }

        public void DrawForeground(SpriteBatch sb, Controller.GameState state)
        {
            if (this.snowSimulation != null)
                this.snowSimulation.Draw(sb);
        }

        public void DrawBackground(SpriteBatch sb, Controller.GameState state)
        {
            if (this.depthSimulation.BackgroundDepth6 != null && state != Controller.GameState.MainMenu)
                foreach (GFX.Plane plane in this.depthSimulation.BackgroundDepth6)
                    plane.Draw(sb);

            if (this.depthSimulation.BackgroundDepth4 != null && state != Controller.GameState.MainMenu)
                foreach (GFX.Plane plane in this.depthSimulation.BackgroundDepth4)
                    plane.Draw(sb);
        }
    }
}
