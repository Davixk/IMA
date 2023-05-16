Public Interface IChatService
    Event PartecipanteHaFattoAccesso(ByVal partecipante As Utente)
    Event PartecipanteSiEDisconnesso(ByVal nome As String)
    Event PartecipanteDisconnesso(ByVal nome As String)
    Event PartecipanteRiconnesso(ByVal nome As String)
    Event ConnessioneRiconnessione()
    Event ConnessioneRistabilita()
    Event ConnessioneChiusa()
    Event NuovoMsgTesto(ByVal mittente As String, ByVal testo As String, ByVal tipoMsg As TipoDiMessaggio)
    Event NuovoMsgImmagine(ByVal mittente As String, ByVal img As Byte(), ByVal tipoMsg As TipoDiMessaggio)
    Event PartecipanteStaScrivendo(ByVal nome As String)

    Function ConnessioneAsync() As Task
    Function AccessoAsincr(nome As String, imgProfilo As Byte()) As Task(Of List(Of Utente))
    Function DisconnessioneAsincr() As Task

    Function MandaMsgBroadcastAsincr(testo As String) As Task
    Function MandaMsgBroadcastAsincr(img As Byte()) As Task
    Function MandaMsgUnicastAsincr(ByVal destinatario As String, ByVal testo As String) As Task
    Function MandaMsgUnicastAsincr(ByVal destinatario As String, ByVal img As Byte()) As Task
    Function StaScrivendoAsincr(destinatario As String) As Task
End Interface