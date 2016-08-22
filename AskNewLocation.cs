using System.Device.Location;
using System.Windows.Forms;

namespace geolocation
{
    static class AskNewLocation
    {
        static internal string Ask(GeoCoordinate location, string addr)
        {
            var form = new AskNewLocationForm(location, addr);

            form.Icon = geolocation.App;

            Application.Run(form);

            return form.GetNewName();
        }
    }
}
