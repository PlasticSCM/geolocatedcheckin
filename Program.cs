using System;
using System.IO;
using System.Device.Location;

using Codice.CmdRunner;

namespace geolocation
{
    class Program
    {
        static int Main(string[] args)
        {
            CommandLineArguments cla = CommandLineArguments.Parse(args);

            if (cla == null)
            {
                CommandLineArguments.ShowUsage();
                return 1;
            }

            LaunchCommand.Get().SetCm(cla.CmCommand);

            GeoCoordinate location = CalculateLocation.Get();

            if (location == null)
            {
                // failed to retrieve the location
                Notify("Error", "Failed to retrieve the location");
                return 1;
            }

            if (cla.Test)
            {
                Notify("This is just a test",
                    string.Format("Your location is:"+Environment.NewLine+
                        "Latitude: {0}"+Environment.NewLine+
                        "Longitude: {1}", location.Latitude, location.Longitude));

                return 0;
            }

            var known = KnownLocations.Find(location, 2000);

            if (known != null)
            {
                Notify(
                    "Checkin from " + known.LocationName,
                    GetPrettyLocation(known));

                // Create an attribute entry associated to the cset
                PlasticAutomation.AddAttribute(cla.CmCommand, cla.Attribute, known);

                return 0;
            }

            // unknown location, add to the list
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
                GetPrettyLocation(newLocation));

            PlasticAutomation.AddAttribute(cla.CmCommand, cla.Attribute, newLocation);

            return 0;
        }

        static string GetPrettyLocation(KnownLocations.GeolocatedCheckin geoci)
        {
            return string.Format(
                "You are checking in from {0}" + Environment.NewLine +
                "Lat: {1}. Lon: {2}" + Environment.NewLine +
                "Address: {3}",
                geoci.LocationName,
                geoci.Latitude, geoci.Longitude,
                geoci.FullAddress);
        }

        class CommandLineArguments
        {
            internal string CmCommand = "cm";

            internal string Attribute = "geoloc";

            internal bool Test = false;

            static internal CommandLineArguments Parse(string[] args)
            {
                CommandLineArguments result = new CommandLineArguments();

                int i = 0;

                while (i < args.Length)
                {
                    string arg = args[i++];

                    switch (arg)
                    {
                        case "--cm":
                            if (args.Length == i) return null;
                            result.CmCommand = args[i++];
                            break;
                        case "--attribute":
                            if (args.Length == i) return null;
                            result.Attribute = args[i++];
                            break;
                        case "--test":
                            result.Test = true;
                            break;
                        case "help":
                            return null;
                    }
                }

                return result;
            }

            static internal void ShowUsage()
            {
                Console.WriteLine("geolocation [help] | [--cm command]");
                Console.WriteLine("\t--cm command:           to specify an alternative cm. We use it internally");
                Console.WriteLine("\t--attribute attrname:   to specify the attr. 'geoloc' is the default");
            }
        }

        static void Notify(string title, string msg)
        {
            Notification.Show(title, msg);
        }

        static class PlasticAutomation
        {
            internal static void AddAttribute(
                string cmCommand,
                string attrName,
                KnownLocations.GeolocatedCheckin geoci)
            {
                try
                {
                    string cset = GetChangeset(cmCommand);

                    string repo = GetRepoFromChangeset(cset);

                    CreateAttributeIfNeeded(cmCommand, repo, attrName);

                    string attrValue = string.Format("{0} - {1} {2}",
                        geoci.LocationName, geoci.Latitude, geoci.Longitude);

                    SetAttribute(cmCommand, attrName, cset, repo, attrValue);
                }
                catch (Exception e)
                {
                    Notify("Error", e.Message);

                    throw;
                }
            }

            static string GetChangeset(string cmCommand)
            {
                // Get a path from the stdin

                string path = ExtractPath(Console.ReadLine());

                string changeset = CmdRunner.ExecuteCommandWithStringResult(
                    string.Format("{0} status {1}", Path.GetFileName(cmCommand), path),
                    Environment.CurrentDirectory,
                    true);

                // will be something as cs:6982102@rep:codice@repserver:diana.codicefactory.com:9095

                changeset = changeset.Replace('\n', ' ');

                return changeset.Trim();
            }

            static string ExtractPath(string triggerInput)
            {
                int iniPos = triggerInput.IndexOf('"');
                int endPos = triggerInput.IndexOf('"', iniPos + 1);

                string result = triggerInput.Substring(iniPos + 1, endPos -1 - iniPos);

                return result;
            }

            static string GetRepoFromChangeset(string cset)
            {
                // cs:6982102@rep:codice@repserver:diana.codicefactory.com:9095
                int repoPos = cset.IndexOf('@');

                return cset.Substring(repoPos + 1);
            }

            static void CreateAttributeIfNeeded(
                string cmCommand,
                string repo,
                string attrName)
            {
                string result = CmdRunner.ExecuteCommandWithStringResult(
                    string.Format(
                        "{0} find attributetype where name='{1}' on repository '{2}' --nototal",
                        Path.GetFileName(cmCommand),
                        attrName,
                        repo) + " --format={name}",
                    Environment.CurrentDirectory,
                    true);

                if (result.IndexOf(attrName) >= 0)
                    return;

                CmdRunner.ExecuteCommandWithStringResult(
                    string.Format(
                        "{0} mkatt att:{1}@{2}",
                        attrName,
                        Path.GetFileName(cmCommand),
                        repo),
                    Environment.CurrentDirectory,
                    true);
            }

            static void SetAttribute(
                string cmCommand,
                string attrName,
                string cset,
                string repo,
                string value)
            {
                string result = CmdRunner.ExecuteCommandWithStringResult(
                    string.Format(
                        "{0} statt att:{1}@{2} {3} \"{4}\"",
                        Path.GetFileName(cmCommand),
                        attrName,
                        repo,
                        cset,
                        value),
                    Environment.CurrentDirectory,
                    true);
            }
        }
    }
}
