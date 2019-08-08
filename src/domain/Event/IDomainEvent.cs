
namespace Masha.Foundation.Domain
{
    using System;

    public interface IDomainEvent
    {
        DateTime TimeStamp { get; set; }
        string UserId { get; set; }
    }
}
