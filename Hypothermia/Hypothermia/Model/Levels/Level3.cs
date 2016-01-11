using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.Model.Levels
{
    public class Level3 : Level
    {
        private View.Camera camera;
        private Model.Player player;
        private Collection.MapType map;

        private SpriteFont font;

        private View.GFX.SnowSimulation snowSimulation;
        private View.GFX.PlaneHandler planeHandler;

        public Level3(ContentManager content, View.Camera camera, Model.Player player, Collection.MapType map, int count)
        {
            this.camera = camera;
            this.player = player;
            this.map = map;
            base.count = count;

            this.font = content.Load<SpriteFont>("Fonts/SpriteFont1");

            this.CreateMap(content);
            this.CreateEnemies(content);

            this.snowSimulation = new View.GFX.SnowSimulation(content, this.camera);
            this.planeHandler = new View.GFX.PlaneHandler(this.camera);

            this.CreatePlanes(content);

            this.SetPlayer();

            base.LevelState = LevelState.Created;
        }

        private void CreateMap(ContentManager content)
        {
            base.Tiles = map.GetTileList(content.Load<Texture2D>("TexturePacks/tileSpriteSheet"), this.camera.TileSize);
            this.camera.MapWidth = map.MapWidth;
            this.camera.MapHeight = map.MapHeight;
        }

        private void CreateEnemies(ContentManager content)
        {
            base.Enemies = new Enemy[9];
            base.Enemies[0] = new Enemy(ENEMY_DUCK, content.Load<Texture2D>("enemyTmp1"), this.camera.GetMapCoordinates(17, 11));
            base.Enemies[1] = new Enemy(ENEMY_DUCK, content.Load<Texture2D>("enemyTmp1"), this.camera.GetMapCoordinates(10, 15));
            base.Enemies[2] = new Enemy(ENEMY_DUCK, content.Load<Texture2D>("enemyTmp1"), this.camera.GetMapCoordinates(16, 7));
            base.Enemies[3] = new Enemy(ENEMY_FROG, content.Load<Texture2D>("enemyTmp2"), this.camera.GetMapCoordinates(10, 2));
            base.Enemies[4] = new Enemy(ENEMY_FROG, content.Load<Texture2D>("enemyTmp2"), this.camera.GetMapCoordinates(10, 19));
            base.Enemies[5] = new Enemy(ENEMY_WISP, content.Load<Texture2D>("enemyTmp3"), this.camera.GetMapCoordinates(5, 7));
            base.Enemies[6] = new Enemy(ENEMY_WISP, content.Load<Texture2D>("enemyTmp3"), this.camera.GetMapCoordinates(10, 15));
            base.Enemies[7] = new Enemy(ENEMY_WISP, content.Load<Texture2D>("enemyTmp3"), this.camera.GetMapCoordinates(15, 12));
            base.Enemies[8] = new Enemy(ENEMY_WISP, content.Load<Texture2D>("enemyTmp3"), this.camera.GetMapCoordinates(16, 7));
        }

        private void CreatePlanes(ContentManager content)
        {
            this.planeHandler.AddTextures(content.Load<Texture2D>("Planes/bgDepth4"));
            this.planeHandler.GenerateDepth(4, 0.4f);

            this.planeHandler.AddTextures(content.Load<Texture2D>("Planes/bgDepth6a"));
            this.planeHandler.AddTextures(content.Load<Texture2D>("Planes/bgDepth6b"));
            this.planeHandler.GenerateDepth(6, 0.1f);
        }

        private void SetPlayer()
        {
            this.player.Position = new Vector2(this.camera.TileSize, this.camera.MapHeight - this.camera.TileSize * 4);
            this.player.Velocity = new Vector2(0, 0);
            this.player.Health = 100;
            this.player.CurrentPlayerState = Model.PlayerState.Idle;
        }

        public override void Update(float elapsedTime)
        {
            if (base.LevelState == LevelState.Playing)
            {
                if (this.planeHandler != null)
                    this.planeHandler.Update(this.player.Velocity, this.player.Position);
                if (this.snowSimulation != null)
                    this.snowSimulation.Update(elapsedTime);

                this.player.Update(elapsedTime, base.Tiles);

                foreach (Enemy enemy in base.Enemies)
                {
                    this.player.Combat(enemy);
                    enemy.Update(elapsedTime, tiles, this.camera.MapWidth, this.camera.MapHeight, this.camera.TileSize);
                }
            }

            else if (base.LevelState == LevelState.Created)
            {
                base.loadTimer -= elapsedTime;

                if (this.snowSimulation != null)
                    this.snowSimulation.Update(elapsedTime);
            }

            else if (base.LevelState == LevelState.Finished)
            {
                this.player.SFXHandler.HandleIdleSFX();
                if (this.snowSimulation != null)
                    this.snowSimulation.Update(elapsedTime);
            }

            base.Update(elapsedTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            if (base.LevelState == LevelState.Playing)
            {
                this.DrawBackground(sb);

                this.player.Draw(sb);

                foreach (View.Tile tile in base.Tiles)
                    tile.Draw(sb);

                foreach (Enemy enemy in base.Enemies)
                    enemy.Draw(sb);

                this.DrawForeground(sb);
            }

            else if (base.LevelState == LevelState.Created)
            {
                sb.DrawString(this.font, "Level 3", this.camera.GetDeviceCoordinates(0, 0), Color.Black);
                sb.DrawString(this.font, "Lifes x " + this.player.Lives, this.camera.GetDeviceCoordinates(0, -20), Color.Black);
            }

            else if (base.LevelState == LevelState.Finished)
            {
                this.DrawBackground(sb);
                this.DrawForeground(sb);
                sb.DrawString(this.font, "Completed Level 3, Press Enter to continue", this.camera.GetDeviceCoordinates(0, 0), Color.Black);
            }
        }

        private void DrawForeground(SpriteBatch sb)
        {
            if (this.snowSimulation != null)
                this.snowSimulation.DrawForeground(sb);
        }

        private void DrawBackground(SpriteBatch sb)
        {
            if (this.planeHandler != null)
                this.planeHandler.DrawBackground(sb);
            if (this.snowSimulation != null)
                this.snowSimulation.DrawBackground(sb);
        }

        public override bool IsFinished()
        {
            if (this.player.Position.X >= this.camera.MapWidth - player.Rect.Width)
                return true;
            return false;
        }
    }
}
