namespace Masha.Foundation.Tests
{
    using System;
    using System.Threading.Tasks;
    using Masha.Foundation;
    using static Masha.Foundation.Core;

    public class AsyncService
    {
        public async Task<Result<string>> Greet(string name)
        {
            await Task.Delay(20);
            return $"Hello, {name}";
        }

        public async Task<Result<string>> KickOff(string name)
        {
            await Task.Delay(20);
            throw new Exception($"{name} kicked off");
        }

        public async Task<Result<string>> NonGreet(string name)
        {
            await Task.Delay(100);
            return Error.Of(1000);
        }

        public async Task<Result<int>> GeoPoint(string greet)
        {
            await Task.Delay(100);
            return greet.GetHashCode();
        }

        public async Task<Result<int>> GivePriority(int location)
        {
            await Task.Delay(50);
            return new Random().Next(0, System.Math.Abs(location));
        }

        public async Task<string> Welcome(string userName)
        {
            await Task.Delay(50);
            return $"Welcome {userName}";
        }

        public string JustNormalHello(string greetMessage)
        {
            return $"Your greet message is {greetMessage}";
        }
    }
}
