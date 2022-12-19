using Dicer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Dicer.Interfaces;

namespace Dicer.Hubs
{
    [AllowAnonymous]
    public class ChatHub : Hub
    {
        private readonly IChatMessageService _chatMessageService;

        public ChatHub(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }
        /*public async Task SendMessage(string user, string message)
        {
            //to use identity
            await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
        }*/

        public async Task JoinChatRoom(string chatRoomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomName).ConfigureAwait(false);

            var RetVals = await _chatMessageService.GetChatMessage(chatRoomName);

            await Clients.Group(chatRoomName).SendAsync("InitReceiveMessage", RetVals);
        }

        public async Task SendMessageToGroup(string group, string message, string email)
        {
            //await this.DatabaseManager.SaveChatHistory(chatRoomName, message).ConfigureAwait(false);
            var date = DateTime.UtcNow;
            
            await Clients.Group(group).SendAsync("ReceiveMessage", message, date.ToString());
        }
    }
}