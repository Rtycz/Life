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

namespace Life {
    public partial class Form1 : Form {
        int Age = 50;       //Эра - счетчик
        int Era = -1;       //Глобальная эра
        int cd = 50;

        MealEmitter MealEmitter = new MealEmitter();
        CellManEmitter CellManEmitter = new CellManEmitter();     //Экземпляр класса CellWomanEmitter 
        //создается не здесь чтобы не было проблем с передвижением элементов двух разных эмиттеров 
        CellWomanEmitter CellWomanEmitter;

        Random rand = new Random();

        public Form1() {
            InitializeComponent();
            CellWomanEmitter = new CellWomanEmitter();
        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            Age++;
            Era++;

            Graphics g = CreateGraphics();

            //Отрисовка всего содержимого на поле
            renderAll(g);

            drawEra(g);

            //Каждые 50 лет накидывается 10 еды на поле
            if (Age >= 50) {
                addMeal();
            }

            //Проверяется пересечение каждой из мужских клеток с едой
            foreach (CellMan cell in CellManEmitter.getCellList()) {
                MealEmitter.CheckIntersectionM(cell);
            }

            //Проверяется пересечение каждой из мужских клеток с едой
            foreach (CellWoman cell in CellWomanEmitter.getCellList()) {
                MealEmitter.CheckIntersectionW(cell);
            }

            Reproduction();

            MoveAll1(g);
        }

        private void timer1_Tick(object sender, EventArgs e) {
            Invalidate();
        }

        //Добавляет 10 единиц еды и обнуляет счетчик эры
        private void addMeal() {
            Age = 0;
            MealEmitter.Add();
        }

        //Отрисовывает в нижнем левом углу номер глобальной эры
        private void drawEra(Graphics g) {
            Font fnt = new Font("Coyrier", 20);
            g.DrawString(Era.ToString(), fnt, Brushes.Black, 900, 630);
        }

        //Отрисовывает все объекты на поле
        private void renderAll(Graphics g) {
            //Отрисовка еды
            MealEmitter.Render(g);

            //Отрисовка мужских клеток
            CellManEmitter.Render(g);

            //Отрисовка женских клеток
            CellWomanEmitter.Render(g);
        }

        public void MoveAll() {
            List<CellMan> ManList = CellManEmitter.getCellList();
            List<CellWoman> WomanList = CellWomanEmitter.getCellList();
            List<Meal> MealList = MealEmitter.getMeal();

            foreach (CellMan cell in ManList) {
                if (cell.getState() != 0) {
                    cell.setHp(cell.getHp() - 1);   //уменьшение жизней клетки
                    cell.setCd(cell.getCd() - 1);   //Уменьшение перезарядки размножения

                    if (cell.getHp() < 0)          //проверка на количество жизней
                        cell.setState(0);

                    Meal nearestMeal = MealEmitter.MinDistantion(cell);     //Ищется близжайшая к клетке еда

                    int cellx = cell.getX();
                    int celly = cell.getY();
                    int mealx = nearestMeal.getX();
                    int mealy = nearestMeal.getY();


                    int vectorX = Math.Sign(mealx - cellx);
                    int vectorY = Math.Sign(mealy - celly);

                    int dirx = 0, diry = 0;

                    if (cell.getHp() < 25) {
                        dirx = rand.Next(12, 16);
                        diry = rand.Next(12, 16);
                    }
                    else {
                        if (cell.getHp() < 50) {
                            dirx = rand.Next(8, 12);
                            diry = rand.Next(8, 12);
                        }
                        else {
                            CellWoman NearestWoman = CellWomanEmitter.MinDistantion(cell);

                            int nearestWomanX = NearestWoman.getX();
                            int nearestWomanY = NearestWoman.getY();

                            vectorX = Math.Sign(nearestWomanX - cellx);
                            vectorY = Math.Sign(nearestWomanY - celly);

                            dirx = rand.Next(16, 20);
                            diry = rand.Next(16, 20);
                        }
                    }

                    cell.Move(dirx * vectorX, diry * vectorY);

                }
            }

            foreach (CellWoman cell in WomanList) {
                if (cell.getState() != 0) {
                    cell.setHp(cell.getHp() - 1);   //уменьшение жизней клетки
                    cell.setCd(cell.getCd() - 1);

                    if (cell.getHp() < 0)          //проверка на количество жизней
                        cell.setState(0);

                    Meal nearestMeal = MealEmitter.MinDistantion(cell);

                    int cellx = cell.getX();
                    int celly = cell.getY();
                    int mealx = nearestMeal.getX();
                    int mealy = nearestMeal.getY();

                    int vectorX = Math.Sign(mealx - cellx);
                    int vectorY = Math.Sign(mealy - celly);

                    int dirx = 0, diry = 0;

                    if (cell.getHp() < 25) {
                        dirx = rand.Next(12, 16);
                        diry = rand.Next(12, 16);
                    }
                    else {
                        if (cell.getHp() < 50) {
                            dirx = rand.Next(8, 12);
                            diry = rand.Next(8, 12);
                        }
                        else {
                            if (cell.getHp() < 75) {
                                dirx = rand.Next(4, 8);
                                diry = rand.Next(4, 8);
                            }
                        }
                    }
                    cell.Move(dirx * vectorX, diry * vectorY);
                }
            }

        }

