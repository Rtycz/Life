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
        int Age = 50;       //Эра - счетчик
        int Era = -1;       //Глобальная эра

        MealEmitter         MealEmitter         = new MealEmitter();
        CellManEmitter      CellManEmitter      = new CellManEmitter();     //Экземпляр класса CellWomanEmitter 
        //создается не здесь чтобы не было проблем с передвижением элементов двух разных эмиттеров 
        CellWomanEmitter    CellWomanEmitter    ;

        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
            CellWomanEmitter = new CellWomanEmitter();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Age++;
            Era++;

            Graphics g = CreateGraphics();
            

            //Каждые 50 лет накидывается 10 еды на поле
            if (Age >= 50)
            {
                CellManEmitter.Add(CellManEmitter.getCell(0));
                CellWomanEmitter.Add(CellWomanEmitter.getCell(0));

                addMeal();
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

            MoveAll();

            //Отрисовка всего содержимого на поле
            renderAll(g);

            drawEra(g);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        //Добавляет 10 единиц еды и обнуляет счетчик эры
        private void addMeal()
        {
            Age = 0;
            MealEmitter.Add();
        }

        //Отрисовывает в нижнем левом углу номер глобальной эры
        private void drawEra(Graphics g)
        {
            Font fnt = new Font("Coyrier", 20);
            g.DrawString(Era.ToString(), fnt, Brushes.Black, 900, 630);
        }

        //Отрисовывает все объекты на поле
        private void renderAll(Graphics g)
        {
            //Отрисовка еды
            MealEmitter.Render(g);

            //Отрисовка мужских клеток
            CellManEmitter.Render(g);

            //Отрисовка женских клеток
            CellWomanEmitter.Render(g);
        }

        public void MoveAll()
        {
            List<CellMan> ManList = CellManEmitter.getCellList();
            List<Meal> MealList = MealEmitter.getMeal();

            foreach (CellMan cell in ManList)
            {
                if (cell.getState() != 0)
                {
                    cell.setHp(cell.getHp() - 1);   //уменьшение жизней клетки

                    Meal nearestMeal = MealEmitter.MinDistantionM(cell);

                    int cellx = cell.getX();
                    int celly = cell.getY();
                    int mealx = nearestMeal.getX();
                    int mealy = nearestMeal.getY();

                    int vectorX = Math.Sign(mealx - cellx);
                    int vectorY = Math.Sign(mealy - celly);

                    int dirx = rand.Next(-3, 16);
                    int diry = rand.Next(-3, 16);


                    cell.Move(dirx * vectorX, diry * vectorY);

                    if (cell.getHp() < 0)           //проверка на количество жизней
                        cell.setState(0);

                }



            }
        }

    }

    

}
