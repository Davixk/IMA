Imports System.Net
Imports ChatClientVB
Imports Microsoft.AspNet.SignalR.Client

Public Class ChatService
    Implements IChatService

    Public Event PartecipanteDisconnesso(name As String) Implements IChatService.PartecipanteDisconnesso
    Public Event PartecipanteHaFattoAccesso(participant As Utente) Implements IChatService.PartecipanteHaFattoAccesso
    Public Event PartecipanteSiEDisconnesso(name As String) Implements IChatService.PartecipanteSiEDisconnesso
    Public Event PartecipanteRiconnesso(name As String) Implements IChatService.PartecipanteRiconnesso
    Public Event ConnessioneRiconnessione() Implements IChatService.ConnessioneRiconnessione
    Public Event ConnessioneRistabilita() Implements IChatService.ConnessioneRistabilita
    Public Event ConnessioneChiusa() Implements IChatService.ConnessioneChiusa
    Public Event NuovoMsgTesto(sender As String, msg As String, mt As TipoDiMessaggio) Implements IChatService.NuovoMsgTesto
    Public Event NuovoMsgImmagine(sender As String, img As Byte(), mt As TipoDiMessaggio) Implements IChatService.NuovoMsgImmagine
    Public Event PartecipanteStaScrivendo(name As String) Implements IChatService.PartecipanteStaScrivendo

    Private hubProxy As IHubProxy
    Private connessione As HubConnection
    Private url As String = "http://localhost:8080/InstantMessagingApp"

    Public Async Function ConnessioneAsync() As Task Implements IChatService.ConnessioneAsync
        connessione = New HubConnection(url)
        hubProxy = connessione.CreateHubProxy("ChatHub")
        hubProxy.On(Of Utente)("LoginPartecipante", Sub(u) RaiseEvent PartecipanteHaFattoAccesso(u))
        hubProxy.On(Of String)("LogoutPartecipante", Sub(n) RaiseEvent PartecipanteSiEDisconnesso(n))
        hubProxy.On(Of String)("DisconnessionePartecipante", Sub(n) RaiseEvent PartecipanteDisconnesso(n))
        hubProxy.On(Of String)("RiconnessionePartecipante", Sub(n) RaiseEvent PartecipanteRiconnesso(n))
        hubProxy.On(Of String, String)("MsgTestoBroadcast", Sub(n, m) RaiseEvent NuovoMsgTesto(n, m, TipoDiMessaggio.Broadcast))
        hubProxy.On(Of String, Byte())("MsgImmagineBroadcast", Sub(n, m) RaiseEvent NuovoMsgImmagine(n, m, TipoDiMessaggio.Broadcast))
        hubProxy.On(Of String, String)("MsgTestoUnicast", Sub(n, m) RaiseEvent NuovoMsgTesto(n, m, TipoDiMessaggio.Unicast))
        hubProxy.On(Of String, Byte())("MsgImmagineUnicast", Sub(n, m) RaiseEvent NuovoMsgImmagine(n, m, TipoDiMessaggio.Unicast))
        hubProxy.On(Of String)("PartecipanteStaScrivendo", Sub(p) RaiseEvent PartecipanteStaScrivendo(p))

        AddHandler connessione.Reconnecting, AddressOf Riconnessione
        AddHandler connessione.Reconnected, AddressOf Riconnesso
        AddHandler connessione.Closed, AddressOf Disconnesso

        ServicePointManager.DefaultConnectionLimit = 10
        Await connessione.Start()
    End Function

    Private Sub Disconnesso()
        RaiseEvent ConnessioneChiusa()
    End Sub

    Private Sub Riconnessione()
        RaiseEvent ConnessioneRiconnessione()
    End Sub

    Private Sub Riconnesso()
        RaiseEvent ConnessioneRistabilita()
    End Sub

    Public Async Function AccessoAsincr(nome As String, immagineProf As Byte()) As Task(Of List(Of Utente)) Implements IChatService.AccessoAsincr
        Dim utenti = Await hubProxy.Invoke(Of List(Of Utente))("Accesso", New Object() {nome, immagineProf})
        Return utenti
    End Function

    Public Async Function DisconnessioneAsincr() As Task Implements IChatService.DisconnessioneAsincr
        Await hubProxy.Invoke("Logout")
    End Function

    Public Async Function MandaMsgBroadcastAsincr(msg As String) As Task Implements IChatService.MandaMsgBroadcastAsincr
        Await hubProxy.Invoke("MsgTestoBroadcast", msg)
    End Function

    Public Async Function MandaMsgBroadcastAsincr(img As Byte()) As Task Implements IChatService.MandaMsgBroadcastAsincr
        Await hubProxy.Invoke("MsgImmagineBroadcast", img)
    End Function

    Public Async Function MandaMsgUnicastAsincr(destinatario As String, msg As String) As Task Implements IChatService.MandaMsgUnicastAsincr
        Await hubProxy.Invoke("MsgTestoUnicast", New Object() {destinatario, msg})
    End Function

    Public Async Function MandaMsgUnicastAsincr(destinatario As String, img As Byte()) As Task Implements IChatService.MandaMsgUnicastAsincr
        Await hubProxy.Invoke("MsgImmagineUnicast", New Object() {destinatario, img})
    End Function

    Public Async Function StaScrivendoAsincr(destinatario As String) As Task Implements IChatService.StaScrivendoAsincr
        Await hubProxy.Invoke("StaScrivendo", destinatario)
    End Function
End Class
