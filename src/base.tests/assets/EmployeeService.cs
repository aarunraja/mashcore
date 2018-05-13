namespace Masha.Foundation.Tests
{
    using System;
    using Masha.Foundation;
    using static Masha.Foundation.Core;
    using System.Threading.Tasks;

    public class EmployeeService
    {
        public async Task<Result<EmployeeCreated>> Add(Func<UpdateEmployee, Task<Result<Employee>>> repo, UpdateEmployee cmd)
        {
            return await cmd
                .Pipe(repo)
                .Map(e => new EmployeeCreated
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = e.Name
                });                
        }
    }
}
