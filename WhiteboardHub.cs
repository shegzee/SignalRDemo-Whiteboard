using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRDemo
{
    public class WhiteboardHub : Hub
    {
        public async Task SendDraw(DrawData data)
        {
            try
            {
                await Clients.Others.SendAsync("ReceiveDraw", data);
                // Console.WriteLine("Drawn: {0}, {1}", data.X, data.Y);
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine($"Error in SendDraw: {ex.Message}");
            }
        }
    }

    public class DrawData
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
