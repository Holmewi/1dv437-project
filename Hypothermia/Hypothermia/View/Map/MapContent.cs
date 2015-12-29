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
        private List<Tile> tiles = new List<Tile>();

        private Camera camera;

        public MapContent(Camera camera)
        {
            this.camera = camera;
        }

        public void Level1(ContentManager content)
        {
            this.tiles.Clear();

            Texture2D tempTiles = content.Load<Texture2D>("TexturePacks/tileSpriteSheet");

              // 0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0
            this.tiles = base.GetTileList(new int[,]{
                {5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},    // 0
                {5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},    // 1
                {5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},    // 2
                {5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},    // 3
                {5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},    // 4
                {5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},    // 5
                {5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},    // 6
                {5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},    // 7
                {5,0,0,0,0,1,2,2,4,5,0,1,5,0,0,1,5,0,0,0,0,1,2,4,5,0,0,0,0,0,1},    // 8
                {5,0,1,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,5,1},    // 9
                {5,0,0,0,1,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,5,0,0,0,1},    // 0
                {5,0,0,0,0,0,1,5,0,0,0,0,0,0,0,1,4,2,5,0,0,1,5,0,0,0,0,0,0,0,1},    // 1
                {5,0,0,0,0,0,0,0,1,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},    // 2
                {5,0,0,0,0,0,0,0,0,0,0,1,3,4,2,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},    // 3
                {5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,5,0,0,0,0,0,0,0,0,0,0,0,1},    // 4
                {5,0,0,0,0,0,0,0,1,3,2,4,2,2,3,3,5,0,0,0,0,0,0,0,0,0,0,0,0,0,1},    // 5
                {5,1,3,4,3,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,4,2,2,3,5,1},    // 6
            }, tempTiles, this.camera.TileSize);

            this.camera.MapWidth = base.MapWidth;
            this.camera.MapHeight = base.MapHeight;
        }

        public void Level2(ContentManager content)
        {

        }

        public void Level3(ContentManager content)
        {

        }

        public List<Tile> Tiles { get { return this.tiles; } }
    }
}
