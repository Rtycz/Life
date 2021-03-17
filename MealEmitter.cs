﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Life
{
    class MealEmitter
    {
        private int     CountMeal   = 0;
        private Random  rand        = new Random();

        List<Meal>      Meal;


        //Конструктор
        public MealEmitter() 
        {
            Meal = new List<Meal>();
        }

        //Добавить 10 единиц еды на игровое поле
        public void Add() 
        {
            CountMeal += 10;
            for (int i = CountMeal - 10; i < CountMeal; i++)
            {
                Meal.Add(new Meal());

                Meal[i].setX(rand.Next(0, 960));
                Meal[i].setY(rand.Next(0, 695));
            }
        }

        //Отрисовать всю еду
        public void Render(Graphics g) 
        {
            foreach (Meal meal in Meal)
            {
                meal.Draw(g);
            }
        }

        //Возвращает дистанцию от клетки x до еды у
        private double Dist(Cell x, Meal y) 
        {
            int cellx = x.getX();
            int celly = x.getY();

            int mealx = y.getX();
            int mealy = y.getY();
            double dist = Math.Sqrt(Math.Pow(mealx - cellx, 2) + Math.Pow(mealy - celly, 2));

            return (dist);
        }

        //Проверяет еду на съеденность клеткой cell, если игра съедена то ставит на нее статус 0
        public void CheckIntersectionM(CellMan cell)
        {
            int x = cell.getX();
            int y = cell.getY();

            foreach (Meal meal in Meal)
            {
                int mealx = meal.getX();
                int mealy = meal.getY();

                //g.DrawLine(Pens.Blue, cellx, celly, mealx, mealy);

                //double dist = Math.Sqrt(Math.Abs(Math.Pow(mealx - cellx, 2) + Math.Pow(mealy - celly, 2)));
                double dist = Dist(cell, meal);

                if ((dist < 24) && (meal.getState() == 1))
                {
                    meal.setState(0);
                }
            }
        }

        //Проверяет еду на съеденность клеткой cell, если игра съедена то ставит на нее статус 0
        public void CheckIntersectionW(CellWoman cell)
        {
            int x = cell.getX();
            int y = cell.getY();

            foreach (Meal meal in Meal)
            {
                int mealx = meal.getX();
                int mealy = meal.getY();

                //g.DrawLine(Pens.Blue, cellx, celly, mealx, mealy);

                //double dist = Math.Sqrt(Math.Abs(Math.Pow(mealx - cellx, 2) + Math.Pow(mealy - celly, 2)));
                double dist = Dist(cell, meal);

                if ((dist < 24) && (meal.getState() == 1))
                {
                    meal.setState(0);
                }
            }
        }           

    }
}