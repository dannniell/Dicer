using Dicer.Models;

namespace Dicer.Interfaces
{
    public interface IChatMessageService
    {
        public Task<List<ChatMessage>> GetChatMessage(string roomGroup);
    }
}
