using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Client_Graphic
{
    public class Wall
    {
        public bool IsActive { set; get; }
        public Rectangle rec { set; get; }

        public Wall(Rectangle rect, bool active)
        {
            rec = rect;

            IsActive = active;
        }
    }
}
