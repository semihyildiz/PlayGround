using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigDataTechnoloy.Business.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
             
           var result=  base.OnConnectedAsync();
            return result;
        }
        public async Task TestOk(string message)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "Communication","Tested");
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
