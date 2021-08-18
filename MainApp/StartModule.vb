Module StartModule
    Public Sub Main()
        Dim mainVM As New MainViewModel()
        Dim mainVw As New MainWindow() With {.DataContext = mainVM}

        mainVw.ShowDialog()

    End Sub

End Module
