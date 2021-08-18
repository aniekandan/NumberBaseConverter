Imports MainApp

Public Class PrimitiveArgumentVM
    Implements IArgumentVM

    Private _value As Object

    Public Sub New(value As Object)
        Me._value = value

    End Sub

    Public ReadOnly Property Value As Object Implements IArgumentVM.Value
        Get
            Return Me._value

        End Get
    End Property
End Class
