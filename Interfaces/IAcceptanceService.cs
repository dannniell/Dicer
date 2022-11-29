using Dicer.Models;

namespace Dicer.Interfaces
{
    public interface IAcceptanceService
    {
        public Task<List<AcceptanceUser>> GetRegistrant(int campaignId);
    }
}
