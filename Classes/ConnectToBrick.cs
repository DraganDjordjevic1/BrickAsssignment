using Lego.Ev3.Core;
using Lego.Ev3.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    
   public class ConnectToBrick
    {
        public Brick brick { get; set; }
        async void Connect()
        {
            brick = new Brick(new UsbCommunication());
            await brick.ConnectAsync();
        }
    }
}
