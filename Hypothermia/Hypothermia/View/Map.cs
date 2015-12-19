using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View
{
    public class Map
    {
        private Box[] boxes;
        private int width;
        private int height;

        private Texture2D boxTextures;

        public Map(Texture2D boxTextures)
        {
            this.boxTextures = boxTextures;
        }

        public void GenerateMap(int[,] map, int size)
        {
            List<Box> boxes = new List<Box>();
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if(number > 0)
                        boxes.Add(new View.Box(this.boxTextures, new Rectangle(x * size, y * size, size, size), this.Texture(number), 0, 0));

                    this.width = (x + 1) * size;
                    this.height = (y + 1) * size;
                }
            }
            this.boxes = boxes.ToArray();
        }


        public View.Box[] Boxes { get { return this.boxes; } }
        public int Width { get { return this.width; } }
        public int Height { get { return this.height; } }

        public Rectangle Texture(int number)
        {
            switch (number)
            {
                case 1:
                    return new Rectangle(0, 0, 64, 64);
                case 2:
                    return new Rectangle(64, 0, 64, 64);
                default:
                    throw new Exception();
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < this.boxes.Length; i++)
                this.boxes[i].Draw(sb);
        }
    }
}
