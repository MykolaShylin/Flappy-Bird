using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_BirdWinFormsApp
{
    public class Bird
    {
        public float x;
        public float y;
        public Image bird;
        public int size;
        public float gravityValue;
        public Bird(int x, int y)
        {
            bird = new Bitmap(Properties.Resources.Bird2);
            this.x = x; 
            this.y = y;
            size = 40;
            gravityValue = 0.125f;
        }
    }
}
