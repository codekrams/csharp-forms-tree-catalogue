using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsBaumkatalogPrüfung
{
    public partial class Baumprogram : Form
    {
        List<Baum> baumliste = new List<Baum>();
        int index;

        public Baumprogram()
        {
            InitializeComponent();
        }

        private void Baumprogram_Load(object sender, EventArgs e)
        {
            listeErstellen();
            listeAnzeigen();
        }
        private void leeren_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(textBox3.Text) && !String.IsNullOrEmpty(textBox4.Text) && !String.IsNullOrEmpty(textBox5.Text) && !String.IsNullOrEmpty(textBox6.Text))
            {
                bool nummer = baumnrPruefen(textBox1.Text);


                if (nummer == true)
                {
                    Baum nbaum = new Baum(Convert.ToInt32(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text), Convert.ToInt32(textBox4.Text), Convert.ToDouble(textBox5.Text), textBox6.Text);
                    baumliste.Add(nbaum);
                    listeSpeichern();
                    listeAnzeigen();
                    MessageBox.Show(nbaum.getArt() + " ist eingetragen");
                }
                else {
                    MessageBox.Show("Nummer bereits vergeben");
                }
            }
            else {
                MessageBox.Show("Bitte alle Felder ausfüllen");
            }
            /* prüfen, ob was ausgewählt ist, dann nur einen Button für speichern und ändern
            if (comboBox1.SelectedIndex == -1)
            {
                int abtnr = abt[comboBox2.SelectedIndex].getAbteilungsnummer();
                db.safePersonal(textBox1.Text, textBox2.Text, abtnr.ToString());
            }

            if (comboBox1.SelectedIndex > -1)
            {
                int id = liPers[comboBox1.SelectedIndex].getPersonalnummer();
                int abtnr = abt[comboBox2.SelectedIndex].getAbteilungsnummer();
                db.updatePersonal(textBox1.Text, textBox2.Text, abtnr.ToString(), id);
            }*/
        }

        private void Baumliste_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Baumliste.SelectedIndex > -1)
            {
                anzeigen(Baumliste.SelectedIndex);
                anzeigenRichtextbox(Baumliste.SelectedIndex);
                comboBox1.SelectedIndex = Baumliste.SelectedIndex;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                anzeigen(comboBox1.SelectedIndex);
                anzeigenRichtextbox(comboBox1.SelectedIndex);
                Baumliste.SelectedIndex = comboBox1.SelectedIndex;

            }
        }
        private void aendern_Click(object sender, EventArgs e)
        {
            Baum nbaum = new Baum(Convert.ToInt32(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text), Convert.ToInt32(textBox4.Text), Convert.ToDouble(textBox5.Text), textBox6.Text);
            baumliste[comboBox1.SelectedIndex] = nbaum;
            MessageBox.Show("Änderung an dem Eintrag mit der Nummer " + baumliste[comboBox1.SelectedIndex].getNr() + " übernommen");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            index = 0;
            if (baumliste.Count > index)
            {
                anzeigen(index);
                anzeigenRichtextbox(index);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (index > 0)
            {
                index -= 1;
                anzeigen(index);
                anzeigenRichtextbox(index);
                indexGleichsetzen(index);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (index < baumliste.Count - 1)
            {
                index += 1;
                anzeigen(index);
                anzeigenRichtextbox(index);
                indexGleichsetzen(index);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            index = baumliste.Count - 1;
            anzeigen(index);
            anzeigenRichtextbox(index);
            indexGleichsetzen(index);
        }

        public bool baumnrPruefen(string nummer) {

            int pruef = 0;

            FileStream fs = new FileStream("Baumliste.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);


            while (sr.Peek() != -1)
            {
                if (sr.ReadLine() == nummer)
                {
                    
                    pruef = 0;
                    break;
                }
                else
                {
                    pruef = 1;
                }
            }

            sr.Close();
            fs.Close();

            if (pruef == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void listeSpeichern() {
            FileStream fs = new FileStream("Baumliste.txt", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            foreach (Baum b in baumliste)
            {
                sw.WriteLine(b.getNr());
                sw.WriteLine(b.getX());
                sw.WriteLine(b.getY());
                sw.WriteLine(b.getSta());
                sw.WriteLine(b.getKro());
                sw.WriteLine(b.getArt());
            }

            sw.Close();
            fs.Close();
        }

        private void listeAnzeigen()
        {
            Baumliste.Items.Clear();
            comboBox1.Items.Clear();

            foreach (Baum b in baumliste)
            {
                Baumliste.Items.Add(b.getNr() + ", " +b.getArt());
                comboBox1.Items.Add(b.getNr() + ", " + b.getArt());
            }
        }

        private void listeErstellen() {
            comboBox1.Items.Clear();
            Baumliste.Items.Clear();

            FileStream fs = new FileStream("Baumliste.txt", FileMode.Open, FileAccess.Read);

            StreamReader sr = new StreamReader(fs);

            while (sr.Peek() != -1)
            {
                Baum nbaum = new Baum(Convert.ToInt32(sr.ReadLine()), Convert.ToDouble(sr.ReadLine()), Convert.ToDouble(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()), Convert.ToDouble(sr.ReadLine()), sr.ReadLine());
                baumliste.Add(nbaum);
            }
            sr.Close();
            fs.Close();

            listeAnzeigen();
        }

        private void indexGleichsetzen(int i)
        {
            comboBox1.SelectedIndex = i;
            Baumliste.SelectedIndex = i;
        }


        private void anzeigen(int which)
        {
            textBox1.Text = baumliste[which].getNr().ToString();
            textBox2.Text = baumliste[which].getX().ToString(); 
            textBox3.Text = baumliste[which].getY().ToString();
            textBox4.Text = baumliste[which].getSta().ToString();
            textBox5.Text = baumliste[which].getKro().ToString();
            textBox6.Text = baumliste[which].getArt();
        }

        private void anzeigenRichtextbox(int which)
        {
            richTextBox1.Text = "";
            richTextBox1.Text = "Baumnummer: " + baumliste[which].getNr().ToString() + "\nX-Koordinate: " + baumliste[which].getX().ToString() + "\nY-Koordinate" + baumliste[which].getY().ToString() + "\nStammumfang" + baumliste[which].getSta().ToString() + "\nKronendurchmesser: " + baumliste[which].getKro().ToString() + "\nBaumaurt: " + baumliste[which].getArt();
        }


    }
}
/* Delegates

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms_Taschenrechner_Delegate
{
    public delegate int TaschenRecher(int x, int y);

    public partial class Form1 : Form
    {
        private TaschenRecher tr;
        private int ergebnis;


        public int Addieren(int x, int y)
        {
            return ergebnis = x + y;
        }
        public int Subtrahieren(int x, int y)
        {
            return ergebnis = x - y;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tr = Addieren;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tr = Subtrahieren;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tr != null && !String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text))
            {
                label1.Text = tr(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text)).ToString();
            }
            else
            {
                MessageBox.Show("Bitte Rechenoperation auswählen");
            }
        }
    }
}
*/