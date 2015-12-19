using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View
{
    public class Box
    {
        private Texture2D texture;
        private Rectangle textureRect;
        private Rectangle rect;

        private Model.BoxCollider boxCollider;

        /**
         * Constructor used with to create a Box without a BoxCollider
         */
        public Box(Texture2D texture, Rectangle rect, Rectangle textureRect)
        {
            this.texture = texture;
            this.rect = rect;
            this.textureRect = textureRect;
        }

        /**
         * Constructor used with to create a Box with a BoxCollider
         */
        public Box(Texture2D texture, Rectangle rect, Rectangle textureRect, int startX, int endX)
        {
            this.texture = texture;
            this.rect = rect;
            this.textureRect = textureRect;

            this.boxCollider = new Model.BoxCollider(this.rect, startX, endX);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(this.texture, this.rect, this.textureRect, Color.White);
        }

        public Texture2D Texture { get { return this.texture; } }
        public Rectangle TextureRect { get { return this.textureRect; } }
        public Rectangle Rect { get { return this.rect; } }
        public Model.BoxCollider Collider { get { return this.boxCollider; } }
    }
}
