﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auto
{
    class Auto
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
}