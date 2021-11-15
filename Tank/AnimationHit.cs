using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankDLL
{
    public class AnimationHit
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Iterator { get; set; }
        public bool Draw { get; set; }
        public AnimationHit()
        {

        }
        public void Animate()
        {
            Iterator++;
            Y--;
            if (Iterator > 100)
            {
                Draw = false;
                Iterator = 0;
            }
        }
    }
}
