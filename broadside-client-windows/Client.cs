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
        UdpClient udpClient;
        ConsoleGUI GUI = new ConsoleGUI();

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

        public void TestGUI()
        {
            ConsoleGUI.LogBox console = new ConsoleGUI.LogBox(1, 1, 30, 20, BorderStyles.oneLine);
            ConsoleGUI.LogBox console2 = new ConsoleGUI.LogBox(32, 1, 20, 20, BorderStyles.twoLine);

            GUI.AddElement(console, true);
            GUI.AddElement(console2, false);

            //console.WriteLine("I'd just like to interject for moment. What you're refering to as Linux, is in fact, GNU/Linux, or as I've recently taken to calling it, GNU plus Linux. Linux is not an operating system unto itself, but rather another free component of a fully functioning GNU system made useful by the GNU corelibs, shell utilities and vital system components comprising a full OS as defined by POSIX. Many computer users run a modified version of the GNU system every day, without realizing it. Through a peculiar turn of events, the version of GNU which is widely used today is often called Linux, and many of its users are not aware that it is basically the GNU system, developed by the GNU Project. There really is a Linux, and these people are using it, but it is just a part of the system they use. Linux is the kernel: the program in the system that allocates the machine's resources to the other programs that you run. The kernel is an essential part of an operating system, but useless by itself; it can only function in the context of a complete operating system. Linux is normally used in combination with the GNU operating system: the whole system is basically GNU with Linux added, or GNU/Linux. All the so-called Linux distributions are really distributions of GNU/Linux!");

            //Basic loop

            //Some things to make a fun little animation.
            int frame = 0;
            bool forwards = true;

            while (true) {
                GUI.PaintScreen();
                GUI.Interact();
                if (frame == 1)
                    forwards = true;
                if (frame == 28)
                    forwards = false;
                string placeholder = "";
                for (int temp = 0; temp < frame; temp++)
                    placeholder += "#";
                console.WriteLine(placeholder);
                if (forwards)
                    frame++;
                else
                    frame--;
            }
        }

    }
}
