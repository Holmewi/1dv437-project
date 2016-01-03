using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View.GFX
{
    public class Plane : Model.GameObject
    {
        private float scrollingSpeed;

        public Plane(Texture2D texture, Rectangle rect, Vector2 position, float scrollingSpeed)
        {
            base.Texture = texture;
            base.Rect = rect;
            base.Position = position;
            this.scrollingSpeed = scrollingSpeed;
        }

        public void Update(Vector2 playerVelocity, Vector2 playerPosition, int mapWidth)
        {
            if (playerPosition.X > 150 && playerPosition.X < (float)mapWidth - 150)
                base.PositionX = base.Position.X - playerVelocity.X * this.scrollingSpeed;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(base.Texture, base.Position, Color.White);
        }
    }
}
