using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View
{
    public class EnemySpawner : Model.GameTypes
    {
        private List<Model.Enemy> enemies = new List<Model.Enemy>();

        private Camera camera;

        public EnemySpawner(Camera camera)
        {
            this.camera = camera;
        }

        public void Level1(ContentManager content)
        {
            this.enemies.Clear();

            Texture2D wolfTexture  = content.Load<Texture2D>("player");

            this.enemies.Add(new Model.Enemy(ENEMY_WOLF, wolfTexture, this.camera.GetMapCoordinates(17, 10)));
            this.enemies.Add(new Model.Enemy(ENEMY_WOLF, wolfTexture, this.camera.GetMapCoordinates(10, 15)));
            this.enemies.Add(new Model.Enemy(ENEMY_WOLF, wolfTexture, this.camera.GetMapCoordinates(16, 7)));
        }

        public List<Model.Enemy> Enemies { get { return this.enemies; } }
    }
}
