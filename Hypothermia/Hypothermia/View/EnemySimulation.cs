using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hypothermia.Model
{
    public class EnemySimulation
    {
        private View.Camera camera;
        private Rectangle pathFindingRect;

        public EnemySimulation(View.Camera camera)
        {
            this.camera = camera;
        }

        public void Update(float elapsedTime, Enemy enemy, List<View.Map.Tile> tiles)
        {
            this.Movement(enemy);
            this.MapCollision(enemy);
            enemy.Update(elapsedTime, tiles);
            if (enemy.Type.BoundToPlatform)
                this.PathFinding(enemy, tiles);
        }

        private void Movement(Enemy enemy)
        {
            if (enemy.EnemyState == EnemyState.MoveLeft)
                enemy.MoveLeft();
            else if (enemy.EnemyState == EnemyState.MoveRight)
                enemy.MoveRight();
        }

        private void MapCollision(Enemy enemy)
        {
            if (enemy.Position.X < 0)
                enemy.CurrentEnemyState = EnemyState.MoveRight;
            if (enemy.Position.X > this.camera.MapWidth)
                enemy.CurrentEnemyState = EnemyState.MoveLeft;
            if (enemy.Position.Y > this.camera.MapHeight)
                enemy.Health = 0;
        }

        /**
         *  @author Pikuchan
         *  Source https://joshcodev.wordpress.com/2013/11/08/xna-basic-enemy-ai/
         */
        private void PathFinding(Enemy enemy, List<View.Map.Tile> tiles)
        {
            bool endOfPath = true;

            if (enemy.EnemyState == EnemyState.MoveRight)
                this.pathFindingRect = new Rectangle((int)enemy.Position.X - enemy.Texture.Width / 2 + this.camera.TileSize,
                                                        (int)enemy.Rect.Y + 1 + this.camera.TileSize, this.camera.TileSize, this.camera.TileSize);

            else if (enemy.EnemyState == EnemyState.MoveLeft)
                this.pathFindingRect = new Rectangle((int)enemy.Position.X - enemy.Texture.Width / 2 - this.camera.TileSize,
                                                        (int)enemy.Position.Y - this.camera.TileSize + 1, this.camera.TileSize, this.camera.TileSize);

            for (var i = 0; i < tiles.Count(); i++)
                if (this.pathFindingRect.Intersects(tiles[i].Rect))
                    endOfPath = false;

            if (endOfPath)
                if (enemy.EnemyState == EnemyState.MoveLeft)
                    enemy.CurrentEnemyState = EnemyState.MoveRight;
                else if (enemy.EnemyState == EnemyState.MoveRight)
                    enemy.CurrentEnemyState = EnemyState.MoveLeft;
        }
    }
}
