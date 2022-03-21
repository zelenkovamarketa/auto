using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace auto
{
     enum TranslateLocation { vyrazit, normal, most, tunel};
    public class CarErrorEventArgs : EventArgs
    {
        public Guid FromCar { get; set; }
        public int Severity { get; set; }
    }
    public class CarSpeedArgs : EventArgs
    {
        public Guid FromCar { get; set; }
        public double Speed { get; set; }
    }
    public delegate void CarError(CarErrorEventArgs e);
    public delegate void CarSpeed(CarSpeedArgs e);

    public class Car
    {
        Random rnd = new Random();
        private List<int> trasa = new List<int>();
        public int Poloha { get; private set; }
        public Guid Id { get; set; }
        public Car()
        {
            Id = Guid.NewGuid();
        }

        public event CarError ErrorJustHappened;
        public event CarSpeed CarSpeedChanged;
        public void RouteGeneration()
        {
            Poloha = 0;
            trasa.Clear();
            for (int i = 0; i < rnd.Next(1,50); i++)
                trasa.Add(rnd.Next(1,4));
        }
        public string GetRoute()
        {
            string s = "";
            foreach (var item in trasa)
                s += item.ToString();
            return s;
        }
        public string GetLocation()
        {
            if (Poloha >= trasa.Count)
                 return "Dojeli jste do cíle";
            if (trasa.Count == 0)
                return "Nebyla dána trasa";
            return ((TranslateLocation)trasa[Poloha]).ToString();
        }
        public void RunInCaseOfError()
        {
            ErrorJustHappened(new CarErrorEventArgs { Severity = rnd.Next(0, 200), FromCar = Id });
        }
        public void RunInCaseOfSpeedChange()
        {
            Poloha++;
            CarSpeedChanged(new CarSpeedArgs { FromCar = Id, Speed = rnd.Next(0,100) });
        }
        // až bude hotová diagnóza, udělej akci podle doporučení
        public void SubscribeToService(Center center)
        {
            center.ServiceActions += ServicingOnAdvice;
        }
        public void SubsrcibeToMeteo(Meteo meteo)
        {
            meteo.SpeedActions += Servicing;
        }
        public void ServicingOnAdvice(CarRepareEventArgs e)
        {
            if (e.ForCar == Id)
                Debug.WriteLine($"Car: Přijatá oprava pro auto id={e.ForCar} je {e.ServiceAction}");
        }
        public void Servicing(CarGetSpeedEventArgs e)
        {
            if (e.ForCar == Id)
                Debug.WriteLine($"Car: Doporučená ruchlost pro auto {e.ForCar} je {e.ServiceAction}");
        }
    }
}
