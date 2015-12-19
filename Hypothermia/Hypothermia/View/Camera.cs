using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.View
{
    public class Camera
    {
        private GraphicsDevice device;
        private Matrix transform;
        private Vector2 center;

        public Camera(GraphicsDevice device)
        {
            this.device = device;
        }

        public Vector2 GetVisualCoordinates(int logicX, int logicY)
        {
            return new Vector2(0, 0);
        }

        public void Update(float elapsedTime, Model.Player player)
        {
            this.center.X = player.Position.X - this.device.Viewport.Width / 2;
            this.center.Y = player.Position.Y - this.device.Viewport.Height / 2;
            this.transform = Matrix.CreateScale(new Vector3(1,1,0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }

        public Matrix Transform { get { return this.transform; } }
    }
}
