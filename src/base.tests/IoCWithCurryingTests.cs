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
        public void Simple_Func_Mapping_IoC_Repository()
        {
            Func<Device, Task<Result<Device>>> repo = new DeviceRepository().Save;
            var svc = new DeviceService();

            var cmd = new CreateDevice
            {
                BluetoothName = "HK_Living",
                Generation = 1,
                SerialNumber = "ABC123"
            };

            var result = svc.Add(repo, cmd).Result;
            Assert.True(result.HasValue);
            var emp = result.GetOrElse(new DeviceRegistered());
            Assert.True(emp.Name.Contains(cmd.BluetoothName));
        }

        [Fact]
        public void IRepository_Mapping_IoC_Repository()
        {            
            var svc = new DeviceService().AddDeviceFn(new DeviceRepository());
            

            var cmd = new CreateDevice
            {
                BluetoothName = "HK_Living",
                Generation = 1,
                SerialNumber = "ABC123"
            };

            var result = svc(cmd).Result;
            Assert.True(result.HasValue);
            var emp = result.GetOrElse(new DeviceRegistered());
            Assert.True(emp.Name.Contains(cmd.BluetoothName));
        }
    }
}