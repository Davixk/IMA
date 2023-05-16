using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Drawing;
using ChatClientCS.Services;
using ChatClientCS.Enums;
using ChatClientCS.Models;
using ChatClientCS.Commands;
using System.Windows.Input;
using System.Windows.Media;
using System.Diagnostics;
using System.Reactive.Linq;

namespace ChatClientCS.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IChatService chatService;
        private IDialogService dialogService;
        private TaskFactory ctxTaskFactory;
        private const int MAX_IMAGE_WIDTH = 150;
        private const int MAX_IMAGE_HEIGHT = 150;

        private string _nomeUtente;
        public string NomeUtente
        {
            get { return _nomeUtente; }
            set
            {
                _nomeUtente = value;
                OnPropertyChanged();
            }
        }

        private string _imgProfilo;
        public string imgProfilo
        {
            get { return _imgProfilo; }
            set
            {
                _imgProfilo = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Partecipante> _partecipanti = new ObservableCollection<Partecipante>();
        public ObservableCollection<Partecipante> Partecipanti
        {
            get { return _partecipanti; }
            set
            {
                _partecipanti = value;
                OnPropertyChanged();
            }
        }

        private Partecipante _partecipanteSelezionato;
        public Partecipante PartecipanteSelezionato
        {
            get { return _partecipanteSelezionato; }
            set
            {
                _partecipanteSelezionato = value;
                if (PartecipanteSelezionato.HaInviatoNuovoMessaggio) PartecipanteSelezionato.HaInviatoNuovoMessaggio = false;
                OnPropertyChanged();
            }
        }

        private ModalitaUser _modalitaUser;
        public ModalitaUser ModalitaUser
        {
            get { return _modalitaUser; }
            set
            {
                _modalitaUser = value;
                OnPropertyChanged();
            }
        }

        private string _msgTesto;
        public string MsgTesto
        {
            get { return _msgTesto; }
            set
            {
                _msgTesto = value;
                OnPropertyChanged();
            }
        }

        private bool _connesso;
        public bool Connesso
        {
            get { return _connesso; }
            set
            {
                _connesso = value;
                OnPropertyChanged();
            }
        }

        private bool _loggato;
        public bool Loggato
        {
            get { return _loggato; }
            set
            {
                _loggato = value;
                OnPropertyChanged();
            }
        }

        #region Operazioni di connessione
        private ICommand _opConnessione;
        public ICommand OpConnessione
        {
            get
            {
                return _opConnessione ?? (_opConnessione = new RelayCommandAsync(() => Connetti()));
            }
        }

        private async Task<bool> Connetti()
        {
            try
            {
                await chatService.ConnessioneAsync();
                Connesso = true;
                return true;
            }
            catch (Exception) { return false; }
        }
        #endregion

        #region Comandi di accesso
        private ICommand _comandoLogin;
        public ICommand ComandoAccesso
        {
            get
            {
                return _comandoLogin ?? (_comandoLogin =
                    new RelayCommandAsync(() => Accesso(), (o) => PuoAccedere()));
            }
        }

        private async Task<bool> Accesso()
        {
            try
            {
                List<Utente> utenti = new List<Utente>();
                utenti = await chatService.AccessoAsincr(_nomeUtente, Avatar());
                if (utenti != null)
                {
                    utenti.ForEach(u => Partecipanti.Add(new Partecipante { Nome = u.Nome, imgProfilo = u.imgProfilo }));
                    ModalitaUser = ModalitaUser.Chat;
                    Loggato = true;
                    return true;
                }
                else
                {
                    dialogService.ShowNotification("Nome utente già in uso");
                    return false;
                }

            }
            catch (Exception) { return false; }
        }

        private bool PuoAccedere()
        {
            return !string.IsNullOrEmpty(NomeUtente) && NomeUtente.Length >= 2 && Connesso;
        }
        #endregion

        #region Comandi di Logout
        private ICommand _opLogout;
        public ICommand OpLogout
        {
            get
            {
                return _opLogout ?? (_opLogout =
                    new RelayCommandAsync(() => Logout(), (o) => CanLogout()));
            }
        }

        private async Task<bool> Logout()
        {
            try
            {
                await chatService.DisconnessioneAsincr();
                ModalitaUser = ModalitaUser.Login;
                return true;
            }
            catch (Exception) { return false; }
        }

        private bool CanLogout()
        {
            return Connesso && Loggato;
        }
        #endregion

        #region Comandi Scrittura
        private ICommand _opStaScivendo;
        public ICommand OpStaScrivendo
        {
            get
            {
                return _opStaScivendo ?? (_opStaScivendo =
                    new RelayCommandAsync(() => StaScrivendo(), (o) => PuoUsareStaScrivendo()));
            }
        }

        private async Task<bool> StaScrivendo()
        {
            try
            {
                await chatService.StaScrivendoAsincr(PartecipanteSelezionato.Nome);
                return true;
            }
            catch (Exception) { return false; }
        }

        private bool PuoUsareStaScrivendo()
        {
            return (PartecipanteSelezionato != null && PartecipanteSelezionato.Loggato);
        }
        #endregion

        #region Comando per inviare messaggi di testo
        private ICommand _opInviaMsgTesto;
        public ICommand OpInviaMsgTesto
        {
            get
            {
                return _opInviaMsgTesto ?? (_opInviaMsgTesto =
                    new RelayCommandAsync(() => InviaMsgTesto(), (o) => PuoInviareMsgTesto()));
            }
        }

        private async Task<bool> InviaMsgTesto()
        {
            try
            {
                var destinatario = _partecipanteSelezionato.Nome;
                await chatService.MandaMsgUnicastAsincr(destinatario, _msgTesto);
                return true;
            }
            catch (Exception) { return false; }
            finally
            {
                MessaggioChat msg = new MessaggioChat
                {
                    Autore = NomeUtente,
                    Testo = _msgTesto,
                    DataOra = DateTime.Now,
                    IsOriginNative = true
                };
                PartecipanteSelezionato.Chatter.Add(msg);
                MsgTesto = string.Empty;
            }
        }

        private bool PuoInviareMsgTesto()
        {
            return (!string.IsNullOrEmpty(MsgTesto) && Connesso &&
                _partecipanteSelezionato != null && _partecipanteSelezionato.Loggato);
        }
        #endregion

        #region Comando per inviare immagini
        private ICommand _opInviaMsgImmagine;
        public ICommand OpInviaMsgImmagine
        {
            get
            {
                return _opInviaMsgImmagine ?? (_opInviaMsgImmagine =
                    new RelayCommandAsync(() => InviaMsgImmagine(), (o) => PuoInviareMsgImmagine()));
            }
        }

        private async Task<bool> InviaMsgImmagine()
        {
            var immagine = dialogService.OpenFile("Seleziona file immagine", "Immagini (*.jpg;*.png)|*.jpg;*.png");
            if (string.IsNullOrEmpty(immagine)) return false;

            var img = await Task.Run(() => File.ReadAllBytes(immagine));

            try
            {
                var destinatario = _partecipanteSelezionato.Nome;
                await chatService.MandaMsgUnicastAsincr(destinatario, img);
                return true;
            }
            catch (Exception) { return false; }
            finally
            {
                MessaggioChat msg = new MessaggioChat { Autore = NomeUtente, Immagine = immagine, DataOra = DateTime.Now, IsOriginNative = true };
                PartecipanteSelezionato.Chatter.Add(msg);
            }           
        }

        private bool PuoInviareMsgImmagine()
        {
            return (Connesso && _partecipanteSelezionato != null && _partecipanteSelezionato.Loggato);
        }
        #endregion

        #region Operazione Selezione Immagine Profilo
        private ICommand _opSelezionaImgProfilo;
        public ICommand OpSelezionaImgProfilo
        {
            get
            {
                return _opSelezionaImgProfilo ?? (_opSelezionaImgProfilo =
                    new RelayCommand((o) => SelezionaImgProfilo()));
            }
        }

        private void SelezionaImgProfilo()
        {
            var _imgProfilo = dialogService.OpenFile("Seleziona file immagine", "Immagini (*.jpg;*.png)|*.jpg;*.png");
            if (!string.IsNullOrEmpty(_imgProfilo))
            {
                var img = Image.FromFile(_imgProfilo);
                if (img.Width > MAX_IMAGE_WIDTH || img.Height > MAX_IMAGE_HEIGHT)
                {
                    dialogService.ShowNotification($"Le dimensioni della immagine devono esesere di {MAX_IMAGE_WIDTH} x {MAX_IMAGE_HEIGHT} o inferiori.");
                    return;
                }
                imgProfilo = _imgProfilo;
            }
        }
        #endregion

        #region Comando per aprire immagini
        private ICommand _opApriImmagine;
        public ICommand OpApriImmagine
        {
            get
            {
                return _opApriImmagine ?? (_opApriImmagine =
                    new RelayCommand<MessaggioChat>((m) => ApriImmagine(m)));
            }
        }

        private void ApriImmagine(MessaggioChat messaggio)
        {
            var img = messaggio.Immagine;
            if (string.IsNullOrEmpty(img) || !File.Exists(img)) return;
            Process.Start(img);
        }
        #endregion

        #region Gestore Eventi (Event Handlers)
        private void NuovoMsgTesto(string nome, string testo, TipoDiMessaggio tipoMsg)
        {
            if (tipoMsg == TipoDiMessaggio.Unicast)
            {
                MessaggioChat msgChat = new MessaggioChat { Autore = nome, Testo = testo, DataOra = DateTime.Now };
                var mittente = _partecipanti.Where((u) => string.Equals(u.Nome, nome)).FirstOrDefault();
                ctxTaskFactory.StartNew(() => mittente.Chatter.Add(msgChat)).Wait();

                if (!(PartecipanteSelezionato != null && mittente.Nome.Equals(PartecipanteSelezionato.Nome)))
                {
                    ctxTaskFactory.StartNew(() => mittente.HaInviatoNuovoMessaggio = true).Wait();
                }
            }
        }

        private void NuovoMsgImmagine(string nome, byte[] immagine, TipoDiMessaggio tipoMsg)
        {
            if (tipoMsg == TipoDiMessaggio.Unicast)
            {
                var CartellaImmagini = Path.Combine(Environment.CurrentDirectory, "Image Messages");
                if (!Directory.Exists(CartellaImmagini)) Directory.CreateDirectory(CartellaImmagini);

                var contImmagini = Directory.EnumerateFiles(CartellaImmagini).Count() + 1;
                var percorsoImmagine = Path.Combine(CartellaImmagini, $"IMG_{contImmagini}.jpg");

                ImageConverter convertitoreImmagini = new ImageConverter();
                using (Image img = (Image)convertitoreImmagini.ConvertFrom(immagine))
                {
                    img.Save(percorsoImmagine);
                }

                MessaggioChat msgChat = new MessaggioChat { Autore = nome, Immagine = percorsoImmagine, DataOra = DateTime.Now };
                var mittente = _partecipanti.Where(u => string.Equals(u.Nome, nome)).FirstOrDefault();
                ctxTaskFactory.StartNew(() => mittente.Chatter.Add(msgChat)).Wait();

                if (!(PartecipanteSelezionato != null && mittente.Nome.Equals(PartecipanteSelezionato.Nome)))
                {
                    ctxTaskFactory.StartNew(() => mittente.HaInviatoNuovoMessaggio = true).Wait();
                }
            }
        }

        private void LoginPartecipante(Utente u)
        {
            var _partecipante = Partecipanti.FirstOrDefault(partecipante => string.Equals(partecipante.Nome, u.Nome));
            if (_loggato && _partecipante == null)
            {
                ctxTaskFactory.StartNew(() => Partecipanti.Add(new Partecipante
                {
                    Nome = u.Nome,
                    imgProfilo = u.imgProfilo
                })).Wait();
            }
        }

        private void DisconnessionePartecipante(string nome)
        {
            var tizio = Partecipanti.Where((partecipante) => string.Equals(partecipante.Nome, nome)).FirstOrDefault();
            if (tizio != null) tizio.Loggato = false;
        }

        private void RiconnessionePartecipante(string nome)
        {
            var tizio = Partecipanti.Where((partecipante) => string.Equals(partecipante.Nome, nome)).FirstOrDefault();
            if (tizio != null) tizio.Loggato = true;
        }

        private void Riconnessione()
        {
            Connesso = false;
            Loggato = false;
        }

        private async void Riconnesso()
        {
            var imgProfilo = Avatar();
            if (!string.IsNullOrEmpty(_nomeUtente)) await chatService.AccessoAsincr(_nomeUtente, imgProfilo);
            Connesso = true;
            Loggato = true;
        }

        private async void Disconnesso()
        {
            var taskConnessione = chatService.ConnessioneAsync();
            await taskConnessione.ContinueWith(task => {
                if (!task.IsFaulted)
                {
                    Connesso = true;
                    chatService.AccessoAsincr(_nomeUtente, Avatar()).Wait();
                    Loggato = true;
                }
            });
        }

        private void PartecipanteStaScrivendo(string nome)
        {
            var tizio = Partecipanti.Where((p) => string.Equals(p.Nome, nome)).FirstOrDefault();
            if (tizio != null && !tizio.StaScrivendo)
            {
                tizio.StaScrivendo = true;
                Observable.Timer(TimeSpan.FromMilliseconds(1500)).Subscribe(t => tizio.StaScrivendo = false);
            }
        }
        #endregion

        private byte[] Avatar()
        {
            byte[] imgProfilo = null;
            if (!string.IsNullOrEmpty(_imgProfilo)) imgProfilo = File.ReadAllBytes(_imgProfilo);
            return imgProfilo;
        }

        public MainWindowViewModel(IChatService chatSvc, IDialogService diagSvc)
        {
            dialogService = diagSvc;
            chatService = chatSvc;

            chatSvc.NuovoMsgTesto += NuovoMsgTesto;
            chatSvc.NuovoMsgImmagine += NuovoMsgImmagine;
            chatSvc.PartecipanteHaFattoAccesso += LoginPartecipante;
            chatSvc.PartecipanteSiEDisconnesso += DisconnessionePartecipante;
            chatSvc.PartecipanteDisconnesso += DisconnessionePartecipante;
            chatSvc.PartecipanteRiconnesso += RiconnessionePartecipante;
            chatSvc.PartecipanteStaScrivendo += PartecipanteStaScrivendo;
            chatSvc.RiconnessioneInCorso += Riconnessione;
            chatSvc.ConnessioneRistabilita += Riconnesso;
            chatSvc.ConnessioneChiusa += Disconnesso;

            ctxTaskFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());
        }

    }
}