Public Class FormMoveDistributor
  Private ControlCode As ControlCode

  Public ReadOnly Property SelectedPort As DistributorPort
    Get
      Return controlMain.SelectedPort
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