using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace auto
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Auto a = new Auto();
            a.GenTrasy();
            tb.Text = a.ToString();

        }
    }
    public class Auto
    {
        event RidiciStanice.porucha JePorucha;
        public delegate double pocasi(double pocasi);
        Random ran = new Random();
        List<int> Trasa = new List<int>();
        double _rychlost;
        int _poloha;
        enum TypTrasy { normal, most, tunel };
        double Rychlost { get { return _rychlost; } set { _rychlost = value; } }
        int Poloha { get { return _poloha; } set { _poloha = value; } }
        bool Svetla;
        bool Aktivni;
        bool Porucha;
        public Auto() { }
        public void GenTrasy()
        {
            Trasa.Clear();
            int i = ran.Next(1, 51);
            for (int j = 0; j < i; j++)
            {
                Trasa.Add(ran.Next(3));
            }
            ZmenaPolohy();
        }
        private void ZmenaPolohy()
        {
            Poloha++;
            if (Poloha == Trasa.Count)
            {
                MessageBox.Show("dojeli jste do cíle");
                return;
            }
            
            double d = MetStanice.Viditelnost();
            MessageBox.Show(d.ToString());
            SpocitejRychlost(MetStanice.Srazky(), d, MetStanice.Vitr()); // předělat
            JePorouchane();
            StavSvetel(d); // předělat
            MessageBox.Show(ToString());
            System.Threading.Thread.Sleep(ran.Next(4000, 8000));
            ZmenaPolohy();
        }
        public bool JePorouchane()
        {
            
            if (ran.Next(51) == 50)
            {
                JePorucha?.Invoke(true);
            }
            return false;
        }
        public void SpocitejRychlost(double srazky, double viditelnost, double vitr)
        {
            double rychlost;
            if (Trasa[Poloha] == 0) rychlost = 90;
            else if (Trasa[Poloha] == 1) rychlost = 30;
            else rychlost = 50;
            _rychlost = rychlost * 1 / 16000 * viditelnost * (1 - 1 / 150 * vitr) * (1 - 1 / 10000 * srazky);
        }
        public void StavSvetel(double viditelnost)
        {
            if (viditelnost < 1000 || Trasa[Poloha] == 1) Svetla = true;
            else Svetla = false;
        }
        public override string ToString()
        {
            string s = "";
            foreach (int item in Trasa)
            {
                s += item.ToString();
            }
            return $@"Stav: {(Aktivni ? "aktivní" : "neaktivní")}
Světla: {(Svetla ? "svítí" : "nesvítí")}
Rychlost = {Rychlost}
Trasa = {s}
Poloha = {Poloha}";
        }
    }
    public class MetStanice
    {
        static Random ran = new Random();
        static public double Srazky()
        {
            double d = ran.Next(0, 11001);
            if (d < 1000) d = 0;
            else  d -= 1000;
            return d;
        }
        static public double Viditelnost()
        {
            double d = ran.Next(0, 17001);
            if (d > 16000) d = 16000;
            return d;
        }
        static public double Vitr()
        {
            return ran.Next(0, 101);
        }
    }
    static class RidiciStanice
    {
        public delegate bool porucha(bool porucha);

        static Random ran = new Random();
        static List<int> Servis(int poloha, bool aktivni)
        {
            List<int> NovaTrasa = new List<int>();
            NovaTrasa.Add(poloha);
            int i = ran.Next(1, 21);
            for (int j = 1; j < i; j++)
            {
                NovaTrasa.Add(ran.Next(3));
            }
            return NovaTrasa;
        }
    }
}
