using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lego.Ev3.Desktop;
using Lego.Ev3.Core;
using System.Threading;

namespace BasicConnection
{
    class Program
    {
        public static Brick brick { get; set; }
        public static bool drive { get; set; }
        public static bool armDown { get; set; }

        static void Main(string[] args)
        {
            ConnectToBrick();

            for (int i = 0; i < 3; i++)
            {
                RaiseArm();
                Thread.Sleep(1000);
                TurnAround();
                Thread.Sleep(1000);
                DriveMotors();
                Thread.Sleep(1000);
                DropArm();
                Thread.Sleep(1000);
                TurnAround();
                Thread.Sleep(1000);
                DriveMotors();
                Thread.Sleep(1000);
            }

            Console.ReadLine();

            brick.Disconnect();
        }

        static async void ConnectToBrick()
        {
            brick = new Brick(new UsbCommunication());
            await brick.ConnectAsync();
        }

        static async void DriveMotors()
        {
            //A = right wheel 
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 40, 1000, false);
            //B = left wheel
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.B, 40, 1000, false);
            
            await brick.BatchCommand.SendCommandAsync();
        }

        static async void DropArm()
        {
            await brick.DirectCommand.TurnMotorAtPowerForTimeAsync(OutputPort.D, -20, 1000, false);
            armDown = true;
        }

        static async void RaiseArm()
        {
            await brick.DirectCommand.TurnMotorAtPowerForTimeAsync(OutputPort.D, 20, 1000, false);
            armDown = false;
        }

        static async void TurnAround()
        {
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 55, 1000, false);
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.B, -55, 1000, false);
            brick.BatchCommand.PlayTone(50, 1000, 500);
            await brick.BatchCommand.SendCommandAsync();
        }
    }
}
