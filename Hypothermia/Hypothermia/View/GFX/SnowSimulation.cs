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
    public class SnowSimulation : Collection.GameTypes
    {
        private List<Particle> snowForeground = new List<Particle>();
        private List<Particle> snowBackground = new List<Particle>();

        private Random random;
        private Camera camera;
        private Texture2D snowflakeTexture;

        private const int MAX_AMOUNT_PARTICLES = 1500;
        private const int PARTICLE_LIFE_TIME = 5;

        private float fgSpawnTime = 0;
        private float bgSpawnTime = 0;

        private float windStrenght;

        public SnowSimulation(ContentManager content, Camera camera)
        {
            this.random = new Random();
            this.camera = camera;
            this.snowflakeTexture = content.Load<Texture2D>("Particles/snowflake1");
        }

        private void Add(bool isForeground)
        {
            if(isForeground) {
                this.snowForeground.Add(new Particle(PARTICLE_SNOWFLAKE_FG, this.snowflakeTexture));
                this.fgSpawnTime = 0;
            }
            else
            {
                this.snowBackground.Add(new Particle(PARTICLE_SNOWFLAKE_BG, this.snowflakeTexture));
                this.bgSpawnTime = 0;
            }
        }

        private void SpawnParticle(Particle particle)
        {
            particle.Size = (float)this.random.NextDouble() * (particle.Type.MaxSize - particle.Type.MinSize) + particle.Type.MinSize;
            particle.Fade = (float)this.random.NextDouble() * (1.0f - 0.2f) + 0.2f;

            float posY = (this.camera.Target.Y - (float)this.camera.DeviceHeight / 2) - 150;
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
            this.fgSpawnTime += elapsedTime;
            this.bgSpawnTime += elapsedTime;

            if (fgSpawnTime >= 0.05f && this.snowForeground.Count <= MAX_AMOUNT_PARTICLES)
                this.Add(true);
            if (bgSpawnTime >= 0.01f && this.snowBackground.Count <= MAX_AMOUNT_PARTICLES)
                this.Add(false);

            for (int i = 0; i < this.snowForeground.Count; i++)
            {
                if (this.snowForeground != null)
                {
                    if (this.snowForeground[i].Position.Y > this.camera.MapHeight)
                        this.snowForeground[i].Life = 0;
                    if (this.snowForeground[i].IsParticleDead())
                        this.SpawnParticle(this.snowForeground[i]);

                    this.WindBearing(this.snowForeground[i]);
                    this.snowForeground[i].Update(elapsedTime);
                }
            }

            for (int i = 0; i < this.snowBackground.Count; i++)
            {
                if (this.snowBackground != null)
                {
                    if (this.snowBackground[i].Position.Y > this.camera.MapHeight)
                        this.snowBackground[i].Life = 0;
                    if (this.snowBackground[i].IsParticleDead())
                        this.SpawnParticle(this.snowBackground[i]);

                    this.WindBearing(this.snowBackground[i]);
                    this.snowBackground[i].Update(elapsedTime);
                }
            }
        }

        public void DrawForeground(SpriteBatch sb)
        {
            for (int i = 0; i < this.snowForeground.Count; i++)
            {
                if(this.snowForeground != null)
                   this.snowForeground[i].Draw(sb);
            }
        }

        public void DrawBackground(SpriteBatch sb)
        {
            for (int i = 0; i < this.snowBackground.Count; i++)
            {
                if (this.snowBackground != null)
                    this.snowBackground[i].Draw(sb);
            }
        }
    }
}
