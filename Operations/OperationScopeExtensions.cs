using System;
using JetBrains.Annotations;

namespace Operations
{
    public static class OperationScopeExtensions
    {
        public static void Update([NotNull] this IOperationScope scope, [NotNull] IOperationProgress progress)
        {
            if (scope == null) throw new ArgumentNullException(nameof(scope));
            if (progress == null) throw new ArgumentNullException(nameof(progress));
            scope.Runner.RootTracker.UpdateOperation(scope.Operation, progress);
        }

        public static void Update([NotNull] this IOperationScope scope, [NotNull] string progress)
        {
            if (scope == null) throw new ArgumentNullException(nameof(scope));
            if (progress == null) throw new ArgumentNullException(nameof(progress));
            scope.Runner.RootTracker.UpdateOperation(scope.Operation, new StringProgress(progress));
        }

        public static void Error([NotNull] this IOperationScope scope, [NotNull] Exception error)
        {
            if (scope == null) throw new ArgumentNullException(nameof(scope));
            if (error == null) throw new ArgumentNullException(nameof(error));

            scope.Runner.RootTracker.OperationError(scope.Operation, error);
        }
    }
}