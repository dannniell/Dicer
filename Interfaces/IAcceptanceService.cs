using Dicer.Models;

namespace Dicer.Interfaces
{
    public interface IAcceptanceService
    {
        public Task<List<AcceptanceUser>> GetRegistrant(int campaignId);
        public Task<bool> AcceptParticipant(int campaignId, AcceptParticipant model);
    }
}
