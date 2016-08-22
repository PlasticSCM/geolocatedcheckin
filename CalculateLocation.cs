using System;
using System.Device.Location;
using System.Threading;

namespace geolocation
{
    static internal class CalculateLocation
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
