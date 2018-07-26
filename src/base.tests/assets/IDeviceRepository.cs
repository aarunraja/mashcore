using System.Threading.Tasks;

namespace Masha.Foundation.Tests
{
    public interface IDeviceRepository
    {
        Task<Result<Device>> Save(Device entity);
        //Task<Result<Device>> Get(Specification<Device> spec);
    }
}