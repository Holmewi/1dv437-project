using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.Model
{
    public class BoxCollider
    {
        private bool isSolid;
        private int startX;
        private int endX;

        private Rectangle rect;

        public BoxCollider(Rectangle rect, int startX, int endX)
        {
            this.rect = rect;
            this.startX = startX;
            this.endX = endX;

            if (startX == endX)
                this.isSolid = true;
            else
                this.isSolid = false;
        }

        public bool IsSolid { get { return this.isSolid; } }

        public int StartX { get { return this.startX; } }

        public int EndX { get { return this.endX; } }

        public Rectangle Rect { get { return this.rect; } }
    }
}
