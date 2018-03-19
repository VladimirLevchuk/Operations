using System;

namespace Operations.Debugging
{
    public class OperationsLog
    {
        private static Action<string> _output;
        public static bool Enabled => _output != null;

        // todo: add levels
        public static void Enable(Action<string> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));
            _output = output;
        }

        public static void Disable()
        {
            _output = null;
        }

        //public static void WriteLine(string line)
        //{
        //    var output = _output;
        //    output?.Invoke(line);
        //}

        public static void WriteLine(Func<string> lazyLine)
        {
            var output = _output;
            output?.Invoke(lazyLine());
        }
    }
}