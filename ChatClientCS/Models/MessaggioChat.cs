using System;

namespace ChatClientCS.Models
{
    public class MessaggioChat
    {
        public string Testo { get; set; }
        public string Autore { get; set; }
        public DateTime DataOra { get; set; }
        public string Immagine { get; set; }
        public bool IsOriginNative { get; set; }
    }
}
