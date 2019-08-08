

namespace Masha.Foundation.Domain
{
    using System.Threading.Tasks;

    public interface IDispatcher
    {
        Task<Result<TResult>> SendCommand<T,TResult>(T command) where T : DomainCommand;
        Task RaiseEvent<T>(T @event) where T : DomainEvent;
    }
}
