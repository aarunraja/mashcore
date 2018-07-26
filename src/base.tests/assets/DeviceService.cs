namespace Masha.Foundation.Tests
{
    using System;
    using Masha.Foundation;
    using static Masha.Foundation.Core;
    using System.Threading.Tasks;

    public class DeviceService
    {
        public async Task<Result<DeviceRegistered>> Add(Func<Device, Task<Result<Device>>> save, CreateDevice cmd)
        {
            return await Result(cmd)
                .Map(Spec<CreateDevice>(c => c.Generation > 0 && c.SerialNumber.Length == 6),
                    () => Error.Of(1001))
                .Map(Create)
                .Map(save)
                .Map(d => new DeviceRegistered
                {
                    Id = d.Id,
                    Name = d.Name
                });
        }

        public Func<IDeviceRepository, Func<CreateDevice, Task<Result<DeviceRegistered>>>> AddDeviceFn
            => repo => new Func<CreateDevice, Task<Result<DeviceRegistered>>>(cmd => this.AddDevice(repo, cmd));

        public async Task<Result<DeviceRegistered>> AddDevice(IDeviceRepository repository, CreateDevice cmd)
        {
            return await Result(cmd)
                .Map(Spec<CreateDevice>(c => c.Generation > 0 && c.SerialNumber.Length == 6),
                    () => Error.Of(1001))
                .Map(Create)
                .Map(repository.Save)
                .Map(d => new DeviceRegistered
                {
                    Id = d.Id,
                    Name = d.Name
                });
        }

        public Reader<IDeviceRepository, Task<Result<DeviceRegistered>>> AddDeviceWithReaderMonad(CreateDevice cmd)
        {
            return new Reader<IDeviceRepository, Task<Result<DeviceRegistered>>>(async repo =>
            {
                return await this.AddDevice(repo, cmd);
            });
        }

        public Result<Device> Create(CreateDevice cmd)
        {
            var newDevice = new Device
            {
                BluetoothName = cmd.BluetoothName,
                Generation = cmd.Generation,
                SerialNumber = cmd.SerialNumber,
                Id = Guid.NewGuid().ToString(),
            };
            newDevice.Name = $"{newDevice.BluetoothName}_{newDevice.Generation}";
            return newDevice;
        }
    }
}
