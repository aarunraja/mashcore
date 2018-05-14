namespace Masha.Foundation.Tests
{
    using System;
    using Masha.Foundation;
    using static Masha.Foundation.Core;
    using System.Threading.Tasks;

    public class DeviceRepository
    {
        public async Task<Result<Device>> Save(Device entity)
        {            
            return await Task.FromResult(entity);
        }
    }
}
