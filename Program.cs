using System;
using System.Xml;
using System.Device.Location;

using Codice.CmdRunner;

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
                Notify("Error", "Failed to retrieve the location");
                return 1;
            }

            var known = KnownLocations.Find(location, 2000);

            if (known != null)
            {
                Notify(
                    "Checkin from " + known.LocationName,
                    string.Format(
                        "You are checking in from {0}" + Environment.NewLine +
                        "Lat: {1}. Lon: {2}" + Environment.NewLine +
                        "Address: {3}",
                        known.LocationName,
                        known.Latitude, known.Longitude,
                        known.FullAddress));

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

            Notify(
                "Checkin from new location: " + newLocation.LocationName,
                string.Format("You are checking in from {0}", newLocation.LocationName));

            return 0;
        }

        static void Notify(string title, string msg)
        {
            Notification.Show(title, msg);
        }

        static void NotifyCoordinates(KnownLocations.GeolocatedCheckin gci)
        {

        }

        static string GetChangeset()
        {
            // Get a path from the stdin

            string path = Console.ReadLine();

            string changeset = CmdRunner.ExecuteCommandWithStringResult(
                string.Format("cm status {0}", path),
                Environment.CurrentDirectory,
                true);

            // will be something as cs:6982102@rep:codice@repserver:diana.codicefactory.com:9095

            return changeset;
        }

        static void CreateAttributeIfNeeded(string repo)
        {
            string result = CmdRunner.ExecuteCommandWithStringResult(
                string.Format(
                    "cm find attributetype where name='geoloc' on repository '{0}' --nototal",
                    repo) + " --format={name}",
                Environment.CurrentDirectory,
                true);

            if (result.IndexOf("geoloc") >= 0)
                return;

            CmdRunner.ExecuteCommandWithStringResult(
                string.Format("cm mkatt att:geoloc@{0}", repo),
                Environment.CurrentDirectory,
                true);
        }

        static void SetAttribute(string cset, string repo, string value)
        {
            // cm statt att:geotest@quake@localhost:6060 cs:711@quake@localhost:6060 "home - {lat} {lon}"
        }
    }
}
