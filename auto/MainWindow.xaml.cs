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
        Car car1 = new Car();
        Car car2 = new Car();

        Center center = new Center();
        Meteo meteo = new Meteo();

        public MainWindow()
        {
            InitializeComponent();

            car1.SubscribeToService(center);
            car1.ErrorJustHappened += (c) =>
            {
                if (c.Severity > 100)
                {
                    btnCarError.Background = Brushes.Red;
                }
            };
            car1.CarSpeedChanged += (c) =>
            {
                if (c.Speed > 80)
                    btnCarSpeed.Background = Brushes.Blue;
                else if (c.Speed > 60)
                    btnCarSpeed.Background = Brushes.Green;
                else if (c.Speed > 40)
                    btnCarSpeed.Background = Brushes.Yellow;
                else if (c.Speed > 20)
                    btnCarSpeed.Background = Brushes.Orange;
                else
                    btnCarSpeed.Background = Brushes.Red;


            };
            car1.SubsrcibeToMeteo(meteo);
            car2.SubscribeToService(center);
            meteo.SubscribeToGetSpeed(car1);
            center.SubscribeToFixCarErrors(car1);
            center.SubscribeToFixCarErrors(car2);
            center.ServiceActions += Center_ServiceActions;
            meteo.SpeedActions += Meteo_SpeedActions;
        }

        private void Center_ServiceActions(CarRepareEventArgs e)
        {
            btnCenter.Content = e.ServiceAction;
        }
        private void Meteo_SpeedActions(CarGetSpeedEventArgs e)
        {
            btnCarSpeed.Content = e.ServiceAction;
        }
        private void btnCarUpdate_Click(object sender, RoutedEventArgs e)
        {
            car1.RunInCaseOfSpeedChange();
            BtnCarLocation.Content = car1.GetLocation();
            btnMeteo.Content = meteo.ToString();
        }


        private void btnCarError_Click(object sender, RoutedEventArgs e)
        {
            car1.RunInCaseOfError();
        }

        private void btnCarGen_Click(object sender, RoutedEventArgs e)
        {
            car1.RouteGeneration();
            btnCarRoute.Content = car1.GetRoute();
            BtnCarLocation.Content = car1.GetLocation();
        }

    }
}

