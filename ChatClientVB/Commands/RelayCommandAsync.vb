Public Class RelayCommandAsync
    Implements ICommand

    Private ReadOnly _esegui As Func(Of Task)
    Private ReadOnly _puoEseguire As Predicate(Of Object)
    Private inEsecuzione As Boolean

    Public Sub New(esegui As Func(Of Task))
        Me.New(esegui, Nothing)
    End Sub

    Public Sub New(esegui As Func(Of Task), eseguibile As Predicate(Of Object))
        _esegui = esegui
        _puoEseguire = eseguibile
    End Sub

    Public Function CanExecute(parametro As Object) As Boolean Implements ICommand.CanExecute
        If Not inEsecuzione AndAlso _puoEseguire Is Nothing Then Return True
        Return Not inEsecuzione AndAlso _puoEseguire(parametro)
    End Function

    Public Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
        AddHandler(value As EventHandler)
            AddHandler CommandManager.RequerySuggested, value
        End AddHandler

        RemoveHandler(value As EventHandler)
            RemoveHandler CommandManager.RequerySuggested, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As EventArgs)
        End RaiseEvent
    End Event

    Public Async Sub Execute(parametro As Object) Implements ICommand.Execute
        inEsecuzione = True
        Try
            Await _esegui()
        Finally
            inEsecuzione = False
        End Try
    End Sub
End Class
