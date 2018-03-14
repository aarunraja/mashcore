namespace Masha.Foundation.Tests
{
    using System;
    using Masha.Foundation;
    using static Masha.Foundation.Core;
    using Unit = System.ValueTuple;
    using System.Threading.Tasks;

    public interface IEmployeeRepository
    {
        Result<Employee> Update(UpdateEmployee paramter);
        Result<Employee> Insert(UpdateEmployee parameter);
        Task<Result<Employee>> GetById(string deviceId);
    }
}
