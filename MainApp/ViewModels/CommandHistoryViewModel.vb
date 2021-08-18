Public Class CommandHistoryViewModel
    Inherits NotifierBase

    Private _commandVMList As RangeObservableCollection(Of CommandVM)
    Private _newCommandViewModel As CommandVM
    Public Event NewCommandViewModelChanged(newItem As CommandVM)

    Public Sub New()
        With Me
            ._commandVMList = New RangeObservableCollection(Of CommandVM)

        End With
    End Sub

    Public ReadOnly Property CommandViewModels As RangeObservableCollection(Of CommandVM)
        Get
            Return Me._commandVMList

        End Get
    End Property

    Public Sub AddNewCommand(newCmd As CommandVM)
        AddHandler newCmd.RemoveRequest,
                                Sub(itemToRemove As CommandVM)
                                    Me._commandVMList.Remove(itemToRemove)

                                End Sub

        AddHandler newCmd.IsEditModeChanged,
                                Sub(item As CommandVM)
                                    ' reset the new command to null
                                    ' if the edit mode is false
                                    ' needed since i need to disable the buttons
                                    If Not item.IsEditMode Then
                                        Me.NewCommandViewModel = Nothing

                                    Else
                                        Me.NewCommandViewModel = item

                                    End If

                                End Sub

        Me._commandVMList.Add(newCmd)

        Me.NewCommandViewModel = newCmd

    End Sub

    Public Property NewCommandViewModel As CommandVM
        Get
            Return Me._newCommandViewModel

        End Get
        Private Set(value As CommandVM)
            Me._newCommandViewModel = value

            OnPropertyChanged("NewCommandViewModel")

            ' also notify the mainUI of this change
            RaiseEvent NewCommandViewModelChanged(value)

        End Set
    End Property

End Class
