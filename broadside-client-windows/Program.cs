using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace broadside_client_windows
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            client.Connect();
            client.Receive();
            client.Disconnect();
        }
    }
}
