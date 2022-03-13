﻿using System;
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
            SpocitejRychlost(1000, 120, 5000); // předělat
            JePorouchane();
            StavSvetel(120); // předělat
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
            int i = ran.Next(0, 11001);
            if (i < 1000) Srazky = 0;
            else Srazky = i - 1000;
            IfCompleted();
        }
        public void ZmenViditelnost()
        {
            int i = ran.Next(0, 17001);
            if (i > 16000) Viditelnost = 16000;
            else Viditelnost = i;
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
