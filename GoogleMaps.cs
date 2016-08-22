using System.Xml;
using System.Device.Location;

namespace geolocation
{
    static class GoogleMaps
    {
        static internal string GetAddressFrom(GeoCoordinate location)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(string.Format(
                "http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false",
                location.Latitude, location.Longitude));

            XmlNodeList xNodelst = xDoc.GetElementsByTagName("result");
            XmlNode xNode = xNodelst.Item(0);

            return xNode.SelectSingleNode("formatted_address").InnerText;
        }
    }
}
