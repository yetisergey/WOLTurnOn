namespace WebApplication.Hub
{
    using System.Collections.Generic;
    using Microsoft.AspNet.SignalR;
    using Models;
    using System.Threading;
    using Controllers.Utils;
    using System.Linq;
    using System;
    using Models.Dto;
    [Authorize]
    public class MyHub : Hub
    {
        static List<string> Users = new List<string>();
        public void Connect()
        {
            var id = Context.ConnectionId;
            Clients.Caller.onConnected(id);
            Users.Add(id);
        }
        public void GetList(string ClientId)
        {
            //BaseContext db = new BaseContext(User.Identity);

            //while (true)
            //{
            //    if (Users.Contains(ClientId))
            //    {
            //        Clients.Client(ClientId).sendList(PingComputers.Run(
            //                db.GetComputers()
            //                    .Select(c => new ComputerExtended()
            //                    {
            //                        Id = (Guid)c.Element("Id"),
            //                        IpAddress = (string)c.Element("IpAddress"),
            //                        MacAddress = (string)c.Element("MacAddress"),
            //                        Name = (string)c.Element("Name")
            //                    })
            //                    .ToList()));
            //    }
            //    else
            //    {
            //        break;
            //    }
            //    Thread.Sleep(8000);
            //}
        }
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var id = Context.ConnectionId;
            Users.Remove(id);
            return base.OnDisconnected(stopCalled);
        }
    }
}