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
        private Rectangle spriteRect;
        private Rectangle rect;

        private Model.BoxCollider boxCollider;

        /**
         * Constructor used to create a Box without a BoxCollider
         */
        public Box(Texture2D texture, Rectangle rect, Rectangle textureRect)
        {
            this.texture = texture;
            this.rect = rect;
            this.spriteRect = textureRect;
        }

        /**
         *  Constructor used to create a Box with a BoxCollider
         *  @param int startX - start of slope collision
         *  @param int endX - end of slope collison
         */
        public Box(Texture2D texture, Rectangle rect, Rectangle textureRect, int startX, int endX)
        {
            this.texture = texture;
            this.rect = rect;
            this.spriteRect = textureRect;

            this.boxCollider = new Model.BoxCollider(this.rect, startX, endX);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(this.texture, this.rect, this.spriteRect, Color.White);
        }

        public Texture2D Texture { get { return this.texture; } }
        public Rectangle TextureRect { get { return this.spriteRect; } }
        public Rectangle Rect { get { return this.rect; } }
        public Model.BoxCollider Collider { get { return this.boxCollider; } }
    }
}
