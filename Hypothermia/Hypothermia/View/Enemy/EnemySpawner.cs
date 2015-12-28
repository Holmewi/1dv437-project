using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View.Enemy
{
    public class EnemySpawner : Model.EnemyTypes
    {
        private List<Model.Enemy> enemies = new List<Model.Enemy>();

        private Camera camera;

        public EnemySpawner(Camera camera)
        {
            this.camera = camera;
        }

        public void Start()
        {

        }

        public void Level1(ContentManager content)
        {
            this.enemies.Clear();

            Texture2D wolfTexture  = content.Load<Texture2D>("player");

            this.enemies.Add(new Model.Enemy(ENEMY_WOLF, wolfTexture, this.camera.GetMapCoordinates(1, 15)));
            this.enemies.Add(new Model.Enemy(ENEMY_WOLF, wolfTexture, this.camera.GetMapCoordinates(10, 15)));
            this.enemies.Add(new Model.Enemy(ENEMY_WOLF, wolfTexture, this.camera.GetMapCoordinates(19, 9)));
        }

        public List<Model.Enemy> Enemies { get { return this.enemies; } }
    }
}
