using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankDLL
{
    public class Bullet
    {
        public int Damage { get; set; }
        public int Speed { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        public float Rotation { get; set; }
        public Bullet()
        {
            this.Damage = 25;
            this.Speed = 5;
            this.CoordX = -10;
            this.CoordY = -10;
            this.Rotation = 0;

        }


    }
}
