using Lego.Ev3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
   public class Turn
    {
        public Brick brick { get; set; }


        //Turn the Brick 90 degrees to the left
        async void Turn90Left()
        {
            //Right
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 40, 1000, false);
            //Left
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -40, 1000, false);

            await brick.BatchCommand.SendCommandAsync();
        }

        //Turn the Brick 90 degrees to the right
        async void Turn90Right()
        {
            //Right
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, -40, 1000, false);
            //Left
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, 40, 1000, false);

            await brick.BatchCommand.SendCommandAsync();
        }

        //Turn the Brick around 180 degrees
        async void Turn180()
        {
            //Right
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 50, 1000, false);
            //Left
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -50, 1000, false);

            await brick.BatchCommand.SendCommandAsync();
        }
    }
}
