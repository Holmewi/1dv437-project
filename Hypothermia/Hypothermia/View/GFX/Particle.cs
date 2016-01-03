using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View.GFX
{
    public class Particle : Model.GameObject
    {
        private Model.RigidBody rigidBody;
        private Collection.ParticleType type;

        private float life = 0;
        private float size;
        private float fade = 1.0f;

        public Particle(Collection.ParticleType type, Texture2D texture)
        {
            this.type = type;

            base.Texture = texture;

            if (type.IsRigidBody)
                this.rigidBody = new Model.RigidBody(this, type.Mass, type.FrontArea);
        }

        public void Spawn(Vector2 position, Vector2 velocity, Vector2 acceleration)
        {
            this.life = this.type.MaxLife;

            base.Position = position;
            base.Velocity = velocity;
            base.Acceleration = acceleration;
        }

        public bool IsParticleDead()
        {
            if (this.life <= 0)
                return true;
            return false;
        }

        public void Update(float elapsedTime)
        {
            base.Velocity = base.Velocity + base.Acceleration * elapsedTime;
            base.Position = base.Position + base.Velocity * elapsedTime;

            if(this.rigidBody != null)
                this.rigidBody.Fall(elapsedTime);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(base.Texture, base.Position, null, 
                    new Color(this.fade, this.fade, this.fade, this.fade), 0f, Vector2.Zero, this.size, SpriteEffects.None, 0f);
        }

        public Collection.ParticleType Type { get { return this.type; } }

        public float Life { 
            get { return this.life; }
            set { this.life = value; }
        }

        public float Size
        {
            get { return this.size; }
            set { this.size = value; }
        }

        public float Fade { 
            get { return this.fade; }
            set { this.fade = value; }
        }
    }
}
