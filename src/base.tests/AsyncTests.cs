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
        public async void Return_TRt__When_Asyncs_Invoked_Serially()
        {
            var svc = new AsyncService();
            var r = await Async("Sheik")
                    .Map(svc.Greet)
                    .Map(svc.At)
                    .Map(svc.GivePriority);
            Assert.True(r.HasValue);
        }
    }
}
