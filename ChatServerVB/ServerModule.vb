Imports Microsoft.AspNet.SignalR
Imports Microsoft.Owin.Cors
Imports Microsoft.Owin.Hosting
Imports Owin

Module ServerModule

    Sub Main()
        Dim url = "http://localhost:8080/"
        Using WebApp.Start(Of Avvio)(url)
            Console.WriteLine($"Server inizializzato all'indirizzo {url}")
            Console.ReadLine()
        End Using
    End Sub

End Module

Public Class Avvio
    Public Sub Configuration(app As IAppBuilder)
        app.UseCors(CorsOptions.AllowAll)
        app.MapSignalR("/InstantMessagingApp", New HubConfiguration())

        GlobalHost.Configuration.MaxIncomingWebSocketMessageSize = Nothing
    End Sub
End Class