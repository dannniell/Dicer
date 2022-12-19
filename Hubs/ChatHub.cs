using Dicer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Dicer.Hubs
{
    [AllowAnonymous]
    public class ChatHub : Hub
    {
        public ChatHub()
        {

        }
        /*public async Task SendMessage(string user, string message)
        {
            //to use identity
            await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
        }*/

        public async Task JoinChatRoom(string chatRoomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomName).ConfigureAwait(false);

/*            var date = DateTime.UtcNow;
            var retVals = new List<ChatMessage>();
            retVals.Add(new ChatMessage
            {
                Message = "tes1",
                date = date
            });
            retVals.Add(new ChatMessage
            {
                Message = "tes2",
                date = date
            });
            await Clients.Group(chatRoomName).SendAsync("InitReceiveMessage", retVals, date.ToString());*/
        }

        public async Task SendMessageToGroup(string group, string message, string email)
        {
            //await this.DatabaseManager.SaveChatHistory(chatRoomName, message).ConfigureAwait(false);
            var date = DateTime.UtcNow;
            
            await Clients.Group(group).SendAsync("ReceiveMessage", message, date.ToString());
        }
    }
}