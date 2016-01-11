using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.Model
{
    public enum LevelState
    {
        Created,
        Playing,
        Finished
    }

    public class Level : Collection.GameTypes
    {
        private LevelState levelState;

        protected List<View.Tile> tiles;
        protected Enemy[] enemies;
        protected View.GFX.PlaneHandler depth;

        protected int count;
        protected float loadTimer = 2.0f;

        public virtual void Update(float elapsedTime)
        {

        }

        public virtual void Draw(SpriteBatch sb)
        {

        }

        public virtual bool IsFinished()
        {
            return false;
        }

        public LevelState LevelState
        {
            get { return this.levelState; }
            set { this.levelState = value; }
        }

        public List<View.Tile> Tiles
        {
            get { return this.tiles; }
            set { this.tiles = value; }
        }

        public Enemy[] Enemies
        {
            get { return this.enemies; }
            set { this.enemies = value; }
        }

        public int Count
        {
            get { return this.count; }
            set { this.count = value; }
        }

        public float LoadTimer { get { return this.loadTimer; } }
    }
}
