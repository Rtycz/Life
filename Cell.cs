using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Life
{
    abstract class Cell
    {
        private int x;
        private int y;
        private int r = 30;
        private int hp; //хп от еды
        private int cd; //cooldown размножения
        private int era;//эра не больше 500
        private int state;

        public Cell()
        {
            hp = 20;
            cd = 0;
            era = 0;
        }

        public int getX() { return (x + r / 2); }
        public int getY() { return (y + r / 2); }
        public int getR() { return (r); }
        public int getHp() { return (hp); }
        public int getCd() { return (cd); }
        public int getEra() { return (era); }
        public int getState() { return (state); }

        public void setX(int xx) { x = xx - r / 2; }
        public void setY(int yy) { y = yy - r / 2; }
        public void setR(int rr) { r = rr; }
        public void setHp(int hhp) { hp = hhp; }
        public void setCd(int ccd) { cd = ccd; }
        public void setEra(int eera) { era = eera; }
        public void setState(int sstate) { state = sstate; }

        public abstract void Draw(Graphics g);

        //Двигает клетку на x,y относительно текущей позиции
        public void Move(int x, int y)
        {
            this.x += x;
            this.y += y;
            era++;
            
            if (this.x < -15)
            {
                this.x = -15;
            }

            if (this.y < -15)
            {
                this.y = -15;
            }

            if (this.x > 930)
            {
                this.x = 930;
            }

            if (this.y > 665)
            {
                this.y = 665;
            }
        }

        //Поедание еды, которая приносит 20 единиц жизни
        public void Eat()
        {
            hp += 20;

            if (hp >= 100)
            {
                hp = 100;
            }
        }
    }
}
