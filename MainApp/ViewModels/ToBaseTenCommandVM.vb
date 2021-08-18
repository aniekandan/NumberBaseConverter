Public Class ToBaseTenCommandVM
    Inherits CommandVM

    Private _fromBaseArg As CommandArgumentVM
    Private _digitStringArg As CommandArgumentVM

    Public Sub New()
        MyBase.New()

        With Me
            Dim model As New ToBaseTenCommandModel()
            ._model = model

            Dim errorStateChangedHandler = Sub(errorState As Boolean)
                                               Me._inErrorState = errorState

                                           End Sub

            Dim isBaseInputValid = Function(base As UInteger, range As Range) As Boolean
                                       Dim numericValue As Integer = UInteger.Parse(base)

                                       If range.Min <= numericValue And numericValue <= range.Max Then
                                           Return True

                                       Else
                                           Return False

                                       End If

                                   End Function

            Dim rangeValidationArgument As New PrimitiveArgumentVM(New Range(min:=2, max:=9))

            ._fromBaseArg = New CommandArgumentVM(model:=model, modelProperty:="FromBase", label:="",
                                                     errorMsg:="Please Enter a value in the range 2 to 9",
                                                     errorStateChangedHandler:=errorStateChangedHandler,
                                                     isValid:=isBaseInputValid, validationArg:=rangeValidationArgument)


            'AddHandler ._fromBaseArg.ErrorStateChanged, errorStateChangedHandler

            Dim isDigitInputValid = Function(digitString As String, base As UInteger) As Boolean
                                        ' check if digitString is valid for base base
                                        Dim retVal As Boolean = True

                                        For Each digit In digitString
                                            If UInteger.Parse(digit) >= base Then
                                                retVal = False
                                                Exit For

                                            End If
                                        Next

                                        Return retVal

                                    End Function

            ._digitStringArg = New CommandArgumentVM(model:=model, modelProperty:="DigitString", propAsType:=GetType(UInteger), label:="",
                                                errorMsg:="Please Enter a non-negative whole number with digits less than the base",
                                                isValid:=isDigitInputValid, validationArg:= ._fromBaseArg,
                                                errorStateChangedHandler:=errorStateChangedHandler)

            'AddHandler ._digitStringArg.ErrorStateChanged, errorStateChangedHandler

        End With

    End Sub

    Public ReadOnly Property FromBase As CommandArgumentVM
        Get
            Return Me._fromBaseArg

        End Get

    End Property

    Public ReadOnly Property DigitString As CommandArgumentVM
        Get
            Return Me._digitStringArg

        End Get

    End Property

End Class
