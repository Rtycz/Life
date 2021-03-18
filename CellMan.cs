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
        //Конструктор класса
        public CellMan()
        {
            this.setX(500);
            this.setY(350);
            this.setR(30);
            this.setState(1);
        }

        //Отрисовка мужской клетки
        public override void Draw(Graphics g)
        {
            if (this.getState() != 0)       //Отрисовка живой клетки
            {
                g.FillEllipse(Brushes.Blue, this.getX() - getR() / 2, this.getY() - getR() / 2, this.getR(), this.getR());
            }
            else                            //Отрисовка Мертвой клетки клетки
            {
                g.DrawEllipse(Pens.Black, this.getX() - getR() / 2, this.getY() - getR() / 2, this.getR(), this.getR());
                //g.FillEllipse(Brushes.Black, this.getX() - getR() / 2, this.getY() - getR() / 2, this.getR(), this.getR());
            }
            //Раскомментить чтобы увидеть количество хп на клетке
            //Font fnt = new Font("Coyrier", 20);
            //g.DrawString(this.getHp().ToString(), fnt, Brushes.Red, this.getX() - getR() / 2, this.getY() - getR() / 2);
        }

        //Функция возвращает новую мужскую клетку в координате (+15, +15) от мужской клетки-родителя
        public CellMan AddChild()
        {
            CellMan newcell = new CellMan();

            newcell.setX(this.getX() + 15);
            newcell.setY(this.getY() + 15);

            return (newcell);
        }
    }
}
