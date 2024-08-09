using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace Events.Domain
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }
        public DateTime EventDateTime { get; set; }
        public string Location { get; set; } 
        public string Category { get; set; }
        public int MaxParticipants { get; set; }
        public List<Participation> Participations { get; set; } = new List<Participation>();
        public string ImageUrl { get; set; }
    }
}
