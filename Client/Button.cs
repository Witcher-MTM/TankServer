using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Client_Graphic
{
    public class Button
    {
        public Rectangle Rectangle { get; set; }
        public Texture2D Texture { get; set; }

        public Button()
        {

        }
        public Button(Rectangle rec , Texture2D texture)
        {
            this.Rectangle = rec;
            this.Texture = texture;
        }
    }
}
