using API.SignalRHub.Tracker;
using APIExtension.ClaimsPrinciple;
using DataLayer.DBObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ShareResource.DTO.Connection;

namespace API.SignalRHub
{
    /// <summary>
    /// Use to count number of ppl in rooms
    /// </summary>
    [Authorize]
    public class DrawHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            HttpContext httpContext = Context.GetHttpContext();
            string meetingIdString = httpContext.Request.Query["meetingId"].ToString();

            bool isDrawExisted = Drawings.ContainsKey(meetingIdString);
            if (!isDrawExisted)
            {
                Drawings.Add(meetingIdString, new List<Drawing>());
            }
            Clients.Caller.SendAsync("get-drawings", Drawings[meetingIdString]);
            await Groups.AddToGroupAsync(Context.ConnectionId, meetingIdString);
            base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            HttpContext httpContext = Context.GetHttpContext();
            string meetingIdString = httpContext.Request.Query["meetingId"].ToString();
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, meetingIdString);
                                                     //if(Clients.Group(meetingIdString).)
            await base.OnDisconnectedAsync(exception);
        }
        public async Task Draw(int prevX, int prevY, int currentX, int currentY, string color, int size)
        {
            HttpContext httpContext = Context.GetHttpContext();
            int meetingId = int.Parse(httpContext.Request.Query["meetingId"].ToString());
            Drawings[meetingId.ToString()]
                .Add(new Drawing
                {
                    PrevX = prevX,
                    PrevY = prevY,
                    CurrentX = currentX,
                    CurrentY = currentY,
                    Color = color,
                    Size = size
                }
            );

            await Clients.GroupExcept(meetingId.ToString(), Context.ConnectionId).SendAsync("draw", prevX, prevY, currentX, currentY, color, size);
        }

        public static readonly Dictionary<string, List<Drawing>> Drawings = new Dictionary<string, List<Drawing>>();
        public class Drawing
        {
            public int PrevX { get; set; }
            public int PrevY { get; set; }
            public int CurrentX { get; set; }
            public int CurrentY { get; set; }
            public string Color { get; set; }
            public int Size { get; set; }
        }
    }
}
