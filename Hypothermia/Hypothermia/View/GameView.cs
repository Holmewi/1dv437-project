using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hypothermia.View
{
    public class GameView
    {
        private Model.Box[] boxes = new Model.Box[10];
        private Texture2D tiles;

        private Rectangle noCol = new Rectangle(0, 0, 64, 64);
        private Rectangle collider = new Rectangle(64, 0, 64, 64);

        public GameView(GraphicsDevice device, ContentManager content)
        {
            this.tiles = content.Load<Texture2D>("tempTiles");
            LoadContent();
        }

        public void LoadContent()
        {
            for (int i = 0; i < 10; i++)
            {
                this.boxes[i] = new Model.Box(this.tiles, new Rectangle(64 * i, 400, 64, 64), this.collider, 0, 0);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Model.Box box in this.boxes)
            {
                spriteBatch.Draw(box.Texture, box.Rect, box.TextureRect, Color.White);
            }
        }

        public Model.Box[] Boxes { get { return this.boxes; } }
    }
}
