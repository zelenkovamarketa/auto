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
        }
    }
    public class Auto
    {
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
        }
        public void PocitaniRychlosti(double srazky, double viditelnost, double vitr)
        {
            double rychlost;
            if (Poloha == 0) rychlost = 90;
            else if (Poloha == 1) rychlost = 30;
            else rychlost = 50;
            if (viditelnost > 15000) viditelnost = 15000;
            if (vitr > 150) vitr = 150;
            if (srazky > 10000) viditelnost = 10000;
            _rychlost = rychlost * 1/15000 * viditelnost  * (1 - 1/150 * vitr) * (1 - 1/10000 * srazky);
        }
        public void StavSvetel(double viditelnost)
        {
            if (viditelnost < 1000 || Poloha == 1) Svetla = true;
            else Svetla = false;
        }
    }
    public class MetStanice
    {
        Random ran = new Random();
        event Auto.pocasi pocasiZmena;
        double _srazky;
        double _vitr;
        double _viditelnost;
        double Srazky { get { return _srazky; } set { _srazky = value; } }
        double Vitr { get { return _vitr; } set { _vitr = value; } }
        Double Viditelnost { get { return _viditelnost; } set { _viditelnost = value; } }

        public void ZmenSrazky()
        {
            Srazky = ran.Next(0, 10001);
            IfCompleted();
        }
        public void ZmenViditelnost()
        {
            Viditelnost = ran.Next(0, 15001);
            IfCompleted();
        }
        public void ZmenVitr()
        {
            Srazky = ran.Next(0, 101);
            IfCompleted();
        }
        protected virtual void IfCompleted()
        {
            pocasiZmena?.Invoke(Srazky);
            pocasiZmena?.Invoke(Viditelnost);
            pocasiZmena?.Invoke(Vitr);
        }

    }
    class RidiciStanice
    {
        List<Auto> auta = new List<Auto>();
        Random ran = new Random();
        int _poloha;
        int Poloha { get { return _poloha; } set { _poloha = value; } }
        bool Stav;
        bool Nahrada;
        public List<int> Servis(int poloha, bool aktivni)
        {
            List<int> NovaTrasa = new List<int>();
            NovaTrasa.Add(poloha);
            int i = ran.Next(1, 21);
            for (int j = 1; j < i; j++)
            {
                NovaTrasa.Add(ran.Next(3));
            }
            return null;
        }
    }
}
