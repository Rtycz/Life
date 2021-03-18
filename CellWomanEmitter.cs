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

        //Конструктор класса
        public CellWomanEmitter()                     
        {
            CellWoman = new List<CellWoman>();
            CellWoman.Add(new CellWoman());
        }

        //Возвращает элемент с порядковым номером i
        public CellWoman getCell(int i)               
        {
            return (CellWoman[i]);
        }

        //Возвращает список клеток
        public List<CellWoman> getCellList()          
        {
            return (CellWoman);
        }

        //Добавить ребенка женского пола в список рядом с клеткой-аргументом
        public void Add(CellWoman cell)               
        {
            CellWoman.Add(cell.AddChild());
        }

        //Отрисовывает все клетки женского пола
        public void Render(Graphics g)              
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
