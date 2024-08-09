using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Common.Exceptions
{
    public class NoPlacesLeftException : Exception
    {
        public NoPlacesLeftException(object key, int maxParticipants) 
            : base($"Event {key} has reached maximum number of participants ({maxParticipants})")
        { }
    }
}
