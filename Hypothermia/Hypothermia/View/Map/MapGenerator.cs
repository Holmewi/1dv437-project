using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View.Map
{
    public class MapGenerator
    {
        private int mapWidth;
        private int mapHeight;

        public List<Tile> GetTileList(int[,] map, Texture2D tileTextures, int size)
        {
            List<Tile> tiles = new List<Tile>();
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if(number > 0)
                        tiles.Add(new Tile(tileTextures, new Rectangle(x * size, y * size, size, size), this.Sprite(number), 0, 0));

                    this.mapWidth = (x + 1) * size;
                    this.mapHeight = (y + 1) * size;
                }
            }
            return tiles;
        }

        public Rectangle Sprite(int number)
        {
            switch (number)
            {
                case 1:
                    return new Rectangle(0, 0, 64, 64);
                case 2:
                    return new Rectangle(64, 0, 64, 64);
                default:
                    throw new ArgumentOutOfRangeException("The texture didn't exist in the sprite sheet");
            }
        }

        public int MapWidth { get { return this.mapWidth; } }
        public int MapHeight { get { return this.mapHeight; } }
    }
}
