Imports System.Collections.ObjectModel

Public Class Partecipante
    Inherits ViewModelBase

    Public Property Nome As String
    Public Property imgProfilo As Byte()
    Public Property Chatter As New ObservableCollection(Of MessaggioChat)

    Private _loggato As Boolean = True
    Public Property Loggato As Boolean
        Get
            Return _loggato
        End Get
        Set(value As Boolean)
            _loggato = value
            OnPropertyChanged()
        End Set
    End Property

    Private _haMandatoNuovoMsg As Boolean
    Public Property HaMandatoNuovoMsg As Boolean
        Get
            Return _haMandatoNuovoMsg
        End Get
        Set(value As Boolean)
            _haMandatoNuovoMsg = value
            OnPropertyChanged()
        End Set
    End Property

    Private _staScrivendo As Boolean
    Public Property StaScrivendo As Boolean
        Get
            Return _staScrivendo
        End Get
        Set(value As Boolean)
            _staScrivendo = value
            OnPropertyChanged()
        End Set
    End Property
End Class
