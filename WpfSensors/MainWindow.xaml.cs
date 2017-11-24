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
            { 
            case "Blue / Red":
                {
                    homebase = new int[] { 2, 4 };
                    brick.brick.BrickChanged += BrickChanged;
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
                break;
            default:
                {
                    throw new NotImplementedException();
                }
            }
           

        }
        public void BrickChanged(object sender, BrickChangedEventArgs e)
        {
            DetectColor detectcolor = new DetectColor();
            DHomeBase dhomebase = new DHomeBase();
            DriveMotors drivemotors = new DriveMotors();
            Turn turn = new Turn();

            DistanceText.Text = e.Ports[InputPort.Two].SIValue.ToString();
            ColorText.Text = e.Ports[InputPort.Four].SIValue.ToString();
            float distance = e.Ports[InputPort.Two].SIValue;
            float color = e.Ports[InputPort.Four].SIValue;
 
            if (color == 1)
            {
                turn.Turn90Left(brick.brick);
            }
            else
            {
                turn.Turn90Right(brick.brick);
            }

            detectcolor.ColorDetection(e, brick.brick);

            if (distance > 10)
            {
                drivemotors.Stop(brick.brick);
                Thread.Sleep(1000);
                dhomebase.HomeBase(homebase, e, brick.brick);
            }
            if (distance <= 6)
            {
                drivemotors.Stop(brick.brick);
                Thread.Sleep(1000);
                dhomebase.HomeBase(homebase, e, brick.brick);
            }
        }
    }
}