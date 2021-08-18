Public Class MainViewModel
    Inherits NotifierBase

    Private _commandHistory As CommandHistoryViewModel

    Private _toBaseTenCommand As RelayCommand
    Private _fromBaseTenCommand As RelayCommand

    Private _isEditMode As Boolean

    Public Sub New()
        With Me
            ._commandHistory = New CommandHistoryViewModel()
            .IsEditMode = False

            AddHandler ._commandHistory.NewCommandViewModelChanged,
                                                    Sub(newItem As CommandVM)
                                                        '' notify
                                                        'OnPropertyChanged("IsEditMode")
                                                        Me.IsEditMode = If(Me._commandHistory.NewCommandViewModel Is Nothing, False, True)

                                                    End Sub

            ._toBaseTenCommand = New RelayCommand(execute:=Sub(dontCareParam As Boolean)
                                                               ' create a new To Base Ten Command and add to
                                                               ' the command history
                                                               Me._commandHistory.AddNewCommand(New ToBaseTenCommandVM())

                                                           End Sub,
                                                  canExecute:=New Predicate(Of Object)(
                                                            Function(commandVMIsEditMode As Boolean) As Boolean
                                                                If commandVMIsEditMode Then
                                                                    Return False

                                                                Else
                                                                    Return True

                                                                End If
                                                            End Function))

            ._fromBaseTenCommand = New RelayCommand(execute:=Sub(dontCareParam As Boolean)
                                                                 ' create a new From Base Ten Command and add to
                                                                 ' the command history
                                                                 Me._commandHistory.AddNewCommand(New FromBaseTenCommandVM())

                                                             End Sub,
                                                 canExecute:=New Predicate(Of Object)(
                                                            Function(commandVMIsEditMode As Boolean) As Boolean
                                                                If commandVMIsEditMode Then
                                                                    Return False

                                                                Else
                                                                    Return True

                                                                End If
                                                            End Function))

        End With
    End Sub

    Public ReadOnly Property CommandHistory As CommandHistoryViewModel
        Get
            Return Me._commandHistory

        End Get
    End Property

    Public ReadOnly Property ToBaseTenCommand As RelayCommand
        Get
            Return Me._toBaseTenCommand

        End Get
    End Property

    Public ReadOnly Property FromBaseTenCommand As RelayCommand
        Get
            Return Me._fromBaseTenCommand

        End Get
    End Property

    Public Property IsEditMode As Boolean
        Get
            'Return If(Me._commandHistory.NewCommandViewModel Is Nothing, False, True)
            Return Me._isEditMode

        End Get
        Set(value As Boolean)
            Me._isEditMode = value

            ' notify
            OnPropertyChanged("IsEditMode")

        End Set
    End Property

End Class