        public void Reproduction() {
            int i = -1;
            int j = -1;

            List<int> coeffm = new List<int>();
            List<int> coeffw = new List<int>();

            Random rnd = new Random();

            foreach (CellMan cellman in CellManEmitter.getCellList()) {
                i++;
                foreach (CellWoman cellwoman in CellWomanEmitter.getCellList()) {
                    j++;
                    if (cellwoman.DistM(cellman) < 29) {
                        if ((cellman.getCd() < 0) && (cellwoman.getCd() < 0)) {
                            if ((cellman.getHp() <= 10) || (cellwoman.getHp() <= 10)) {
                                int type = rand.Next(0, 2);
                                if (type == 0) {
                                    coeffm.Add(j);
                                }
                                else {
                                    coeffw.Add(i);
                                }
                            }
                            if ((cellman.getHp() > 10) && (cellwoman.getHp() > 10) && (cellman.getHp() <= 20) && (cellwoman.getHp() <= 20)) {
                                for (int k = 0; k < 2; k++) {
                                    int type = rand.Next(0, 2);
                                    if (type == 0) {
                                        coeffm.Add(j);
                                    }
                                    else {
                                        coeffw.Add(i);
                                    }
                                }
                            }
                            if ((cellman.getHp() > 20) && (cellwoman.getHp() > 20) && ((cellman.getHp() <= 40) || (cellwoman.getHp() <= 40))) {
                                for (int k = 0; k < 3; k++) {
                                    int type = rand.Next(0, 2);
                                    if (type == 0) {
                                        coeffm.Add(j);
                                    }
                                    else {
                                        coeffw.Add(i);
                                    }
                                }
                            }
                            if ((cellman.getHp() > 40) && (cellwoman.getHp() > 40) && (cellman.getHp() <= 60) && (cellwoman.getHp() <= 60)) {
                                for (int k = 0; k < 4; k++) {
                                    int type = rand.Next(0, 2);
                                    if (type == 0) {
                                        coeffm.Add(j);
                                    }
                                    else {
                                        coeffw.Add(i);
                                    }
                                }
                            }
                            if ((cellman.getHp() > 60) && (cellwoman.getHp() > 60)) {
                                for (int k = 0; k < 5; k++) {
                                    int type = rand.Next(0, 2);
                                    if (type == 0) {
                                        coeffm.Add(j);
                                    }
                                    else {
                                        coeffw.Add(i);
                                    }
                                }
                            }
                            cellman.setCd(cd);
                            cellwoman.setCd(cd);
                        }
                    }
                    j = -1;
                }
            }


            foreach (int coeff in coeffm) {
                CellManEmitter.Add(CellManEmitter.getCell(coeff));
            }

            foreach (int coeff in coeffw) {
                CellWomanEmitter.Add(CellWomanEmitter.getCell(coeff));
            }
        }

