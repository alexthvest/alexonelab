using Replikit.Abstractions.Messages.Models;
using Replikit.Extensions.Storage.Entities;

namespace AlexOneLab.Events.Models
{
    public class Countdown : Entity
    {
        public Countdown(Event @event, MessageIdentifier messageId)
        {
            Event = @event;
            MessageId = messageId;
        }

        public Event Event { get; private set; }

        public MessageIdentifier MessageId { get; private set; }
    }
}
