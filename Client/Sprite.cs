using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TankDLL;
namespace Client_Graphic
{
    public class Sprite
    {
        public Texture2D TankTexture { get; set; }
        public Texture2D BulletTexture { get; set; }
        public Texture2D MapTexture { get; set; }
        public Tank tank { get; set; }
        public Sprite(Texture2D textureT, Tank tank, Texture2D textureB, Bullet bullet)
        {
            this.TankTexture = textureT;
            this.tank = tank;
            this.BulletTexture = textureB;
            this.tank.bullet = bullet;
        }
    }
}
