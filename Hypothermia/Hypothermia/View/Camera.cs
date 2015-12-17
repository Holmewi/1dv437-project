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
        // TODO: Decide if these values should be in a model or in another view
        //private int tileSize = 64;
        //private int worldTileHight = 20;
        //private int worldTileWidth = 100;

        public Camera(GraphicsDevice device)
        {

        }

        public Vector2 GetVisualCoordinates(int logicX, int logicY)
        {
            return new Vector2(0, 0);
        }
    }
}
