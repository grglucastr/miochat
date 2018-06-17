using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRSelfHost
{
    public class MyHub: Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }

        public override Task OnConnected()
        {
            Console.WriteLine("Conectou caraleo! É Penta! #zsmmn!");
            NotifyAllClients();
            BroadcastSimpleString("Aqui é uma mensagem de seu comandante! O SERVERRRR!!! MUAHUHAUHA");
            return base.OnConnected();
        }

        public void NotifyAllClients()
        {
            Clients.All.Notify();
        }

        public void BroadcastSimpleString(string str)
        {
            Clients.All.UpdateSimpleString(str);
        }

        public void SendMessage(string str)
        {
            BroadcastSimpleString(str);
        }
    }
}
