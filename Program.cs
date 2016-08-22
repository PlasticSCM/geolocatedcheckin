using System;
using System.Xml;
using System.Device.Location;

namespace geolocation
{
    class Program
    {
        static int Main(string[] args)
        {
            GeoCoordinate location = CalculateLocation.Get();

            if (location == null)
            {
                // failed to retrieve the location
                Console.WriteLine("Failed to retrieve the location");
                return 1;
            }

            var known = KnownLocations.Find(location, 2000);

            if (known != null)
            {
                Console.WriteLine("You are checking in from {0}", known.LocationName);

                // Create an attribute entry associated to the cset

                return 0;
            }

            // unkwnown location, add to the list
            string address = GoogleMaps.GetAddressFrom(location);

            string newName = AskNewLocation.Ask(location, address);

            var newLocation = new KnownLocations.GeolocatedCheckin()
            {
                LocationName = newName,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                FullAddress = address
            };

            KnownLocations.AddNew(newLocation);

            return 0;
        }

        static string GetChangeset()
        {
            // input is something like
            // cs:23@br:/main@rep:default@repserver:DARKTOWER:8084; cs:19@br:/main@rep:secondrep@repserver:DARKTOWER:8084

            // check https://www.plasticscm.com/documentation/triggers/plastic-scm-version-control-triggers-guide.shtml#Checkin
            string csetEnv = Environment.GetEnvironmentVariable("PLASTIC_CHANGESET");

            string[] csets = csetEnv.Split(
                new char[]{';'},
                StringSplitOptions.RemoveEmptyEntries);

            return csets[0];
        }
    }
}
