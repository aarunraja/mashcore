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
        public void Return_Result__When_MapTo_Result()
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
                .Map(ageShouldBe45, () => Error.Of(1011))
                .Map(Spec<UpdateEmployee>(e => e.City == "Vickramasingapuram"), () => Error.Of(1012))
                .Map(repo.Insert);
            var actualResult = actual.HasError;
            Assert.Equal(!expected.IsNone, actualResult);
        }

        [Fact]
        public void Return_Result_Obj__ForSuccessfulAndSpecs()
        {
            var command = new UpdateEmployee
            {
                EmployeeId = "A00684",
                City = "Vickramasingapuram",
                Age = 40,
            };

            var ageShouldBe30 = Spec<UpdateEmployee>(u => u.Age > 30);
            var cityNotBeMumbai = Spec<UpdateEmployee>(u => !u.City.Equals("Mumbai", StringComparison.InvariantCultureIgnoreCase));
            var empIdShouldStartsWithA = Spec<UpdateEmployee>(u => u.EmployeeId.StartsWith("A"));
            var allSpec = ageShouldBe30 & cityNotBeMumbai & empIdShouldStartsWithA;

            var actual = Result(command)
                .Map(allSpec, () => Error.Of(1011));
            Assert.True(actual.HasValue);
        }

        [Fact]
        public void Return_Result_Error__ForFailedAndOrSpecs()
        {
            var command = new UpdateEmployee
            {
                EmployeeId = "A00684",
                City = "Vickramasingapuram",
                Age = 40,
            };

            var ageShouldBe45 = Spec<UpdateEmployee>(u => u.Age > 45);
            var cityNotBeMumbai = Spec<UpdateEmployee>(u => !u.City.Equals("Mumbai", StringComparison.InvariantCultureIgnoreCase));
            var empIdShouldStartsWithE = Spec<UpdateEmployee>(u => u.EmployeeId.StartsWith("E"));
            var allSpec = ageShouldBe45 | (cityNotBeMumbai & empIdShouldStartsWithE);

            var actual = Result(command)
                .Map(allSpec, () => Error.Of(1011));
            Assert.True(actual.HasError);
        }
    }
}
