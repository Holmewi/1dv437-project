using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View.Map
{
    public class MapContent : MapGenerator
    {
        private List<GFX.Background> backgrounds = new List<View.GFX.Background>();
        private List<Tile> tiles = new List<Tile>();

        private Camera camera;

        public MapContent(Camera camera)
        {
            this.camera = camera;
        }

        public void Level1(ContentManager content)
        {
            this.backgrounds.Clear();
            this.tiles.Clear();

            Texture2D bg1 = content.Load<Texture2D>("Backgrounds/background1");
            Texture2D bg2 = content.Load<Texture2D>("Backgrounds/background2");
            Texture2D tempTiles = content.Load<Texture2D>("tempTiles");

            this.tiles = base.GetTileList(new int[,]{
                {0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,2,2,0,0,0,0,0,0,2,2,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,2,2,0,0,0,0,0,0,2,2,0},
                {0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {0,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {0,0,1,0,0,0,0,1,0,0,0,0,0,1,1,1,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1},
                {0,0,0,0,0,0,1,0,0,0,0,0,0,2,2,0,0,0,0,0,2,2,0,0,0,0,0,0,2,2,0},
                {0,0,0,0,0,1,1,1,1,1,0,0,1,1,0,0,0,0,0,0,2,2,0,0,0,0,0,0,2,2,0},
                {0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {0,0,1,0,0,1,1,1,0,0,0,0,0,1,1,1,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1},
                {1,1,1,1,1,1,0,0,1,1,0,0,0,1,0,0,1,1,1,1,1,0,0,1,1,1,1,1,1,0,0},
                {0,0,0,0,0,2,2,0,0,0,0,0,1,2,2,0,0,0,0,0,2,2,0,0,0,0,0,0,2,2,0},
                {0,0,0,0,1,1,0,0,0,0,0,0,1,1,0,0,0,0,0,0,2,2,0,0,0,0,0,0,2,2,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {0,0,0,0,0,1,1,1,0,0,0,0,0,1,1,1,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1},
                {1,1,1,1,1,1,0,0,1,1,1,1,1,1,0,0,1,1,1,1,1,0,0,1,1,1,1,1,1,0,0},
            }, tempTiles, this.camera.TileSize);

            this.camera.MapWidth = base.MapWidth;
            this.camera.MapHeight = base.MapHeight;

            Vector2 bg1Position = this.camera.GetLogicCoordinates(0, bg1.Height);
            Vector2 bg2Position = this.camera.GetLogicCoordinates(bg1.Width, bg1.Height);

            this.backgrounds.Add(new GFX.Background(bg1, new Rectangle((int)bg1Position.X, (int)bg1Position.Y, bg1.Width, bg1.Height), 0.5f));
            this.backgrounds.Add(new GFX.Background(bg2, new Rectangle((int)bg2Position.X, (int)bg2Position.Y, bg1.Width, bg1.Height), 0.5f));
        }

        public void Level2(ContentManager content)
        {

        }

        public void Level3(ContentManager content)
        {

        }

        public List<GFX.Background> Backgrounds { get { return this.backgrounds; } }
        public List<Tile> Tiles { get { return this.tiles; } }
    }
}
