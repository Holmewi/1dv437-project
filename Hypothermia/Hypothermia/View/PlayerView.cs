using Hypothermia.Model;
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
    public class PlayerView
    {
        private Camera camera;
        private GFX.Animation animation;

        public PlayerView(Camera camera)
        {
            this.camera = camera;
        }

        public void LoadContent(ContentManager content)
        {
            this.animation = new GFX.Animation(content.Load<Texture2D>("TexturePacks/playerAnimation"), 15, 5);
        }

        public void Update(float elapsedTime, bool faceForward, PlayerState state)
        {
            this.animation.Update();

            if (state == PlayerState.Jump)
            {
                if (faceForward)
                    this.animation.Animate(elapsedTime, 3, 1, 9, 0.05f);
                else
                    this.animation.Animate(elapsedTime, 4, 1, 9, 0.05f);
            }

            else if (state == PlayerState.Fall)
            {
                if (faceForward)
                    this.animation.Animate(elapsedTime, 3, 9, 13, 0.05f);
                else
                    this.animation.Animate(elapsedTime, 4, 9, 13, 0.05f);
            }

            else if (state == PlayerState.MoveLeft)
                this.animation.Animate(elapsedTime, 2, 3, 13, 0.03f);

            else if (state == PlayerState.MoveRight)
                this.animation.Animate(elapsedTime, 1, 3, 13, 0.03f);

            else if (state == PlayerState.Idle)
            {
                if (faceForward)
                    this.animation.Animate(elapsedTime, 5, 1, 6, 0.03f);
                else
                    this.animation.Animate(elapsedTime, 5, 7, 12, 0.03f);
            }
        }

        public void Draw(SpriteBatch sb, Vector2 playerPosition)
        {
            sb.Draw(this.animation.Texture, playerPosition, this.animation.Rect, Color.White, 0f, this.animation.Origin, 1f, SpriteEffects.None, 0f);
        }


    }
}
