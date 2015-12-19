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
        private Model.Box[] boxes = new Model.Box[20];
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
            var count = 0;
            int j = 3;
            for (int i = 0; i < this.boxes.Length; i++)
            {
                
                if (count == 4)
                {
                    this.boxes[i] = new Model.Box(this.tiles, new Rectangle(64 * i, 350, 64, 64), this.collider, 0, 0);
                }
                else
                {
                    //int start = 4 * i;          //  0, 4, 8, 12
                    //int end = start + 2 + i;    //  3, 7, 11, 15
                    int start = 0;
                    int end = 0;
                    if (count <= 10)
                    {
                        this.boxes[i] = new Model.Box(this.tiles, new Rectangle(64 * i, 400, 64, 64), this.collider, start, end);
                    }

                    else
                    {
                        this.boxes[i] = new Model.Box(this.tiles, new Rectangle(64 * j, 200, 64, 64), this.collider, start, end);
                        j++;
                    }
                    
                }
                
                count++;
                
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
