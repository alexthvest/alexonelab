using System;
using Replikit.Extensions.Storage.Entities;

namespace AlexOneLab.Events.Models
{
    public class Event : Entity<long>
    {
        public Event(string name, DateTimeOffset dateTime)
        {
            Name = name;
            DateTime = dateTime;
        }
        
        public string Name { get; private set; }
        
        public DateTimeOffset DateTime { get; private set; }
    }
}
