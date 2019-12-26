using Gmts.Csv;
using Gmts.Gpx;
using Gmts.Processors;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

[assembly: InternalsVisibleToAttribute("Gmts.Tests")]

namespace Gmts
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLineApplication.Execute<Program>(args);
        }

        [Required]
        [Option(Description = "GPX file with cache details")]
        public string Input { get; }

        [Required]
        [Option(Description = "CSV file with calculated coordinates")]
        public string Output { get; }

        [Required]
        [Option(Description = "Trail to calculate final coordinates for (supported: PirateCruise)")]
        public TrailProcessor Trail { get; set; }

        private void OnExecute()
        {
            var gpxDocument = XDocument.Load(Input);

            var gpxParser = new GpxFileParser();
            var parsedCaches = gpxParser.Parse(gpxDocument);

            var processor = GetProcessor();
            var processedCaches = parsedCaches.Select(cache => processor.Process(cache));

            var csvWriter = new CsvFileWriter();
            using(var writer = new StreamWriter(Output))
            {
                csvWriter.Write(processedCaches, writer);
            }
        }

        private IProcessor GetProcessor()
        {
            var enumType = typeof(TrailProcessor);
            var enumValueMemberInfo = enumType.GetMember(Trail.ToString())
                .FirstOrDefault(member => member.DeclaringType == enumType);

            var processorAttribute = (ProcessorAttribute)Attribute.GetCustomAttribute(
                enumValueMemberInfo, 
                typeof(ProcessorAttribute)
            );

            return (IProcessor)Activator.CreateInstance(processorAttribute.ProcessorType);
        }
    }
}
