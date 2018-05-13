namespace Masha.Foundation.Tests
{
    using System;
    using Xunit;
    using FakeItEasy;
    using Masha.Foundation;
    using static Masha.Foundation.Core;
    using static Masha.Foundation.Tests.General;
    using System.Threading.Tasks;

    public class IoCWithCurryingTests
    {
        [Fact]
        public void CurryingTest1()
        {
            Func<UpdateEmployee, Task<Result<Employee>>> repo = new EmployeeRepository().Save;
            var svc = new EmployeeService();

            var cmd = new UpdateEmployee
            {
                Name = "Sheik",
                Age = 40,
                City = "Chennai"
            };

            var result = svc.Add(repo, cmd).Result;
            Assert.True(result.HasValue);
            var emp = result.GetOrElse(new EmployeeCreated());
            Assert.Equal(cmd.Name, emp.Name);
        }
    }
}