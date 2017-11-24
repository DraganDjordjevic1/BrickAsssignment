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
            Console.ReadLine();

        }

        public static void EventCalled(object sender, BrickChangedEventArgs e)
        {
            int[] colorArray = new int[10];           
            for (int i = 0; i < 10; i++)
            {
                float color = e.Ports[InputPort.Four].SIValue;
                colorArray[0] = Convert.ToInt32(color);             
            }

            Console.WriteLine(Convert.ToString(colorArray));


        }

    }
}
