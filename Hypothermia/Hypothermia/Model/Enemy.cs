using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hypothermia.Model
{
    public enum EnemyState
    {
        Idle,
        MoveLeft,
        MoveRight,
        Jump,
        Fall,
        Dead
    }

    public class Enemy : GameObject
    {
        public EnemyState CurrentEnemyState = EnemyState.MoveRight;

        private EnemyType type;
        private RigidBody rigidBody;

        private int health;
        private bool detectCollision = true;

        public Enemy(EnemyType type, Texture2D texture, Vector2 position)
        {
            this.type = type;
            this.health = type.MaxLife;
            base.Texture = texture;
            base.Position = position;
            base.Velocity = type.MovementSpeed;
            base.Acceleration = type.Acceleration;
            base.Rect = new Rectangle((int)Math.Round(base.Position.X) - base.Texture.Width / 2, 
                                      (int)Math.Round(base.Position.Y) - base.Texture.Height,
                                      base.Texture.Width, base.Texture.Height);

            if(type.IsRigidBody)
                this.rigidBody = new RigidBody(this, type.Mass, type.FrontArea);
        }

        public void Update(float elapsedTime, List<View.Map.Tile> tiles)
        {
            base.Position = base.Position + base.Velocity * (elapsedTime * 10);

            if (this.health <= 0)
                this.detectCollision = false;

            if (!this.rigidBody.OnGround || !this.detectCollision)
                this.rigidBody.Fall(elapsedTime);

            if (this.detectCollision)
            {
                if (base.Velocity.Y == 0)
                    this.rigidBody.IsOnGround(tiles);
                if (base.Velocity.X >= 0)
                    if (this.rigidBody.DetectRightCollision(tiles))
                        CurrentEnemyState = EnemyState.MoveLeft;
                if (base.Velocity.X <= 0)
                    if (this.rigidBody.DetectLeftCollision(tiles))
                        CurrentEnemyState = EnemyState.MoveRight;
                if (base.Velocity.Y > 0)
                    this.rigidBody.DetectBottomCollision(tiles);
                if (base.Velocity.Y < 0)
                    this.rigidBody.DetectTopCollision(tiles);
            }
        }

        public void MoveLeft()
        {
            if (base.Velocity.X >= -this.type.MovementSpeed.X)
                base.VelocityX = base.Velocity.X - base.Acceleration.X;
        }

        public void MoveRight()
        {
            if (base.Velocity.X <= this.type.MovementSpeed.X)
                base.VelocityX = base.Velocity.X + base.Acceleration.X;
        }
        

        public void Draw(SpriteBatch sb)
        {
            Vector2 temp = new Vector2(base.Texture.Width / 2, base.Texture.Height);
            sb.Draw(base.Texture, base.Position, null, Color.White, 0f, temp, 1f, SpriteEffects.None, 0f);
        }

        public RigidBody RigidBody { get { return this.rigidBody; } }
        public EnemyType Type { get { return this.type; } }

        public int Health
        {
            get { return this.health; }
            set { this.health = value; }
        }

        public EnemyState EnemyState { get { return this.CurrentEnemyState; } }
    }
}
