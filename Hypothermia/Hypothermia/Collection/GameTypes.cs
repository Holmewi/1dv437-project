using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.Collection
{
    public class GameTypes
    {
        /*
         *  Enemy types
         *  string <name>, int <maxLife>, int <damage>, float <maxSpeed>,
         *  vector2 <movementSpeed>, vector2 <acceleration>,
         *  bool <isBountToPlatform>, bool <isFlying>, 
         *  int <aggression>, float <mass>, float <frontArea>
         */
        public static EnemyType ENEMY_WOLF = new EnemyType("Wolf", 100, 25, 3.0f,
                                                           new Vector2(3.0f, 0), new Vector2(0.5f, 0),
                                                           true, false, true,
                                                           0, 70f, 5.5f);
        public static EnemyType ENEMY_RAVEN = new EnemyType("Raven", 20, 50, 4.0f,
                                                            new Vector2(4.0f, 0), new Vector2(0.5f, 0),
                                                            false, true, true,
                                                            0, 20f, 1.0f);
        public static EnemyType ENEMY_WISP = new EnemyType("Wisp", 20, 50, 6.0f,
                                                            new Vector2(6.0f, 0), new Vector2(0.5f, 0),
                                                            false, true, true,
                                                            0, 30f, 1.0f);

        /*
         *  Particle types
         *  float <maxLife>, float <maxSpeed>, float <minSpeed>, 
         *  float <maxSize>, float <minSize>, bool <isRigidBody>,
         *  float <mass>, float <frontArea>
         */
        public static ParticleType PARTICLE_SNOWFLAKE_FG = new ParticleType(5.0f, 10.0f, 5.0f,
                                                                         1.0f, 0.5f, true,
                                                                         0.1f, 0.1f);

        public static ParticleType PARTICLE_SNOWFLAKE_BG = new ParticleType(5.0f, 10.0f, 5.0f,
                                                                         0.4f, 0.1f, true,
                                                                         0.1f, 0.1f);

    }

    public struct EnemyType
    {
        private string name;
        private int maxLife;
        private int damage;
        private float maxSpeed;
        private Vector2 movementSpeed;
        private Vector2 acceleration;
        private bool isBoundToPlatform;
        private bool isFlying;
        private bool isRigidBody;
        private int aggression;
        private float mass;
        private float frontArea;

        public EnemyType(string name, int maxLife, int damage, float maxSpeed, 
                         Vector2 movementSpeed, Vector2 acceleration, 
                         bool isBoundToPlatform, bool isFlying, bool isRigidBody, 
                         int aggression, float mass, float frontArea)
        {
            this.name = name;
            this.maxLife = maxLife;
            this.damage = damage;
            this.maxSpeed = maxSpeed;
            this.movementSpeed = movementSpeed;
            this.acceleration = acceleration;
            this.isBoundToPlatform = isBoundToPlatform;
            this.isFlying = isFlying;
            this.isRigidBody = isRigidBody;
            this.aggression = aggression;
            this.mass = mass;
            this.frontArea = frontArea;
        }

        public string Name { get { return this.name; } }

        public int MaxLife { 
            get { return this.maxLife; }
            set { this.maxLife = value; }
        }

        public int Damage { get { return this.damage; } }
        public Vector2 MovementSpeed { get { return this.movementSpeed; } }
        public Vector2 Acceleration { get { return this.acceleration; } }
        public bool BoundToPlatform { get { return this.isBoundToPlatform; } }
        public bool IsRigidBody { get { return this.isRigidBody; } }
        public float Mass { get { return this.mass; } }
        public float FrontArea { get { return this.frontArea; } }
    }

    public struct ParticleType
    {
        private float maxLife;
        private float maxSpeed;
        private float minSpeed;
        private float maxSize;
        private float minSize;
        private bool isRigidBody;
        private float mass;
        private float frontArea;

        public ParticleType(float maxLife, float maxSpeed, float minSpeed, float maxSize, float minSize, bool isRigidBody, float mass, float frontArea)
        {
            this.maxLife = maxLife;
            this.maxSpeed = maxSpeed;
            this.minSpeed = minSpeed;
            this.maxSize = maxSize;
            this.minSize = minSize;
            this.isRigidBody = isRigidBody;
            this.mass = mass;
            this.frontArea = frontArea;
        }

        public float MaxLife { get { return this.maxLife; } }
        public float MaxSpeed { get { return this.maxSpeed; } }
        public float MinSpeed { get { return this.minSpeed; } }
        public float MaxSize { get { return this.maxSize; } }
        public float MinSize { get { return this.minSize; } }
        public bool IsRigidBody { get { return this.isRigidBody; } }
        public float Mass { get { return this.mass; } }
        public float FrontArea { get { return this.frontArea; } }
    }
}
