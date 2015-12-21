using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hypothermia.Model
{
    public class RigidBody
    {
        private bool onGround = false;
        private bool collideRight = false;
        private bool collideLeft = false;

        private float gravityAcceleration = 9.8f; // (unit g) 9.8f is the gravity acceleration on earth
        private Vector2 gravity; // (unit Q) objects mass (m) * g

        private float drag; // (Unit F)
        private float dragCoefficient = 0.4f;  // (Unit c) 0.4f is a humans drag coefficient
        private float airDensity = 1.3f; // (Unit d) 1.3f earth air density 

        private CollisionHandler collisionHandler;
        private GameObject gameObject;

        public RigidBody(GameObject gameObject, float mass, float frontArea)
        {
            this.gameObject = gameObject;
            this.gameObject.Mass = mass;
            this.gameObject.FrontArea = frontArea;
            this.collisionHandler = new CollisionHandler();
        }

        public void Fall(float elapsedTime)
        {
            //  Calculates the force of the air drag: F = cA * (d (v*v)/2)
            this.drag = this.dragCoefficient * this.gameObject.FrontArea * (this.airDensity * (this.gameObject.Velocity.Y * this.gameObject.Velocity.Y) / 2);

            //  Calculates the gravitation towards the ground floor (earth): Q = mg
            this.gravity.Y = this.gameObject.Mass * this.gravityAcceleration;

            if (this.drag < this.gravity.Y)
            {
                //  Calculates the new speed after a period of time: v = v + Square root [2mg / (c A d)] * elapsedTime
                this.gameObject.VelocityY = this.gameObject.Velocity.Y + ((float)Math.Sqrt((2 * this.gameObject.Mass * this.gravityAcceleration) / (this.dragCoefficient * this.gameObject.FrontArea * this.airDensity))) * elapsedTime;
            }
        }

        public void DetectCollision(List<View.Map.Tile> tiles)
        {
            /**
             *  1st - Check which direction the GameObject is moving (X directions needs to be checked first)
             *  2nd - (CollisionHandler) Check if the BoxCollider is in that direction and if it's within it's range
             *  3rd - (CollisionHandler) Check if the GameObject is about to collide with the BoxCollider
             *  4th - (CollisionHandler) If true - Calculate the collision position smoothly
             *  5th - (CollisionHandler) When GameObject has collided with bottom, left or right - set it's position to the BoxCollider this.gameObject.
            */

            for (var i = 0; i < tiles.Count(); i++)
            {
                if (this.gameObject.Velocity.X >= 0)
                {
                    this.collideLeft = false;
                    if (this.collisionHandler.DetectCollisionRight(tiles[i].Collider, this.gameObject))
                    {
                        gameObject.PositionX = tiles[i].Collider.Rect.Left - (gameObject.Texture.Width / 2);
                        gameObject.VelocityX = 0;
                        this.collideRight = true;
                        break;
                    }
                    else
                        this.collideRight = false;
                }
            }

            for (var i = 0; i < tiles.Count(); i++)
            {
                if (this.gameObject.Velocity.X <= 0)
                {
                    this.collideRight = false;

                    if (this.collisionHandler.DetectCollisionLeft(tiles[i].Collider, this.gameObject))
                    {
                        gameObject.PositionX = tiles[i].Collider.Rect.Right + (gameObject.Texture.Width / 2);
                        gameObject.VelocityX = 0;
                        this.collideLeft = true;
                        break;
                    }
                    else
                        this.collideLeft = false;
                }
            }

            for (var i = 0; i < tiles.Count(); i++)
            {
                if (this.gameObject.Velocity.Y >= 0)
                {
                    if (this.collisionHandler.DetectCollisionBottom(tiles[i].Collider, this.gameObject))
                    {
                        gameObject.PositionY = tiles[i].Collider.Rect.Top;
                        gameObject.VelocityY = 0;
                        this.onGround = true;
                        break;
                    }
                    else
                        this.onGround = false;
                }
            }

            for (var i = 0; i < tiles.Count(); i++)
            {
                if (this.gameObject.Velocity.Y < 0)
                {
                    this.onGround = false;
                    if (this.collisionHandler.DetectCollisionTop(tiles[i].Collider, this.gameObject))
                    {
                        gameObject.VelocityY = 0;
                        break;
                    }
                }
            }
        }

        public bool OnGround
        {
            get { return this.onGround; }
            set { this.onGround = value; }
        }

        public bool CollideRight
        {
            get { return this.collideRight; }
            set { this.collideRight = value; }
        }

        public bool CollideLeft
        {
            get { return this.collideLeft; }
            set { this.collideLeft = value; }
        }
    }
}
