using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SignalRServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        IHubContext<BroadcastHub> messageHubContext;
        public MessageController(IHubContext<BroadcastHub> messageHubContext)
        {
            this.messageHubContext = messageHubContext;
        }

        [HttpGet]
        public async Task<JsonResult> Get(string user, string message, string connId)
        {
            if (string.IsNullOrEmpty(connId))
            {
                await this.messageHubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
            }
            else
            {
                await this.messageHubContext.Clients.Client(connId).SendAsync("ReceieveMessage", user, message);
            }
            return new JsonResult("Message Sent");
        }
    }
}
