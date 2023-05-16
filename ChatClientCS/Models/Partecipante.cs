using ChatClientCS.ViewModels;
using System.Collections.ObjectModel;

namespace ChatClientCS.Models
{
    public class Partecipante : ViewModelBase
    {
        public string Nome { get; set; }
        public byte[] imgProfilo { get; set; }
        public ObservableCollection<MessaggioChat> Chatter { get; set; }

        private bool _loggato = true;
        public bool Loggato
        {
            get { return _loggato; }
            set { _loggato = value; OnPropertyChanged(); }
        }

        private bool _haInviatoNuovoMessaggio;
        public bool HaInviatoNuovoMessaggio
        {
            get { return _haInviatoNuovoMessaggio; }
            set { _haInviatoNuovoMessaggio = value; OnPropertyChanged(); }
        }

        private bool _staScrivendo;
        public bool StaScrivendo
        {
            get { return _staScrivendo; }
            set { _staScrivendo = value; OnPropertyChanged(); }
        }

        public Partecipante() { Chatter = new ObservableCollection<MessaggioChat>(); }
    }
}