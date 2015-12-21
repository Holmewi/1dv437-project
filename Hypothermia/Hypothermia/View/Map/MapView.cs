using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View.Map
{
    public class MapView
    {
        private Camera camera;
        private MapContent content;

        public MapView(Camera camera)
        {
            this.camera = camera;
            this.content = new MapContent(camera.TileSize);
        }

        public void LoadContent(ContentManager content, int level)
        {
            switch (level)
            {
                case 1:
                    this.content.Level1(content);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

        public void Update(Vector2 playerVelocity, float elapsedTime)
        {
            foreach (GFX.Background background in this.content.Backgrounds)
                background.Update(playerVelocity);
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (GFX.Background background in this.content.Backgrounds)
                background.Draw(sb);
            
            for (int i = 0; i < this.content.Tiles.Count(); i++)
                this.content.Tiles[i].Draw(sb);
        }

        public List<Tile> Tiles { get { return this.content.Tiles; } }
        public int Width { get { return this.content.Width; } }
        public int Height { get { return this.content.Height; } }
    }
}
