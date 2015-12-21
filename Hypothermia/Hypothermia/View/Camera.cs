﻿using Hypothermia.Controller;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hypothermia.View
{
    public class Camera
    {
        private GraphicsDevice device;
        private Matrix transform;
        private Vector2 cameraTargetPosition;

        private float offset = 0;
        private int mapWidth;
        private int mapHeight;

        public Camera(GraphicsDevice device)
        {
            this.device = device;
            this.cameraTargetPosition = this.GetVisualCoordinates(this.device.Viewport.Width / 2, this.device.Viewport.Height / 2);
        }

        public int MapWidth { set { this.mapWidth = value; } }
        public int MapHeight { set { this.mapHeight = value; } }

        public Vector2 GetVisualCoordinates(float logicX, float logicY)
        {
            float visualX = logicX;
            float visualY = this.mapHeight - logicY;
            return new Vector2(visualX, visualY);
        }

        /*
         * This method is used for interface to render on screen
         */
        public Vector2 GetLogicCoordinates(int visualX, int visualY)
        {
            float logicX = this.cameraTargetPosition.X - visualX;
            float logicY = this.cameraTargetPosition.Y - visualY;

            return new Vector2(logicX, logicY);
        }

        private int Panning(float elapsedTime, Vector2 playerVelocity)
        {
            if (playerVelocity.X > 0 && offset <= 125)
                offset = offset + 25 * elapsedTime;
            else if (playerVelocity.X < 0 && offset >= -125)
                offset = offset - 25 * elapsedTime;
            else
            {
                if (offset < 125)
                    offset = offset + 25 * elapsedTime;
                if (offset > 125)
                    offset = offset - 25 * elapsedTime;
            }
            return (int)Math.Round(offset);
        }

        public void FocusOnPlayer(float elapsedTime, Vector2 playerPosition, Vector2 playerVelocity, int mapWidth, int mapHeight)
        {
            if (playerPosition.X < this.device.Viewport.Width / 2 - this.Panning(elapsedTime, playerVelocity))
                this.cameraTargetPosition.X = this.device.Viewport.Width / 2;
            else if (playerPosition.X > mapWidth - (this.device.Viewport.Width / 2) - this.Panning(elapsedTime, playerVelocity))
                this.cameraTargetPosition.X = mapWidth - (this.device.Viewport.Width / 2);
            else
                this.cameraTargetPosition.X = playerPosition.X + this.Panning(elapsedTime, playerVelocity);

            if (playerPosition.Y > mapHeight - (this.device.Viewport.Height / 2))
                this.cameraTargetPosition.Y = mapHeight - (this.device.Viewport.Height / 2);
            else
                this.cameraTargetPosition.Y = playerPosition.Y;

            this.transform = Transform;
        }

        public Matrix Transform {
            get
            {
                return Matrix.CreateScale(new Vector3(1, 1, 0)) *
                       Matrix.CreateTranslation(new Vector3(-this.cameraTargetPosition.X + (this.device.Viewport.Width / 2),
                                                            -this.cameraTargetPosition.Y + (this.device.Viewport.Height / 2), 0));
            } 
        }

        public Vector2 Target { get { return this.cameraTargetPosition; } }
    }
}
