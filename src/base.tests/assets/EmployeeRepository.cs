namespace Masha.Foundation.Tests
{
    using System;
    using Masha.Foundation;
    using static Masha.Foundation.Core;
    using System.Threading.Tasks;

    public class EmployeeRepository
    {
        public async Task<Result<Employee>> Save(UpdateEmployee cmd)
        {
            var employee = new Employee(cmd.Name)
            {
                City = cmd.City,
                Age = cmd.Age
            };
            return await Task.FromResult(employee);
        }
    }
}
