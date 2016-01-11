using Hypothermia.Controller;
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
        private const int MAX_OFFSET = 125;
        private const int PANNING_SPEED = 25;
        private float offset = 0;

        private int tileSize;
        private int mapWidth;
        private int mapHeight;
        private int deviceWidth;
        private int deviceHeight;

        public Camera(GraphicsDevice device, int tileSize)
        {
            this.deviceWidth = device.Viewport.Width;
            this.deviceHeight = device.Viewport.Height;
            this.device = device;
            this.tileSize = tileSize;
            this.cameraTargetPosition = new Vector2(this.device.Viewport.Width / 2, this.device.Viewport.Height / 2);
        }

        /*
         * This method is used for interface to render on screen
         */
        public Vector2 GetDeviceCoordinates(int visualX, int visualY)
        {
            float logicX = this.cameraTargetPosition.X - visualX;
            float logicY = this.cameraTargetPosition.Y - visualY;

            return new Vector2(logicX, logicY);
        }

        /*
         * This method is used to get tile based position
         */
        public Vector2 GetMapCoordinates(int tileX, int tileY)
        {
            Vector2 mapTiles = new Vector2(this.mapWidth / this.tileSize, this.mapHeight / this.tileSize);

            if (tileX > mapTiles.X || tileY > mapTiles.Y)
                throw new ArgumentOutOfRangeException("The tileX does not exist in the map.");

            float visualX = tileX * this.tileSize;
            float visualY = tileY * this.tileSize;
            return new Vector2(visualX, visualY);
        }

        public Vector2 GetLogicCoordinates(int visualX, int visualY)
        {
            float logicX = 0 + visualX;
            float logicY = this.mapHeight - visualY;

            return new Vector2(logicX, logicY);
        }

        private int Panning(float elapsedTime, bool faceForward)
        {
            if (faceForward && offset <= MAX_OFFSET)
                offset = offset + PANNING_SPEED * elapsedTime;
            else if (!faceForward && offset >= -MAX_OFFSET)
                offset = offset - PANNING_SPEED * elapsedTime;
            else
            {
                if (offset < MAX_OFFSET)
                    offset = offset + PANNING_SPEED * elapsedTime;
                if (offset > MAX_OFFSET)
                    offset = offset - PANNING_SPEED * elapsedTime;
            }
            return (int)Math.Round(offset);
        }

        public void FocusOnPlayer(float elapsedTime, Vector2 playerPosition, bool faceForward, int mapWidth, int mapHeight)
        {
            if (playerPosition.X < this.device.Viewport.Width / 2 - this.Panning(elapsedTime, faceForward))
                if (mapWidth > this.device.Viewport.Width)
                    this.cameraTargetPosition.X = this.device.Viewport.Width / 2;
                else
                    this.cameraTargetPosition.X = mapWidth / 2;
            else if (playerPosition.X > mapWidth - (this.device.Viewport.Width / 2) - this.Panning(elapsedTime, faceForward))
                if(mapWidth > this.device.Viewport.Width)
                    this.cameraTargetPosition.X = mapWidth - (this.device.Viewport.Width / 2);
                else
                    this.cameraTargetPosition.X = mapWidth / 2;
            else
                this.cameraTargetPosition.X = playerPosition.X + this.Panning(elapsedTime, faceForward);

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

        public int TileSize { get { return this.tileSize; } }

        public int MapWidth
        {
            get { return this.mapWidth; }
            set { this.mapWidth = value; }
        }
        public int MapHeight
        {
            get { return this.mapHeight; }
            set { this.mapHeight = value; }
        }

        public int DeviceWidth { 
            get { return this.deviceWidth; }
            set { this.deviceWidth = value; }
        }

        public int DeviceHeight { 
            get { return this.deviceHeight; }
            set { this.deviceHeight = value; }
        }

        public int MaxOffset { get { return MAX_OFFSET; } }
    }
}
