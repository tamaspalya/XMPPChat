using Artalk.Xmpp.Client;
using System;
using System.Text.RegularExpressions;

namespace Artalk
{
   
        class Program
        {
            static void Main(string[] args)
            {
                string hostname = "localhost";
                string username = "admin";
                string password = "password123";

                using (ArtalkXmppClient client = new ArtalkXmppClient(hostname, username, password))
                {
                    // Setup any event handlers.
                    // ...
                    client.Connect();

                    Console.WriteLine("Type 'quit' to exit.");
                    while (Console.ReadLine() != "quit") ;
                }
            }
        }

    }
