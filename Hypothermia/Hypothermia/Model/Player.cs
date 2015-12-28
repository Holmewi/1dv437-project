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
    public enum PlayerState
    {
        Idle,
        MoveLeft,
        MoveRight,
        Jump,
        Fall,
        Dead
    }

    public class Player : GameObject
    {
        private RigidBody rigidBody;

        public PlayerState CurrentPlayerState = PlayerState.Idle;

        private float movementSpeed = 3.0f;
        private float jumpingSpeed = 8.0f;
        private bool faceForward = true;

        private int health = 100;

        public Player(int width, int height)
        {
            //base.Position = new Vector2(30,570);
            base.Position = new Vector2(900, 500);
            base.Acceleration = new Vector2(0.3f, 0.0f);
            base.Rect = new Rectangle((int)Math.Round(base.Position.X) - width / 2, (int)Math.Round(base.Position.Y) - height, width, height);

            this.rigidBody = new RigidBody(this, 70f, 5.5f);
        }

        public void MapCollision(int mapWidth, int mapHeight)
        {
            if (base.Position.X < 0) 
                base.PositionX = 0;
            if (base.Position.X > mapWidth)
                base.PositionX = mapWidth;
            if (base.Position.Y > mapHeight)
                this.health = 0;
        }

        public void Update(float elapsedTime, List<View.Map.Tile> tiles)
        {
            base.Position = base.Position + base.Velocity;

            if (this.health <= 0)
                this.CurrentPlayerState = PlayerState.Dead;
            else
            {
                if (!this.rigidBody.OnGround)
                {
                    this.rigidBody.Fall(elapsedTime);
                    if (base.Velocity.Y <= 0)
                        this.CurrentPlayerState = PlayerState.Jump;
                    else if (base.Velocity.Y > 0)
                        this.CurrentPlayerState = PlayerState.Fall;
                }
                
                if (base.Velocity.Y == 0)
                    this.rigidBody.IsOnGround(tiles);
                if (base.Velocity.X >= 0)
                    this.rigidBody.DetectRightCollision(tiles);
                if (base.Velocity.X <= 0)
                    this.rigidBody.DetectLeftCollision(tiles);
                if (base.Velocity.Y > 0)
                    this.rigidBody.DetectBottomCollision(tiles);
                if (base.Velocity.Y < 0)
                    this.rigidBody.DetectTopCollision(tiles);
            }  
        }

        public void MoveLeft()
        {
            if (base.Velocity.X >= -movementSpeed)
                base.VelocityX = base.Velocity.X - base.Acceleration.X;
            if (this.rigidBody.OnGround)
                this.CurrentPlayerState = PlayerState.MoveLeft;
            this.faceForward = false;
        }

        public void MoveRight()
        {
            if (base.Velocity.X <= movementSpeed)
                base.VelocityX = base.Velocity.X + base.Acceleration.X;
            if(this.rigidBody.OnGround)
                this.CurrentPlayerState = PlayerState.MoveRight;
            this.faceForward = true;
        }

        public void Jump(float elapsedTime)
        {
            base.VelocityY = base.Velocity.Y - jumpingSpeed;
        }

        public void Idle()
        {
            if (base.Velocity.X > -base.Acceleration.X && base.Velocity.X < base.Acceleration.X) {
                base.VelocityX = 0;
                this.CurrentPlayerState = PlayerState.Idle;
            }
            else if (base.Velocity.X < 0)
                base.VelocityX = base.Velocity.X + base.Acceleration.X;
            else if (base.Velocity.X > 0)
                base.VelocityX = base.Velocity.X - base.Acceleration.X;
        }

        public void Sprint(bool isSprinting)
        {
            if (isSprinting)
                this.movementSpeed = 5.0f;
            else
                this.movementSpeed = 3.0f;
        }

        public RigidBody RigidBody { get { return this.rigidBody; } }
        public bool FaceForward { get { return this.faceForward; } }
        public int Health { get { return this.health; } }
        public PlayerState PlayerState { get { return this.CurrentPlayerState; } }
    }
}
