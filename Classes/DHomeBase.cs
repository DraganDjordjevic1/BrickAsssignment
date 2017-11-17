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
        public void HomeBase(int[] dhomebase, BrickChangedEventArgs e, Brick brick)
        {
            DetectColor dc = new DetectColor();
            Turn turns = new Turn();
            DriveMotors dm = new DriveMotors();
     
            // takes two numbers as a homebase
            // reads a color and checks if that value is in the array
            int[] arena = { 2, 5, 1, 4, };

            int currentColour = dc.ColorDetection(e, brick);

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
                    turns.Turn90Left(brick);
                    dm.Drive(brick);
                }
                else
                    turns.Turn90Right(brick);
                dm.Drive(brick);
            }

            else turns.Turn180(brick);
            dm.Drive(brick);
            //HomeBase(dhomebase, sender, e, brick);
        }
    }
}
