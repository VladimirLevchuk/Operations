using System.Collections.Generic;

namespace Operations.Trackers
{
    public class Status
    {
        public bool Ok = false;
        public List<StatusError> Errors = new List<StatusError>();
    }
}