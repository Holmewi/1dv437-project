using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.Controller
{
    public class PlayerController
    {
        private bool onGround = false;

        private float movementSpeed = 5.0f;
        private float jumpingSpeed = 5.0f;

        private Model.GameObject gameObject;

        public PlayerController(Model.GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public void Movement()
        {
            if (this.onGround)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    this.Jump();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    this.MovingLeft();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    this.MovingRight();
                }
                else if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
                {
                    this.Idle();
                }
            }
        }

        public void MovingLeft()
        {
            if (this.gameObject.Velocity.X >= -movementSpeed)
                this.gameObject.VelocityX = this.gameObject.Velocity.X - this.gameObject.Acceleration.X;
        }

        public void MovingRight()
        {
            if (this.gameObject.Velocity.X <= movementSpeed)
                this.gameObject.VelocityX = this.gameObject.Velocity.X + this.gameObject.Acceleration.X;
        }

        public void Idle()
        {
            if(this.gameObject.Velocity.X < 0)
                this.gameObject.VelocityX = this.gameObject.Velocity.X + this.gameObject.Acceleration.X;
            else if(this.gameObject.Velocity.X > 0)
                this.gameObject.VelocityX = this.gameObject.Velocity.X - this.gameObject.Acceleration.X;
        }

        public void Jump()
        {
            this.gameObject.VelocityY = this.gameObject.Velocity.Y - jumpingSpeed;
        }

        public bool OnGround { 
            get { return this.onGround; } 
            set { this.onGround = value; } 
        }
    }
}
