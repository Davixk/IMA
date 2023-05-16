Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Drawing
Imports System.Reactive.Linq

Public Class MainWindowViewModel
    Inherits ViewModelBase

    Private chatService As IChatService
    Private dialogService As IDialogService
    Private ctxTaskFactory As TaskFactory
    Private Const MAX_IMAGE_WIDTH As Integer = 150
    Private Const MAX_IMAGE_HEIGHT As Integer = 150

    Private _nomeUtente As String
    Public Property NomeUtente As String
        Get
            Return _nomeUtente
        End Get
        Set(value As String)
            _nomeUtente = value
            OnPropertyChanged()
        End Set
    End Property

    Private _imgProfilo As String
    Public Property imgProfilo As String
        Get
            Return _imgProfilo
        End Get
        Set(value As String)
            _imgProfilo = value
            OnPropertyChanged()
        End Set
    End Property

    Private _partecipanti As New ObservableCollection(Of Partecipante)
    Public Property Partecipanti As ObservableCollection(Of Partecipante)
        Get
            Return _partecipanti
        End Get
        Set(value As ObservableCollection(Of Partecipante))
            _partecipanti = value
            OnPropertyChanged()
        End Set
    End Property

    Private _partecipanteSelezionato As Partecipante
    Public Property PartecipanteSelezionato As Partecipante
        Get
            Return _partecipanteSelezionato
        End Get
        Set(value As Partecipante)
            _partecipanteSelezionato = value
            If PartecipanteSelezionato.HaMandatoNuovoMsg Then PartecipanteSelezionato.HaMandatoNuovoMsg = False
            OnPropertyChanged()
        End Set
    End Property

    Private _modalitaUser As ModalitaUser
    Public Property ModalitaUser As ModalitaUser
        Get
            Return _modalitaUser
        End Get
        Set(value As ModalitaUser)
            _modalitaUser = value
            OnPropertyChanged()
        End Set
    End Property

    Private _msgTesto As String
    Public Property MsgTesto As String
        Get
            Return _msgTesto
        End Get
        Set(value As String)
            _msgTesto = value
            OnPropertyChanged()
        End Set
    End Property

    Private _connesso As Boolean
    Public Property Connesso As Boolean
        Get
            Return _connesso
        End Get
        Set(value As Boolean)
            _connesso = value
            OnPropertyChanged()
        End Set
    End Property

    Private _loggato As Boolean
    Public Property Loggato As Boolean
        Get
            Return _loggato
        End Get
        Set(value As Boolean)
            _loggato = value
            OnPropertyChanged()
        End Set
    End Property

#Region "Comandi di connessione"
    Private _opConnessione As ICommand
    Public ReadOnly Property OpConnessione As ICommand
        Get
            Return If(_opConnessione, New RelayCommandAsync(Function() Connetti()))
        End Get
    End Property

    Private Async Function Connetti() As Task(Of Boolean)
        Try
            Await chatService.ConnessioneAsync()
            Connesso = True
            Return True
        Catch eccezione As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Comandi di accesso"
    Private _comandoLogin As ICommand
    Public ReadOnly Property ComandoAccesso As ICommand
        Get
            Return If(_comandoLogin, New RelayCommandAsync(Function() Accesso(), AddressOf PuoAccedere))
        End Get
    End Property

    Private Async Function Accesso() As Task(Of Boolean)
        Try
            Dim utenti As List(Of Utente)
            utenti = Await chatService.AccessoAsincr(_nomeUtente, Avatar())
            If utenti IsNot Nothing Then
                utenti.ForEach(Sub(u) Partecipanti.Add(New Partecipante With {.Nome = u.Nome, .imgProfilo = u.imgProfilo}))
                ModalitaUser = ModalitaUser.Chat
                Loggato = True
                Return True
            Else
                dialogService.ShowNotification("Nome utente già in uso")
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function PuoAccedere() As Boolean
        Return Not String.IsNullOrEmpty(NomeUtente) AndAlso NomeUtente.Length >= 2 AndAlso Connesso
    End Function
