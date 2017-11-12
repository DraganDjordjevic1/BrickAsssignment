using Lego.Ev3.Core;
using Lego.Ev3.Desktop;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfSensors
{
    public partial class MainWindow : Window

    {
        public Brick brick { get; set; }
        private SerialPort bluetoothConnection = new SerialPort();

        public MainWindow()
        {
            InitializeComponent();

            ConnectToBrick();
            brick.BrickChanged +=  DetectColor;
            DriveMotors();
            brick.BrickChanged += BrickChanged;

            this.DataContext = brick;
        }

        async void ConnectToBrick()
        {
            brick = new Brick(new UsbCommunication());
            await brick.ConnectAsync();
        }

        async void DriveMotors()
        {
            //Move the Brick Forwards and Backwards
            //A = right wheel 
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 50, 1000, false);
            //B = left wheel
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, 50, 1000, false);

            await brick.BatchCommand.SendCommandAsync();
        }
        async void Slow()
        {
            //Move the Brick Forwards and Backwards
            //A = right wheel 
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 5, 1000, false);
            //B = left wheel
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, 5, 1000, false);

            await brick.BatchCommand.SendCommandAsync();
        }


        async void Turn90()
        {
            //Turn the Brick 90 degrees to the left
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 40, 1000, false);
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -40, 1000, false);

            await brick.BatchCommand.SendCommandAsync();


        }

        async void Turn180()
        {
            //Turn the Brick around 180 degrees
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 50, 1000, false);
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -50, 1000, false);

            await brick.BatchCommand.SendCommandAsync();
        }

        async void Reverse()
        {
            // Should move the brick backward

            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, -50, 1000, false);
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, -50, 1000, false);

            await brick.BatchCommand.SendCommandAsync();
        }

        async void Stop()
        {
            //Stop the Brick from moving
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 0, 1000, true);
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, 0, 1000, true);

            await brick.BatchCommand.SendCommandAsync();
        }

        void Brake()
        {
            brick.DirectCommand.StopMotorAsync(OutputPort.All, true);
        }

        void DetectMotion()
        {

        }

        //CollisionDectector, stop the brick to avoid an object
        async void CollisionDectector()
        {
            brick.BatchCommand.StopMotor(OutputPort.A, false);
            brick.BatchCommand.StopMotor(OutputPort.D, false);

            await brick.BatchCommand.SendCommandAsync();
        }

        void BrickChanged(object sender, BrickChangedEventArgs e)
        {
            ButtonText.Text = e.Ports[InputPort.One].SIValue.ToString();
            DistanceText.Text = e.Ports[InputPort.Two].SIValue.ToString();
            GyroText.Text = e.Ports[InputPort.Three].SIValue.ToString();
            ColorText.Text = e.Ports[InputPort.Four].SIValue.ToString();

                   
            float distance = e.Ports[InputPort.Two].SIValue;
            float color = e.Ports[InputPort.Four].SIValue;

            if(distance > 10)
            {
                DriveMotors();
            }
            else if(distance <= 10 && distance > 3)
            {
                Slow();
            }

            else if (distance < 15)
            {
                CollisionDectector();
            }
            else if(distance <= 3)
            {
                Stop();
            }
        }

        void DetectColor(object sender, BrickChangedEventArgs e)
        {

            ColorText.Text = e.Ports[InputPort.Four].SIValue.ToString();
            float color = e.Ports[InputPort.Four].SIValue;

            if (color == 0)
            {
                DriveMotors();
            }
            else
            {
                Stop();
            }

            if (color == 1) //if it hits a black wall
            {
                Brake();
                Reverse();
                Turn90();
                brick.BrickChanged += BrickChanged;
            }

            if (color == 2) // if it hits a blue wall
            {
                Brake();
                Reverse();
                Turn90();
                brick.BrickChanged += BrickChanged;
            }

            if (color == 4) // if it hits a yellow wall
            {
                Brake();
                Reverse();
                Turn90();
                brick.BrickChanged += BrickChanged;
            }

            if (color == 5) // if it hits a red wall
            {
                Brake();
                Reverse();
                Turn90();
                brick.BrickChanged += BrickChanged;
            }



            //https://au.mathworks.com/help/supportpkg/legomindstormsev3/ref/colorsensor.html?s_tid=gn_loc_drop

        }


    }
}
/*
 * Find out what corner we are located in
 * Detect the colors of the corner which was first located in
 * Turn slightly to the left to see the first corner
 * Turn slightly to the right to see the second corner
 * By knowning the two colors in the corner that the brick is located, it now knows where it is located
 * 
 * From its current location, move the brick to the home base
 * Detect the colors from the walls which are included in the homebase
 * Once both colors have been detected, it will park in that corner
 * 
 */