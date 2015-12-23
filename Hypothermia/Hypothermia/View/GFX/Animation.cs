using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hypothermia.View.GFX
{
    public class Animation
    {
        private Texture2D texture;
        private Rectangle rect;
        private Vector2 origin;

        private int currentFrame;
        private int frameWidth;
        private int frameHeight;
        private int spriteLine;

        private float timer;
        private float interval = 0.01f;

        public Animation(Texture2D texture, int framesX, int framesY)
        {
            this.texture = texture;
            this.frameWidth = this.texture.Width / framesX;
            this.frameHeight = this.texture.Height / framesY;
        }

        public void Update()
        {
            this.rect = new Rectangle(this.currentFrame * this.frameWidth, this.frameHeight * this.spriteLine, this.frameWidth, this.frameHeight);
            this.origin = new Vector2(this.rect.Width / 2, this.rect.Height);
        }

        public void Animate(float elapsetTime, int spriteLine, int firstFrame, int lastFrame, float interval)
        {
            this.spriteLine = spriteLine - 1;
            this.interval = interval;
            timer += elapsetTime / 2;
            if (timer > interval)
            {
                this.currentFrame++;
                timer = 0;
                if (this.currentFrame < firstFrame - 1 || this.currentFrame > lastFrame - 1)
                    this.currentFrame = firstFrame - 1;
            }
        }

        public Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        public Rectangle Rect
        {
            get { return this.rect; }
            set { this.rect = value; }
        }

        public Vector2 Origin
        {
            get { return this.origin; }
            set { this.origin = value; }
        }

        public int CurrentFrame
        {
            get { return this.currentFrame; }
            set { this.currentFrame = value; }
        }

        public int FrameWidth
        {
            get { return this.frameWidth; }
            set { this.frameWidth = value; }
        }

        public int FrameHeight
        {
            get { return this.frameHeight; }
            set { this.frameHeight = value; }
        }

        public int SpriteLine
        {
            get { return this.spriteLine; }
            set { this.spriteLine = value; }
        }
    }
}
