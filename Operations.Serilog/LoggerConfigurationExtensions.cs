using System.Collections.Generic;
using Newtonsoft.Json;
using Operations.Trackers;
using Serilog;

namespace Operations.Serilog
{
    public static class LoggerConfigurationExtensions
    {
        public static LoggerConfiguration DestructureToJson<T>(this LoggerConfiguration configuration)
        {
            return configuration.Destructure.ByTransforming<T>(x => JsonConvert.SerializeObject(x));
        }

        public static LoggerConfiguration ConfigureDefaultOperationsDestructuring(this LoggerConfiguration configuration)
        {
            return configuration.DestructureStatus().DestructureStructuredData();
        }

        public static LoggerConfiguration DestructureStatus(this LoggerConfiguration configuration)
        {
            return configuration.Destructure.ByTransforming<Status>(x => x.Errors.Count == 0
                    ? (object)x.StatusText
                    : new { text = x.StatusText, errors = x.Errors })
                .DestructureToJson<List<StatusError>>();
        }

        public static LoggerConfiguration DestructureStructuredData(this LoggerConfiguration configuration)
        {
            return configuration.Destructure.ByTransforming<IStructuredData>(x => JsonConvert.SerializeObject(x.ToDictionary()));
        }
    }
}