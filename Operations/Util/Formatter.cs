using System;

namespace Operations.Util
{
    public class Formatter
    {
        private static readonly IFormatter DefaultFormatter = new JsonFormatter(JsonFormatter.DefaultSettings);
        public static Func<IFormatter> Getter { get; set; } = () => DefaultFormatter;
        [Obsolete("The formatter concept will be removed")] 
        public static IFormatter Current => Getter();
    }
}