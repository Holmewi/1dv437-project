using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.Model
{
    public class EnemyTypes
    {
        public static EnemyType ENEMY_WOLF = new EnemyType("Wolf", 100, 25, 3.0f, true, false);
        public static EnemyType ENEMY_BAT = new EnemyType("Bat", 20, 50, 5.0f, true, true);
    }

    public struct EnemyType
    {
        private string name;
        private int health;
        private int damage;
        private float maxSpeed;
        private bool isIntelligent;
        private bool isFlying;

        public EnemyType(string name, int health, int damage, float maxSpeed, bool isIntelligent, bool isFlying)
        {
            this.name = name;
            this.health = health;
            this.damage = damage;
            this.maxSpeed = maxSpeed;
            this.isIntelligent = isIntelligent;
            this.isFlying = isFlying;
        }

        public string Name { get { return this.name; } }

        public int Health { 
            get { return this.health; }
            set { this.health = value; }
        }

        public int Damage { get { return this.damage; } }
        public float MaxSpeed { get { return this.maxSpeed; } }
        public bool IsIntelligent { get { return this.isIntelligent; } }
    }
}
