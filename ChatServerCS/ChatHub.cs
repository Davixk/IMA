using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.AspNet.SignalR;

namespace ChatServerCS
{
    public class ChatHub : Hub<IClient>
    {
        private static ConcurrentDictionary<string, Utente> ChatClients = new ConcurrentDictionary<string, Utente>();

        public override Task OnDisconnected(bool stopCalled)
        {
            var userName = ChatClients.SingleOrDefault((c) => c.Value.ID == Context.ConnectionId).Key;
            if (userName != null)
            {
                Clients.Others.DisconnessionePartecipante(userName);
                Console.WriteLine($"<> {userName} ha chiuso il client");
            }
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            var nomeUtente = ChatClients.SingleOrDefault((c) => c.Value.ID == Context.ConnectionId).Key;
            if (nomeUtente != null)
            {
                Clients.Others.RiconnessionePartecipante(nomeUtente);
                Console.WriteLine($"== {nomeUtente} riconnesso");
            }
            return base.OnReconnected();
        }

        public List<Utente> Accesso(string nome, byte[] foto)
        {
            if (!ChatClients.ContainsKey(nome))
            {
                Console.WriteLine($"++ {nome} si è connesso");
                List<Utente> utenti = new List<Utente>(ChatClients.Values);
                Utente nuovoUtente = new Utente { Nome = nome, ID = Context.ConnectionId, imgProfilo = foto };
                var aggiunto = ChatClients.TryAdd(nome, nuovoUtente);
                if (!aggiunto) return null;
                Clients.CallerState.UserName = nome;
                Clients.Others.AccessoPartecipante(nuovoUtente);
                return utenti;
            }
            return null;
        }

        public void Logout()
        {
            var nome = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(nome))
            {
                Utente client = new Utente();
                ChatClients.TryRemove(nome, out client);
                Clients.Others.LogoutPartecipante(nome);
                Console.WriteLine($"-- {nome} si è disconnesso");
            }
        }

        public void MsgTestoBroadcast(string messaggio)
        {
            var nome = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(messaggio))
            {
                Clients.Others.MsgTestoBroadcast(nome, messaggio);
            }
        }

        public void MsgImmagineBroadcast(byte[] img)
        {
            var nome = Clients.CallerState.UserName;
            if (img != null)
            {
                Clients.Others.MsgImmagineBroadcast(nome, img);
            }
        }

        public void MsgTestoUnicast(string destinatario, string messaggio)
        {
            var mittente = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(mittente) && destinatario != mittente &&
                !string.IsNullOrEmpty(messaggio) && ChatClients.ContainsKey(destinatario))
            {
                Utente client = new Utente();
                ChatClients.TryGetValue(destinatario, out client);
                Clients.Client(client.ID).MsgTestoUnicast(mittente, messaggio);
            }
        }

        public void MsgImmagineUnicast(string destinatario, byte[] img)
        {
            var mittente = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(mittente) && destinatario != mittente &&
                img != null && ChatClients.ContainsKey(destinatario))
            {
                Utente client = new Utente();
                ChatClients.TryGetValue(destinatario, out client);
                Clients.Client(client.ID).MsgImmagineUnicast(mittente, img);
            }
        }

        public void StaScrivendo(string destinatario)
        {
            if (string.IsNullOrEmpty(destinatario)) return;
            var mittente = Clients.CallerState.UserName;
            Utente client = new Utente();
            ChatClients.TryGetValue(destinatario, out client);
            Clients.Client(client.ID).PartecipanteStaScrivendo(mittente);
        }
    }
}