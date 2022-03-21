using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
namespace auto
{
    public class CarGetSpeedEventArgs : EventArgs
    {
        public Guid ForCar { get; set; }
        public string ServiceAction { get; set; }
    }
    public delegate void GetSpeed(CarGetSpeedEventArgs e);
    public class Meteo
    {
        Random rnd = new Random();
        private double OldSpeed = 0;
        private double Wind { get; set; }
        private double Precipitation { get; set; }
        private double Temperature { get; set; }

        public event GetSpeed SpeedActions;
        public void SubscribeToGetSpeed(Car car)
        {
            car.CarSpeedChanged += GetSpeedM;
        }
        private double GetSpeedCoeficient()
        {
            Wind = rnd.NextDouble();
            Precipitation = rnd.NextDouble();
            Temperature = rnd.NextDouble();
            return (Wind + Temperature + Precipitation) / 3;
        }
        public void GetSpeedM(CarSpeedArgs e)
        {
            e.Speed *= GetSpeedCoeficient();
            Debug.WriteLine($"Meteo: Přijata změna { e.Speed}");
            if (e.Speed <= OldSpeed)
                SpeedActions(new CarGetSpeedEventArgs { ServiceAction = $"Zpomal na {e.Speed.ToString("0.00")}", ForCar = e.FromCar });
            else
                SpeedActions(new CarGetSpeedEventArgs { ServiceAction = $"Zrychli na {e.Speed.ToString("0.00")}", ForCar = e.FromCar });
            OldSpeed = e.Speed;
        }
        public override string ToString()
        {
            return $@"Meteostanice
Vítr: {Wind}
Teplota: {Temperature}
Srážky: {Precipitation}
Koeficient pro rychlost: {((Wind+Temperature+Precipitation)/3).ToString("0.00")}";
        }
    }
}
