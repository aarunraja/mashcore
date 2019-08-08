
namespace Masha.Foundation.Domain
{
    using System;


    public abstract class  DomainCommand : IDomainCommand
    {
        public DomainCommand()
        {
            TimeStamp = DateTime.Now;
        }

        public DateTime TimeStamp { get; set; }
        public  string UserId { get; set; }
    }
}
