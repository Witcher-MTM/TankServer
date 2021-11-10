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
        public int CD { get; set; }
        public Direction bulletDirection { get; set; }
        public bool IsActive { get; set; }
        public Bullet()
        {
            this.Damage = 25;
            this.Speed = 10;
            this.CoordX = -10;
            this.CoordY = -10;
            this.Rotation = 0;
            this.IsActive = false;
            this.CD = 0;
        }
        public void CheckDirection()
        {
            switch (bulletDirection)
            {
                case Direction.UP:
                    {
                        Rotation = 0f;
                        CoordY -= 40;

                        break;
                    }
                case Direction.DOWN:
                    {
                        Rotation = 15.7f;
                        CoordY += 40;

                        break;
                    }
                case Direction.LEFT:
                    {
                        Rotation = -7.85f;
                        CoordX -= 40;

                        break;
                    }
                case Direction.RIGHT:
                    {
                        Rotation = 7.85f;
                        CoordX += 40;

                        break;
                    }
                default:
                    break;

            }


        }
    }
}
