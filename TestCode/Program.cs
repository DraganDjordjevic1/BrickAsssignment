using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lego.Ev3.Core;
using Lego.Ev3.Desktop;

namespace TestCode
{
    class Program
    {
        public static ConnectToBrick brick;

        static void Main(string[] args)
        {
            brick = new ConnectToBrick();

            brick.Connect();
            
            brick.brick.BrickChanged += EventCalled;

           // Turn turns = new Turn();

           // turns.Turn90Left(b.brick);

            //DriveMotors dm = new DriveMotors();

            //dm.Drive(b.brick);
            Console.ReadLine();

        }

        public static void EventCalled(object sender, BrickChangedEventArgs e)
        {
            DetectColor dc = new DetectColor();

            

            Console.WriteLine(dc.ColorDetection(e, brick.brick));
        }
    }
}
