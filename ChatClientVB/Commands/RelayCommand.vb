Public Class RelayCommand
    Implements ICommand

    Private ReadOnly _esegui As Action(Of Object)
    Private ReadOnly _eseguibile As Predicate(Of Object)

    Public Sub New(ByVal execute As Action(Of Object))
        Me.New(execute, Nothing)
    End Sub

    Public Sub New(ByVal esegui As Action(Of Object), ByVal eseguibile As Predicate(Of Object))
        If esegui Is Nothing Then Throw New ArgumentNullException("esegui")
        _esegui = esegui
        _eseguibile = eseguibile
    End Sub

    Public Function CanExecute(parametro As Object) As Boolean Implements ICommand.CanExecute
        Return If(_eseguibile Is Nothing, True, _eseguibile(parametro))
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

    Public Sub Execute(parametro As Object) Implements ICommand.Execute
        _esegui(parametro)
    End Sub
End Class

Public Class RelayCommand(Of T)
    Implements ICommand

    Private ReadOnly _esegui As Action(Of T)
    Private ReadOnly _eseguibile As Predicate(Of T)

    Public Sub New(ByVal esegui As Action(Of T))
        Me.New(esegui, Nothing)
    End Sub

    Public Sub New(ByVal esegui As Action(Of T), ByVal eseguibile As Predicate(Of T))
        If esegui Is Nothing Then Throw New ArgumentNullException("esegui")
        _esegui = esegui
        _eseguibile = eseguibile
    End Sub

    <DebuggerStepThrough()>
    Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
        Return If(_eseguibile Is Nothing, True, _eseguibile(CType(parameter, T)))
    End Function

    Public Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
        AddHandler(ByVal value As EventHandler)
            AddHandler CommandManager.RequerySuggested, value
        End AddHandler

        RemoveHandler(ByVal value As EventHandler)
            RemoveHandler CommandManager.RequerySuggested, value
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
        End RaiseEvent
    End Event

    Public Sub Execute(ByVal parametro As Object) Implements ICommand.Execute
        _esegui(CType(parametro, T))
    End Sub
End Class