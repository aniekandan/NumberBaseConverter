Imports System.Reflection

Public Class CommandArgumentVM
    Inherits NotifierBase
    Implements IArgumentVM

    Protected _label As String

    Protected _hasError As Boolean
    Protected _errorValue As String
    Protected _errorMessage As String

    Protected _modelProperty As ModelProperty

    Public Event ErrorStateChanged(state As Boolean)

    'Public Sub New(model As CommandModel, modelProperty As String, label As String, errorMsg As String)
    '    With Me
    '        ._model = model
    '        ._modelProperty = modelProperty
    '        ._modelPropertyType = Nothing

    '        If label = "" Then
    '            ._label = modelProperty

    '        Else
    '            ._label = label

    '        End If

    '        ._hasError = False
    '        ._errorMessage = errorMsg

    '    End With
    'End Sub

    'Public Sub New(model As CommandModel, modelProperty As String, Optional propAsType As Type = Nothing,
    '               Optional label As String = "", Optional errorMsg As String = "",
    '               Optional argValidator As CommandArgumentValidator = Nothing)
    '    With Me
    '        If label = "" Then
    '            ._label = modelProperty

    '        Else
    '            ._label = label

    '        End If

    '        ._hasError = False
    '        ._errorMessage = errorMsg

    '        ._modelProperty = New ModelProperty(model:=model, propName:=modelProperty,
    '                                            altPropType:=propAsType, isInputValid:=isInputValid,
    '                                            isInputValidParams:=isInputValidParams)

    '    End With
    'End Sub

    Public Sub New(model As CommandModel, modelProperty As String, errorStateChangedHandler As Action(Of Boolean),
                   Optional propAsType As Type = Nothing,
                   Optional label As String = "", Optional errorMsg As String = "",
                   Optional isValid As Func(Of Object, Object, Boolean) = Nothing,
                   Optional validationArg As IArgumentVM = Nothing)

        With Me
            If label = "" Then
                ._label = modelProperty

            Else
                ._label = label

            End If

            ._hasError = False
            ._errorMessage = errorMsg

            ._modelProperty = New ModelProperty(model:=model, propName:=modelProperty,
                                                altPropType:=propAsType, isValid:=isValid,
                                                validationArg:=validationArg)

            AddHandler Me.ErrorStateChanged, Sub(errorState As Boolean)
                                                 errorStateChangedHandler(errorState)

                                             End Sub

        End With
    End Sub

    Protected Class ModelProperty
        Private _propType As Type

        Private _model As CommandModel, _modelPropInfo As PropertyInfo
        Private _propName As String

        Private _isValid As Func(Of Object, Object, Boolean)
        Private _validationArg As IArgumentVM

        Public Sub New(model As Object, propName As String,
                       Optional altPropType As Type = Nothing,
                       Optional isValid As Func(Of Object, Object, Boolean) = Nothing,
                       Optional validationArg As IArgumentVM = Nothing)

            Me._model = model : Me._modelPropInfo = model.GetType().GetProperty(propName)
            Me._propName = propName

            Me._isValid = isValid
            Me._validationArg = validationArg

            ' Use reflection to get the model's property value
            Dim modelTyp As Type = model.GetType()
            Dim prop As PropertyInfo = modelTyp.GetProperty(propName)

            If altPropType IsNot Nothing Then
                Me._propType = altPropType

            Else
                Me._propType = prop.PropertyType()

            End If

        End Sub

        Public Property Value As Object
            Get
                Dim propValue = CTypeDynamic(Me._modelPropInfo.GetValue(Me._model), Me._propType)
                Return propValue

            End Get
            Set(value As Object)
                ' parse will throw exception
                ' CommandArg will handle
                If _isValid IsNot Nothing Then

                    If _isValid(value, Me._validationArg.Value) Then
                        Dim propValue = CTypeDynamic(value, Me._modelPropInfo.PropertyType)
                        Me._modelPropInfo.SetValue(Me._model, propValue)

                        'Me._modelPropInfo.SetValue(Me._model, value)

                    Else
                        Throw New Exception("input value is not valid")

                    End If

                Else
                    Me._modelPropInfo.SetValue(Me._model, value)

                End If

            End Set
        End Property

        Public Property Text As String
            Get
                Return ValueToString(Me.Value(), Me._propType)

            End Get
            Set(value As String)
                ' parse will throw exception
                ' CommandArg will handle
                Me.Value = ParseValue(value, Me._propType)

            End Set
        End Property

        Public ReadOnly Property Type As Type
            Get
                Return Me._propType

            End Get
        End Property

        Private Function ValueToString(val As Object, propTyp As Type) As String
            Dim toStringMthd As MethodInfo = propTyp.GetMethod(name:="ToString", types:={})

            Return toStringMthd.Invoke(obj:=Value, parameters:={})

        End Function

        Private Function ParseValue(s As String, propTyp As Type) As Object
            ' Use reflection to parse the input string
            ' into the property
            Dim parseMthd As MethodInfo = propTyp.GetMethod(name:="Parse", types:={GetType(String)})
            Return parseMthd.Invoke(Nothing, {s})

        End Function

    End Class

    Public Overridable Property Text As String
        Get
            If _hasError Then
                ' return invalid string value entered by user
                Return Me._errorValue

            Else
                ' return the valid model property value
                Return Me._modelProperty.Text()

            End If

        End Get
        Set(value As String)
            ' validate number
            Try
                ' might throw exception
                Me._modelProperty.Text = value

                ' reset error state
                Me._hasError = False

            Catch ex As Exception
                ' set the errorneous value
                Me._errorValue = value

                ' set the error state
                Me._hasError = True

            End Try

            ' notify
            OnPropertyChanged("Text")
            OnPropertyChanged("HasError")

            RaiseEvent ErrorStateChanged(Me._hasError)

        End Set
    End Property

    Public ReadOnly Property Value As Object Implements IArgumentVM.Value
        Get
            Return Me._modelProperty.Value

        End Get
    End Property

    Public Property Label As String
        Get
            Return Me._label

        End Get
        Set(value As String)
            Me._label = value

            ' notify
            OnPropertyChanged("Label")

        End Set
    End Property

    Public ReadOnly Property HasError As Boolean
        Get
            Return Me._hasError

        End Get
    End Property

    Public ReadOnly Property ErrorMessage As String
        Get
            Return Me._errorMessage

        End Get
    End Property

    Protected Sub OnErrorStateChanged(state As Boolean)
        RaiseEvent ErrorStateChanged(state)

    End Sub

