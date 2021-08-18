Public Class RangeCommandArgumentVM
    Inherits CommandArgumentVM

    Private _min As Integer
    Private _max As Integer

    Public Sub New(model As CommandModel, modelProperty As String, label As String,
                   errorMsg As String, min As Integer, max As Integer, errorStateChangedHandler As Action(Of Boolean),
                   Optional isValid As Func(Of Object, Object, Boolean) = Nothing,
                   Optional validationArg As IArgumentVM = Nothing)

        MyBase.New(model:=model, modelProperty:=modelProperty,
                   label:=label, errorMsg:=errorMsg, errorStateChangedHandler:=errorStateChangedHandler,
                   isValid:=isValid, validationArg:=validationArg)

        With Me
            If min < max Then
                ._min = min
                ._max = max

            Else
                Throw New ArgumentException("RangeCommandArgumentVM.New: min must be less than max")

            End If

        End With

    End Sub

    Public Overrides Property Text As String
        Get
            Return MyBase.Text()

        End Get
        Set(value As String)
            MyBase.Text = value

        End Set
    End Property

    'Public Overrides Property Value As String
    '    Get
    '        Return MyBase.Value()

    '    End Get
    '    Set(value As String)
    '        ' validate number
    '        Try
    '            Dim numericValue As Integer = Integer.Parse(value)

    '            If _min <= numericValue And numericValue <= _max Then
    '                ' might throw exception
    '                SetModelValue(value)

    '                Me._value = value

    '                ' reset error state
    '                Me._hasError = False

    '                ' notify
    '                OnPropertyChanged("Value")
    '                OnPropertyChanged("HasError")

    '            Else
    '                ' set the errorneous value
    '                Me._errorValue = value

    '                ' set the error state
    '                Me._hasError = True

    '                ' notify
    '                OnPropertyChanged("Value")
    '                OnPropertyChanged("HasError")

    '            End If

    '        Catch ex As Exception
    '            ' set the errorneous value
    '            Me._errorValue = value

    '            ' set the error state
    '            Me._hasError = True

    '            ' notify
    '            OnPropertyChanged("Value")
    '            OnPropertyChanged("HasError")

    '        End Try

    '        OnErrorStateChanged(Me._hasError)

    '    End Set
    'End Property



End Class