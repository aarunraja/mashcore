namespace Masha.Foundation.Tests
{
    using System;
    using Masha.Foundation;
    using static Masha.Foundation.Core;
    using System.Threading.Tasks;

    public class EmployeeService
    {
        public Result<EmployeeCreated> Add(Func<UpdateEmployee, Result<Employee>> repo, UpdateEmployee emp)
        {
            return emp
                .Pipe(repo)
                .Map(e => new EmployeeCreated
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = e.Name
                }.AsResult());
        }
    }
}
