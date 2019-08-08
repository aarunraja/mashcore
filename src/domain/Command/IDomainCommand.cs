
namespace Masha.Foundation.Domain
{
    using System;

    public interface IDomainCommand
    {
        DateTime TimeStamp { get; set; }
        string UserId { get; set; }
    }
}
