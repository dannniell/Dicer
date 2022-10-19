using Dicer.Models;

namespace Dicer.Interfaces
{
    public interface IApiIgService
    {
        Task<IEnumerable<ApiIGModel>> GetDataIg();
    }
}
