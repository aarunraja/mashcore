namespace Masha.Foundation.Domain
{
    using System;
    using System.Threading.Tasks;


    public class CommandDispatcher : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task RaiseEvent<T>(T @event) where T : DomainEvent
        {
            return Task.FromResult(0);
        }

        public async Task<Result<TResult>> SendCommand<T, TResult>(T command) where T : DomainCommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command),
                                                "Command can not be null.");
            }

            var handler = (ICommandHandler<T, TResult>)_serviceProvider.GetService(typeof(ICommandHandler<T, TResult>));
            return await handler.HandleAsync(command);
        }
    }

}
