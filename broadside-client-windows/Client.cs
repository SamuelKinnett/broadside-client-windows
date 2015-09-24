﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using System.IO;

namespace broadside_client_windows
{
    class Client
    {
        string hostName = "asukaserver.ddns.net";
        UdpClient udpClient;

        public void Connect() {
            Console.WriteLine("Sending data to " + hostName + ":19440");
            udpClient = new UdpClient(19440);
            udpClient.Connect(hostName, 19440);

            Byte[] sendBytes = Encoding.ASCII.GetBytes("Now you see me, now I'm gay.");

            udpClient.Send(sendBytes, sendBytes.Length);

            IPEndPoint RemoteIpEndPoint = new IPEndPoint(Dns.GetHostAddresses(hostName)[0], 19440);

            Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
            string returnData = Encoding.ASCII.GetString(receiveBytes);
            Console.WriteLine("Received message: " + returnData.ToString());

            udpClient.Close();
        }

    }
}
