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
        public CellManEmitter()                     //Конструктор
        {
            CellMan = new List<CellMan>();
            CellMan.Add(new CellMan());
        }

        public CellMan getCell(int i)               //Возвращает элемент с порядковым номером i
        {
            return (CellMan[i]);
        }

        public List<CellMan> getCellList()          //Возвращает список клеток
        {
            return (CellMan);
        }

        public void Add(CellMan cell)               //Добавить ребенка мужского пола в координате (+15, +15) от родителя-аргумента
        {
            CellMan.Add(cell.AddChild());
        }

        public void Render(Graphics g)              //Отрисовывает все клетки мужского пола
        {
            foreach (CellMan cell in CellMan)
            {
                int dirx = rand.Next(-15, 16);
                int diry = rand.Next(-15, 16);

                cell.Move(dirx, diry);

                cell.Draw(g);
            }
        }
    }
}
