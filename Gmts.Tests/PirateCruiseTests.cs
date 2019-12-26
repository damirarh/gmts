using FluentAssertions;
using Gmts.Models;
using Gmts.Processors;
using NUnit.Framework;
using System.IO;

namespace Gmts.Tests
{
    public class PirateCruiseTests
    {
        internal static readonly string gpxFile = Path.Combine(TestContext.CurrentContext.TestDirectory, "PirateCruise.gpx");
        internal static readonly string longDescription = @"<div style=""background-image:url(https://img.geocaching.com/cache/large/3a569cff-4fa2-4590-b51b-31d1481c12e3.jpg);background-repeat:no-repeat;background-position:center;padding-top:140px;padding-left:100px;padding-right:100px;height:300px;margin-bottom:10px;font-size:12pt;font-style:italic;color:black;""><img src=""https://ringzer0team.com/images/fs/55.png"" /><br />
Tropske endemske bolesti često nazivaju prokletstvo bijelog čovijeka, jer nije izgledalo da utječe na domaće stanovništvo. Suvremeni povjesničari vjeruju da je to bio tifus, uzročen kontaminiranom pitkom vodom. No, zapravo riječ „žuta groznica“ vjerojatno pokriva cijeli niz tropskih bolesti.<br />
<br /></div>
<div style=""background-image:url(https://img.geocaching.com/cache/large/3a569cff-4fa2-4590-b51b-31d1481c12e3.jpg);background-repeat:no-repeat;background-position:center;padding-top:140px;padding-left:100px;padding-right:100px;height:300px;margin-bottom:10px;font-size:12pt;font-style:italic;color:black;""><img src=""https://ringzer0team.com/images/fs/81.png"" /><br />
Diese in den Tropen endemische Krankheit wurde oft als Fluch des weißen Mannes bezeichnet, da sie die Eingeborenen nicht zu treffen schien. Moderne Historiker glauben, es sei Typhus, verursacht durch verunreinigtes Trinkwasser. Tatsächlich aber umfasste der Begriff „Gelbfieber“ wahrscheinlich eine ganze Reihe von Tropenkrankheiten.<br />
<br /></div>
<div style=""background-image:url(https://img.geocaching.com/cache/large/3a569cff-4fa2-4590-b51b-31d1481c12e3.jpg);background-repeat:no-repeat;background-position:center;padding-top:140px;padding-left:100px;padding-right:100px;height:300px;margin-bottom:10px;font-size:12pt;font-style:italic;color:black;""><img src=""https://ringzer0team.com/images/fs/230.png"" /><br />
A disease endemic to the tropics often called the curse of the white man as it did not seem to affect the natives. Modern historians believe it to have been typhus, caused by contaminated drinking water. But in actual fact the term “yellow fever” probably covered a whole range of tropical diseases.<br />
<br /></div>
<h3><span style=""padding:10px;border:4px solid #ff0000;"">Smjer/Peilung/Bearing: 905.656 m / -23.896 °</span></h3>

<p>Additional Waypoints</p>".Replace("\r\n", "\n");

        [Test]
        public void ParseDistanceAndBearing()
        {
            var processor = new PirateCruiseProcessor();

            var distanceAndBearing = processor.ParseDistanceAndBearing(longDescription);

            var expected = (905.656, -23.896);
            distanceAndBearing.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ProcessCacheData()
        {
            var cacheData = new CacheData("GC7WP8Y", new LatLng(43.550767, 16.51405), longDescription);
            var processor = new PirateCruiseProcessor();

            var processed = processor.Process(cacheData);

            var expected = new ProcessedCacheData(cacheData, new LatLng(43.558220, 16.509510));
            processed.Should().BeEquivalentTo(expected, options => options
                .ComparingByMembers<LatLng>()
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, 0.000001)).WhenTypeIs<double>()
            );
        }
    }
}
