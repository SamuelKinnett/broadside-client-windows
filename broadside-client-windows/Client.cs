using System;
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
        TcpClient tcpClient;
        Stream inputStream;
        StreamWriter streamWriter;
        StreamReader streamReader;

        public int Connect() {
            Console.WriteLine("Attempting to connect to : " + hostName + ":19940");
            tcpClient = new TcpClient(hostName, 19440);
            Console.WriteLine("Connected!");
            try { 
                inputStream = tcpClient.GetStream();
                streamWriter = new StreamWriter(inputStream);
                streamReader = new StreamReader(inputStream);
                streamWriter.AutoFlush = true;
                return 0;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return 1;
            }
        }

        public void Receive()
        {
            Console.WriteLine("Receiving Data...");
            string receivedData = streamReader.ReadLine();
            Console.Write("Received! The time according to the server is: " + receivedData);
            Console.ReadLine();
        }

        public void Disconnect()
        {
            inputStream.Close();
        }

    }
}
