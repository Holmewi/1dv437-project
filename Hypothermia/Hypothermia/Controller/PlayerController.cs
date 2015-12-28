using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.Controller
{
    public class PlayerController
    {
        private View.Camera camera;
        private Model.Player player;
        private View.PlayerView playerView;

        public PlayerController(View.Camera camera)
        {
            this.camera = camera;
            this.playerView = new View.PlayerView(this.camera);
        }

        public void Start()
        {
            this.player = new Model.Player(this.playerView.FrameWidth, this.playerView.FrameHeight);
        }

        public void LoadContent(ContentManager content)
        {
            this.playerView.LoadContent(content);
        }

        public void Update(float elapsedTime, List<View.Map.Tile> tiles)
        {
            this.Movement(elapsedTime);
            this.camera.FocusOnPlayer(elapsedTime, this.player.Position, this.player.FaceForward, this.camera.MapWidth, this.camera.MapHeight);
            this.player.MapCollision(this.camera.MapWidth, this.camera.MapHeight);
            this.player.Update(elapsedTime, tiles);
            this.playerView.Update(elapsedTime, this.player.FaceForward, this.player.PlayerState);
        }

        public void Draw(SpriteBatch sb)
        {
            this.playerView.Draw(sb, this.player.Position);
        }

        private void Movement(float elapsedTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && this.player.RigidBody.OnGround)
                this.player.Jump(elapsedTime);
            
            if (Keyboard.GetState().IsKeyDown(Keys.A) && !this.player.RigidBody.CollideLeft)
                this.player.MoveLeft();

            if (Keyboard.GetState().IsKeyDown(Keys.D) && !this.player.RigidBody.CollideRight)
                this.player.MoveRight();

            if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D) && this.player.RigidBody.OnGround)
                this.player.Idle();

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && this.player.RigidBody.OnGround)
                this.player.Sprint(true);

            if (Keyboard.GetState().IsKeyUp(Keys.LeftShift) && this.player.RigidBody.OnGround)
                this.player.Sprint(false);
        }

        public Model.Player Player { get { return this.player; } }
    }
}
