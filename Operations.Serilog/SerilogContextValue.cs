using System.Linq;

namespace Operations.Serilog
{
    public class SerilogContextValue
    {
        public SerilogContextValue(object data, bool destructure)
        {
            Data = data;
            Destructure = destructure;
        }

        public object Data { get; }

        public bool Destructure { get; }

        public static SerilogContextValue Create(object data)
        {
            return new SerilogContextValue(data, false);
        }

        public static SerilogContextValue CreateDestructured(object data)
        {
            return new SerilogContextValue(data, true);
        }
    }
}