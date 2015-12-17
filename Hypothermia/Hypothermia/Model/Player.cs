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
            base.Position = new Vector2(100, 100);
            base.Velocity = new Vector2(0, 0);
            base.Acceleration = new Vector2(0.5f, 0.0f);

            this.rigidBody = new RigidBody(this);
            this.controller = new Controller.PlayerController(this);
        }

        public bool DetectCollision(BoxCollider boxCollider)
        {
            return this.rigidBody.CollisionDetection(boxCollider);
        }

        public void Update(float elapsedTime)
        {
            if (!this.controller.OnGround)
            {
                this.rigidBody.Fall(elapsedTime);
            }
            
            this.controller.Movement();
            

            base.Rect = new Rectangle((int)base.Position.X, (int)base.Position.Y, base.Texture.Width, base.Texture.Height);
            base.Position = base.Position + base.Velocity;
        }

        public bool OnGround { set { this.controller.OnGround = value; } }
    }
}
