using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankDLL
{
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    public class Tank
    {

        public int HP { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }
        public int[] Color { get; set; }
        public int TankID { get; set; }
        public float Rotation { get; set; }
        public bool IsAlive { get; set; }

        public int CD_Respawn { get; set; }
        public Bullet bullet { get; set; }
        public Direction tankDirection { get; set; }
        public int TankRealWidth { get; set; }
        public int TankRealHeight { get; set; }
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
            TankID = 0;
            tankDirection = Direction.UP;
            CD_Respawn = 0;
            IsAlive = true;
            TankRealWidth = 19;
            TankRealHeight = 23;
        }

        public Tank(int x, int y, int speed, int[] color, int rotation)
        {
            X = x;
            Y = y;
            Speed = speed;
            Color = color;
            Rotation = rotation;
        }
        public void CheckHP()
        {
            if (IsAlive)
            {
                if (HP == 75)
                {
                    Color[0] = Color[0] - 15;
                    Color[1] = Color[1] - 15;
                    Color[2] = Color[2] - 15;
                }
                else if (HP == 50)
                {
                    Color[0] = Color[0] - 15;
                    Color[1] = Color[1] - 15;
                    Color[2] = Color[2] - 15;
                }
                else if (HP == 25)
                {
                    Color[0] = Color[0] - 15;
                    Color[1] = Color[1] - 15;
                    Color[2] = Color[2] - 15;
                }
                else if (HP <= 0)
                {
                    IsAlive = false;
                    CD_Respawn = 600;
                }
            }

        }
        
        public bool TankRespawn()
        {
            bool isRespawnWas = false;
            if (isRespawnWas == false)
            {
                if (IsAlive == false)
                {
                    CD_Respawn--;
                    if (CD_Respawn <= 0)
                    {
                        HP = 100;
                        CD_Respawn = 0;
                        IsAlive = true;
                        X = 300;
                        Y = 300;
                        isRespawnWas = true;
                        return true;
                    }
                }
            }
            return false;
        }
        


    }
}
