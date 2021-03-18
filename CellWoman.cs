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
        //Конструктор класса
        public CellWoman()
        {
            this.setX(535);
            this.setY(385);
            this.setR(30);
            this.setState(2);
        }

        //Отрисовка женской клетки 
        public override void Draw(Graphics g)
        {
            if (this.getState() != 0)       //отрисовка живой клетки
            {
                g.FillEllipse(Brushes.Red, this.getX() - getR() / 2, this.getY() - getR() / 2, this.getR(), this.getR());
            }
            else                            //отрисовка мертвой клетки
            {
                g.FillEllipse(Brushes.Black, this.getX() - getR() / 2, this.getY() - getR() / 2, this.getR(), this.getR());
            }
        }

        //Функция возвращает новую женскую клетку в координате (-15, -15) от женской клетки-родителя
        public CellWoman AddChild()
        {
            CellWoman newcell = new CellWoman();

            newcell.setX(this.getX() - 15);
            newcell.setY(this.getY() - 15);

            return (newcell);
        }
    }
}
