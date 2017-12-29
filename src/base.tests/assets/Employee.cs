using System;

namespace Masha.Foundation.Tests
{
    public class Employee : IEquatable<Employee>
    {
        public string Name { get; set; }

        public Employee(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Employee)) return false;
            var other = (Employee)obj;
            return this.Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public bool Equals(Employee other)
        {
            return this.Name == other.Name;
        }
    }
}
