Public Class FormAddSaltDispense
  Private ControlCode As ControlCode

  Public ReadOnly Property gallons As Double
    Get
      Return ControlMain.Gallons
    End Get
  End Property

  Public ReadOnly Property Pounds As Double
    Get
      Return 0  ' TODO
    End Get
  End Property


  Public ReadOnly Property SelectedPort As DistributorPort
    Get
      Return ControlMain.SelectedPort
    End Get
  End Property

  Public Sub New(controlcode As ControlCode)
    ' This call is required by the designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.ControlCode = controlcode
    Me.controlMain.Connect(controlcode)
  End Sub

End Class