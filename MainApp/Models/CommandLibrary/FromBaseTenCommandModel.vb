Public Class FromBaseTenCommandModel
    Inherits CommandModel

    Private _toBase As UInteger
    Private _number As UInteger

    Public Sub New()
        MyBase.New()

        With Me
            ._toBase = 2
            ._number = 0

        End With

    End Sub

    Public Overrides ReadOnly Property StringRepresentation As String
        Get
            Return "FromBaseTenCommandModel"

        End Get
    End Property

    Public Overrides Function Compute() As Object
        Return ConvertFromBaseTen(n:=Me._number, toBase:=Me._toBase)

    End Function

    Public Property ToBase As UInteger
        Get
            Return Me._toBase

        End Get
        Set(value As UInteger)
            ' validate the base here
            Me._toBase = value

        End Set
    End Property

    Public Property Number As UInteger
        Get
            Return Me._number

        End Get
        Set(value As UInteger)
            Me._number = value

        End Set
    End Property

    ''' <summary>
    ''' Converts the number given in base ten to the target base
    ''' </summary>
    ''' <param name="n">The number to convert</param>
    ''' <param name="toBase">The target base. Should be from two to nine</param>
    ''' <returns></returns>
    Private Function ConvertFromBaseTen(n As UInteger, toBase As UInteger) As String
        ' Check if the base is 1 < toBase < 10
        If 1 < toBase And toBase < 10 Then
            Dim qOld As UInteger, remdr As UInteger
            Dim qNew As UInteger = n

            Dim digitString As String = ""

            Do Until qNew = 0
                qOld = qNew                 ' save old quotient

                qNew = qOld \ toBase        ' get new quotient
                remdr = qOld Mod toBase     ' get new remainder

                ' update the digitString
                digitString = Strings.Trim(remdr.ToString()) + digitString

            Loop

            If digitString = "" Then
                Return "0"

            Else
                Return digitString

            End If

        Else
            Throw New ArgumentOutOfRangeException("CalculatorModel.ConvertFromBaseTen: tobase is out of range = " _
                                                  + toBase.ToString() + vbCrLf _
                                                  + "Should lie in the range  2 <= toBase <= 9")

        End If

    End Function

End Class
