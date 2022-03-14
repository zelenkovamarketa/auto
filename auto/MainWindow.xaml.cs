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

        }
    }
    public class Auto
    {
        public delegate double pocasi();
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
            Poloha = -1;
            ZmenaPolohy();
        }
        private void ZmenaPolohy() // předělat
        {
            Poloha++;
            if (Poloha == Trasa.Count)
            {
                MessageBox.Show("dojeli jste do cíle");
                return;
            }
            
            SpocitejRychlost(MetStanice.Srazky, MetStanice.Viditelnost, MetStanice.Vitr); 
            JePorouchane();
            StavSvetel(MetStanice.Viditelnost); 
            MessageBox.Show(ToString());
            System.Threading.Thread.Sleep(ran.Next(400, 800));
            ZmenaPolohy();
        }
        public void JePorouchane() // dodělat
        {
            if (ran.Next(31) == 30)
            {
                Aktivni = false;
                if (ran.Next(11) == 10)
                    Porucha = true;
                else Porucha = false;
            }
            else Aktivni = true;
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
Porucha: {Porucha}
Světla: {(Svetla ? "svítí" : "nesvítí")}
Rychlost = {Rychlost}
Trasa = {s}
Poloha = {(TypTrasy)Trasa[Poloha]}";
        }
    }
    public class MetStanice
    {
        static public double Srazky { get; private set;}
        static public double Viditelnost { get; private set; }
        static public double Vitr { get; private set; }
        static Random ran = new Random();
        static private void ZmenSrazky()
        {
            Srazky = ran.Next(0, 11001) - 1000;
            if (Srazky < 0) Srazky = 0;
        }
        static private void ZmenViditelnost()
        {
            Viditelnost = ran.Next(0, 17001);
            if (Viditelnost > 16000) Viditelnost = 16000;
        }
        static private void ZmenVitr()
        {
            Vitr = ran.Next(0, 101);
        }
    }
    static class RidiciStanice
    {
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
