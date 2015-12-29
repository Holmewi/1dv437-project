using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hypothermia.View.GFX
{
    public class SnowSystem : Model.GameTypes
    {
        private List<Particle> particles = new List<Particle>();

        private Random random;
        private Camera camera;
        private Texture2D snowflakeTexture;

        private const int MAX_AMOUNT_PARTICLES = 500;
        private const int PARTICLE_LIFE_TIME = 5;

        private float spawnTime = 0;
        private int count = 0;

        private float windStrenght;

        public SnowSystem(Camera camera)
        {
            this.random = new Random();
            this.camera = camera;
        }

        public void LoadContent(ContentManager content)
        {
            this.snowflakeTexture = content.Load<Texture2D>("Particles/snowflake1");
        }

        private void Add()
        {
            this.particles.Add(new Particle(PARTICLE_SNOWFLAKE, this.snowflakeTexture));
            this.count++;
            this.spawnTime = 0;
        }

        private void SpawnParticle(Particle particle)
        {
            particle.Size = (float)this.random.NextDouble() * (particle.Type.MaxSize - particle.Type.MinSize) + particle.Type.MinSize;
            particle.Fade = (float)this.random.NextDouble() * (1.0f - 0.2f) + 0.2f;

            float posY = (this.camera.Target.Y - (float)this.camera.DeviceHight / 2) - 150;
            float randPosXLeft = -600;
            float randPosXRight = (float)this.camera.MapWidth + 600;

            Vector2 position = new Vector2((float)this.random.NextDouble() * (randPosXRight - randPosXLeft) + randPosXLeft, posY);
            Vector2 acceleration = new Vector2(0, (float)this.random.NextDouble() * (particle.Type.MaxSpeed - particle.Type.MinSpeed) + particle.Type.MinSpeed);
            Vector2 velocity = new Vector2(0, 0);

            particle.Spawn(position, velocity, acceleration);
        }

        // TODO: Make a weather system that handles this in a later version
        private void WindBearing(Particle particle)
        {
            this.windStrenght = (float)this.random.NextDouble() * (2.0f - -2.0f) + -2.0f;

            particle.VelocityX = particle.Velocity.X + this.windStrenght;
        }

        public void Update(float elapsedTime)
        {
            this.spawnTime += elapsedTime;

            if (spawnTime >= 0.05f && this.particles.Count <= MAX_AMOUNT_PARTICLES)
                this.Add();

            for (int i = 0; i < this.particles.Count; i++)
            {
                if (this.particles != null)
                {
                    if (this.particles[i].Position.Y > this.camera.MapHeight)
                        this.particles[i].Life = 0;
                    if (this.particles[i].IsParticleDead())
                        this.SpawnParticle(this.particles[i]);

                    this.WindBearing(this.particles[i]);
                    this.particles[i].Update(elapsedTime);
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < this.particles.Count; i++)
            {
                if(this.particles != null)
                    this.particles[i].Draw(sb);
            }
        }
    }
}
