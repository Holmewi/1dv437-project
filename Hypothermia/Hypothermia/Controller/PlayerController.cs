using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.Controller
{
    public class PlayerController
    {
        private bool isIdle = false;

        private float movementSpeed = 5.0f;
        private float jumpingSpeed = 8.0f;

        private Model.GameObject gameObject;
        private Model.RigidBody rigidBody;

        public PlayerController(Model.GameObject gameObject, Model.RigidBody rigidBody)
        {
            this.gameObject = gameObject;
            this.rigidBody = rigidBody;
        }

        public void Movement()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && this.rigidBody.OnGround)
            {
                this.Jump();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && !this.rigidBody.CollideLeft)
            {
                this.MoveLeft();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && !this.rigidBody.CollideRight)
            {
                this.MoveRight();
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D) && this.rigidBody.OnGround)
            {
                this.Idle();
            }
        }

        public void MoveLeft()
        {
            if (this.gameObject.Velocity.X >= -movementSpeed)
                this.gameObject.VelocityX = this.gameObject.Velocity.X - this.gameObject.Acceleration.X;
        }

        public void MoveRight()
        {
            if (this.gameObject.Velocity.X <= movementSpeed)
                this.gameObject.VelocityX = this.gameObject.Velocity.X + this.gameObject.Acceleration.X;
        }

        public void Jump()
        {
            this.gameObject.VelocityY = this.gameObject.Velocity.Y - jumpingSpeed;
        }

        public void Idle()
        {
            if (this.gameObject.Velocity.X < 0)
                this.gameObject.VelocityX = this.gameObject.Velocity.X + this.gameObject.Acceleration.X;
            else if (this.gameObject.Velocity.X > 0)
                this.gameObject.VelocityX = this.gameObject.Velocity.X - this.gameObject.Acceleration.X;
        }
    }
}
