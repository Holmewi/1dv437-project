using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View.Map
{
    public class MapContent
    {
        private List<GFX.Background> backgrounds = new List<View.GFX.Background>();
        private List<Tile> tiles = new List<Tile>();

        private MapGenerator map;
        private int tileSize;

        public MapContent(int tileSize)
        {
            this.tileSize = tileSize;
            this.map = new MapGenerator();
        }

        public void Level1(ContentManager content)
        {
            this.backgrounds.Clear();
            this.tiles.Clear();

            Texture2D bg1 = content.Load<Texture2D>("Background/background1");
            Texture2D bg2 = content.Load<Texture2D>("Background/background2");
            Texture2D tempTiles = content.Load<Texture2D>("tempTiles");

            this.backgrounds.Add(new GFX.Background(bg1, new Rectangle(0, 0, bg1.Width, bg1.Height), 0.5f));
            this.backgrounds.Add(new GFX.Background(bg2, new Rectangle(bg1.Width, 0, bg1.Width, bg1.Height), 0.5f));

            this.tiles = this.map.GetTileList(new int[,]{
                {0,0,0,0,0,2,2,0,0,0,0,0,0,2,2,0,0,0,0,0,2,2,0,0,0,0,0,0,2,2,0},
                {0,0,0,0,1,1,0,0,0,0,0,0,1,1,0,0,0,0,0,0,2,2,0,0,0,0,0,0,2,2,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {0,0,0,0,0,1,1,1,0,0,0,0,0,1,1,1,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1},
                {1,1,1,1,1,1,0,0,1,1,0,0,0,1,0,0,1,1,1,1,1,0,0,1,1,1,1,1,1,0,0},
                {0,0,0,0,0,2,2,0,0,0,0,0,1,2,2,0,0,0,0,0,2,2,0,0,0,0,0,0,2,2,0},
                {0,0,0,0,1,1,0,0,0,0,1,0,1,1,0,0,0,0,0,0,2,2,0,0,0,0,0,0,2,2,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {0,0,0,0,0,1,1,1,0,0,0,0,0,1,1,1,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1},
                {1,1,1,1,1,1,0,0,1,1,1,1,1,1,0,0,1,1,1,1,1,0,0,1,1,1,1,1,1,0,0},
            }, tempTiles, this.tileSize);
        }

        public void Level2(ContentManager content)
        {

        }

        public void Level3(ContentManager content)
        {

        }

        public List<GFX.Background> Backgrounds { get { return this.backgrounds; } }
        public List<Tile> Tiles { get { return this.tiles; } }
        public int Width { get { return this.map.Width; } }
        public int Height { get { return this.map.Height; } }
    }
}