#End Region

#Region "Comandi di Logout"
    Private _opLogout As ICommand
    Public ReadOnly Property OpLogout As ICommand
        Get
            Return If(_opLogout, New RelayCommandAsync(Function() Logout(), AddressOf PuoDisconnettersi))
        End Get
    End Property

    Private Async Function Logout() As Task(Of Boolean)
        Try
            Await chatService.DisconnessioneAsincr()
            ModalitaUser = ModalitaUser.Accesso
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function PuoDisconnettersi() As Boolean
        Return Connesso AndAlso Loggato
    End Function
#End Region

#Region "Comandi Scrittura"
    Private _opStaScrivendo As ICommand
    Public ReadOnly Property OpStaScrivendo As ICommand
        Get
            Return If(_opStaScrivendo, New RelayCommandAsync(Function() StaScrivendo(), AddressOf PuoUsareStaScrivendo))
        End Get
    End Property

    Private Async Function StaScrivendo() As Task(Of Boolean)
        Try
            Await chatService.StaScrivendoAsincr(PartecipanteSelezionato.Nome)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function PuoUsareStaScrivendo() As Boolean
        Return PartecipanteSelezionato IsNot Nothing AndAlso PartecipanteSelezionato.Loggato
    End Function
#End Region

#Region "Comando per inviare messaggio di testo"
    Private _opInviaMsgTesto As ICommand
    Public ReadOnly Property OpInviaMsgTesto As ICommand
        Get
            Return If(_opInviaMsgTesto, New RelayCommandAsync(Function() InviaMsgTesto(),
                                                                     AddressOf PuoInviareMsgTesto))
        End Get
    End Property

    Private Async Function InviaMsgTesto() As Task(Of Boolean)
        Try
            Dim destinatario = _partecipanteSelezionato.Nome
            Await chatService.MandaMsgUnicastAsincr(destinatario, _msgTesto)
            Return True
        Catch ex As Exception
            Return False
        Finally
            Dim msg As New MessaggioChat With {.Autore = NomeUtente, .Testo = _msgTesto,
                .DataOra = DateTime.Now, .IsOriginNative = True}
            PartecipanteSelezionato.Chatter.Add(msg)
            MsgTesto = String.Empty
        End Try
    End Function

    Private Function PuoInviareMsgTesto() As Boolean
        Return Not String.IsNullOrEmpty(MsgTesto) AndAlso Connesso AndAlso
            _partecipanteSelezionato IsNot Nothing AndAlso _partecipanteSelezionato.Loggato
    End Function
#End Region

#Region "Comando per inviare immagini"
    Private _opInviaMsgImmagine As ICommand
    Public ReadOnly Property OpInviaMsgImmagine As ICommand
        Get
            Return If(_opInviaMsgImmagine, New RelayCommandAsync(Function() InviaMsgImmagine(),
                                                                     AddressOf PuoMandareMsgImmagine))
        End Get
    End Property

    Private Async Function InviaMsgImmagine() As Task(Of Boolean)
        Dim immagine = dialogService.OpenFile("Seleziona file immagine", "Immagini (*.jpg;*.png)|*.jpg;*.png")
        If String.IsNullOrEmpty(immagine) Then Return False

        Dim img = Await Task.Run(Function() File.ReadAllBytes(immagine))

        Try
            Dim destinatario = _partecipanteSelezionato.Nome
            Await chatService.MandaMsgUnicastAsincr(destinatario, img)
            Return True
        Catch ex As Exception
            Return False
        Finally
            Dim msg As New MessaggioChat With {.Autore = NomeUtente, .Immagine = immagine, .DataOra = DateTime.Now, .IsOriginNative = True}
            PartecipanteSelezionato.Chatter.Add(msg)
        End Try
    End Function

    Private Function PuoMandareMsgImmagine() As Boolean
        Return Connesso AndAlso _partecipanteSelezionato IsNot Nothing AndAlso _partecipanteSelezionato.Loggato
    End Function
