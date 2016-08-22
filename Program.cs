using System;
using System.Xml;
using System.Device.Location;

namespace geolocation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Displaying location updates. Press any key to exit...");

            GeoCoordinate location = CalculateLocation.Get();

            if (location == null)
            {
                // failed to retrieve the location
                return;
            }

            var known = KnownLocations.Find(location, 2000);

            if (known != null)
            {
                Console.WriteLine("You are checking in from {0}", known.LocationName);

                return;
            }

            // unkwnown location, add to the list
            string address = GoogleMaps.GetAddressFrom(location);

            Console.WriteLine("This location is not close ({0} m) to any previous location you used before.", 2000);
            Console.WriteLine("Lat: {0}, Lon: {1}. {2}",
                location.Latitude, location.Longitude, address);

            Console.Write("Enter a new name for the location: ");

            string newName = Console.ReadLine();

            var newLocation = new KnownLocations.GeolocatedCheckin()
            {
                LocationName = newName,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                FullAddress = address
            };

            KnownLocations.AddNew(newLocation);

            Console.WriteLine("{0} correctly added", newLocation.LocationName);
        }
    }
}
