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
            return Convert.ToInt32(color);


        }
    }
}
