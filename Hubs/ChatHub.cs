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

        public async Task JoinChatRoom(string chatRoomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomName).ConfigureAwait(false);
            //TODO
            var RetVals = await _chatMessageService.GetChatMessage(chatRoomName);
            var data = new List<ChatMessageViewModel>();
            if (RetVals.Count > 0)
            {
                foreach (var item in RetVals)
                {
                    data.Add(new ChatMessageViewModel
                    {
                        MessageData = item.MessageData,
                        MessageTime = item.MessageTime.ToString(),
                        Email = item.Email
                    });
                }
            }
            await Clients.Caller.SendAsync("InitReceiveMessage", data);
        }

        public async Task SendMessageToGroup(string group, string message, string currentEmail)
        {
            var dateNow = DateTime.UtcNow;
            var data = new ChatMessage
            {
                MessageData = message,
                MessageTime = dateNow,
                Email = currentEmail,
                GroupName = group
            };

            await _chatMessageService.SaveChatMessage(data);

            await Clients.Group(group).SendAsync("ReceiveMessage", message, dateNow.ToString(), currentEmail);
        }
    }
}