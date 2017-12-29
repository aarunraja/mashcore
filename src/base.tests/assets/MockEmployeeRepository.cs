namespace Masha.Foundation.Tests
{
    using System;    
    using Masha.Foundation;
    using static Masha.Foundation.Core;

    public class MockEmployeeRepository
    {
        public Result<Employee> GetById(int empId)
        {
            if (empId >= 1000 && empId < 2000)
            {
                return new Employee("Bob");
            }
            else
            {
                return Error.Of(1010);
            }
        }
    }
}
