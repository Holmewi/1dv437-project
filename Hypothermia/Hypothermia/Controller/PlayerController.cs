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

        private KeyboardState pastKey;

        public PlayerController(View.Camera camera, Model.Player player)
        {
            this.camera = camera;
            this.player = player;
        }

        public void Update(float elapsedTime)
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
                this.player.RangeAttack();

            this.pastKey = Keyboard.GetState();
        }
    }
}
