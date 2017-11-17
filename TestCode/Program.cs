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
        static void Main(string[] args)
        {
            ConnectToBrick b = new ConnectToBrick();

            b.Connect();

            DetectColor dc = new DetectColor();
            
            dc.ColorDetection(.e, b.brick);

           // Turn turns = new Turn();

           // turns.Turn90Left(b.brick);

            //DriveMotors dm = new DriveMotors();

            //dm.Drive(b.brick);
            Console.ReadLine();

        }
    }
}
