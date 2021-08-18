Class MainWindow
    Private Sub scVwrParent_ScrollChanged(sender As Object, e As ScrollChangedEventArgs) Handles scVwrParent.ScrollChanged
        If e.ExtentHeightChange > 0 Then
            scVwrParent.ScrollToEnd()

        End If

    End Sub
End Class
