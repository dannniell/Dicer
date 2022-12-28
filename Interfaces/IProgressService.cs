using Dicer.Models;

namespace Dicer.Interfaces
{
    public interface IProgressService
    {
        public Task<List<AcceptanceUser>> GetRegistrant(int campaignId);
        public Task<bool> AcceptParticipant(int campaignId, AcceptParticipant model);

        public Task<bool> Completed(int campaignId);
    }
}
