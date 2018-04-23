namespace Masha.Foundation.Tests
{
    using System.Threading.Tasks;
    using Masha.Foundation;
    using static Masha.Foundation.Core;

    public class AsyncService
    {
        public async Task<Result<string>> GreetAsync(string name)
        {
            await Task.Delay(100);
            return $"Hello, {name}";
        }

        public async Task<Result<string>> NonGreetAsync(string name)
        {
            await Task.Delay(100);
            return Error.Of(1000);
        }

        public async Task<Result<int>> At(string greet)
        {
            await Task.Delay(100);
            return greet.GetHashCode();
        }
    }
}
