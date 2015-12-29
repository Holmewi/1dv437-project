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
        private GFX.SnowSystem snowSystem;

        public GFXRenderer(Camera camera)
        {
            this.camera = camera;
            this.snowSystem = new GFX.SnowSystem(this.camera);
        }

        public void LoadContent(ContentManager content, int level) 
        {
            switch (level)
            {
                case 1:
                    this.snowSystem.LoadContent(content);
                    // TODO: Add a background render system
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

        public void Update(float elapsedTime)
        {
            if (this.snowSystem != null)
                this.snowSystem.Update(elapsedTime);
        }

        public void DrawForeground(SpriteBatch sb)
        {
            if (this.snowSystem != null)
                this.snowSystem.Draw(sb);
        }

        public void DrawBackground(SpriteBatch sb)
        {
            
        }
    }
}
