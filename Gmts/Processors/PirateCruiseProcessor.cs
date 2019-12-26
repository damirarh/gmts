using Geo.Geodesy;
using Geo.Geometries;
using Gmts.Models;

namespace Gmts.Processors
{
    public class PirateCruiseProcessor
    {
        private readonly SpheroidCalculator calculator = new SpheroidCalculator(Spheroid.Wgs84);

        public ProcessedCacheData Process(CacheData cacheData)
        {
            var startPoint = new Point(cacheData.OriginalCoords.Lat, cacheData.OriginalCoords.Lng);
            var destinationPoint = calculator.CalculateOrthodromicLine(startPoint, cacheData.Bearing, cacheData.Distance).Coordinate2;
            return new ProcessedCacheData(cacheData, new LatLng(destinationPoint.Latitude, destinationPoint.Longitude));
        }
    }
}
