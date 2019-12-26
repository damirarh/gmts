using FluentAssertions;
using Gmts.Csv;
using Gmts.Gpx;
using Gmts.Models;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Gmts.Tests
{
    public class CommonTests
    {

        [Test]
        public void ParseGpxFile()
        {
            var gpxDocument = XDocument.Load(PirateCruiseTests.gpxFile);
            var parser = new GpxFileParser();

            var parsed = parser.Parse(gpxDocument).Single();

            var expected = new CacheData("GC7WP8Y", new LatLng(43.550767, 16.51405), PirateCruiseTests.pirateCruiseLongDescription);
            parsed.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void WriteCsvFile()
        {
            var cacheData = new[]
            {
                new ProcessedCacheData(
                    new CacheData("GC7WP8Y", new LatLng(43.550767, 16.51405), PirateCruiseTests.pirateCruiseLongDescription),
                    new LatLng(43.558220, 16.509510)
                )
            };

            var csvWriter = new CsvFileWriter();

            using (var writer = new StringWriter())
            {
                csvWriter.Write(cacheData, writer);
                var csvString = writer.ToString();

                var expected = "GC7WP8Y,43.55822,16.50951" + Environment.NewLine;

                csvString.Should().Be(expected);
            }
        }
    }
}