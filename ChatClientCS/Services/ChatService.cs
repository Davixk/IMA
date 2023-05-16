/*
Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. 
Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.


INSERISCI COMMENTO SPIEGAZIONE QUI



*/
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatClientCS.Enums;
using ChatClientCS.Models;
using System.Net;
using Microsoft.AspNet.SignalR.Client;

namespace ChatClientCS.Services
{
    public class ChatService : IChatService
    {
        public event Action<string, string, TipoDiMessaggio> NuovoMsgTesto;
        public event Action<string, byte[], TipoDiMessaggio> NuovoMsgImmagine;
        public event Action<string> PartecipanteDisconnesso;
        public event Action<Utente> PartecipanteHaFattoAccesso;
        public event Action<string> PartecipanteSiEDisconnesso;
        public event Action<string> PartecipanteRiconnesso;
        public event Action RiconnessioneInCorso;
        public event Action ConnessioneRistabilita;
        public event Action ConnessioneChiusa;
        public event Action<string> PartecipanteStaScrivendo;

        private IHubProxy hubProxy;
        private HubConnection connessione;
        private string url = "http://localhost:8080/InstantMessagingApp";

        public async Task ConnessioneAsync()
        {
            connessione = new HubConnection(url);
            hubProxy = connessione.CreateHubProxy("ChatHub");
            hubProxy.On<Utente>("AccessoPartecipante", (u) => PartecipanteHaFattoAccesso?.Invoke(u));
            hubProxy.On<string>("LogoutPartecipante", (n) => PartecipanteSiEDisconnesso?.Invoke(n));
            hubProxy.On<string>("DisconnessionePartecipante", (n) => PartecipanteDisconnesso?.Invoke(n));
            hubProxy.On<string>("RiconnessionePartecipante", (n) => PartecipanteRiconnesso?.Invoke(n));
            hubProxy.On<string, string>("MsgTestoBroadcast", (n, m) => NuovoMsgTesto?.Invoke(n, m, TipoDiMessaggio.Broadcast));
            hubProxy.On<string, byte[]>("MsgImmagineBroadcast", (n, m) => NuovoMsgImmagine?.Invoke(n, m, TipoDiMessaggio.Broadcast));
            hubProxy.On<string, string>("MsgTestoUnicast", (n, m) => NuovoMsgTesto?.Invoke(n, m, TipoDiMessaggio.Unicast));
            hubProxy.On<string, byte[]>("MsgImmagineUnicast", (n, m) => NuovoMsgImmagine?.Invoke(n, m, TipoDiMessaggio.Unicast));
            hubProxy.On<string>("PartecipanteStaScrivendo", (p) => PartecipanteStaScrivendo?.Invoke(p));

            connessione.Reconnecting += Riconnessione;
            connessione.Reconnected += Riconnesso;
            connessione.Closed += Disconnesso;

            ServicePointManager.DefaultConnectionLimit = 10;
            await connessione.Start();
        }

        

        private void Riconnesso()
        {
            ConnessioneRistabilita?.Invoke();
        }
        private void Disconnesso()
        {
            ConnessioneChiusa?.Invoke();
        }
        private void Riconnessione()
        {
            RiconnessioneInCorso?.Invoke();
        }

        public async Task<List<Utente>> AccessoAsincr(string nome, byte[] immagineProf)
        {
            return await hubProxy.Invoke<List<Utente>>("Accesso", new object[] { nome, immagineProf });
        }

        public async Task DisconnessioneAsincr()
        {
            await hubProxy.Invoke("Logout");
        }

        public async Task MandaMsgBroadcastAsincr(string msg)
        {
            await hubProxy.Invoke("MsgTestoBroadcast", msg);
        }

        public async Task MandaMsgBroadcastAsincr(byte[] img)
        {
            await hubProxy.Invoke("MsgImmagineBroadcast", img);
        }

        public async Task MandaMsgUnicastAsincr(string destinatario, string msg)
        {
            await hubProxy.Invoke("MsgTestoUnicast", new object[] { destinatario, msg });
        }

        public async Task MandaMsgUnicastAsincr(string destinatario, byte[] img)
        {
            await hubProxy.Invoke("MsgImmagineUnicast", new object[] { destinatario, img });
        }

        public async Task StaScrivendoAsincr(string destinatario)
        {
            await hubProxy.Invoke("StaScrivendo", destinatario);
        }
    }
}