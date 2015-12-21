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
        public CollisionHandler()
        {

        }

        public bool DetectCollisionRight(BoxCollider boxCollider, GameObject gameObject)
        {
            if (boxCollider.Rect.Left >= gameObject.Rect.Left &&
                boxCollider.Rect.Left <= gameObject.Rect.Right + 5 &&
                boxCollider.Rect.Top <= gameObject.Rect.Bottom - (gameObject.Rect.Width / 4) &&
                boxCollider.Rect.Bottom >= gameObject.Rect.Top + (gameObject.Rect.Width / 4))
            {
                if (CollidingRight(gameObject.Position.X + (gameObject.Texture.Width / 2) + gameObject.Velocity.X, gameObject.Position.Y, boxCollider))
                {
                    while (!CollidingRight(gameObject.Position.X + (gameObject.Texture.Width / 2) + Math.Sign(gameObject.Velocity.X), gameObject.Position.Y, boxCollider))
                    {
                        gameObject.PositionX = gameObject.Position.X + (gameObject.Texture.Width / 2) + Math.Sign(gameObject.Velocity.X);
                    }
                    return true;
                }
            }
            return false;
        }

        public bool DetectCollisionLeft(BoxCollider boxCollider, GameObject gameObject)
        {
            if (boxCollider.Rect.Right <= gameObject.Rect.Right &&
                boxCollider.Rect.Right >= gameObject.Rect.Left - 5 &&
                boxCollider.Rect.Top <= gameObject.Rect.Bottom - (gameObject.Rect.Width / 4) &&
                boxCollider.Rect.Bottom >= gameObject.Rect.Top + (gameObject.Rect.Width / 4))
            {
                if (CollidingLeft(gameObject.Position.X - (gameObject.Texture.Width / 2) + gameObject.Velocity.X, gameObject.Position.Y, boxCollider))
                {
                    while (!CollidingLeft(gameObject.Position.X - (gameObject.Texture.Width / 2) + Math.Sign(gameObject.Velocity.X), gameObject.Position.Y, boxCollider))
                    {
                        gameObject.PositionX = gameObject.Position.X - (gameObject.Texture.Width / 2) + Math.Sign(gameObject.Velocity.X);
                    }
                    return true;
                }
            }
            return false;
        }

        public bool DetectCollisionBottom(BoxCollider boxCollider, GameObject gameObject)
        {
            if (boxCollider.Rect.Top <= gameObject.Rect.Bottom + (gameObject.Rect.Height / 5) &&
                boxCollider.Rect.Top >= gameObject.Rect.Bottom - 1 &&
                boxCollider.Rect.Right >= gameObject.Rect.Left + (gameObject.Rect.Width / 5) &&
                boxCollider.Rect.Left <= gameObject.Rect.Right - (gameObject.Rect.Width / 5))
            {
                if (CollidingBottom(gameObject.Position.X, gameObject.Position.Y + gameObject.Velocity.Y, boxCollider))
                {
                    while (!CollidingBottom(gameObject.Position.X, gameObject.Position.Y + Math.Sign(gameObject.Velocity.Y), boxCollider))
                    {
                        gameObject.PositionY = gameObject.Position.Y + Math.Sign(gameObject.Velocity.Y);
                    }
                    return true;
                }
            }
            return false;
        }

        public bool DetectCollisionTop(BoxCollider boxCollider, GameObject gameObject)
        {
            if (boxCollider.Rect.Bottom >= gameObject.Rect.Top - 1 &&
               boxCollider.Rect.Bottom <= gameObject.Rect.Top + (boxCollider.Rect.Height / 2) &&
               boxCollider.Rect.Right >= gameObject.Rect.Left + (gameObject.Rect.Width / 5) &&
               boxCollider.Rect.Left <= gameObject.Rect.Right - (gameObject.Rect.Width / 5))
            {
                if (CollidingTop(gameObject.Position.X, gameObject.Position.Y - gameObject.Texture.Height + gameObject.Velocity.Y, boxCollider))
                {
                    while (!CollidingTop(gameObject.Position.X, gameObject.Position.Y - gameObject.Texture.Height + Math.Sign(gameObject.Velocity.Y), boxCollider))
                    {
                        gameObject.PositionY = gameObject.Position.Y + Math.Sign(gameObject.Velocity.Y);
                    }
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
