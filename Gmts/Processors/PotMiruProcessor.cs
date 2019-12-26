using Gmts.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gmts.Processors
{
    public class PotMiruProcessor : IProcessor
    {
        private readonly Regex keyLatLngRegex = new Regex(
            @"<strong>Ključ/Key: <span style=""color:#ff0000;"">(?<key>[A-Z0-9]{2})</span>.*<strong>\[N\]<span style=""color:#0000ff;"">(?<lat>[A-Z0-9 \.]{9})</span>( )*\[E\]<span style=""color:#0000ff;"">(?<lng>[A-Z0-9 \.]{10})</span></strong>", RegexOptions.Compiled | RegexOptions.Singleline);

        private readonly string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public ProcessedCacheData Process(CacheData cacheData)
        {
            var (key, lat, lng) = ParseKeyLatLng(cacheData.LongDescription);
            var offset = CalculateOffset(key);
            var decodedLat = ParseCoordinates(DecodeString(lat, offset));
            var decodedLng = ParseCoordinates(DecodeString(lng, offset));
            return new ProcessedCacheData(cacheData, new LatLng(decodedLat, decodedLng));
        }

        internal double ParseCoordinates(string coordinates)
        {
            var degreesAndMinutes = coordinates.Split(' ');
            var degrees = Double.Parse(degreesAndMinutes[0], CultureInfo.InvariantCulture);
            var minutes = Double.Parse(degreesAndMinutes[1], CultureInfo.InvariantCulture);
            return degrees + minutes / 60;
        }

        internal string DecodeString(string input, int offset)
        {
            return new string(input
                .ToCharArray()
                .Select(c => DecodeChar(c, offset))
                .ToArray());
        }

        internal char DecodeChar(char input, int offset)
        {
            if (!characters.Contains(input))
            {
                return input;
            }
            else
            {
                var inputIndex = characters.IndexOf(input);
                var outputIndex = (inputIndex + offset) % characters.Length;
                return characters[outputIndex];
            }
        }

        internal int CalculateOffset(string key)
        {
            var inner = characters.IndexOf(key[0]);
            var outer = characters.IndexOf(key[1]);
            var delta = outer - inner;
            return delta >= 0 ? delta : delta + characters.Length;
        }

        internal (string key, string lat, string lng) ParseKeyLatLng(string longDescription)
        {
            var match = keyLatLngRegex.Match(longDescription);
            var key = match.Groups["key"].Value;
            var lat = match.Groups["lat"].Value;
            var lng = match.Groups["lng"].Value;
            return (key, lat, lng);
        }
    }
}
