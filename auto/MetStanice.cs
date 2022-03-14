using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace auto
{
    public class MetStanice
    {
        static public double Srazky { get; private set; }
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
}
