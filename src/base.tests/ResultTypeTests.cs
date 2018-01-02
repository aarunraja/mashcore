namespace Masha.Foundation.Tests
{
    using System;
    using Xunit;   
    using FakeItEasy;
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

        #region Map
        [Fact]
        public void Return_Mapped_Value__When_Result_Apply_Map()
        {
            var employeeId = "A00684";
            var command = new UpdateEmployee {
                EmployeeId = employeeId,
                City = "Vickramasingapuram"
            };
            var repo = A.Fake<IEmployeeRepository>();

            var expected = Result(new Employee("Udooz"));
            A.CallTo(() => repo.Insert(command)).Returns(expected);

            var actual = Result(command)
                .Map(repo.Insert);
            Assert.Equal(expected, actual);
        }  

        [Fact]
        public void Return_Mapped_Value__When_NGeneric_Result_Apply_Map()
        {
            var employeeId = "A00684";
            var command = new UpdateEmployee {
                EmployeeId = employeeId,
                City = "Vickramasingapuram"
            };
            var repo = A.Fake<IEmployeeRepository>();

            var expected = Error.As(1010);
            A.CallTo(() => repo.Update(command)).Returns(expected);

            var actual = Result(command)
                .Map(repo.Update);
            Assert.Equal(expected.HasError, actual.Match(r => r.HasError, (e) => false));
        }    
        #endregion
    }
}