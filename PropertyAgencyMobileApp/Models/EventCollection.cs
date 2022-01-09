using System.Collections.Generic;

namespace PropertyAgencyMobileApp.Models
{
    public class EventCollection : List<Event>
    {
        public EventCollection()
        {
        }

        public Event[] Events => ToArray();
    }
}
