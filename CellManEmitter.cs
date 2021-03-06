using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    class CellManEmitter
    {
        private int             CountCellMan = 0;

        private List<CellMan>   CellMan;
        Random                  rand                = new Random();

        //Конструктор
        public CellManEmitter() 
        {
            CellMan = new List<CellMan>();
            CellMan.Add(new CellMan());
        }

        //Возвращает элемент с порядковым номером i
        public CellMan getCell(int i) 
        {
            return (CellMan[i]);
        }

        //Возвращает список клеток
        public List<CellMan> getCellList() 
        {
            return (CellMan);
        }

        //Добавить ребенка мужского пола в список рядом с клеткой-аргументом
        public void Add(CellMan cell)
        {
            CellMan.Add(cell.AddChild());
        }

        //Отрисовывает все клетки мужского пола
        public void Render(Graphics g)
        {
            foreach (CellMan cell in CellMan)
            {
                cell.Draw(g);
            }
        }

        //Возвращает близжайшую к клетке-аргументу клетку-мужчину
        public CellMan MinDistantion(CellWoman cell)
        {
            double minDist = 10000;
            double curDist = 0;

            CellMan nearestMan = CellMan[0];

            foreach (CellMan cell1 in CellMan)
            {
                if (cell1.getState() != 0)
                {
                    curDist = cell1.Dist(cell);

                    if (minDist > curDist)
                    {
                        nearestMan = cell1;
                        minDist = curDist;
                    }
                }
            }

            return (nearestMan);
        }
    }
}
