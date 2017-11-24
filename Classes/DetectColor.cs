using Lego.Ev3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class DetectColor
    {
        public int[] colorArray;

        public void ColorAverage(BrickChangedEventArgs e, Brick brick)
        {
            colorArray = new int[10];

            for (int i = 0; i < 10; i ++)
            {
                float color = e.Ports[InputPort.Four].SIValue;
                colorArray[i++] = Convert.ToInt32(color);
            }

            Console.WriteLine(colorArray);
        }

        public int ColorDetection(BrickChangedEventArgs e, Brick brick)
        {
            float color = e.Ports[InputPort.Four].SIValue;
            return Convert.ToInt32(color);
        }
    }
}
