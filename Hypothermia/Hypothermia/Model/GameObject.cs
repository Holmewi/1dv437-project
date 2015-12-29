using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.Model
{
    public class GameObject
    {
        private Texture2D texture;
        private Rectangle rect;

        private Vector2 position;
        private Vector2 velocity;
        private Vector2 acceleration;
        private Vector2 origin;

        private float mass;
        private float frontArea;
        private float radius;

        public GameObject()
        {
            
        }

        public GameObject(Texture2D texture, Rectangle rect)
        {
            this.texture = texture;
            this.rect = rect;
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { this.texture = value; }
        }

        public Rectangle Rect
        {
            get { return rect; }
            set { this.rect = value; }
        }
       
        public Vector2 Position { 
            get { return position; }
            set { 
                this.position = value;
                this.rect.X = (int)Math.Round(value.X) - this.rect.Width / 2;
                this.rect.Y = (int)Math.Round(value.Y) - this.rect.Height;
            }
        }

        public float PositionX { set { this.position.X = value;  } }
        public float PositionY { set { this.position.Y = value; } }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { this.velocity = value; }
        }

        public float VelocityX { set { this.velocity.X = value; } }
        public float VelocityY { set { this.velocity.Y = value; } }

        public Vector2 Acceleration
        {
            get { return acceleration; }
            set { this.acceleration = value; }
        }

        public float AccelerationX { set { this.acceleration.X = value; } }
        public float AccelerationY { set { this.acceleration.Y = value; } }

        public Vector2 Origin
        {
            get { return origin; }
            set { this.origin = value; }
        }

        public float Mass
        {
            get { return mass; }
            set { this.mass = value; }
        }

        public float FrontArea
        {
            get { return frontArea; }
            set { this.frontArea = value; }
        }

        public float Radius
        {
            get { return radius; }
            set { this.radius = value; }
        }
    }
}
