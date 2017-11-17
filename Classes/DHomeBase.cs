using Lego.Ev3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class DHomeBase
    {
        public Brick brick { get; set; }
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
    }
}
