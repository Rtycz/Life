using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    class CellWomanEmitter
    {
        private int CountCellWoman = 0;

        private List<CellWoman> CellWoman;
        Random rand = new Random();
        public CellWomanEmitter()                     //Конструктор
        {
            CellWoman = new List<CellWoman>();
            CellWoman.Add(new CellWoman());
        }

        public CellWoman getCell(int i)               //Возвращает элемент с порядковым номером i
        {
            return (CellWoman[i]);
        }

        public List<CellWoman> getCellList()          //Возвращает список клеток
        {
            return (CellWoman);
        }

        public void Add(CellWoman cell)               //Добавить ребенка мужского пола в координате (+15, +15) от родителя-аргумента
        {
            CellWoman.Add(cell.AddChild());
        }

        public void Render(Graphics g)              //Отрисовывает все клетки мужского пола
        {
            foreach (CellWoman cell in CellWoman)
            {
                int dirx = rand.Next(-15, 16);
                int diry = rand.Next(-15, 16);

                cell.Move(dirx, diry);

                cell.Draw(g);
            }
        }
    }
}
