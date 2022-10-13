using Dicer.Models;

namespace Dicer.Interfaces
{
    public interface IKotaService
    {
        Task<IEnumerable<Kota>> GetAllKota(int provinsiId);
    }
}
