using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using UM_LOGISTIC_V1.Services;

namespace UM_LOGISTIC_V1.WebHooks
{
    [HubName("eventsHub")]
    public class EventsHub : Hub
    {
        private UserService userService = new UserService();
        public override Task OnConnected()
        {
            var userName = Context.QueryString["userName"];
            var connectionId = Context.ConnectionId;
            var successConnectToDb = userService.ConnectUser(userName, connectionId);
            this.Clients.Clients(userService.GetAdminConnectionIds()).onlineStateChanged(true, userName);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var userName = Context.QueryString["userName"];
            var successDisconnectFromDb = userService.DisconnectUser(userName);
            this.Clients.Clients(userService.GetAdminConnectionIds()).onlineStateChanged(false, userName);
            return base.OnDisconnected(stopCalled);
        }
    }
}