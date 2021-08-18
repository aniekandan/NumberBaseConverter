Public Class FiguresToWords
    '  This Structure stores the names Of Single digit numbers
    Private Shared _SingleDigitNames As SortedList(Of Char, String)

    ' This structure stores the names of two digit numbers
    ' ending with zero (from 10 to 90) and the "Teens"
    Private Shared _TensAndTeens As SortedList(Of String, String)

    Shared Sub New()
        _SingleDigitNames = New SortedList(Of Char, String)
        _TensAndTeens = New SortedList(Of String, String)

        With _SingleDigitNames
            .Add("0"c, "Zero")
            .Add("1"c, "One")
            .Add("2"c, "Two")
            .Add("3"c, "Three")
            .Add("4"c, "Four")
            .Add("5"c, "Five")
            .Add("6"c, "Six")
            .Add("7"c, "Seven")
            .Add("8"c, "Eight")
            .Add("9"c, "Nine")

        End With

        With _TensAndTeens
            .Add("10", "Ten")
            .Add("11", "Eleven")
            .Add("12", "Twelve")
            .Add("13", "Thirteen")
            .Add("14", "Fourteen")
            .Add("15", "Fifteen")
            .Add("16", "Sixteen")
            .Add("17", "Seventeen")
            .Add("18", "Eighteen")
            .Add("19", "Nineteen")
            .Add("20", "Twenty")
            .Add("30", "Thirty")
            .Add("40", "Forty")
            .Add("50", "Fifty")
            .Add("60", "Sixty")
            .Add("70", "Seventy")
            .Add("80", "Eighty")
            .Add("90", "Ninety")

        End With

    End Sub

    ''' <summary>
    ''' Converts a two digit number to words
    ''' </summary>
    ''' <param name="num">the two digit number as a string</param>
    ''' <returns>A string representing the figure as a word</returns>
    Public Shared Function ConvertTwoDigits(ByVal num As String) As String
        ' Converts the input number 'num' to words
        Try
            ' First parse the input string to confirm 
            ' whether the input is a valid number
            ' Also, any leading zeros will be removed

            Dim numString As String = UInteger.Parse(num).ToString

            ' Check if numString is a one or two digit number
            Select Case numString.Length
                Case 1      ' numString is a one digit number
                    Return _SingleDigitNames(numString)

                Case 2      ' numString is a two digit number
                    Try
                        Return _TensAndTeens(numString)

                    Catch ex As Exception
                        Return _TensAndTeens(numString.Chars(0) + "0") + " " + _SingleDigitNames(numString.Chars(1))

                    End Try

                Case Else
                    Throw New ArgumentException("ConvertTwoDigits expects a one or two digit number. But number of digits is " _
                                                + numString.Length.ToString())

            End Select

        Catch ex As Exception
            Throw New ArgumentException("ConvertTwoDigits expects a one or two digit number. But input number is """ _
                                        + num + """.")

        End Try

    End Function

End Class
