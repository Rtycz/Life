using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Life
{
    class Meal
    {
        private int state;
        private int x;
        private int y;
        private int r = 20;

        public Meal()
        {
            state = 1;
            setX(0);
            setY(0);
        }

        public int getX() { return (x + r / 2); }
        public int getY() { return (y + r / 2); }
        public int getState() { return (state); }

        public void setX(int xx) { x = xx - r / 2; }
        public void setY(int yy) { y = yy - r / 2; }
        public void setState(int st) { state = st; }

        public void Draw(Graphics g)
        {
            if (state == 1)
            {
                Brush brr = Brushes.DarkOliveGreen;
                g.FillEllipse(brr, x, y, r, r);
            }
            
        }
    }
}
