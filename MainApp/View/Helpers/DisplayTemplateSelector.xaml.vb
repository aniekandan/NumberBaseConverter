Public Class DisplayTemplateSelector
    Public Shared ReadOnly ViewTemplateProperty As DependencyProperty
    Public Shared ReadOnly EditTemplateProperty As DependencyProperty
    Public Shared ReadOnly MonitorProperty As DependencyProperty

    Shared Sub New()
        Dim coerceValueCallBack = Function(d As DependencyObject, value As Object)
                                      If value IsNot Nothing And TypeOf value Is UIElement Then
                                          Return value

                                      Else
                                          Return DependencyProperty.UnsetValue

                                      End If

                                  End Function

        Dim editTemplateChangedCallBack = Sub(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
                                              Dim owner As DisplayTemplateSelector = d

                                              With owner.mainPanel.Children
                                                  .Remove(e.OldValue)
                                                  .Add(e.NewValue)

                                                  Dim monitor As Boolean = owner.Monitor
                                                  If monitor = True Then
                                                      CType(e.NewValue, UIElement).Visibility = Visibility.Visible

                                                  Else
                                                      CType(e.NewValue, UIElement).Visibility = Visibility.Collapsed

                                                  End If

                                              End With

                                          End Sub

        Dim viewTemplateChangedCallBack = Sub(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
                                              Dim owner As DisplayTemplateSelector = d

                                              With owner.mainPanel.Children
                                                  .Remove(e.OldValue)
                                                  .Add(e.NewValue)

                                                  Dim monitor As Boolean = owner.Monitor
                                                  If monitor = True Then
                                                      CType(e.NewValue, UIElement).Visibility = Visibility.Collapsed

                                                  Else
                                                      CType(e.NewValue, UIElement).Visibility = Visibility.Visible

                                                  End If

                                              End With

                                          End Sub

        ViewTemplateProperty = DependencyProperty.Register(name:="ViewTemplate",
                                                         propertyType:=GetType(UIElement), ownerType:=GetType(DisplayTemplateSelector),
                                                         typeMetadata:=New PropertyMetadata(defaultValue:=Nothing, coerceValueCallback:=coerceValueCallBack,
                                                                                            propertyChangedCallback:=viewTemplateChangedCallBack))

        EditTemplateProperty = DependencyProperty.Register(name:="EditTemplate",
                                                         propertyType:=GetType(UIElement), ownerType:=GetType(DisplayTemplateSelector),
                                                         typeMetadata:=New PropertyMetadata(defaultValue:=Nothing, coerceValueCallback:=coerceValueCallBack,
                                                                                            propertyChangedCallback:=editTemplateChangedCallBack))

        Dim monitorChangedCallBack = Sub(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
                                         Dim owner As DisplayTemplateSelector = d

                                         If owner.ViewTemplate IsNot Nothing And owner.EditTemplate IsNot Nothing Then
                                             If e.NewValue = True Then
                                                 ' EditMode is true
                                                 owner.ViewTemplate.Visibility = Visibility.Collapsed
                                                 owner.EditTemplate.Visibility = Visibility.Visible

                                             Else
                                                 ' ViewMode is true
                                                 owner.ViewTemplate.Visibility = Visibility.Visible
                                                 owner.EditTemplate.Visibility = Visibility.Collapsed

                                             End If

                                         End If

                                     End Sub

        MonitorProperty = DependencyProperty.Register(name:="Monitor",
                                                         propertyType:=GetType(Boolean),
                                                         ownerType:=GetType(DisplayTemplateSelector),
                                                         typeMetadata:=New PropertyMetadata(defaultValue:=True,
                                                                                            propertyChangedCallback:=monitorChangedCallBack))

    End Sub

    Public Property ViewTemplate As UIElement
        Get
            Return GetValue(ViewTemplateProperty)

        End Get
        Set(value As UIElement)
            SetValue(ViewTemplateProperty, value)

        End Set
    End Property

    Public Property EditTemplate As UIElement
        Get
            Return GetValue(EditTemplateProperty)

        End Get
        Set(value As UIElement)
            SetValue(EditTemplateProperty, value)

        End Set
    End Property

    Public Property Monitor As Boolean
        Get
            Return GetValue(MonitorProperty)

        End Get
        Set(value As Boolean)
            SetValue(MonitorProperty, value)

        End Set
    End Property

End Class
