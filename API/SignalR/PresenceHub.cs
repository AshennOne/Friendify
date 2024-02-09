
using API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    /// <summary>
    /// SignalR hub for handling presence tracking of users.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PresenceHub : Hub
    {
        /// <summary>
        /// Instance of the PresenceTracker service.
        /// </summary>
        private readonly PresenceTracker _tracker;
        /// <summary>
        /// Constructs a new instance of the <see cref="PresenceHub"/> class.
        /// </summary>
        /// <param name="tracker">Instance of the PresenceTracker service.</param>
        public PresenceHub(PresenceTracker tracker)
        {
            _tracker = tracker;

        }
        /// <summary>
        /// Invoked when a connection with the hub is established.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override async Task OnConnectedAsync()
        {
            await _tracker.UserConnected(Context.User.GetUsernameFromToken(), Context.ConnectionId);
            await Clients.Others.SendAsync("UserIsOnline", Context.User.GetUsernameFromToken());
            var currentUsers = await _tracker.GetOnlineUsers();
            await Clients.All.SendAsync("GetOnlineUsers",currentUsers);

        }
        /// <summary>
        /// Invoked when a connection with the hub is terminated.
        /// </summary>
        /// <param name="exception">The exception that caused the disconnection, if any.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await _tracker.UserDisconnected(Context.User.GetUsernameFromToken(), Context.ConnectionId);
            await Clients.Others.SendAsync("UserIsOffline", Context.User.GetUsernameFromToken());
            await base.OnDisconnectedAsync(exception);
            var currentUsers = await _tracker.GetOnlineUsers();
            await Clients.All.SendAsync("GetOnlineUsers",currentUsers);
        }
    }
}