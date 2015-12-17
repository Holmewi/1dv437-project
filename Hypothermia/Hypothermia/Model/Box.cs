using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.Model
{
    public class Box
    {
        private Texture2D texture;
        private Rectangle textureRect;
        private Rectangle rect;

        private BoxCollider boxCollider;

        public Box(Texture2D texture, Rectangle rect, Rectangle textureRect)
        {
            this.texture = texture;
            this.rect = rect;
            this.textureRect = textureRect;
        }

        public Box(Texture2D texture, Rectangle rect, Rectangle textureRect, int startX, int endX)
        {
            this.texture = texture;
            this.rect = rect;
            this.textureRect = textureRect;

            this.boxCollider = new BoxCollider(this, startX, endX);
        }

        public Texture2D Texture { get { return this.texture; } }
        public Rectangle TextureRect { get { return this.textureRect; } }
        public Rectangle Rect { get { return this.rect; } }
        public BoxCollider Collider { get { return this.boxCollider; } }
    }
}
