using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;

namespace ChatServerCS
{
    class ProgrammaPrincipale
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:8080/";
            using (WebApp.Start<Avvio>(url))
            {
                Console.WriteLine($"Server inizializzato all'indirizzo {url}");
                Console.ReadLine();
            }
        }
    }

    public class Avvio
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR("/InstantMessagingApp", new HubConfiguration());

            GlobalHost.Configuration.MaxIncomingWebSocketMessageSize = null;
        }
    }
}