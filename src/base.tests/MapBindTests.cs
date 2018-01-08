namespace Masha.Foundation.Tests
{
    using System;
    using Xunit;
    using FakeItEasy;
    using Masha.Foundation;
    using static Masha.Foundation.Core;
    using static Masha.Foundation.Tests.General;

    public class MapBindTests
    {
        [Fact]
        public void Return_Option_Of_Option__When_OptionA_Bind_OptionB()
        {
            var genderOfMozhi = new UserRepository()
                .FindById(3)
                .Bind(u => u.Gender);
            Assert.IsType<Option<Option<string>>>(genderOfMozhi);
        }

        [Fact]
        public void Return_Just_Option__When_OptionA_Map_OptionB()
        {
            var genderOfMozhi = new UserRepository()
                .FindById(3)
                .Map(u => u.Gender);
            Assert.IsType<Option<string>>(genderOfMozhi);
        }

        private Func<User, string, Result<string>> Greet => (u, g) => Result($"{g},  {u.Name}");

        [Fact]
        public void Return_Result_Of_Result__When_ResultA_Bind_ResultB()
        {
            var genderOfMozhi = new UserRepository()
                .FindByIdAsResult(3)
                .Bind(u => Greet(u, "Hi"));
            Assert.IsType<Result<Result<string>>>(genderOfMozhi);
        }

        [Fact]
        public void Return_Just_Result__When_ResultA_Map_ResultB()
        {
            var genderOfMozhi = new UserRepository()
                .FindByIdAsResult(3)
                .Map(u => Greet(u, "Hello"));
            Assert.IsType<Result<string>>(genderOfMozhi);
        }

        [Fact]
        public void Return_Result_Option__When_Callee_Binds_Option()
        {
            var genderOfMozhi = new UserRepository()
                .FindByIdAsResult(3)
                .Bind(u => u.Gender);
            Assert.IsType<Result<Option<string>>>(genderOfMozhi);
        }

        public void Return_Result_Option__When_Callee_Maps_Option()
        {
            var genderOfMozhi = new UserRepository()
                .FindByIdAsResult(3)
                .Map<User, Result<string>>(u => u.Gender);
            Assert.IsType<Result<string>>(genderOfMozhi);
        }
    }
}
