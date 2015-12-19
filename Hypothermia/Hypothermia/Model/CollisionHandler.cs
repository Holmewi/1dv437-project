using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hypothermia.Model
{
    /**
    *   Component class that handles rectangle collision
    *   This component class needs to be instansiated by the RigidBody class
    */
    public class CollisionHandler
    {
        private RigidBody rigidBody;

        public CollisionHandler(RigidBody rigidBody)
        {
            this.rigidBody = rigidBody;
        }

        public bool DetectCollisionRight(BoxCollider boxCollider, GameObject gameObject)
        {
            // If the objects right is to the left of the colliders left
            // and if the objects bottom is bigger the colliders top
            // and if the objects top is lesser the colliders bottom
            if (gameObject.Rect.Right <= boxCollider.Rect.Left + 1 &&
                gameObject.Rect.Bottom > boxCollider.Rect.Top &&
                gameObject.Rect.Top < boxCollider.Rect.Bottom)
            {
                if (CollidingRight(gameObject.Position.X + (gameObject.Texture.Width / 2) + gameObject.Velocity.X, gameObject.Position.Y, boxCollider))
                {
                    while (!CollidingRight(gameObject.Position.X + (gameObject.Texture.Width / 2) + Math.Sign(gameObject.Velocity.X), gameObject.Position.Y, boxCollider))
                    {
                        gameObject.PositionX = gameObject.Position.X + (gameObject.Texture.Width / 2) + Math.Sign(gameObject.Velocity.X);
                    }
                    gameObject.PositionX = boxCollider.Rect.Left - (gameObject.Texture.Width / 2);
                    gameObject.VelocityX = 0;
                    return true;
                }
            }
            return false;
        }

        public bool DetectCollisionLeft(BoxCollider boxCollider, GameObject gameObject)
        {
            // If the objects left is to the right of the colliders right
            // and if the objects bottom is bigger the colliders top
            // and if the objects top is lesser the colliders bottom
            if (gameObject.Rect.Left >= boxCollider.Rect.Right - 1 &&
                gameObject.Rect.Bottom > boxCollider.Rect.Top &&
                gameObject.Rect.Top < boxCollider.Rect.Bottom)
            {
                if (CollidingLeft(gameObject.Position.X - (gameObject.Texture.Width / 2) + gameObject.Velocity.X, gameObject.Position.Y, boxCollider))
                {
                    while (!CollidingLeft(gameObject.Position.X - (gameObject.Texture.Width / 2) + Math.Sign(gameObject.Velocity.X), gameObject.Position.Y, boxCollider))
                    {
                        gameObject.PositionX = gameObject.Position.X - (gameObject.Texture.Width / 2) + Math.Sign(gameObject.Velocity.X);
                    }
                    gameObject.PositionX = boxCollider.Rect.Right + (gameObject.Texture.Width / 2);
                    gameObject.VelocityX = 0;
                    return true;
                }
            }
            return false;
        }

        public bool DetectCollisionBottom(BoxCollider boxCollider, GameObject gameObject)
        {
            // If the objects bottom is above the colliders top
            // and if the object moves through the collider
            // and if the objects right is bigger the colliders left
            // and if the objects left is lesser the colliders right
            if (gameObject.Rect.Bottom + (gameObject.Texture.Height / 5) >= boxCollider.Rect.Top &&
                gameObject.Rect.Bottom - 1 <= boxCollider.Rect.Top &&
                gameObject.Rect.Right > boxCollider.Rect.Left &&
                gameObject.Rect.Left < boxCollider.Rect.Right)
            {
                if (CollidingBottom(gameObject.Position.X, gameObject.Position.Y + gameObject.Velocity.Y, boxCollider))
                {
                    while (!CollidingBottom(gameObject.Position.X, gameObject.Position.Y + Math.Sign(gameObject.Velocity.Y), boxCollider))
                    {
                        gameObject.PositionY = gameObject.Position.Y + Math.Sign(gameObject.Velocity.Y);
                    }
                    gameObject.PositionY = boxCollider.Rect.Top;
                    gameObject.VelocityY = 0;
                    return true;
                }
            }
            return false;
        }

        public bool DetectCollisionTop(BoxCollider boxCollider, GameObject gameObject)
        {
            // If the objects Top is beneath the colliders bottom
            // and if the object moves through the collider
            // and if the objects right is bigger the colliders left
            // and if the objects left is lesser the colliders right
            if (gameObject.Rect.Top - 1 <= boxCollider.Rect.Bottom && 
                gameObject.Rect.Top + (gameObject.Texture.Height/2) >= boxCollider.Rect.Bottom &&
                gameObject.Rect.Right > boxCollider.Rect.Left &&
                gameObject.Rect.Left < boxCollider.Rect.Right)
            {
                if (CollidingTop(gameObject.Position.X, gameObject.Position.Y - gameObject.Texture.Height + gameObject.Velocity.Y, boxCollider))
                {
                    while (!CollidingTop(gameObject.Position.X, gameObject.Position.Y - gameObject.Texture.Height + Math.Sign(gameObject.Velocity.Y), boxCollider))
                    {
                        gameObject.PositionY = gameObject.Position.Y + Math.Sign(gameObject.Velocity.Y);
                    }
                    gameObject.VelocityY = 0;
                    return true;
                }
            }
            return false;
        }

        public bool CollidingRight(float x, float y, BoxCollider boxCollider)
        {
            if (boxCollider.IsSolid)
            {
                if (x > boxCollider.Rect.Left)
                {
                    return true;
                }
                return false;
            }
            else
            {
                float boxPosRight = boxCollider.Rect.Right - x;
                float boxPosLeft = boxCollider.Rect.Width - boxPosRight;
                float boxY = (boxPosLeft / boxCollider.Rect.Width) * (boxCollider.EndX - boxCollider.StartX);

                if (y > boxY + boxCollider.Rect.Top)
                {
                    return true;
                }
                return false;
            }
        }

        public bool CollidingLeft(float x, float y, BoxCollider boxCollider)
        {
            if (boxCollider.IsSolid)
            {
                if (x < boxCollider.Rect.Right)
                {
                    return true;
                }
                return false;
            }
            else
            {
                float boxPosRight = boxCollider.Rect.Right - x;
                float boxPosLeft = boxCollider.Rect.Width - boxPosRight;
                float boxY = (boxPosLeft / boxCollider.Rect.Width) * (boxCollider.EndX - boxCollider.StartX);

                if (y > boxY + boxCollider.Rect.Top)
                {
                    return true;
                }
                return false;
            }
        }

        public bool CollidingBottom(float x, float y, BoxCollider boxCollider)
        {       
            if (boxCollider.IsSolid)
            {
                if (y > boxCollider.Rect.Top + boxCollider.StartX)
                {
                    return true;
                }
                return false;
            }
            else
            {
                float boxPosRight = boxCollider.Rect.Right - x;
                float boxPosLeft = boxCollider.Rect.Width - boxPosRight;
                float boxY = (boxPosLeft / boxCollider.Rect.Width) * (boxCollider.EndX - boxCollider.StartX);

                if (y > boxY + boxCollider.Rect.Top)
                {
                    return true;
                }
                return false;
            }
        }

        public bool CollidingTop(float x, float y, BoxCollider boxCollider)
        {
            if (boxCollider.IsSolid)
            {
                if (y < boxCollider.Rect.Bottom)
                {
                    return true;
                }
                return false;
            }
            else
            {
                float boxPosRight = boxCollider.Rect.Right - x;
                float boxPosLeft = boxCollider.Rect.Width - boxPosRight;
                float boxY = (boxPosLeft / boxCollider.Rect.Width) * (boxCollider.EndX - boxCollider.StartX);

                if (y > boxY + boxCollider.Rect.Top)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
