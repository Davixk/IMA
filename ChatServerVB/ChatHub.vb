Imports System.Collections.Concurrent
Imports Microsoft.AspNet.SignalR

Public Class ChatHub
    Inherits Hub(Of IClient)

    Private Shared ChatClients As New ConcurrentDictionary(Of String, User)

    Public Overrides Function OnDisconnected(stopCalled As Boolean) As Task
        Dim nomeUtente = ChatClients.SingleOrDefault(Function(c) c.Value.ID = Context.ConnectionId).Key
        If nomeUtente IsNot Nothing Then
            Clients.Others.DisconnessionePartecipante(nomeUtente)
            Console.WriteLine($"<> {nomeUtente} ha chiuso il client")
        End If
        Return MyBase.OnDisconnected(stopCalled)
    End Function

    Public Overrides Function OnReconnected() As Task
        Dim nomeUtente = ChatClients.SingleOrDefault(Function(c) c.Value.ID = Context.ConnectionId).Key
        If nomeUtente IsNot Nothing Then
            Clients.Others.RiconnessionePartecipante(nomeUtente)
            Console.WriteLine($"== {nomeUtente} riconnesso")
        End If
        Return MyBase.OnReconnected()
    End Function

    Public Function Accesso(ByVal nome As String, ByVal foto As Byte()) As List(Of User)
        If Not ChatClients.ContainsKey(nome) Then
            Console.WriteLine($"++ {nome} accesso effettuato")
            Dim users As New List(Of User)(ChatClients.Values)
            Dim nuovoUtente As New User With {.Nome = nome, .ID = Context.ConnectionId, .imgProfilo = foto}
            Dim added = ChatClients.TryAdd(nome, nuovoUtente)
            If Not added Then Return Nothing
            Clients.CallerState.UserName = nome
            Clients.Others.LoginPartecipante(nuovoUtente)
            Return users
        End If
        Return Nothing
    End Function

    Public Sub Logout()
        Dim nome = Clients.CallerState.UserName
        If Not String.IsNullOrEmpty(nome) Then
            Dim client As New User
            ChatClients.TryRemove(nome, client)
            Clients.Others.LogoutPartecipante(nome)
            Console.WriteLine($"-- {nome} si è disconnesso")
        End If
    End Sub

    Public Sub MsgTestoBroadcast(messaggio As String)
        Dim nome = Clients.CallerState.UserName
        If Not String.IsNullOrEmpty(nome) AndAlso Not String.IsNullOrEmpty(messaggio) Then
            Clients.Others.MsgTestoBroadcast(nome, messaggio)
        End If
    End Sub

    Public Sub MsgImmagineBroadcast(img As Byte())
        Dim nome = Clients.CallerState.UserName
        If img IsNot Nothing Then
            Clients.Others.MsgImmagineBroadcast(nome, img)
        End If
    End Sub

    Public Sub MsgTestoUnicast(destinatario As String, messaggio As String)
        Dim mittente = Clients.CallerState.UserName
        If Not String.IsNullOrEmpty(mittente) AndAlso destinatario <> mittente AndAlso
           Not String.IsNullOrEmpty(messaggio) AndAlso ChatClients.Keys.Contains(destinatario) Then
            Dim client As New User
            ChatClients.TryGetValue(destinatario, client)
            Clients.Client(client.ID).MsgTestoUnicast(mittente, messaggio)
        End If
    End Sub

    Public Sub MsgImmagineUnicast(destinatario As String, img As Byte())
        Dim mittente = Clients.CallerState.UserName
        If Not String.IsNullOrEmpty(mittente) AndAlso destinatario <> mittente AndAlso
           img IsNot Nothing AndAlso ChatClients.Keys.Contains(destinatario) Then
            Dim client As New User
            ChatClients.TryGetValue(destinatario, client)
            Clients.Client(client.ID).MsgImmagineUnicast(mittente, img)
        End If
    End Sub

    Public Sub StaScrivendo(destinatario As String)
        If String.IsNullOrEmpty(destinatario) Then Exit Sub
        Dim mittente = Clients.CallerState.UserName
        Dim client As New User
        ChatClients.TryGetValue(destinatario, client)
        Clients.Client(client.ID).PartecipanteStaScrivendo(mittente)
    End Sub
End Class