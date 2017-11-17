using Lego.Ev3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
   public class DriveMotors
    {
        public Brick brick { get; set; }

        async void Drive()
        {
            //A = right wheel 
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 50, 1000, false);
            //B = left wheel
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, 50, 1000, false);

            await brick.BatchCommand.SendCommandAsync();
        }
        
        //Move the Brick backwards slowly
        async void Reverse()
        {
            //Right
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, -30, 1000, true);
            //Left
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -30, 1000, true);

            await brick.BatchCommand.SendCommandAsync();
        }

    }
}
