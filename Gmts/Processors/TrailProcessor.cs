namespace Gmts.Processors
{
    public enum TrailProcessor
    {
        [Processor(typeof(PirateCruiseProcessor))]
        PirateCruise,
        [Processor(typeof(PotMiruProcessor))]
        PotMiru
    }
}
