Public MustInherit Class CommandModel
    Public MustOverride Function Compute() As Object
    Public MustOverride ReadOnly Property StringRepresentation As String

End Class
