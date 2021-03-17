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
        public CellMan()
        {
            this.setX(500);
            this.setY(350);
            this.setR(30);
            this.setState(1);
        }

        public override void Draw(Graphics g)
        {
            if (this.getState() != 0)
            {
                g.FillEllipse(Brushes.Blue, this.getX() - getR() / 2, this.getY() - getR() / 2, this.getR(), this.getR());
            }
            else
            {
                g.FillEllipse(Brushes.Black, this.getX() - getR() / 2, this.getY() - getR() / 2, this.getR(), this.getR());
            }

            
        }

        public CellMan AddChild()
        {
            CellMan newcell = new CellMan();

            newcell.setX(this.getX() + 15);
            newcell.setY(this.getY() + 15);

            return (newcell);
        }
    }
}