#End Region

#Region "Operazione Selezione Immagine Profilo"
    Private _opSelezionaImgProfilo As ICommand
    Public ReadOnly Property SelectPhotoCommand As ICommand
        Get
            Return If(_opSelezionaImgProfilo, New RelayCommand(AddressOf SelezionaImgProfilo))
        End Get
    End Property

    Private Sub SelezionaImgProfilo()
        Dim _imgProfilo = dialogService.OpenFile("Seleziona file immagine", "Immagini (*.jpg;*.png)|*.jpg;*.png")
        If Not String.IsNullOrEmpty(_imgProfilo) Then
            Dim img = Image.FromFile(_imgProfilo)
            If img.Width > MAX_IMAGE_WIDTH OrElse img.Height > MAX_IMAGE_HEIGHT Then
                dialogService.ShowNotification($"Le dimensioni della immagine devono esesere di {MAX_IMAGE_WIDTH} x {MAX_IMAGE_HEIGHT} o inferiori.")
                Exit Sub
            End If
            imgProfilo = _imgProfilo
        End If
    End Sub
#End Region

#Region "Comando per aprire immagini"
    Private _opApriImmagine As ICommand
    Public ReadOnly Property OpApriImmagine As ICommand
        Get
            Return If(_opApriImmagine, New RelayCommand(Of MessaggioChat)(Sub(m) ApriImmagine(m)))
        End Get
    End Property

    Private Sub ApriImmagine(ByVal messaggio As MessaggioChat)
        Dim img = messaggio.Immagine
        If (String.IsNullOrEmpty(img) OrElse Not File.Exists(img)) Then Exit Sub
        Process.Start(img)
    End Sub
#End Region

