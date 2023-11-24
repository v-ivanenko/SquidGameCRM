using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_FirstIteration;


namespace SG_Third_Iteration
{
    public interface IParticipantContainer
    {
        void AddParticipant(Participant participant);
        void RemoveParticipant(Participant participant);
        Participant GetParticipantByName(string name);
        IEnumerable<Participant> GetAllParticipants();
    }

    public class PlayerMabager : IParticipantContainer
    {
        private List<Participant> participants = new List<Participant>();

        public void AddParticipant(Participant participant)
        {
            participants.Add(participant);
        }

        public void RemoveParticipant(Participant participant)
        {
            participants.Remove(participant);
        }

        public Participant GetParticipantByName(string name)
        {
            return participants.FirstOrDefault(p => p.GetName() == name);
        }

        public IEnumerable<Participant> GetAllParticipants()
        {
            return participants.AsReadOnly();
        }
    }

}
