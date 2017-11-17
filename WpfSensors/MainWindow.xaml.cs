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

        public MainWindow(string dhomebase, object sender, BrickChangedEventArgs e)
        {
            InitializeComponent();

            ConnectToBrick();
            this.DataContext = brick;
        }

        async void ConnectToBrick()
        {
            brick = new Brick(new UsbCommunication());
            await brick.ConnectAsync();
        }

        private void SelectionButton_Click(object sender, RoutedEventArgs e)
        {
            brick.BrickChanged += BrickChanged;
        }

        //Move the Brick Forwards and Backwards
        async void DriveMotors()
        {
            //A = right wheel 
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 50, 1000, false);
            //B = left wheel
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, 50, 1000, false);

            await brick.BatchCommand.SendCommandAsync();
        }

        //Move the Brick forward but much slower than the normal speed
        async void Slow()
        {
            //Right
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.A, 5, 1000, false);
            //Left
            brick.BatchCommand.TurnMotorAtPowerForTime(OutputPort.D, 5, 1000, false);

            await brick.BatchCommand.SendCommandAsync();
        }

        //Stop the Brick
        void Brake()
        {
            brick.DirectCommand.StopMotorAsync(OutputPort.All, true);
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
            

            if (distance > 10)
            {
                DriveMotors();
            }
            else if (distance <= 3)
            {
                CollisionDectector();
            }
        }

        // blue, red, black, yellow
        // 2, 5, 1, 4

        void HomeBase(int[] dhomebase, object sender, BrickChangedEventArgs e)
            {

            // takes two numbers as a homebase
            // reads a color and checks if that value is in the array
            int[] arena = { 2, 5, 1, 4, };

            int currentColour = ColorDetection(sender, e);

            var index = Array.IndexOf(arena, currentColour);

            
            int Left;
            

            switch (index)
                {
                case 0:
                    Left = 3;
                    break;
                default: 
                    Left = index - 1;
                    break;
               }

            

            if (dhomebase.Contains(currentColour))
                {
                if (dhomebase.Contains(arena[Left]))
                    {
                    Turn90Left();
                    DriveMotors();
                    }
                else 
                    Turn90Right();
                    DriveMotors();
                }

            else Turn180();
                 DriveMotors();
            HomeBase(dhomebase, sender, e);
            }
       
        
        int ColorDetection(object sender, BrickChangedEventArgs e)
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