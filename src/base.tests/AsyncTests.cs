namespace Masha.Foundation.Tests
{
    using System;
    using Xunit;
    using FakeItEasy;
    using Masha.Foundation;
    using static Masha.Foundation.Core;
    using static Masha.Foundation.Tests.General;

    public class AsyncTests
    {
        [Fact]
        public async void AsyncTest1()
        {
            var svc = new AsyncService();
            var result = svc
                            .GreetAsync("Sheik")
                            .MapAsync(svc.At);
            
        }
    }
}
