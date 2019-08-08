

namespace Masha.Foundation.Domain
{
    using System.Threading.Tasks;

    public interface ICommandHandler<in TCommand,TResult> where TCommand : IDomainCommand
    {
        Task<Result<TResult>> HandleAsync(TCommand command);
    }
}
