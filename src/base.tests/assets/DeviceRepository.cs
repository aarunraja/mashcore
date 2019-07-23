namespace Masha.Foundation.Tests
{
    using System;
    using Masha.Foundation;
    using static Masha.Foundation.Core;
    using System.Threading.Tasks;

    public class DeviceRepository : IDeviceRepository
    {
        //public Task<Return<Device>> Get(Specification<Device> spec)
        //{
        //    //spec.Match()
        //}

        public async Task<Result<Device>> Save(Device entity)
        {            
            return await Task.FromResult(entity);
        }
    }
}
