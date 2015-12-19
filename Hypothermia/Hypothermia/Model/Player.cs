using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hypothermia.Model
{
    public class Player : GameObject
    {
        private RigidBody rigidBody;
        private Controller.PlayerController controller;

        public Player(Texture2D texture)
        {
            base.Texture = texture;
            base.Position = new Vector2(96, 100);
            base.Velocity = new Vector2(0, 0);
            base.Acceleration = new Vector2(0.5f, 0.0f);
            base.Rect = new Rectangle((int)Math.Round(base.Position.X) - base.Texture.Width / 2, (int)Math.Round(base.Position.Y) - base.Texture.Height, base.Texture.Width, base.Texture.Height);
            
            this.rigidBody = new RigidBody(this);
            this.controller = new Controller.PlayerController(this, this.rigidBody);
        }

        public void MapCollision(int mapWidth)
        {
            if (base.Position.X < 0) 
                base.PositionX = 0;
            if (base.Position.X > mapWidth)
                base.PositionX = mapWidth;
        }

        public void Update(float elapsedTime, View.Box[] boxes)
        {
            this.controller.Movement();
            
            base.Position = base.Position + base.Velocity;

            if (!this.rigidBody.OnGround)
            {
                this.rigidBody.Fall(elapsedTime);
            }

            this.rigidBody.DetectCollision(boxes);
        }
    }
}
