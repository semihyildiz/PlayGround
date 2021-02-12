using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BigDataTechnoloy.Business.Hubs
{
    public class ChatHubDispatcher : IChatHubDispatcher
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatHubDispatcher(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendAllClients(string connectionId, string data)
        {
            await this._hubContext.Clients.All.SendAsync("ReceiveMessage", connectionId, data);
        }
    }
}
