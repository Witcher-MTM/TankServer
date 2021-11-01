﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankDLL
{
    public class Tank
    {
        public int HP { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }
        public int[] Color { get; set; }
        public int TankID { get; set; }
        public float Rotation { get; set; }
        public int CD { get; set; }
        public Bullet bullet { get; set; }
        public Tank()
        {
            Random r = new Random();
            HP = 100;
            bullet = new Bullet();
            X = 300;
            Y = 300;
            Speed = 3;
            Color = new int[] { r.Next(50, 256), r.Next(50, 256), r.Next(50, 256) };
            Rotation = 0f;
            CD = 0;
        }

        public Tank(int x, int y, int speed, int[] color, int rotation)
        {
            X = x;
            Y = y;
            Speed = speed;
            Color = color;
            Rotation = rotation;
        }
       
    }
}
