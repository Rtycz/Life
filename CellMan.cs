using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Life
{
    class CellMan : Cell
    {
        private int state = 1;
        public CellMan()
        {
            this.setX(500);
            this.setY(350);
            this.setR(30);
        }

        public override void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Blue, this.getX() - getR() / 2, this.getY() - getR() / 2, this.getR(), this.getR());
        }
    }
}
