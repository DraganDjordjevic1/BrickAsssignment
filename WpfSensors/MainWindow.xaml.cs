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
using Classes;
using System.Threading;

namespace Wpf_BrickAssignment
{
    public partial class MainWindow : Window
    {
        public static ConnectToBrick brick;
        public int[] homebase;

        public MainWindow()
        {
            InitializeComponent();
            brick = new ConnectToBrick(); // connecting to brick method.
            brick.Connect();

            this.DataContext = brick;
        }

        private void SelectionButton_Click(object sender, RoutedEventArgs e)
        {
            switch (HomeBaseComboBox.Text)

            case "Blue / Red":
                {
                    homebase = new int[] { 2, 4 };
                    brick.brick.BrickChanged += BrickChanged;
                    return homebase;
                }
                break;

            case "Blue / Yellow":
                {
                    homebase = new int[] { 2, 5 };
                    brick.brick.BrickChanged += BrickChanged;
                }
                break;

            case "Black / Red":
                {
                    homebase = new int[] { 1, 4 };
                    brick.brick.BrickChanged += BrickChanged;
                }
                break;

            case "Black / Yellow":
                {
                    homebase = new int[] { 1, 5 };
                    brick.brick.BrickChanged += BrickChanged;
                }
            default:
                {
                    throw new NotImplementedException();
                }
            }
           

        }
        public void BrickChanged(object sender, BrickChangedEventArgs e)
        {
            DetectColor detectcolor = new DetectColor();       //Console.WriteLine(dc.ColorDetection(e, brick.brick));
            DHomeBase dhomebase = new DHomeBase();
            DriveMotors drivemotors = new DriveMotors();
            Turn turn = new Turn();

            DistanceText.Text = e.Ports[InputPort.Two].SIValue.ToString();
            ColorText.Text = e.Ports[InputPort.Four].SIValue();
            float distance = e.Ports[InputPort.Two].SIValue;

            //color detection method
            if (color == 1)
            {
                turn.turn90left;
            }
            else
            {
                turn.turn90left;
            }

            detectcolor.ColorDetection(e, brick.brick);

            if (distance > 10)
            {
                drivemotors.Drive(brick.brick);
                dhomebase.HomeBase(homebase, e, brick.brick);
            }
            else if (distance <= 3)
            {
                drivemotors.Stop(brick.brick);
                Thread.Sleep(1000);
                turn.Turn90Left(brick.brick);
            }
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
