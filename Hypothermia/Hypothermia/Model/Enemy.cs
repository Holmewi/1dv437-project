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
        FlyUp,
        FlyDown,
        Jump,
        Fall,
        Dead
    }

    public class Enemy : GameObject
    {
        public EnemyState CurrentEnemyState = EnemyState.MoveRight;

        private Collection.EnemyType type;
        private RigidBody rigidBody;
        private Rectangle pathFindingRect;

        private int health;

        public Enemy(Collection.EnemyType type, Texture2D texture, Vector2 position)
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

        public void Update(float elapsedTime, List<View.Tile> tiles, int width, int height, int tileSize)
        {
            this.MapCollision(width, height);

            if (CurrentEnemyState == EnemyState.MoveLeft)
                this.MoveLeft();
            else if (CurrentEnemyState == EnemyState.MoveRight)
                this.MoveRight();

            base.Position = base.Position + base.Velocity * (elapsedTime * 10);

            if (this.health <= 0)
                CurrentEnemyState = EnemyState.Dead;

            if (!this.type.IsFlying && !this.rigidBody.OnGround || CurrentEnemyState == EnemyState.Dead)
                this.rigidBody.Fall(elapsedTime);


            if (CurrentEnemyState != EnemyState.Dead)
            {
                if (this.type.BoundToPlatform)
                    this.PathFinding(tiles, width, height, tileSize);
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

        private void MoveLeft()
        {
            if (base.Velocity.X >= -this.type.MovementSpeed.X)
                base.VelocityX = base.Velocity.X - base.Acceleration.X;
        }

        private void MoveRight()
        {
            if (base.Velocity.X <= this.type.MovementSpeed.X)
                base.VelocityX = base.Velocity.X + base.Acceleration.X;
        }

        private void MapCollision(int width, int height)
        {
            if (base.Position.X < 0)
                CurrentEnemyState = EnemyState.MoveRight;
            if (base.Position.X > width)
                CurrentEnemyState = EnemyState.MoveLeft;
            if (base.Position.Y > height)
                this.health = 0;
        }

        /**
         *  @author Pikuchan
         *  Source https://joshcodev.wordpress.com/2013/11/08/xna-basic-enemy-ai/
         */
        private void PathFinding(List<View.Tile> tiles, int width, int height, int tileSize)
        {
            bool endOfPath = true;

            if (CurrentEnemyState == EnemyState.MoveRight)
                this.pathFindingRect = new Rectangle((int)base.Position.X - base.Texture.Width / 2 + tileSize,
                                                        (int)base.Rect.Y + 1 + tileSize, tileSize, tileSize);

            else if (CurrentEnemyState == EnemyState.MoveLeft)
                this.pathFindingRect = new Rectangle((int)base.Position.X - base.Texture.Width / 2 - tileSize,
                                                        (int)base.Position.Y - tileSize + 1, tileSize, tileSize);

            for (var i = 0; i < tiles.Count(); i++)
                if (this.pathFindingRect.Intersects(tiles[i].Rect))
                    endOfPath = false;

            if (endOfPath)
                if (CurrentEnemyState == EnemyState.MoveLeft)
                    CurrentEnemyState = EnemyState.MoveRight;
                else if (CurrentEnemyState == EnemyState.MoveRight)
                    CurrentEnemyState = EnemyState.MoveLeft;
        }

        public void Draw(SpriteBatch sb)
        {
            Vector2 temp = new Vector2(base.Texture.Width / 2, base.Texture.Height);
            sb.Draw(base.Texture, base.Position, null, Color.White, 0f, temp, 1f, SpriteEffects.None, 0f);
        }

        public int Health
        {
            get { return this.health; }
            set { this.health = value; }
        }
    }
}
