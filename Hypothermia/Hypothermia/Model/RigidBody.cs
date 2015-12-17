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
        private float gravityAcceleration = 9.8f; // (unit g) 9.8f is the gravity acceleration on earth
        private Vector2 gravity; // (unit Q) objects mass (m) * g

        private float drag; // (Unit F)
        private float dragCoefficient = 0.4f;  // (Unit c) 0.4f is a humans drag coefficient
        private float airDensity = 1.3f; // (Unit d) 1.3f earth air density 

        // Todo: Decide if this value should stay here or be moved to the GameObject
        private float frontArea = 5.5f; // (Unit A) The objects area that hits the drag

        private GameObject gameObject;

        public RigidBody(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public void Fall(float elapsedTime)
        {

            //  Calculates the force of the air drag: F = cA * (d (v*v)/2)
            this.drag = this.dragCoefficient * this.frontArea * (this.airDensity * (this.gameObject.Velocity.Y * this.gameObject.Velocity.Y) / 2); // c * A * (dv2/2)
  

            //  Calculates the gravitation towards the ground floor (earth): Q = mg
            this.gravity.Y = this.gameObject.Mass * this.gravityAcceleration;

            if (this.drag < this.gravity.Y)
            {
                //  Calculates the new speed after a period of time: v = v + Square root [2mg / (c A d)] * elapsedTime
                this.gameObject.VelocityY = this.gameObject.Velocity.Y + ((float)Math.Sqrt((2 * this.gameObject.Mass * this.gravityAcceleration) / (this.dragCoefficient * this.frontArea * this.airDensity))) * elapsedTime;
            }
        }

        public bool CollisionDetection(BoxCollider boxCollider)
        {
            // Vertical Collision
            if (Colliding(this.gameObject.Position.X, this.gameObject.Position.Y + this.gameObject.Velocity.Y, boxCollider))
            {
                while (!Colliding(this.gameObject.Position.X, this.gameObject.Position.Y + Math.Sign(this.gameObject.Velocity.Y), boxCollider))
                {
                    this.gameObject.PositionY = this.gameObject.Position.Y + Math.Sign(this.gameObject.Velocity.Y);
                }
                //this.gameObject.OnGround = true;
                this.gameObject.VelocityY = 0;
                return true;
            }
            return false;
            /*
             i en tile
             y = player.x/tileWidth * tileXend - tileXstart;
             */

            // Horizontal Collision
            /*
             * Are we about to collide?
             if(functionCollision(x+velocity.X, y, objectCollider)) {
                while(!functionCollision(x+sign(velocity.X), y, objectCollider) { (sign returns 1 or -1 depending on x)
                {
                    x += sign(velocity.X);   
                }
                velocity.x = 0;
             }
             movement left or right();
             */

            //........................................................ Vertical Collision
            /*
            if(functionCollision(x, y+velocity.Y, objectCollider)) {
                while(!functionCollision(x, y + sign(velocity.Y), objectCollider) { (sign returns 1 or -1 depending on x)
                {
                    y += sign(velocity.y);   
                }
                velocity.y = 0;
             }
             * */
        }

        public bool Colliding(float x, float y, BoxCollider boxCollider)
        {
            if (boxCollider.IsSolid)
            {
                Debug.WriteLine("NextY " + y + ", ColliderTop " + boxCollider.Box.Rect.Top);

                if (y > boxCollider.Box.Rect.Top)
                {
                    return true;
                }
                return false;
            }
            return true;
            
        }
    }
}
