namespace ChatServerCS
{
    public interface IClient
    {
        void DisconnessionePartecipante(string nome);
        void RiconnessionePartecipante(string nome);
        void AccessoPartecipante(Utente client);
        void LogoutPartecipante(string nome);
        void MsgTestoBroadcast(string mittente, string messaggio);
        void MsgImmagineBroadcast(string mittente, byte[] img);
        void MsgTestoUnicast(string mittente, string message);
        void MsgImmagineUnicast(string mittente, byte[] img);
        void PartecipanteStaScrivendo(string mittente);
    }
}