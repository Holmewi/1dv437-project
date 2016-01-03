using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        public PlayerState CurrentPlayerState = PlayerState.Idle;
        private ContentManager content;

        private List<Arrow> arrows = new List<Arrow>();

        private View.Camera camera;
        private View.Animation animation;
        private RigidBody rigidBody;
        

        private float movementSpeed = 3.0f;
        private float jumpingSpeed = 8.0f;
        private bool faceForward = true;
        private bool isSprinting = false;

        private int lives = 3;
        private int health = 100;
        private float shootTimer = 0;

        public Player(ContentManager content, View.Camera camera)
        {
            this.content = content;

            this.camera = camera;
            this.animation = new View.Animation(this, content.Load<Texture2D>("TexturePacks/playerAnimation"), 15, 5);
            
            base.Acceleration = new Vector2(0.3f, 0.0f);
            base.Rect = new Rectangle((int)Math.Round(base.Position.X) - this.animation.FrameWidth / 2, 
                                      (int)Math.Round(base.Position.Y) - this.animation.FrameHeight, 
                                      this.animation.FrameWidth, this.animation.FrameHeight);

            this.rigidBody = new RigidBody(this, 70f, 5.5f);
        }

        public void Update(float elapsedTime, List<View.Tile> tiles)
        {
            this.UpdateAnimation(elapsedTime);
            this.MapCollision(this.camera.MapWidth, this.camera.MapHeight);

            base.Position = base.Position + base.Velocity;

            if (this.health <= 0)
                this.CurrentPlayerState = PlayerState.Dead;

            if (CurrentPlayerState != PlayerState.Dead)
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
            else
                this.rigidBody.Fall(elapsedTime);

            for (int i = 0; i < this.arrows.Count; i++)
            {
                if (this.arrows != null)
                {
                    this.arrows[i].Update(elapsedTime, base.Position, tiles);

                    if (this.arrows[i].IsArrowDead())
                    {
                        arrows.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (this.shootTimer > 0)
                this.shootTimer -= elapsedTime;
        }

        public void Draw(SpriteBatch sb)
        {
            this.animation.Draw(sb);
            for (int i = 0; i < this.arrows.Count; i++)
                this.arrows[i].Draw(sb);
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
            this.isSprinting = isSprinting;
            
            if (isSprinting)
                this.movementSpeed = 5.0f;
            else
                this.movementSpeed = 3.0f;
        }

        public void MeleeAttack()
        {

        }

        public void RangeAttack()
        {
            Arrow arrow = new Arrow(this.content.Load<Texture2D>("arrow"));
            this.shootTimer = arrow.ShootTimer;

            if(this.faceForward) {
                arrow.VelocityX = 4.0f + base.Velocity.X;      //new Vector2(5.0f, -0.5f);
                arrow.VelocityY = -1.0f;      //new Vector2(5.0f, -0.5f);
                arrow.PositionX = base.Position.X + arrow.Velocity.X * 5;
                arrow.PositionY = (base.Position.Y - base.Rect.Height / 2) + arrow.Velocity.Y * 5;
                
            }
            else if (!this.faceForward)
            {
                arrow.VelocityX = -4.0f + base.Velocity.X;      //new Vector2(5.0f, -0.5f);
                arrow.VelocityY = -1.0f;      //new Vector2(5.0f, -0.5f);
                arrow.PositionX = base.Position.X + arrow.Velocity.X * 5;
                arrow.PositionY = (base.Position.Y - base.Rect.Height / 2) + arrow.Velocity.Y * 5;
            }

            arrow.Rect = new Rectangle((int)Math.Round((arrow.Position.X - arrow.Texture.Width / 2) * arrow.Size),
                                      (int)Math.Round((arrow.Position.Y - arrow.Texture.Height / 2) * arrow.Size),
                                      (int)Math.Round(arrow.Texture.Width * arrow.Size), (int)Math.Round(arrow.Texture.Height * arrow.Size));

            arrow.IsVisible = true;

            if (this.arrows.Count < 20)
                this.arrows.Add(arrow);
        }

        public void Combat(Enemy enemy)
        {
            if (base.Rect.Intersects(enemy.Rect) && enemy.Health > 0)
                this.Health = 0;

            for (int i = 0; i < this.arrows.Count; i++)
            {
                if (this.arrows[i].Rect.Intersects(enemy.Rect))
                {
                    enemy.Health = enemy.Health - 25;
                    this.arrows[i].IsVisible = false;
                }
            }
        }

        public void UpdateAnimation(float elapsedTime)
        {
            this.animation.Update();

            if (CurrentPlayerState == PlayerState.Jump)
            {
                if (this.faceForward)
                    this.animation.Animate(elapsedTime, 3, 1, 9, 0.05f);
                else
                    this.animation.Animate(elapsedTime, 4, 1, 9, 0.05f);
            }

            else if (CurrentPlayerState == PlayerState.Fall || CurrentPlayerState == PlayerState.Dead)
            {
                if (this.faceForward)
                    this.animation.Animate(elapsedTime, 3, 9, 13, 0.05f);
                else
                    this.animation.Animate(elapsedTime, 4, 9, 13, 0.05f);
            }

            else if (CurrentPlayerState == PlayerState.MoveLeft)
                this.animation.Animate(elapsedTime, 2, 3, 12, 0.03f);

            else if (CurrentPlayerState == PlayerState.MoveRight)
                this.animation.Animate(elapsedTime, 1, 3, 12, 0.03f);

            else if (CurrentPlayerState == PlayerState.Idle)
            {
                if (this.faceForward)
                    this.animation.Animate(elapsedTime, 5, 1, 6, 0.03f);
                else
                    this.animation.Animate(elapsedTime, 5, 7, 12, 0.03f);
            }
        }

        public RigidBody RigidBody { get { return this.rigidBody; } }
        public bool FaceForward { get { return this.faceForward; } }
        public bool IsSprinting { get { return this.isSprinting; } }

        public int Health { 
            get { return this.health; }
            set { this.health = value; }
        }

        public float ShootTimer { get { return this.shootTimer; } }

        public int Lives {
            get { return this.lives; }
            set { this.lives = value; }
        }
    }
}
