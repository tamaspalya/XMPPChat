using Artalk.Xmpp.Client;
using Artalk.Xmpp.Im;
using System;

namespace Artalk
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectedJid;
            string hostname = "localhost";

            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();
            Console.Write("Type a jid of the person you want to chat with: ");
            connectedJid = Console.ReadLine();
            connectedJid += "@" + hostname;

            using (var client = new ArtalkXmppClient(hostname, username, password))
            {
                client.Message += OnNewMessage;
                client.Connect();
                string message;
                while (getMessage(Console.ReadLine(), connectedJid, client));
            }

            /// <summary>
            /// Invoked whenever a new chat-message has been received.
            /// </summary>
            static void OnNewMessage(object sender, MessageEventArgs e)
            {
                Console.WriteLine("\nMessage from <" + e.Jid + ">: " + e.Message.Body);
            }
            static bool getMessage(string message, string jid,ArtalkXmppClient client)
            {
                if (message == "quit")
                {
                    return false;
                }
                else
                {
                    client.SendMessage(jid, message);
                    return true;
                }
            }
        }
    }
}