#Region "Gestione Eventi (Event handlers)"
    Private Sub NuovoMsgTesto(nome As String, testo As String, tipoMsg As TipoDiMessaggio)
        If tipoMsg = TipoDiMessaggio.Unicast Then
            Dim msgChat As New MessaggioChat With {.Autore = nome, .Testo = testo, .DataOra = DateTime.Now}
            Dim mittente = _partecipanti.Where(Function(u) String.Equals(u.Nome, nome)).FirstOrDefault
            ctxTaskFactory.StartNew(Sub() mittente.Chatter.Add(msgChat)).Wait()

            If Not (PartecipanteSelezionato IsNot Nothing AndAlso mittente.Nome.Equals(PartecipanteSelezionato.Nome)) Then
                ctxTaskFactory.StartNew(Sub() mittente.HaMandatoNuovoMsg = True).Wait()
            End If
        End If
    End Sub

    Private Sub NuovoMsgImmagine(nome As String, immagine As Byte(), tipoMsg As TipoDiMessaggio)
        If tipoMsg = TipoDiMessaggio.Unicast Then
            Dim CartellaImmagini = Path.Combine(Environment.CurrentDirectory, "Image Messages")
            If Not Directory.Exists(CartellaImmagini) Then Directory.CreateDirectory(CartellaImmagini)

            Dim contImmagini = Directory.EnumerateFiles(CartellaImmagini).Count() + 1
            Dim percorsoImmagine = Path.Combine(CartellaImmagini, $"IMG_{contImmagini}.jpg")

            Dim convertitoreImmagini As New ImageConverter
            Using img As Image = CType(convertitoreImmagini.ConvertFrom(immagine), Image)
                img.Save(percorsoImmagine)
            End Using

            Dim msgChat As New MessaggioChat With {.Autore = nome, .Immagine = percorsoImmagine, .DataOra = DateTime.Now}
            Dim mittente = _partecipanti.Where(Function(u) String.Equals(u.Nome, nome)).FirstOrDefault
            ctxTaskFactory.StartNew(Sub() mittente.Chatter.Add(msgChat)).Wait()

            If Not (PartecipanteSelezionato IsNot Nothing AndAlso mittente.Nome.Equals(PartecipanteSelezionato.Nome)) Then
                ctxTaskFactory.StartNew(Sub() mittente.HaMandatoNuovoMsg = True).Wait()
            End If
        End If
    End Sub

    Private Sub LoginPartecipante(ByVal u As Utente)
        Dim _partecipante = Partecipanti.FirstOrDefault(Function(partecipante) String.Equals(partecipante.Nome, u.Nome))
        If _loggato AndAlso _partecipante Is Nothing Then
            ctxTaskFactory.StartNew(Sub() Partecipanti.Add(New Partecipante With {.Nome = u.Nome, .imgProfilo = u.imgProfilo})).Wait()
        End If
    End Sub

    Private Sub DisconnessionePartecipante(ByVal nome As String)
        Dim tizio = Partecipanti.Where(Function(partecipante) String.Equals(partecipante.Nome, nome)).FirstOrDefault
        If tizio IsNot Nothing Then tizio.Loggato = False
    End Sub

    Private Sub RiconnessionePartecipante(ByVal nome As String)
        Dim tizio = Partecipanti.Where(Function(p) String.Equals(p.Nome, nome)).FirstOrDefault
        If tizio IsNot Nothing Then tizio.Loggato = True
    End Sub

    Private Sub Riconnessione()
        Connesso = False
        Loggato = False
    End Sub

    Private Async Sub Riconnesso()
        Dim imgProfilo = Avatar()
        If Not String.IsNullOrEmpty(_nomeUtente) Then Await chatService.AccessoAsincr(_nomeUtente, imgProfilo)
        Connesso = True
        Loggato = True
    End Sub

    Private Async Sub Disconnesso()
        Dim taskConnessione = chatService.ConnessioneAsync()
        Await taskConnessione.ContinueWith(Sub(task)
                                               If Not task.IsFaulted Then
                                                   Connesso = True
                                                   chatService.AccessoAsincr(_nomeUtente, Avatar()).Wait()
                                                   Loggato = True
                                               End If
                                           End Sub)
    End Sub

    Private Sub PartecipanteStaScrivendo(ByVal nome As String)
        Dim tizio = Partecipanti.Where(Function(p) String.Equals(p.Nome, nome)).FirstOrDefault()
        If tizio IsNot Nothing AndAlso Not tizio.StaScrivendo Then
            tizio.StaScrivendo = True
            Observable.Timer(TimeSpan.FromMilliseconds(1500)).Subscribe(Sub(t) tizio.StaScrivendo = False)
        End If
    End Sub
#End Region

    Private Function Avatar() As Byte()
        Dim imgProfilo As Byte() = Nothing
        If Not String.IsNullOrEmpty(_imgProfilo) Then imgProfilo = File.ReadAllBytes(_imgProfilo)
        Return imgProfilo
    End Function

    Public Sub New(chatSvc As IChatService, diagSvc As IDialogService)
        dialogService = diagSvc
        chatService = chatSvc
        AddHandler chatSvc.NuovoMsgTesto, AddressOf NuovoMsgTesto
        AddHandler chatSvc.NuovoMsgImmagine, AddressOf NuovoMsgImmagine
        AddHandler chatSvc.PartecipanteHaFattoAccesso, AddressOf LoginPartecipante
        AddHandler chatSvc.PartecipanteSiEDisconnesso, AddressOf DisconnessionePartecipante
        AddHandler chatSvc.PartecipanteDisconnesso, AddressOf DisconnessionePartecipante
        AddHandler chatSvc.PartecipanteRiconnesso, AddressOf RiconnessionePartecipante
        AddHandler chatSvc.PartecipanteStaScrivendo, AddressOf PartecipanteStaScrivendo
        AddHandler chatSvc.ConnessioneRiconnessione, AddressOf Riconnessione
        AddHandler chatSvc.ConnessioneRistabilita, AddressOf Riconnesso
        AddHandler chatSvc.ConnessioneChiusa, AddressOf Disconnesso

        ctxTaskFactory = New TaskFactory(TaskScheduler.FromCurrentSynchronizationContext)
    End Sub
End Class