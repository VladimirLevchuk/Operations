using System;

namespace Operations.Trackers
{
    public class StatusError
    {
        public StatusError(Exception ex)
        {
            Message = ex?.Message;
            Details = ex?.ToString();
        }
        public string Message;
        public string Details;
    }
}