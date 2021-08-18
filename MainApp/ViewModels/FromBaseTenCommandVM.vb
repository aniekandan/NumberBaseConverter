Public Class FromBaseTenCommandVM
    Inherits CommandVM

    Private _numberArg As CommandArgumentVM
    Private _toBaseArg As CommandArgumentVM

    Public Sub New()
        MyBase.New()

        With Me
            Dim model As New FromBaseTenCommandModel()
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

            ._toBaseArg = New CommandArgumentVM(model:=model, modelProperty:="ToBase", label:="",
                                                     errorMsg:="Please Enter a value in the range 2 to 9",
                                                     errorStateChangedHandler:=errorStateChangedHandler,
                                                     isValid:=isBaseInputValid, validationArg:=rangeValidationArgument)

            'AddHandler ._toBaseArg.ErrorStateChanged, errorStateChangedHandler

            ._numberArg = New CommandArgumentVM(model:=model, modelProperty:="Number", label:="",
                                               errorMsg:="Please Enter a non-negative whole number",
                                               errorStateChangedHandler:=errorStateChangedHandler)

            'AddHandler ._numberArg.ErrorStateChanged, errorStateChangedHandler

        End With

    End Sub

    Public ReadOnly Property ToBase As CommandArgumentVM
        Get
            Return Me._toBaseArg

        End Get
    End Property

    Public ReadOnly Property Number As CommandArgumentVM
        Get
            Return Me._numberArg

        End Get
    End Property

End Class