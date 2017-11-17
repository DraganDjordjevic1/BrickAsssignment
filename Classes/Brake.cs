using Lego.Ev3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{

    public class Brake
    {
        public Brick brick { get; set; }

        void Stop()
        {
            brick.DirectCommand.StopMotorAsync(OutputPort.All, true);
        }
    }
}
