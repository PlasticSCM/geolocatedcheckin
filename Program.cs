using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Device.Location;
using System.Threading;

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

            GeolocatedCheckin known = KnownLocations.Find(location, 2000);

            if (known != null)
            {
                Console.WriteLine("You are checking in from {0}", known.LocationName);

                return;
            }

            // unkwnown location, add to the list
            string address = SolveUsingGoogle(location);

            Console.WriteLine("This location is not close ({0} m) to any previous location you used before.", 2000);
            Console.WriteLine("Lat: {0}, Lon: {1}. {2}",
                location.Latitude, location.Longitude, address);

            Console.Write("Enter a new name for the location: ");

            string newName = Console.ReadLine();

            GeolocatedCheckin newLocation = new GeolocatedCheckin()
            {
                LocationName = newName,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                FullAddress = address
            };

            KnownLocations.AddNew(newLocation);

            Console.WriteLine("{0} correctly added", newLocation.LocationName);
        }

        class GeolocatedCheckin
        {
            internal double Latitude;
            internal double Longitude;
            internal string LocationName;
            internal string FullAddress;
        }

        static string SolveUsingGoogle(GeoCoordinate location)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(string.Format(
                "http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false",
                location.Latitude, location.Longitude));

            XmlNodeList xNodelst = xDoc.GetElementsByTagName("result");
            XmlNode xNode = xNodelst.Item(0);

            return xNode.SelectSingleNode("formatted_address").InnerText;
        }

        static class KnownLocations
        {
            static internal GeolocatedCheckin Find(GeoCoordinate location, double range)
            {
                var list = Load(@"c:\users\pablo\appdata\local\plastic4\geolocatedcheckins.conf");

                if (list == null)
                    return null;

                foreach (GeolocatedCheckin knownLocation in list)
                {
                    var coord = new GeoCoordinate(
                        knownLocation.Latitude, knownLocation.Longitude);

                    if (location.GetDistanceTo(coord) < range)
                        return knownLocation;
                }

                return null;
            }

            static internal void AddNew(GeolocatedCheckin newLocation)
            {
                string file = @"c:\users\pablo\appdata\local\plastic4\geolocatedcheckins.conf";

                using (StreamWriter writer = new StreamWriter(file, true))
                {
                    writer.WriteLine("LocationName={0};Lat={1};Lon={2};Address={3}",
                        newLocation.LocationName,
                        newLocation.Latitude,
                        newLocation.Longitude,
                        newLocation.FullAddress);
                }
            }

            static List<GeolocatedCheckin> Load(string file)
            {
                if (!File.Exists(file))
                    return new List<GeolocatedCheckin>();

                var result = new List<GeolocatedCheckin>();

                using (StreamReader reader = new StreamReader(file))
                {
                    string line;

                    while ( (line = reader.ReadLine()) != null)
                    {
                        if (line.Trim().StartsWith("//")
                            || line.Trim().StartsWith("#"))
                        {
                            // skip comments
                            continue;
                        }

                        //LocationName=home;Lat=xxxx;Lon=yyyy;Address=sssss
                        try
                        {
                            GeolocatedCheckin parsed = ParseLine(line);

                            if (parsed == null)
                                continue;

                            result.Add(parsed);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error parsing line [{0}]. Skipping. {1}",
                                line, e.Message);
                        }
                    }
                }

                return result;
            }

            static GeolocatedCheckin ParseLine(string line)
            {
                int locationPos = line.IndexOf(LocationName);
                int latPos = line.IndexOf(Lat);
                int lonPos = line.IndexOf(Lon);
                int addrPos = line.IndexOf(Address);

                if (locationPos == -1 || latPos == -1 || lonPos == -1 || addrPos == -1)
                    return null;

                return new GeolocatedCheckin()
                {
                    LocationName = GetValue(line, locationPos + LocationName.Length, latPos),
                    Latitude = Double.Parse(GetValue(line, latPos + Lat.Length, lonPos)),
                    Longitude = Double.Parse(GetValue(line, lonPos + Lon.Length, addrPos)),
                    FullAddress = GetValue(line, addrPos + Address.Length, line.Length)
                };
            }

            static string GetValue(string s, int start, int end)
            {
                string result = s.Substring(start, end - start);
                return result;
            }

            static string LocationName = "LocationName=";
            static string Lat = ";Lat=";
            static string Lon = ";Lon=";
            static string Address = ";Address=";
        }


        static class CalculateLocation
        {
            static ManualResetEvent mEvent = new ManualResetEvent(false);
            static GeoCoordinate mLocation;

            static internal GeoCoordinate Get()
            {
                GeoCoordinateWatcher locationWatcher = new GeoCoordinateWatcher();

                locationWatcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(PositionChangedEvent);
                locationWatcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(StatusChangedEvent);

                locationWatcher.Start();

                if (!mEvent.WaitOne(15000))
                    return null;

                // coordinates retrieved

                return mLocation;
            }

            static void PositionChangedEvent(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
            {
                // Output the new location coordinate to the console if present
                if (e.Position.Location == GeoCoordinate.Unknown)
                    return;

                mLocation = e.Position.Location;

                mEvent.Set();
            }

            static void StatusChangedEvent(object sender, GeoPositionStatusChangedEventArgs e)
            {
                switch (e.Status)
                {
                    case GeoPositionStatus.Initializing:
                        Console.WriteLine("Initializing");
                        break;

                    case GeoPositionStatus.Ready:
                        Console.WriteLine("Have location");
                        break;

                    case GeoPositionStatus.NoData:
                        Console.WriteLine("No data");
                        break;

                    case GeoPositionStatus.Disabled:
                        Console.WriteLine("Disabled");
                        break;
                }
            }
        }
    }
}
