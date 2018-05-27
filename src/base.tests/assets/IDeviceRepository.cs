using System.Threading.Tasks;

namespace Masha.Foundation.Tests
{
    public interface IDeviceRepository
    {
        Task<Result<Device>> Save(Device entity);
    }
}