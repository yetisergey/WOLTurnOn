namespace WebApplication.Controllers.Utils
{
    using System.Net.Sockets;

    public class WOLClass : UdpClient
    {
        public WOLClass()
            : base()
        { }
        public void SetClientToBrodcastMode()
        {
            if (Active)
                Client.SetSocketOption(SocketOptionLevel.Socket,
                                          SocketOptionName.Broadcast, 0);
        }
    }
}