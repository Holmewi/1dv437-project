using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hypothermia.View.GFX
{
    public class PlaneHandler
    {
        private List<Plane> foregroundDepth1 = new List<Plane>();
        private List<Plane> foregroundDepth2 = new List<Plane>();
        private List<Plane> foregroundDepth3 = new List<Plane>();
        private List<Plane> backgroundDepth4 = new List<Plane>();
        private List<Plane> backgroundDepth5 = new List<Plane>();
        private List<Plane> backgroundDepth6 = new List<Plane>();

        private Camera camera;

        private List<Texture2D> textures = new List<Texture2D>();
        private int tempWidth;

        public PlaneHandler(Camera camera)
        {
            this.camera = camera;
        }

        public void AddTextures(Texture2D texture)
        {
            this.textures.Add(texture);
        }

        public void GenerateDepth(int depth, float scrollingSpeed)
        {
            this.tempWidth = 0;
            int width = 0;

            foreach (Texture2D texture in this.textures)
            {
                width += texture.Width;   
            }

            double loop = (double)((this.camera.MapWidth + (this.camera.MaxOffset * 2) + (this.camera.MapWidth * scrollingSpeed)) / width);


            for (int i = 0; i <= Math.Ceiling(loop); i++)
            {
                foreach (Texture2D texture in this.textures)
                    this.AddToList(texture, depth, scrollingSpeed);
            }

            this.textures.Clear();
        }

        private void AddToList(Texture2D texture, int depth, float scrollingSpeed)
        {
            switch (depth)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    this.backgroundDepth4.Add(this.GetPlane(texture, scrollingSpeed));
                    break;
                case 5:
                    break;
                case 6:
                    this.backgroundDepth6.Add(this.GetPlane(texture, scrollingSpeed));
                    break;
                default:
                    break;
            }
        }

        public GFX.Plane GetPlane(Texture2D texture, float scrollingSpeed)
        {
            Vector2 position = this.camera.GetLogicCoordinates(this.tempWidth, texture.Height);
            this.tempWidth += texture.Width;

            return new GFX.Plane(texture, new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height), position, scrollingSpeed);
        }


        public void Update(Vector2 playerVelocity, Vector2 playerPosition)
        {
            if (this.backgroundDepth4 != null)
                foreach (GFX.Plane plane in this.backgroundDepth4)
                    plane.Update(playerVelocity, playerPosition, this.camera.MapWidth);

            if (this.backgroundDepth6 != null)
                foreach (GFX.Plane plane in this.backgroundDepth6)
                    plane.Update(playerVelocity, playerPosition, this.camera.MapWidth);
        }

        public void DrawForeground(SpriteBatch sb)
        {
            
        }

        public void DrawBackground(SpriteBatch sb)
        {
            if (this.backgroundDepth6 != null)
                foreach (GFX.Plane plane in this.backgroundDepth6)
                    plane.Draw(sb);

            if (this.backgroundDepth4 != null)
                foreach (GFX.Plane plane in this.backgroundDepth4)
                    plane.Draw(sb);
        }
    }
}
