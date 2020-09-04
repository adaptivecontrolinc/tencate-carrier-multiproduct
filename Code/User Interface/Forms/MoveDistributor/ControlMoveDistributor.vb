Public Class ControlMoveDistributor
  Private controlCode As ControlCode
  Private ports As DistributorPorts
  Private SelectedRadioButton As RadioButton

  Property SelectedPort As DistributorPort

  Public Sub New()
    ' This call is required by the designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    InitializeControl()
  End Sub

  Private Sub InitializeControl()
    AddRadioButtons()
  End Sub

  Public Sub Connect(controlCode As ControlCode)
    Me.controlCode = controlCode
    Me.ports = controlCode.DistributorHead1.Ports '???dc
  End Sub

  Private Sub AddRadioButtons()
    Dim x As Integer = 20
    Dim xSpacing As Integer = 30

    ' Do it in pairs
    For i As Integer = 1 To 32 Step 2
      If i = 17 Then x += 20 ' two sets of eight...

      AddRadioButton(i + 0, x, 80)
      AddRadioButton(i + 1, x, 30)
      x += xSpacing
    Next
  End Sub

  Private Sub AddRadioButton(index As Integer, x As Integer, y As Integer)
    Dim newRadioButton As New System.Windows.Forms.RadioButton
    With newRadioButton
      .Location = New System.Drawing.Point(x, y)
      .Name = "RadioButtonPort" & index.ToString
      .Size = New System.Drawing.Size(24, 20)
      .TabIndex = index
      .TabStop = True
      .Tag = index
      .Text = Nothing 'index.ToString("#00")
      .UseVisualStyleBackColor = True
    End With
    AddHandler newRadioButton.Click, AddressOf radioButton_CheckedChanged
    Me.GroupBoxPort.Controls.Add(newRadioButton)

    Dim newLabel As New System.Windows.Forms.Label
    With newLabel
      .Location = New System.Drawing.Point(x - 4, y + 16)
      .Name = "Label" & index.ToString
      .Size = New System.Drawing.Size(24, 16)
      .TabIndex = 32 + index
      .Text = index.ToString("#00")
      .TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    End With
    Me.GroupBoxPort.Controls.Add(newLabel)
  End Sub

  Private Sub radioButton_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
    Dim button As RadioButton = TryCast(sender, RadioButton)
    If button Is Nothing Then Return

    If button.Checked Then
      SelectedRadioButton = button
      SelectedPort = Ports(CType(SelectedRadioButton.Tag, Integer))
    End If
  End Sub

End Class
