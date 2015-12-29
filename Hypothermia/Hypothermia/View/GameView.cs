using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hypothermia.View
{
    public class GameView
    {
        private Camera camera;
        private Map.MapContent content;
        private EnemySpawner spawner;

        public GameView(Camera camera)
        {
            this.camera = camera;
            this.content = new Map.MapContent(this.camera);
            this.spawner = new EnemySpawner(this.camera);
        }

        public void LoadContent(ContentManager content, int level)
        {
            switch (level)
            {
                case 1:
                    this.content.Level1(content);
                    this.spawner.Level1(content);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

        public void Update(float elapsedTime, Vector2 playerVelocity, Vector2 playerPosition)
        {
            foreach (GFX.Background background in this.content.Backgrounds)
                background.Update(playerVelocity, playerPosition, this.camera.MapWidth);

            // TODO: Place this is EnemySimulation
            foreach (Model.Enemy enemy in this.spawner.Enemies)
                enemy.Update(elapsedTime, this.content.Tiles);
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (GFX.Background background in this.content.Backgrounds)
                background.Draw(sb);

            for (int i = 0; i < this.content.Tiles.Count(); i++)
                this.content.Tiles[i].Draw(sb);

            foreach (Model.Enemy enemy in this.spawner.Enemies)
                enemy.Draw(sb);
        }

        public List<Map.Tile> Tiles { get { return this.content.Tiles; } }
        public List<Model.Enemy> Enemies { get { return this.spawner.Enemies; } }
    }
}
