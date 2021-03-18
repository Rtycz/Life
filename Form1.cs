using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Life;

namespace Life
{
    public partial class Form1 : Form
    {
        int Age = 50;
        int Era = -1;
        
        //List<CellWoman> CellWoman = new List<CellWoman>();

        MealEmitter         MealEmitter         = new MealEmitter();
        CellManEmitter      CellManEmitter      = new CellManEmitter();
        CellWomanEmitter    CellWomanEmitter    = new CellWomanEmitter();

        Random              rand                = new Random();

        public Form1()
        {
            InitializeComponent();
            //CellMan.Add(new CellMan());
            //CellWoman.Add(new CellWoman());
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Age++;
            Era++;

            Graphics g = CreateGraphics();
            Font fnt = new Font("Coyrier", 20);

            //Каждые 50 лет накидывается 10 еды на поле
            if (Age >= 50)
            {
                CellManEmitter.Add(CellManEmitter.getCell(0));
                //CellWomanEmitter.Add(CellWomanEmitter.getCell(0));

                Age = 0;

                MealEmitter.Add();
            }

            if (Age == 20)
            {
                //CellManEmitter.Add(CellManEmitter.getCell(0));
                CellWomanEmitter.Add(CellWomanEmitter.getCell(0));

                //Age = 0;

                //MealEmitter.Add();
            }

            //Проверяется пересечение каждой из мужских клеток с едой
            foreach (CellMan cell in CellManEmitter.getCellList())
            {
                MealEmitter.CheckIntersectionM(cell);
            }

            //Проверяется пересечение каждой из мужских клеток с едой
            foreach (CellWoman cell in CellWomanEmitter.getCellList())
            {
                MealEmitter.CheckIntersectionW(cell);
            }


            //MealEmitter.CheckIntersectionW(CellWoman[0]);

            //Отрисовка еды
            MealEmitter.Render(g);

            //Отрисовка мужских клеток
            CellManEmitter.Render(g);

            //Отрисовка женских клеток
            CellWomanEmitter.Render(g);

            //CellWoman[0].Draw(g);

            int dirx = rand.Next(-7, 8);
            int diry = rand.Next(-7, 8);

            //CellWoman[0].Move(dirx, diry);

            //Отрисовка эры
            g.DrawString(Era.ToString(), fnt, Brushes.Black, 900, 630);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

       
    }

    

}
