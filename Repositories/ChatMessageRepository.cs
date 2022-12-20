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
                                  where chat.GroupName == roomGroup
                                  select chat;
            var retVals = await chatMessageData.ToListAsync();

            return retVals;
        }

        public async Task SaveChatMessage(ChatMessage ChatMessage)
        {
            var messageData = new SqlParameter("@MessageData", ChatMessage.MessageData);
            var messageTime = new SqlParameter("@MessageTime", ChatMessage.MessageTime);
            var email = new SqlParameter("@Email", ChatMessage.Email);
            var groupName = new SqlParameter("@GroupName", ChatMessage.GroupName);

            await _context.Database.ExecuteSqlRawAsync(Constants.Constants.saveChat + " @MessageData, @MessageTime, @Email, @GroupName", messageData, messageTime, email, groupName);
        }
    }
}
