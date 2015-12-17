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
    
        private Box box;

        public BoxCollider(Box box, int startX, int endX)
        {
            this.box = box;
            this.startX = startX;
            this.endX = endX;

            if (startX == 0 && endX == 0)
                this.isSolid = true;
            else
                this.isSolid = false;
        }

        public bool IsSolid { get { return this.isSolid; } }

        public int StartX { get { return this.startX; } }

        public int EndX { get { return this.endX; } }

        public Box Box { get { return this.box; } }


        /*
         * if(player.intersects(tileGameObject) {
         *  if(tileGameObject.IsSolid) {
         *      player.position.X 
         *  }
         * }
         * 
         * 
         * 
         */
    }
}
