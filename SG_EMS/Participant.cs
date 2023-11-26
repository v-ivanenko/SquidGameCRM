using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_EMS
{
    public class Participant
    {
        private string name;
        private int age;

        public Participant(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public string GetName()
        {
            return name;
        }

        public int GetAge()
        {
            return age;
        }
    }

}
