using Geo.Geodesy;
using Geo.Geometries;
using Gmts.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Gmts.Processors
{
    public class PirateCruiseProcessor : IProcessor
    {
        private readonly SpheroidCalculator calculator = new SpheroidCalculator(Spheroid.Wgs84);
        private readonly Regex distanceAndBearingRegex = new Regex(
            @"Smjer/Peilung/Bearing: (?<distance>[\d\.]*) m / (?<bearing>[-\d\.]*) °", RegexOptions.Compiled);

        public ProcessedCacheData Process(CacheData cacheData)
        {
            var startPoint = new Point(cacheData.OriginalCoords.Lat, cacheData.OriginalCoords.Lng);
            var (distance, bearing) = ParseDistanceAndBearing(cacheData.LongDescription);
            var destinationPoint = calculator.CalculateOrthodromicLine(startPoint, bearing, distance).Coordinate2;
            return new ProcessedCacheData(cacheData, new LatLng(destinationPoint.Latitude, destinationPoint.Longitude));
        }

        internal (double distance, double bearing) ParseDistanceAndBearing(string longDescription)
        {
            var match = distanceAndBearingRegex.Match(longDescription);
            var distance = double.Parse(match.Groups["distance"].Value, CultureInfo.InvariantCulture);
            var bearing = double.Parse(match.Groups["bearing"].Value, CultureInfo.InvariantCulture);
            return (distance, bearing);
        }
    }
}
