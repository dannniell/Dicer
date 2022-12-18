using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Dicer.Hubs
{
    [AllowAnonymous]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinChatRoom(string chatRoomName)
        {
            await this.Groups.AddToGroupAsync(Context.ConnectionId, chatRoomName).ConfigureAwait(false);

            //retrive history chat. Manual DB
            //Dictionary<string, string> messages = await this.DatabaseManager.GetChatHistory(chatRoomName).ConfigureAwait(false);

            //broadcast Message to groupchat
            await this.Clients.Group(chatRoomName).SendAsync("ReceiveMessage", "message");
        }

        public async Task SendMessageToGroup(string groupName, string message)
        {
            //await this.DatabaseManager.SaveChatHistory(chatRoomName, message).ConfigureAwait(false);

            await this.Clients.Group(groupName).SendAsync("ReceiveMessage", message);
        }
    }
}