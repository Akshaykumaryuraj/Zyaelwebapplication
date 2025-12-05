using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace ZyaelWeb
{
	public static class WebSocketHandler
	{
		private static readonly ConcurrentDictionary<string, WebSocket> _sockets = new();

        public static async Task Handle(HttpContext context, WebSocket socket)
        {
            var buffer = new byte[1024 * 4];
            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine($"Received: {message}");
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed", CancellationToken.None);
                }
            }
        }

        public static async Task BroadcastAsync(string message)
		{
			var bytes = Encoding.UTF8.GetBytes(message);
			foreach (var socket in _sockets.Values)
			{
				if (socket.State == WebSocketState.Open)
					await socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
			}
		}
	}
}
