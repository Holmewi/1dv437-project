using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View
{
    public class PlayerView
    {
        private Texture2D texture;
        private Model.Player player;

        public PlayerView(Camera camera, Model.Player player)
        {
            this.player = player;
        }

        public void LoadContent(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("player");
            this.player.Texture = this.texture;
        }

        public void Draw(SpriteBatch sb)
        {
            Vector2 textureCenterBottomDisplacement = new Vector2((float)this.player.Texture.Bounds.Width / 2, (float)this.player.Texture.Bounds.Height);

            sb.Draw(this.player.Texture, this.player.Position, null, Color.White, 0f, textureCenterBottomDisplacement, 1f, SpriteEffects.None, 0f);
        }
    }
}
