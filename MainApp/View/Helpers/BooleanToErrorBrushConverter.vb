Imports System.Globalization

Public Class BooleanToErrorBrushConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        Dim inErrorState As Boolean = value

        If inErrorState Then
            Return New SolidColorBrush(Colors.Red)

        Else
            Return Nothing

        End If

    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class
