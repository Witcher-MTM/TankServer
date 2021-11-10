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
        public Color color { get; set; }
        public string name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Button()
        {
            this.X = 420;
            this.Y = 320;
        }
        public Button(Rectangle rec , Texture2D texture,string name)
        {
            this.Rectangle = rec;
            this.Texture = texture;
            this.color = Color.Black;
            this.name = name;
        }
        public Button(Rectangle rec, Texture2D texture)
        {
            this.Rectangle = rec;
            this.Texture = texture;
            this.color = Color.Black;
            this.name = name;
        }
    }
}
