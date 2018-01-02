namespace Masha.Foundation.Tests
{
    using System;
    using Xunit;
    using FakeItEasy;
    using Masha.Foundation;
    using static Masha.Foundation.Core;
    using static Masha.Foundation.Tests.General;

    public class SpecificationTests
    {
        [Fact]
        public void Execute_PassFn__When_PassedBy_Match()
        {
            var command = new UpdateEmployee
            {
                EmployeeId = "A00684",
                City = "Vickramasingapuram",
                Age = 40,
            };

            var repo = A.Fake<IEmployeeRepository>();

            var expected = Error.Of(1011);
            A.CallTo(() => repo.Insert(command)).Returns(expected);

            var ageShouldBe45 = Spec<UpdateEmployee>(u => u.Age > 45);

            var actual = Result(command)
                .EMap(ageShouldBe45, () => Error.Of(1011))
                .EMap(Spec<UpdateEmployee>(e => e.City == "Vickramasingapuram"), () => Error.Of(1011))
                .EMap(repo.Insert);
            Assert.Equal(expected, actual.Match(r => r.HasError, (e) => Error.None));
        }
    }
}
