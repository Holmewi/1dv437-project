using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View.GFX
{
    public class Plane
    {
        private Texture2D texture;
        private Rectangle rect;
        private float scrollingSpeed;

        public Plane(Texture2D texture, Rectangle rect, float scrollingSpeed)
        {
            this.texture = texture;
            this.rect = rect;
            this.scrollingSpeed = scrollingSpeed;
        }

        public void Update(Vector2 playerVelocity, Vector2 playerPosition, int mapWidth)
        {
            if(playerPosition.X > 150 && playerPosition.X < (float)mapWidth - 150)
                this.rect.X -= (int)Math.Round(playerVelocity.X * this.scrollingSpeed);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(this.texture, this.rect, Color.White);
        }

        public Rectangle Rect { set { this.rect = value; } }
    }
}
