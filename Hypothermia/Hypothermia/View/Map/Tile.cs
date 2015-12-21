using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View.Map
{
    public class Tile
    {
        private Texture2D texture;
        private Rectangle spriteRect;
        private Rectangle rect;

        private Model.BoxCollider boxCollider;

        /**
         * Constructor used to create a Tile without a BoxCollider
         */
        public Tile(Texture2D texture, Rectangle rect, Rectangle spriteRect)
        {
            this.texture = texture;
            this.rect = rect;
            this.spriteRect = spriteRect;
        }

        /**
         *  Constructor used to create a Tile with a BoxCollider
         *  @param int startX - start of slope collision
         *  @param int endX - end of slope collison
         */
        public Tile(Texture2D texture, Rectangle rect, Rectangle textureRect, int startX, int endX)
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
