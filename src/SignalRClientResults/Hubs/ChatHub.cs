using Microsoft.AspNetCore.SignalR;

namespace SignalRClientResults.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            var res = await WaitForMessage(this.Context.ConnectionId);
            await Clients.All.SendAsync("ReceiveMessage", user, res);
        }

        public async Task<string> WaitForMessage(string connectionId)
        {
            var message = await Clients.Client(connectionId).InvokeAsync<string>(
                "GetMessage", CancellationToken.None);
            return message;
        }
    }
}
