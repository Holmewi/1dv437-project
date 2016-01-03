using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hypothermia.View
{
    public class Animation
    {
        private Model.GameObject gameObject;
        private Texture2D texture;
        private Rectangle rect;
        private Vector2 origin;

        private int currentFrame;
        private int frameWidth;
        private int frameHeight;
        private int spriteLine;

        private float timer;
        private float interval = 0.01f;

        public Animation(Model.GameObject gameObject, Texture2D texture, int framesX, int framesY)
        {
            this.gameObject = gameObject;
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

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(this.texture, this.gameObject.Position, this.Rect, Color.White, 0f, this.Origin, 1f, SpriteEffects.None, 0f);
        }

        public Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        public Rectangle Rect { get { return this.rect; } }

        public Vector2 Origin { get { return this.origin; } }

        public int FrameWidth { get { return this.frameWidth; } }

        public int FrameHeight { get { return this.frameHeight; } }
    }
}
