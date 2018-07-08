using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsBaumkatalogPrüfung
{
    class Baumdatei
    {
        

        public void baumdateiAuslesen()
        {
            FileStream fs = new FileStream("Baumliste.txt", FileMode.Open, FileAccess.Read);

            StreamReader sr = new StreamReader(fs);


            while (sr.Peek() != -1)
            {
                Console.WriteLine(sr.ReadLine());
            }

            sr.Close();
            fs.Close();

        }

        public void baumHinzufuegen()
        {

            Console.WriteLine("Nr:");
            int nr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("X-Koordinate:");
            double x = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Y-Koordinate:");
            double y = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Stammumfang:");
            short sta = short.Parse(Console.ReadLine());
            Console.WriteLine("Kronendurchmesser:");
            float kro = float.Parse(Console.ReadLine());
            Console.WriteLine("Baumart:");
            string art = Console.ReadLine();

            Baum baum = new Baum(nr, x, y, sta, kro, art);
            string eintrag = baum.toString();

            FileStream fs = new FileStream("Baumliste.txt", FileMode.Append, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(eintrag);
            sw.Close();
            fs.Close();
        }
    }
}
