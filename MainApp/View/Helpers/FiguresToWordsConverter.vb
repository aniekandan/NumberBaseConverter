Imports System.Globalization

Public Class FiguresToWordsConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        Try
            Dim base As UInteger = UInteger.Parse(value)

            Return FiguresToWords.ConvertTwoDigits(base.ToString())

        Catch ex As Exception
            Return "#error"

        End Try

    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()

    End Function

End Class
