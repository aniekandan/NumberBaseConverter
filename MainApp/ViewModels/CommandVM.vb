Public MustInherit Class CommandVM
    Inherits NotifierBase

    Protected _isEditMode As Boolean
    Protected _inErrorState As Boolean

    Private _acceptCommand As RelayCommand
    Private _cancelCommand As RelayCommand

    Public Event RemoveRequest(itemToRemove As CommandVM)
    Public Event IsEditModeChanged(item As CommandVM)

    Private _result As String
    Protected _model As CommandModel

    Public Sub New()
        With Me
            ' all commands start in edit mode
            ._isEditMode = True
            ._inErrorState = False

            ._acceptCommand = New RelayCommand(
                                        Sub(commandToCompute As CommandVM)
                                            ' activates the calculator to start calculating
                                            .Result = commandToCompute._model.Compute().ToString()

                                            ' disable edit mode
                                            .IsEditMode = False

                                        End Sub,
                                        New Predicate(Of Object)(Function(commandToCompute As CommandVM) As Boolean
                                                                     If commandToCompute IsNot Nothing Then
                                                                         If commandToCompute._inErrorState Then
                                                                             Return False

                                                                         Else
                                                                             Return True

                                                                         End If

                                                                     Else
                                                                         Return False

                                                                     End If

                                                                 End Function))

            ._cancelCommand = New RelayCommand(Sub(commandVMToRemove As CommandVM)
                                                   ' disable edit mode
                                                   .IsEditMode = False

                                                   ' remove command from command history
                                                   RaiseEvent RemoveRequest(commandVMToRemove)

                                               End Sub)

        End With
    End Sub

    Public Property IsEditMode As Boolean
        Get
            Return Me._isEditMode

        End Get
        Private Set(value As Boolean)
            Me._isEditMode = value

            ' notify
            OnPropertyChanged("IsEditMode")
            RaiseEvent IsEditModeChanged(item:=Me)

        End Set
    End Property

    Public ReadOnly Property AcceptCommand As RelayCommand
        Get
            Return Me._acceptCommand

        End Get
    End Property

    Public ReadOnly Property CancelCommand As RelayCommand
        Get
            Return Me._cancelCommand

        End Get
    End Property

    Public Property Result As String
        Get
            Return Me._result

        End Get
        Private Set(value As String)
            Me._result = value

            ' notify
            OnPropertyChanged("Result")

        End Set
    End Property

End Class
