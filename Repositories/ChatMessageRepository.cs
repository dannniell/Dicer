using Dicer.Models;
using Dicer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Dicer.Repositories
{
    public class ChatMessageRepository : IChatMessageService
    {
        private readonly ApplicationDbContext _context;

        public ChatMessageRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<List<ChatMessage>> GetChatMessage(string roomGroup)
        {
            var chatMessageData = from chat in _context.ChatMessage
                                  where chat.Group == roomGroup
                                  select chat;
            var retVals = await chatMessageData.ToListAsync();

            return retVals;
        }
    }
}
