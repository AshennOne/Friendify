namespace API.SignalR;
/// <summary>
/// Tracks the presence of users by storing their online status and connection IDs.
/// </summary>
public class PresenceTracker
{
    /// <summary>
    /// Dictionary to store the online status and connection IDs of users.
    /// </summary>
    private static readonly Dictionary<string, List<string>> OnlineUsers = new Dictionary<string, List<string>>();
    /// <summary>
    /// Handles the connection of a user by adding their connection ID to the list of online users.
    /// </summary>
    /// <param name="username">The username of the connected user.</param>
    /// <param name="connectionId">The connection ID of the user's connection.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task UserConnected(string username, string connectionId)
    {
        lock (OnlineUsers)
            if (OnlineUsers.ContainsKey(username))
            {
                OnlineUsers[username].Add(connectionId);
            }
            else
            {
                OnlineUsers.Add(username, new List<string>());
            }
        return Task.CompletedTask;
    }
    /// <summary>
    /// Handles the disconnection of a user by removing their connection ID from the list of online users.
    /// If the user has no more connections, they are removed from the list of online users.
    /// </summary>
    /// <param name="username">The username of the disconnected user.</param>
    /// <param name="connectionId">The connection ID of the user's connection to remove.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task UserDisconnected(string username, string connectionId)
    {
        lock (OnlineUsers)
        {
            if (!OnlineUsers.ContainsKey(username))
            {
                return Task.CompletedTask;
            }
            OnlineUsers[username].Remove(connectionId);
            if (OnlineUsers[username].Count == 0)
            {
                OnlineUsers.Remove(username);
            }
        }
        return Task.CompletedTask;
    }
    /// <summary>
    /// Retrieves an array of usernames representing the users currently online.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, containing an array of online usernames.</returns>
    public Task<string[]> GetOnlineUsers()
    {
        string[] onlineUsers;
        lock (OnlineUsers)
        {
            onlineUsers = OnlineUsers.OrderBy(k => k.Key).Select(k => k.Key).ToArray();
        }
        return Task.FromResult(onlineUsers);
    }
}
