Public Class RelayCommand
    Implements ICommand

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return If(_canExecute Is Nothing, True, _canExecute(parameter))

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

    Public Sub Execute(parameter As Object) Implements ICommand.Execute
        _execute(parameter)

    End Sub

    Private ReadOnly _execute As Action(Of Object)
    Private ReadOnly _canExecute As Predicate(Of Object)

    Public Sub New(execute As Action(Of Object))
        Me.New(execute, Nothing)

    End Sub

    Public Sub New(execute As Action(Of Object), canExecute As Predicate(Of Object))
        If execute Is Nothing Then Throw New ArgumentNullException("execute")

        _execute = execute
        _canExecute = canExecute

    End Sub

End Class
