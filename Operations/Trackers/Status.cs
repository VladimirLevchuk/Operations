using System;
using System.Collections.Generic;
using Operations.Util;

namespace Operations.Trackers
{
    public class Status : IStructuredData
    {
        public enum OperationStatus
        {
            Running = 0,
            Ok = 1,
            Unknown = 10,
            Failed = -1,
        }

        public string StatusText;
        public readonly List<StatusError> Errors = new List<StatusError>();

        public static Status Create(OperationStatus status)
        {
            return new Status { StatusText = status != OperationStatus.Unknown ? status.ToString().ToLowerInvariant() : "?" };
        }

        public static Status Unknown()
        {
            return Create(OperationStatus.Unknown);
        }

        public static Status Started()
        {
            return Create(OperationStatus.Running);
        }

        public void Finish()
        {
            var status = Errors.Count > 0 ? OperationStatus.Failed : OperationStatus.Ok;
            StatusText = status.ToString();
        }

        public void Update(string progress)
        {
            StatusText = $"{OperationStatus.Running}: {progress}";
        }

        public void Error(Exception error)
        {
            Errors.Add(new StatusError(error));
        }

        public virtual Dictionary<string, object> ToDictionary()
        {
            var data = new Dictionary<string, object>
            {
                { "status", StatusText },
                { "errors", Errors }
            };

            return data;
        }
#pragma warning disable 618 // TODO: revise Formatter concept
        public override string ToString()
        {
            return Errors.Count == 0 ? StatusText ?? "?" : Formatter.Current.Format(ToDictionary());
        }
#pragma warning restore 618
    }
}