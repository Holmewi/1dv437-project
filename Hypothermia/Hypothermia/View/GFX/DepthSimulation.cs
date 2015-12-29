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
    public class DepthSimulation
    {
        private List<Plane> foregroundDepth1 = new List<Plane>();
        private List<Plane> foregroundDepth2 = new List<Plane>();
        private List<Plane> foregroundDepth3 = new List<Plane>();
        private List<Plane> backgroundDepth4 = new List<Plane>();
        private List<Plane> backgroundDepth5 = new List<Plane>();
        private List<Plane> backgroundDepth6 = new List<Plane>();

        private Camera camera;

        private int tempWidth = 0;

        public DepthSimulation(Camera camera)
        {
            this.camera = camera;
        }

        private void ClearLists()
        {
            this.foregroundDepth1.Clear();
            this.foregroundDepth2.Clear();
            this.foregroundDepth3.Clear();
            this.backgroundDepth4.Clear();
            this.backgroundDepth5.Clear();
            this.backgroundDepth6.Clear();
        }

        public void Level1(ContentManager content)
        {
            this.ClearLists();
            double loop;
            float scrollingSpeed;

            Texture2D bgDepth4 = content.Load<Texture2D>("Planes/bgDepth4");

            scrollingSpeed = 0.4f;
            loop = (double)((this.camera.MapWidth + 300 + (this.camera.MapWidth * scrollingSpeed)) / bgDepth4.Width);
            this.tempWidth = 0;

            for (int i = 0; i <= Math.Ceiling(loop); i++)
                this.AddToList(bgDepth4, 4, scrollingSpeed);
            

            Texture2D bgDepth6a = content.Load<Texture2D>("Planes/bgDepth6a");
            Texture2D bgDepth6b = content.Load<Texture2D>("Planes/bgDepth6b");

            
            scrollingSpeed = 0.1f;
            loop = (double)((this.camera.MapWidth + 300 + (this.camera.MapWidth * scrollingSpeed)) / (bgDepth6a.Width + bgDepth6b.Width));
            this.tempWidth = 0;

            for (int i = 0; i <= Math.Ceiling(loop); i++)
            {
                if (i % 2 == 0)
                    this.AddToList(bgDepth6a, 6, scrollingSpeed);
                else if (i % 2 != 0)
                    this.AddToList(bgDepth6b, 6, scrollingSpeed);
            }
            Debug.WriteLine(this.camera.MapWidth);
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

            return new GFX.Plane(texture, new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height), scrollingSpeed);
        }

        public void Update()
        {

        }

        public List<Plane> ForegroundDepth1 { get { return this.foregroundDepth1; } }
        public List<Plane> ForegroundDepth2 { get { return this.foregroundDepth2; } }
        public List<Plane> ForegroundDepth3 { get { return this.foregroundDepth2; } }
        public List<Plane> BackgroundDepth4 { get { return this.backgroundDepth4; } }
        public List<Plane> BackgroundDepth5 { get { return this.backgroundDepth5; } }
        public List<Plane> BackgroundDepth6 { get { return this.backgroundDepth6; } }

    }
}
