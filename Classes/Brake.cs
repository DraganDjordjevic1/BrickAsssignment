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
        public void Stop(Brick brick)
        {
            brick.DirectCommand.StopMotorAsync(OutputPort.All, true);
        }
    }
}
