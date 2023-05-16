using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatClientCS.Models;
using ChatClientCS.Enums;

namespace ChatClientCS.Services
{
    public interface IChatService
    {
        event Action<Utente> PartecipanteHaFattoAccesso;
        event Action<string> PartecipanteSiEDisconnesso;
        event Action<string> PartecipanteDisconnesso;
        event Action<string> PartecipanteRiconnesso;
        event Action RiconnessioneInCorso;
        event Action ConnessioneRistabilita;
        event Action ConnessioneChiusa;
        event Action<string, string, TipoDiMessaggio> NuovoMsgTesto;
        event Action<string, byte[], TipoDiMessaggio> NuovoMsgImmagine;
        event Action<string> PartecipanteStaScrivendo;

        Task ConnessioneAsync();
        Task<List<Utente>> AccessoAsincr(string nome, byte[] imgProfilo);
        Task DisconnessioneAsincr();

        Task MandaMsgBroadcastAsincr(string testo);
        Task MandaMsgBroadcastAsincr(byte[] img);
        Task MandaMsgUnicastAsincr(string destinatario, string testo);
        Task MandaMsgUnicastAsincr(string destinatario, byte[] img);
        Task StaScrivendoAsincr(string destinatario);
    }
}