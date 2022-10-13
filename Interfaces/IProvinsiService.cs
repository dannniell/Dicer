using Dicer.Models;

namespace Dicer.Interfaces
{
    public interface IProvinsiService
    {
        Task<IEnumerable<Provinsi>> GetAllProvinsi();
    }
}