End Class

Public Interface IArgumentVM
    ReadOnly Property Value As Object

End Interface

Public Class Range
    Private _min As Integer
    Private _max As Integer

    Public Sub New(min As Integer, max As Integer)
        If min < max Then
            Me._min = min
            Me._max = max

        Else
            Throw New ArgumentException("Range.New: min must be less than max")

        End If

    End Sub

    Public ReadOnly Property Min As Integer
        Get
            Return Me._min

        End Get
    End Property

    Public ReadOnly Property Max As Integer
        Get
            Return Me._max

        End Get
    End Property

End Class

'Public Class CommandArgumentValidator
'    Private _isValid As Func(Of Object, Object, Boolean)
'    Private _argToValidate As CommandArgumentVM
'    Private _validationArg As CommandArgumentVM

'    Public Sub New(isValid As Func(Of Object, Object, Boolean),
'                   argToValidate As CommandArgumentVM, validationArg As CommandArgumentVM)

'        With Me
'            ._isValid = isValid
'            ._argToValidate = argToValidate
'            ._validationArg = validationArg

'        End With

'    End Sub

'    Public Function IsValid(value As String) As Boolean
'        Try
'            ' first parse the argument

'        Catch ex As Exception

'        End Try
'        Return Me._isValid(Me._argToValidate.Value(), Me._validationArg.Value())

'    End Function

'    Private Function ParseValue(s As String, propTyp As Type) As Object
'        ' Use reflection to parse the input string
'        ' into the property
'        Dim parseMthd As MethodInfo = propTyp.GetMethod(name:="Parse", types:={GetType(String)})
'        Return parseMthd.Invoke(Nothing, {s})

'    End Function

'End Class