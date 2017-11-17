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

namespace Wpf_BrickAssignment
{
    public partial class MainWindow : Window
    {

        public Brick brick { get; set; }

        public MainWindow(string dhomebase, object sender, BrickChangedEventArgs e)
        {
            InitializeComponent();

            
            this.DataContext = brick;
        }

        private void SelectionButton_Click(object sender, RoutedEventArgs e)
        {
            brick.BrickChanged += BrickChanged;
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
                //DriveMotors();
            }
            else if (distance <= 3)
            {
                CollisionDectector();
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