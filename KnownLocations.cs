using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Device.Location;

namespace geolocation
{
    static internal class KnownLocations
    {
        internal class GeolocatedCheckin
        {
            internal double Latitude;
            internal double Longitude;
            internal string LocationName;
            internal string FullAddress;
        }

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

                while ((line = reader.ReadLine()) != null)
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
}
