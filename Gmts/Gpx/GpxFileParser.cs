using Gmts.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace Gmts.Gpx
{
    public class GpxFileParser
    {
        private readonly XNamespace topografix = "http://www.topografix.com/GPX/1/0";
        private readonly XNamespace groundspeak = "http://www.groundspeak.com/cache/1/0/1";

        public IEnumerable<CacheData> Parse(XDocument gpxDocument)
        {
            return gpxDocument.Root.Elements(topografix + "wpt").Select(ParseWptElement);
        }

        private CacheData ParseWptElement(XElement wptElement)
        {
            var lat = double.Parse(wptElement.Attribute("lat").Value, CultureInfo.InvariantCulture);
            var lng = double.Parse(wptElement.Attribute("lon").Value, CultureInfo.InvariantCulture);
            var code = wptElement.Element(topografix + "name").Value;
            var longDescription = wptElement.Element(groundspeak + "cache").Element(groundspeak + "long_description").Value;

            return new CacheData(code, new LatLng(lat, lng), longDescription);
        }
    }
}
