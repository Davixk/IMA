Public Interface IClient
    Sub DisconnessionePartecipante(nome As String)
    Sub RiconnessionePartecipante(nome As String)
    Sub LoginPartecipante(client As User)
    Sub LogoutPartecipante(nome As String)
    Sub MsgTestoBroadcast(mittente As String, testo As String)
    Sub MsgImmagineBroadcast(mittente As String, ByVal img As Byte())
    Sub MsgTestoUnicast(mittente As String, testo As String)
    Sub MsgImmagineUnicast(mittente As String, ByVal img As Byte())
    Sub PartecipanteStaScrivendo(mittente As String)
End Interface
