using System;

namespace Gmts.Processors
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ProcessorAttribute : Attribute
    {
        public Type ProcessorType { get; set; }

        public ProcessorAttribute(Type processorType)
        {
            ProcessorType = processorType;
        }
    }
}
