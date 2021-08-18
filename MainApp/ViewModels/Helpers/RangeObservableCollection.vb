Imports System.Collections.ObjectModel
Imports System.Collections.Specialized

Public Class RangeObservableCollection(Of T)
    Inherits ObservableCollection(Of T)

    Private _suppressNotifications As Boolean = False

    Protected Overrides Sub OnCollectionChanged(e As Specialized.NotifyCollectionChangedEventArgs)
        If Not _suppressNotifications Then
            MyBase.OnCollectionChanged(e)

        End If

    End Sub

    Public Sub AddRange(list As IEnumerable(Of T))
        If list Is Nothing Then
            Throw New ArgumentNullException("list in AddRange of RangeObservableCollection cannot be nothing")

        Else
            _suppressNotifications = True

            For Each Item As T In list
                Me.Add(Item)

            Next

            _suppressNotifications = False

            OnCollectionChanged(New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset))

        End If

    End Sub

End Class
