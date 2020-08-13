using System;
using System.Net;
using System.Net.NetworkInformation;

namespace JustPing {

    class MainClass {

        public static void Main(string[] args) {

            IPAddress ipAddr = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];

            // splitting the IP Address
            string[] parts = ipAddr.ToString().Split('.');
            int locations = 200;

            for (int local = 0; local <= locations; local++) {

                // reconstructing the 3 first numbers to the IP
                string addr = "";
                for (int i = 0; i < 3; i++) {
                    addr += "" + parts[i] + ".";
                }

                // adding the last number with a location (local)
                addr += local.ToString();
                string[] status = GetStatus(addr); // gets status

                Console.WriteLine("[IP: {0}] STATUS: {1} \n[RUNTIME]: {2} \n", addr, status[0],status[1]);
            }
        }

        // method that receives the status of a given ip address
        static string[] GetStatus(string ip)
        {
            Ping ping = new Ping();
            PingReply reply = ping.Send(ip);

            string[] status = { reply.Status.ToString(), ((float)reply.RoundtripTime/1000).ToString()+"s" };

            return status;
        }
    }
}
