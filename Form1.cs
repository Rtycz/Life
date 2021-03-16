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
        int Age = 20;
        int Era = -1;

        
        List<CellMan> CellMan = new List<CellMan>();
        List<CellWoman> CellWoman = new List<CellWoman>();

        MealEmitter Meal = new MealEmitter();

        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
            CellMan.Add(new CellMan());
            CellWoman.Add(new CellWoman());
        }

        

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Age++;
            Era++;

            Graphics g = CreateGraphics();
            Font fnt = new Font("Coyrier", 20);

            if (Age >= 20)
            {
                Age = 0;
                Meal.Add();
            }

            Meal.CheckIntersectionM(CellMan[0]);
            Meal.CheckIntersectionW(CellWoman[0]);

            Meal.Render(g);

            CellMan[0].Draw(g);
            
            CellWoman[0].Draw(g);

            

            


            int dirx = rand.Next(-7, 8);
            int diry = rand.Next(-7, 8);

            CellMan[0].Move(dirx, diry);

            dirx = rand.Next(-7, 8);
            diry = rand.Next(-7, 8);

            CellWoman[0].Move(dirx, diry);

            g.DrawString(Era.ToString(), fnt, Brushes.Black, 900, 630);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

       
    }

    

}
