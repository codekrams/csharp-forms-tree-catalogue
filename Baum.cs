using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsBaumkatalogPrüfung
{
    class Baum
    {
        private int Nr;
        private double X;
        private double Y;
        private int Sta;
        private double Kro;
        private string Art;

        public int getNr()
        {
            return Nr;
        }

        public Baum(int nr, double x, double y, int sta, double kro, string art)
        {
            Nr = nr;
            X = x;
            Y = y;
            Sta = sta;
            Kro = kro;
            Art = art;
        }

        public string toString()
        {
            string baum;
            baum = Nr.ToString() + "; " + X.ToString() + "; " + Y.ToString() + "; " + Sta.ToString() + "; " + Kro.ToString() + "; " + Art;
            return baum;
        }

        public void setNr(int nummer)
        {
            Nr = nummer;
        }

        public double getX()
        {
            return X;
        }

        public void setX(double x)
        {
            X = x;
        }

        public double getY()
        {
            return Y;
        }

        public void setY(double y)
        {
            Y = y;
        }

        public double getKro()
        {
            return Kro;
        }

        public void setKro(float kro)
        {
            Kro = kro;
        }

        public int getSta()
        {
            return Sta;
        }

        public void setSta(short sta)
        {
            Sta = sta;
        }

        public string getArt()
        {
            return Art;
        }

        public void setArt(string art)
        {
            Art = art;
        }
    }
}
