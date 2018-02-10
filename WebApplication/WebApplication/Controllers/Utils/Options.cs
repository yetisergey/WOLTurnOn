namespace WebApplication.Controllers.Utils
{
    using System;
    using System.Globalization;
    using System.Net;

    public static class Options
    {
        public static void Wake(string MAC_ADDRESS)
        {
            try
            {
                WOLClass client = new WOLClass();
                client.Connect(new IPAddress(0xffffffff), 0x2fff);
                client.SetClientToBrodcastMode();
                int counter = 0;
                byte[] bytes = new byte[1024];
                for (int y = 0; y < 6; y++)
                    bytes[counter++] = 0xFF;
                for (int y = 0; y < 16; y++)
                {
                    int i = 0;
                    for (int z = 0; z < 6; z++)
                    {
                        bytes[counter++] = byte.Parse(MAC_ADDRESS.Substring(i, 2), NumberStyles.HexNumber);
                        i += 2;
                    }
                }
                int reterned_value = client.Send(bytes, 1024);
            }
            catch
            {

            }
        }
    }
}