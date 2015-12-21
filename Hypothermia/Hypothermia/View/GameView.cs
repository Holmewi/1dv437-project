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
        private PlayerView playerView;
        private Map.MapView mapView;

        public GameView(Camera camera, Model.Player player)
        {
            this.playerView = new PlayerView(camera, player);
            this.mapView = new Map.MapView(camera);
        }
        
        public void LoadContent(ContentManager content, int level)
        {
            this.playerView.LoadContent(content);
            this.mapView.LoadContent(content, level);
        }

        public void Update(Vector2 playerVelocity, float elapsedTime)
        {
            this.mapView.Update(playerVelocity, elapsedTime);
        }

        public void Draw(SpriteBatch sb)
        {
            this.mapView.Draw(sb);
            this.playerView.Draw(sb);
        }

        public List<Map.Tile> Tiles { get { return this.mapView.Tiles; } }
        public int MapWidth { get { return this.mapView.Width; } }
        public int MapHeight { get { return this.mapView.Height; } }
    }
}
