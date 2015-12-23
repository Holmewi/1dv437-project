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
        private Map.MapView mapView;

        public GameView(Camera camera)
        {
            this.mapView = new Map.MapView(camera);
        }
        
        public void LoadContent(ContentManager content, int level)
        {
            this.mapView.LoadContent(content, level);
        }

        public void Update(float elapsedTime, Vector2 playerVelocity, Vector2 playerPosition)
        {
            this.mapView.Update(elapsedTime, playerVelocity, playerPosition);
        }

        public void Draw(SpriteBatch sb)
        {
            this.mapView.Draw(sb);
        }

        public List<Map.Tile> Tiles { get { return this.mapView.Tiles; } }
    }
}
