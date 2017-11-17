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
        public int ColorDetection(BrickChangedEventArgs e, Brick brick)
        {
            float color = e.Ports[InputPort.Four].SIValue;
            int currentColor;

            //not sure if this will work, need to test this
            if (color == 1)
            {
                currentColor = 1;
            }
            else if (color == 2)
            {
                currentColor = 2;
            }
            else if (color == 4)
            {
                currentColor = 4;
            }
            else if (color == 5)
            {
                currentColor = 5;
            }

            return Convert.ToInt32(color);

        }
    }
}
