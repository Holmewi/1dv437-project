using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.Model
{
    public class Arrow : GameObject
    {
        private RigidBody rigidBody;

        private bool isVisible;
        private float size = 0.35f;
        private float shootTimer = 0.5f;

        public Arrow(Texture2D texture)
        {
            base.Texture = texture;
            base.Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.isVisible = false;

            this.rigidBody = new RigidBody(this, 0.05f, 1.0f);
        }

        public void Update(float elapsedTime, Vector2 playerPosition, List<View.Tile> tiles)
        {
            base.Position = base.Position + base.Velocity;

            if (Vector2.Distance(base.Position, playerPosition) > 800)
                this.IsVisible = false;

            for (int i = 0; i < tiles.Count; i++)
                if (base.Rect.Intersects(tiles[i].Collider.Rect))
                    this.isVisible = false;
            
            if (!this.rigidBody.OnGround)
                this.rigidBody.Fall(elapsedTime);
        }

        public void Draw(SpriteBatch sb)
        {
            float angle = (float)Math.Atan2(base.Velocity.Y, base.Velocity.X); 
            sb.Draw(base.Texture, base.Position, null, Color.White, angle, base.Origin, this.size, SpriteEffects.None, 0f);
        }

        public bool IsArrowDead()
        {
            if (!this.isVisible)
                return true;
            return false;
        }

        public float Size { get { return this.size; } }
        public float ShootTimer { get { return this.shootTimer; } }
        public bool IsVisible { 
            get { return this.isVisible; }
            set { this.isVisible = value; }
        }
    }
}
