
namespace Masha.Foundation.Domain
{
    using System;


    public abstract class DomainEvent : IDomainEvent
    {
        public DomainEvent()
        {
            TimeStamp = DateTime.Now;
        }

        public DateTime TimeStamp { get; set; }
        public  string UserId { get; set; }
    }
}
