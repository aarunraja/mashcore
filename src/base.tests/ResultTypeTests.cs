namespace Masha.Foundation.Tests
{
    using System;
    using Xunit;   
    using Masha.Foundation;
    using static Masha.Foundation.Core;
    using static Masha.Foundation.Tests.General;

    public class ResultTypeTests
    {
        #region Basic
        [Fact]
        public void Return_Result_Value__When_Calllee_Return_Value()
        {
            var expected = Result<Employee>(new Employee("Bob"));
            var actual = new MockEmployeeRepository().GetById(1050);            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Return_Result_Error__When_Callee_Return_Error()
        {
            var expected = Result<Employee>(Error.Of(1010));
            var actual = new MockEmployeeRepository().GetById(9999);            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Execute_FnPass__When_Pass_Match()
        {
            var expected = "OK(Bob)";
            var repo = new MockEmployeeRepository();
            var actual =
                repo
                .GetById(1010)
                .Match(pass: (v) => $"OK({v.Name})",
                       fail: (f) => "No()");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Execute_FnFail__When_Fail_Match()
        {
            var expected = "No()";
            var repo = new MockEmployeeRepository();
            var actual =
                repo
                .GetById(9999)
                .Match(pass: (v) => $"OK({v.Name})",
                       fail: (f) => "No()");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Should_Match_Error_Code__When_Error_As_Result()
        {
            var expected = Error.Of(1010);
            var repo = new MockEmployeeRepository();
            var actual = repo.GetById(7070);
            Assert.Equal(expected, actual);
        }
        #endregion
    }
}