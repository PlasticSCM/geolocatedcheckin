using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace geolocation
{
    static class Notification
    {
        static internal void Show(string title, string text)
        {
            var form = new NotificationForm(title, text);

            Application.Run(form);
        }
    }
}
