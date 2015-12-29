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
        // TODO: Create an item texture helper in a later version
        private Texture2D arrowTexture;

        private View.Camera camera;
        private Model.Player player;
        private View.PlayerView playerView;

        private KeyboardState pastKey;

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
            this.arrowTexture = content.Load<Texture2D>("arrow");
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
            this.playerView.Draw(sb, this.player.Position, this.player.Arrows);
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

            if (Keyboard.GetState().IsKeyDown(Keys.E))
                this.player.MeleeAttack();

            if (Keyboard.GetState().IsKeyDown(Keys.Q) && this.pastKey.IsKeyUp(Keys.Q) && !this.player.IsSprinting && this.player.ShootTimer <= 0f)
                this.player.RangeAttack(this.arrowTexture);

            this.pastKey = Keyboard.GetState();
        }

        public Model.Player Player { get { return this.player; } }
    }
}
