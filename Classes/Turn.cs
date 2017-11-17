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
        //Turn the Brick 90 degrees to the left
        public async void Turn90Left(Brick brick)
        {
            //Right
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 40, 1000, false);
            //Left
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -40, 1000, false);

            await brick.BatchCommand.SendCommandAsync();
        }

        //Turn the Brick 90 degrees to the right
        public async void Turn90Right(Brick brick)
        {
            //Right
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, -40, 1000, false);
            //Left
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, 40, 1000, false);

            await brick.BatchCommand.SendCommandAsync();
        }

        //Turn the Brick around 180 degrees
        public async void Turn180(Brick brick)
        {
            //Right
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 50, 1000, false);
            //Left
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -50, 1000, false);

            await brick.BatchCommand.SendCommandAsync();
        }
    }
}
