using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigDataTechnoloy.Business.Hubs
{
    public interface IChatHubDispatcher
    {
        Task SendAllClients(string connectionId, string data);
    }
}
