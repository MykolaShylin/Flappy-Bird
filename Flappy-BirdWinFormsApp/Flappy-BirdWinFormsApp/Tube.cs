using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_BirdWinFormsApp
{
    public class Tube
    {
        public int x;
        public int y;
        public int sizeX;
        public int sizeY;
        public Image tube;
        public Tube(int x, int y, bool isRotated = false)
        {
            tube = new Bitmap(Properties.Resources.Tube11);
            this.x = x;
            this.y = y;
            sizeX = 70;
            sizeY = 450;
            if(isRotated)
            {
                tube.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }
    }
}
