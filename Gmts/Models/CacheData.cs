namespace Gmts.Models
{
    public class CacheData
    {
        public string Code { get; }
        public LatLng OriginalCoords { get; }
        public string LongDescription { get; set; }

        public CacheData(string code, LatLng originalCoords, string longDescription)
        {
            Code = code;
            OriginalCoords = originalCoords;
            LongDescription = longDescription;
        }

        public CacheData(CacheData cacheData)
            : this(cacheData.Code, cacheData.OriginalCoords, cacheData.LongDescription)
        { }
    }
}
