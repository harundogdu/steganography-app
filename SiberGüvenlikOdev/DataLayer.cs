using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SiberGüvenlikOdev
{
    public class DataLayer
    {
        public static int APP_PORT = 5000; // Server Portu
        public static string Reverse(string Input)
        {
            char[] charArray = Input.ToCharArray();
            string reversedString = String.Empty;

            for (int i = charArray.Length - 1; i > -1; i--) reversedString += charArray[i];

            return reversedString;
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Wireless Bağlantısı Bulunamadı !");
        }
    }
}
