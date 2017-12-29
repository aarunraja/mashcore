
namespace Masha.Foundation.Tests
{    
    using System;
    using Xunit;   
    using Masha.Foundation;
    using static Masha.Foundation.Core;
    using static Masha.Foundation.Tests.General;
    
    public class OptionTypeTests
    {

        #region Basic
        [Fact]
        public void Return_Some__If_Object_Is_NOTNull()
        {
            //Given
            var empName = "Tamil";
            var expected = new Employee(empName);
            //When
            var notNullEmployee = Some(new Employee(empName));
            //Then            
            Assert.Equal<Employee>(expected, notNullEmployee.Value);
        }

        [Fact]
        public void Return_None__If_Object_Is_NULL()
        {
            Employee nullEmployee = null;
            var optEmployee = Some(nullEmployee);
            Assert.True(optEmployee.Equals(None));
        }

        [Fact]
        public void Return_Some__OnSuccessful_IntConversion()
        {
            Assert.Equal(5, ToInt("5").Value);
        }

        [Fact]
        public void Return_None__OnFailed_IntConversion()
        {
            var actual = ToInt("Abc");
            Assert.Equal(None, actual);
        }

        [Fact]
        public void Execute_FnSome__When_Some_Match()
        {
            var expected = 5;
            var intValue = ToInt("5");
            var actual = intValue.Match(Some: v => v, None: () => -1);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Execute_FnNone__When_None_Match()
        {
            var expected = -1;
            var intValue = ToInt("Abc");
            var actual = intValue.Match(Some: v => v, None: () => -1);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Should_Not_Be_Equal__When_Equals_With_One_None()
        {
            var employee1 = Some(new Employee("Bob"));
            var employee2 = Some<Employee>(null);
            var actual = employee1.Equals(employee2);
            Assert.False(actual);
        }

        [Fact]
        public void Should_Be_Equal__When_Both_None_Equal()
        {
            Option<Employee> employee1 = None;
            Option<Employee> employee2 = Some<Employee>(null);

            Assert.Equal(employee1, employee2);
        }

        public void Should_Be_Equal__When_Both_Equal()
        {
            var expected = true;
            var empName = "Sheik";
            Option<Employee> employee1 = Some(new Employee(empName));
            Option<Employee> employee2 = Some(new Employee(empName));
            var actual = employee1.Equals(employee2);

            Assert.Equal(expected, actual);
        }
        #endregion

        #region Map
        public void Return_Mapped_Value__When_Some_Apply_Map()
        {
            var baseValue = 2;
            var expected = Some(Math.Sqrt(baseValue));
            var actual = Some(baseValue).Map(n => Math.Sqrt(n));
            Assert.Equal(expected, actual);
        }    
        #endregion
    }
}