        public void MoveAll1(Graphics g) {
            List<CellMan> ManList = CellManEmitter.getCellList();
            List<CellWoman> WomanList = CellWomanEmitter.getCellList();
            List<Meal> MealList = MealEmitter.getMeal();

            foreach (CellMan cell in ManList) {
                if (cell.getState() != 0) {
                    cell.setHp(cell.getHp() - 1);   //уменьшение жизней клетки
                    cell.setCd(cell.getCd() - 1);   //Уменьшение перезарядки размножения

                    Meal nearestMeal = MealEmitter.MinDistantion(cell);     //Ищется близжайшая к клетке еда

                    int cellx = cell.getX();
                    int celly = cell.getY();
                    int mealx = nearestMeal.getX();
                    int mealy = nearestMeal.getY();


                    int vectorX = Math.Sign(mealx - cellx);
                    int vectorY = Math.Sign(mealy - celly);

                    int dirx = 0, diry = 0;

                    if (cell.getHp() < 25) {
                        dirx = rand.Next(12, 16);
                        diry = rand.Next(12, 16);

                        //Раскомментить для отображения направления движения
                        g.DrawLine(Pens.Yellow, cell.getX(), cell.getY(), mealx, mealy);
                    }
                    else {
                        if (cell.getHp() < 50) {
                            dirx = rand.Next(8, 12);
                            diry = rand.Next(8, 12);
                            //Раскомментить для отображения направления движения
                            g.DrawLine(Pens.Yellow, cell.getX(), cell.getY(), mealx, mealy);
                        }
                        else {
                            CellWoman NearestWoman = CellWomanEmitter.MinDistantion(cell);

                            if ((NearestWoman.getState() != 0) && (cell.getCd() < 0)) {
                                int nearestWomanX = NearestWoman.getX();
                                int nearestWomanY = NearestWoman.getY();

                                vectorX = Math.Sign(nearestWomanX - cellx);
                                vectorY = Math.Sign(nearestWomanY - celly);

                                dirx = rand.Next(16, 20);
                                diry = rand.Next(20, 20);

                                //Раскомментить для отображения направления движения
                                g.DrawLine(Pens.Yellow, cell.getX(), cell.getY(), nearestWomanX, nearestWomanY);
                            }
                            //Доделать чтобы они не стояли когда сыты и попехнуты
                            else {
                                vectorX = rand.Next(-1, 1); vectorY = rand.Next(-1, 1);
                                dirx = rand.Next(8, 12); diry = rand.Next(8, 12);
                            }
                        }
                    }


                    cell.Move(dirx * vectorX, diry * vectorY);


                    if ((cell.getHp() < 0) || (cell.getEra() >= 500))           //проверка на количество жизней
                        cell.setState(0);
                }
            }

            foreach (CellWoman cell in WomanList) {
                if (cell.getState() != 0) {
                    cell.setHp(cell.getHp() - 1);   //уменьшение жизней клетки
                    cell.setCd(cell.getCd() - 1);

                    Meal nearestMeal = MealEmitter.MinDistantion(cell);

                    int cellx = cell.getX();
                    int celly = cell.getY();
                    int mealx = nearestMeal.getX();
                    int mealy = nearestMeal.getY();

                    int vectorX = Math.Sign(mealx - cellx);
                    int vectorY = Math.Sign(mealy - celly);

                    int dirx = 0, diry = 0;

                    if (cell.getHp() < 25) {
                        dirx = rand.Next(12, 16);
                        diry = rand.Next(12, 16);
                        //Раскомментить для отображения направления движения
                        g.DrawLine(Pens.Green, cell.getX(), cell.getY(), mealx, mealy);
                    }
                    else {
                        if (cell.getHp() < 50) {
                            dirx = rand.Next(8, 12);
                            diry = rand.Next(8, 12);
                            //Раскомментить для отображения направления движения
                            g.DrawLine(Pens.Green, cell.getX(), cell.getY(), mealx, mealy);
                        }
                        else {
                            if (cell.getHp() < 75) {
                                dirx = rand.Next(4, 8);
                                diry = rand.Next(4, 8);
                                //Раскомментить для отображения направления движения
                                g.DrawLine(Pens.Green, cell.getX(), cell.getY(), vectorX, vectorY);
                            }
                        }
                    }

                    //g.DrawLine(Pens.Green, cell.getX(), cell.getY(), vectorX, vectorY);
                    cell.Move(dirx * vectorX, diry * vectorY);

                    if ((cell.getHp() < 0) || (cell.getEra() >= 500))           //проверка на количество жизней
                        cell.setState(0);
                }
            }
        }
    }
}