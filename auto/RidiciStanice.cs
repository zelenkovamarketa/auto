using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace auto
{
    public class CarRepareEventArgs : EventArgs
    {
        public Guid ForCar { get; set; }
        public string ServiceAction { get; set; }
    }

    public delegate void SendService(CarRepareEventArgs e);

    public class Center
    {
        public event SendService ServiceActions;
        public void SubscribeToFixCarErrors(Car car)
        {
            car.ErrorJustHappened += FixError;
        }

        // když přijde informace o chybě, doruč autu opravu
        public void FixError(CarErrorEventArgs err)
        {
            Debug.WriteLine($"Center: Přijata chyba { err.Severity}");
            if (err.Severity < 100)
                ServiceActions(new CarRepareEventArgs { ServiceAction = "Vyměň kolo", ForCar = err.FromCar });
            else
                ServiceActions(new CarRepareEventArgs { ServiceAction = "Odstav auto", ForCar = err.FromCar });
        }
    }
}
