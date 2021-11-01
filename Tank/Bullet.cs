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
            this.Speed = 10;
            this.CoordX = 0;
            this.CoordY = 0;
            this.Rotation = 0;

        }


    }
}
