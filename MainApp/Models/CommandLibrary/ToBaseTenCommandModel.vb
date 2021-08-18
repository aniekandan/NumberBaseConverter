Public Class ToBaseTenCommandModel
    Inherits CommandModel

    Private _fromBase As UInteger
    Private _digitString As String

    Public Sub New()
        MyBase.New()

        With Me
            ._fromBase = 2
            ._digitString = "0"

        End With

    End Sub

    Public Overrides ReadOnly Property StringRepresentation As String
        Get
            Return "ToBaseTenCommandModel"

        End Get
    End Property

    Public Overrides Function Compute() As Object
        Return ConvertToBaseTen(digitString:=Me._digitString, fromBase:=Me._fromBase)

    End Function

    Public Property FromBase As UInteger
        Get
            Return Me._fromBase

        End Get
        Set(value As UInteger)
            ' validate the base here
            Me._fromBase = value

        End Set
    End Property

    Public Property DigitString As String
        Get
            Return Me._digitString

        End Get
        Set(value As String)
            ' validate the digit string here
            Me._digitString = value

        End Set
    End Property

    ''' <summary>
    ''' Converts the number given as a digit string in the source base to base ten
    ''' </summary>
    ''' <param name="digitString">The number to convert to base ten</param>
    ''' <param name="fromBase">The base the number is in. Should be from two to nine</param>
    ''' <returns></returns>
    Private Function ConvertToBaseTen(digitString As String, fromBase As UInteger) As UInteger
        ' Check if the base is 1 < toBase < 10
        ' and also if digitString is valid

        Dim isValidBaseString = Function(dStr As String, b As UInteger) As Boolean
                                    ' check if dStr is valid for base b
                                    Dim retVal As Boolean = True

                                    For Each d In dStr
                                        If UInteger.Parse(d) >= b Then
                                            retVal = False
                                            Exit For

                                        End If
                                    Next

                                    Return retVal

                                End Function

        If 1 < fromBase And fromBase < 10 Then
            If isValidBaseString(digitString, fromBase) Then
                Dim num As UInteger = 0
                Dim revDigitString As String = Strings.StrReverse(digitString)

                For i = 0 To revDigitString.Length() - 1
                    num += UInteger.Parse(revDigitString(i)) * fromBase ^ i

                Next i

                Return num

            Else
                Throw New ArgumentException("CalculatorModel.ConvertToBaseTen: digitString """ + digitString + """ has an invalid format for numbers in base" _
                                                  + fromBase.ToString() + vbCrLf _
                                                  + "All digits should lie in the range 0 to (fromBase - 1)")

            End If

        Else
            Throw New ArgumentOutOfRangeException("CalculatorModel.ConvertToBaseTen: frombase is out of range = " _
                                                  + fromBase.ToString() + "." + vbCrLf _
                                                  + "Should lie in the range  1 < fromBase < 10")

        End If

    End Function

End Class
