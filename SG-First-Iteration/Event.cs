using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_FirstIteration
{
    public class Event
    {
        private string eventName;
        private string eventDescription;

        public Event(string name, string description)
        {
            eventName = name;
            eventDescription = description;
        }

        public string GetEventName()
        {
            return eventName;
        }

        public string GetEventDescription()
        {
            return eventDescription;
        }
    }

}
