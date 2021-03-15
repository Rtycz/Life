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

        int CountMeal = 0;

        List<Meal> Meal = new List<Meal>();
        List<CellMan> CellMan = new List<CellMan>();
        List<CellWoman> CellWoman = new List<CellWoman>();
        

        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
            CellMan.Add(new CellMan());
            CellWoman.Add(new CellWoman());
        }

        private double Dist(Cell x, Meal y)
        {
            int cellx = x.getX();
            int celly = x.getY();

            int mealx = y.getX();
            int mealy = y.getY();



            double dist = Math.Sqrt(Math.Pow(mealx - cellx, 2) + Math.Pow(mealy - celly, 2));

            return (dist);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Age++;
            Era++;

            Graphics g = CreateGraphics();
            Font fnt = new Font("Coyrier", 20);

            //Meal meal1 = new Meal();
            //meal1.Draw(g);

            if (Age >= 20)
            {
                CountMeal += 10;
                Age = 0;
                for (int i = CountMeal - 10; i < CountMeal; i++)
                {
                    Meal.Add(new Meal());
                    
                    Meal[i].setX(rand.Next(0, Width));
                    Meal[i].setY(rand.Next(0, Height));
                    Meal[i].Draw(g);
                }
            }
            foreach (Meal meal in Meal)
            {
                meal.Draw(g);
            }

            CellMan[0].Draw(g);
            
            CellWoman[0].Draw(g);

            foreach (CellWoman cell in CellWoman)
            {
                int cellx = cell.getX();
                int celly = cell.getY();
                foreach (Meal meal in Meal)
                {
                    int mealx = meal.getX();
                    int mealy = meal.getY();

                    //g.DrawLine(Pens.Blue, cellx, celly, mealx, mealy);

                    //double dist = Math.Sqrt(Math.Abs(Math.Pow(mealx - cellx, 2) + Math.Pow(mealy - celly, 2)));
                    double dist = Dist(cell, meal);

                    if ((dist < 24) && (meal.getState() == 1))
                    {

                        meal.setBrush(Brushes.Black);
                    }
                }
            }

            foreach (CellMan cell in CellMan)
            {
                int cellx = cell.getX();
                int celly = cell.getY();
                foreach (Meal meal in Meal)
                {
                    int mealx = meal.getX();
                    int mealy = meal.getY();

                    //g.DrawLine(Pens.Red, cellx, celly, mealx, mealy);

                    //double dist = Math.Sqrt(Math.Abs(Math.Pow(mealx - cellx , 2) + Math.Pow(mealy - celly, 2)));
                    double dist = Dist(cell, meal);


                    /* 
                    Вывод дистанции в адекватном формате
                    NumberFormatInfo setPrecision = new NumberFormatInfo();
                    setPrecision.NumberDecimalDigits = 2;
                    g.DrawString(dist.ToString("N", setPrecision), fnt, Brushes.YellowGreen, mealx, mealy);
                    */

                    if ((dist < 24) && (meal.getState()==1))
                    {
                        
                        meal.setBrush(Brushes.Black);
                    }
                }
            }

            /*foreach (CellMan cell in CellMan)
            {
                int cellx = cell.getX();
                int celly = cell.getY();
                foreach (CellWoman cell1 in CellWoman)
                {
                    int mealx = cell1.getX();
                    int mealy = cell1.getY();
                    g.DrawLine(Pens.Yellow, cellx, celly, mealx, mealy);
                }
            }*/


            int dirx = rand.Next(-7, 8);
            int diry = rand.Next(-7, 8);

            CellMan[0].Move(dirx, diry);

            dirx = rand.Next(-7, 8);
            diry = rand.Next(-7, 8);

            //dirx = -22;
            //diry = -22;

            CellWoman[0].Move(dirx, diry);

            g.DrawString(Era.ToString(), fnt, Brushes.Black, 900, 630);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

       
    }

    

}
