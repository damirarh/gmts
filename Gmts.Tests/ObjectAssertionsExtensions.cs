using FluentAssertions;
using FluentAssertions.Primitives;
using Gmts.Models;

namespace Gmts.Tests
{
    public static class ObjectAssertionsExtensions
    {
        public static void BeEquivalentToProcessedCacheData(this ObjectAssertions objectAssertions, ProcessedCacheData expected)
        {
            objectAssertions.BeEquivalentTo(expected, options => options
                .ComparingByMembers<LatLng>()
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, 0.000001)).WhenTypeIs<double>()
            );
        }
    }
}
