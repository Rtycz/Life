using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Life
{
    class CellWoman : Cell
    {
        public CellWoman()
        {
            this.setX(535);
            this.setY(385);
            this.setR(30);
            this.setState(2);
        }

        public override void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Red, this.getX() - getR() / 2, this.getY() - getR() / 2, this.getR(), this.getR());
        }
    }
}
