Imports System.ComponentModel

Public MustInherit Class NotifierBase
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler _
        Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))

    End Sub

End Class
