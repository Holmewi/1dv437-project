using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypothermia.Model
{
    public class BoxCollider
    {
        private GameObject gameObject;

        public BoxCollider(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}
