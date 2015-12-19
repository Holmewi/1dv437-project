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
        private Texture2D boxTextures;

        private Map map;

        public GameView(GraphicsDevice device, ContentManager content)
        {
            this.boxTextures = content.Load<Texture2D>("tempTiles");
            this.map = new Map(this.boxTextures);

            LoadContent();
        }

        public void LoadContent()
        {
            map.GenerateMap(new int[,]{
                {0,0,0,0,0,2,2,0,0,0,0,0,0,2,2,0},
                {0,0,0,0,1,1,0,0,0,0,0,0,1,1,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
                {0,0,0,0,0,1,1,1,0,0,0,0,0,1,1,1},
                {1,1,1,1,1,1,0,0,1,1,1,1,1,1,0,0},
            }, 64);
        }

        public void Draw(SpriteBatch sb)
        {
            this.map.Draw(sb);
        }

        public View.Box[] Boxes { get { return this.map.Boxes; } }
        public int MapWidth { get { return this.map.Width; } }
        public int MapHeight { get { return this.map.Height; } }
    }
}
