namespace WebApplication.Controllers.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.NetworkInformation;
    using Models.Dto;

    public class PingComputers
    {
        public static List<ComputerExtended> Run(List<ComputerExtended> list)
        {
            foreach (var u in list)
            {
                IPAddress ip;
                if (IPAddress.TryParse(u.IpAddress, out ip))
                {
                    try
                    {
                        Ping pingSender = new Ping();
                        PingReply reply = pingSender.Send(ip, 10);
                        if (reply.Status == IPStatus.Success)
                        {
                            u.Status = true;
                        }
                        else
                        {
                            u.Status = false;
                        }
                    }
                    catch (Exception)
                    {
                        u.Status = false;
                    }
                }
                else
                {
                    u.Status = false;
                }
            }
            return list;
        }
    }
}