using System;
using JetBrains.Annotations;

namespace Operations
{
    public class StringProgress : IOperationProgress
    {
        private readonly string _message;

        public StringProgress([NotNull] string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            _message = message;
        }

        public override string ToString()
        {
            return _message;
        }
    }
}