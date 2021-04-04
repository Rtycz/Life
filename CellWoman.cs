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
                //g.DrawEllipse(Pens.Black, this.getX() - getR() / 2, this.getY() - getR() / 2, this.getR(), this.getR());
                //g.FillEllipse(Brushes.Black, this.getX() - getR() / 2, this.getY() - getR() / 2, this.getR(), this.getR());
            }
            //Раскомментить чтобы увидеть количество хп на клетке
            Font fnt = new Font("Coyrier", 10);
            g.DrawString(this.getHp().ToString(), fnt, Brushes.Blue, this.getX() - 8, this.getY() - getR() / 2);
            g.DrawString(this.getCd().ToString(), fnt, Brushes.Blue, this.getX() - 8, this.getY());
            g.DrawString(this.getEra().ToString(), fnt, Brushes.Orange, this.getX() - 8, this.getY() - getR());
        }

        //Функция возвращает новую женскую клетку в координате (-15, -15) от женской клетки-родителя
        public CellWoman AddChild()
        {
            CellWoman newcell = new CellWoman();

            newcell.setX(this.getX() - 15);
            newcell.setY(this.getY() - 15);

            return (newcell);
        }

        //Возвращает дистанцию между текущей женской клеткой до мужской клетки-аргумента
        public double DistM(CellMan cell)
        {
            int cell1x = this.getX();
            int cell1y = this.getY();

            int cell2x = cell.getX();
            int cell2y = cell.getY();

            double dist = Math.Sqrt(Math.Pow(cell1x - cell2x, 2) + Math.Pow(cell1y - cell2y, 2));

            return (dist);
        }
    }
}
