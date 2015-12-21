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
    public class Button
    {
        private Texture2D texture;
        private Rectangle rect;
        private Vector2 position;
        private int width;
        private int height;

        private Color color = new Color(255, 255, 255, 255);

        private bool isDown;
        private bool isClicked;

        public Button(GraphicsDevice graphics, Texture2D texture)
        {
            this.texture = texture;

            this.width = this.texture.Width;
            this.height = this.texture.Height;
        }

        public void Update(MouseState mouse, Vector2 logicCoords)
        {
            this.rect = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);

            Rectangle mouseRect = new Rectangle((int)logicCoords.X + mouse.X, (int)logicCoords.Y + mouse.Y, 1, 1);

            if (mouseRect.Intersects(this.rect))
            {
                if (this.color.A == 255)
                    this.isDown = false;
                if (this.color.A == 0)
                    this.isDown = true;
                if (this.isDown)
                    this.color.A += 3;
                else
                    color.A -= 3;
                if (mouse.LeftButton == ButtonState.Pressed)
                    isClicked = true;
            }
            else if (color.A < 255)
            {
                color.A += 3;
                this.isClicked = false;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(this.texture, this.rect, this.color);
        }

        public Vector2 Position {
            get { return this.position;  }
            set { this.position = value; } 
        }

        public Texture2D Texture { get { return this.texture; } }

        public bool IsDown { get { return this.isDown; } }

        public bool IsClicked { 
            get { return this.isClicked; } 
            set { this.isClicked = value; } 
        }
    }
}
