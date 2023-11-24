using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_FirstIteration
{
    public class Organizer
    {
        private string organizerName;

        public Organizer(string name)
        {
            organizerName = name;
        }

        public string GetOrganizerName()
        {
            return organizerName;
        }
    }

}
