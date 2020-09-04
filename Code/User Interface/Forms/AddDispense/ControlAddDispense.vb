Public Class ControlAddSaltDispense
  Private controlCode As ControlCode
  Private ports As DistributorPorts
  Private SelectedRadioButton As RadioButton

  Property SelectedPort As DistributorPort

  Public ReadOnly Property Gallons As Double
    Get
      Return numberPad.AmountDouble
    End Get
  End Property

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
    If Me.ports IsNot Nothing Then SetDestinationCodes()
  End Sub

  Private Sub SetDestinationCodes()
    For Each item In GroupBoxDestination.Controls
      If TypeOf item Is Label Then
        SetLabelText(DirectCast(item, Label))
      End If
    Next
  End Sub

  Private Sub SetLabelText(label As Label)
    Dim PortNumber As Integer
    If Integer.TryParse(label.Tag.ToString, PortNumber) Then
      Dim port = ports(PortNumber)
      With port
        If .Machine = -1 OrElse .Tank = -1 Then
          label.Text = Nothing
        Else
          label.Text = .Machine.ToString & "-" & .Tank
        End If
      End With
    End If
  End Sub

  Private Sub AddRadioButtons()
    Dim x As Integer = 20
    Dim xSpacing As Integer = 52

    ' Do it in pairs
    For i As Integer = 1 To 32 Step 4
      If i = 17 Then x += 22 ' two sets

      AddRadioButton(i + 0, x, 30)
      AddRadioButton(i + 1, x, 70)
      AddRadioButton(i + 2, x, 110)
      AddRadioButton(i + 3, x, 150)
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
      .Text = Nothing ' index.ToString("#00")
      .UseVisualStyleBackColor = True
    End With
    AddHandler newRadioButton.Click, AddressOf radioButton_CheckedChanged
    Me.GroupBoxDestination.Controls.Add(newRadioButton)

    Dim newLabel As New System.Windows.Forms.Label
    With newLabel
      .Location = New System.Drawing.Point(x - 10, y + 16)
      .Name = "Label" & index.ToString
      .Size = New System.Drawing.Size(32, 16)
      .TabIndex = 32 + index
      .Tag = index
      .Text = index.ToString("#00")
      .TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    End With
    Me.GroupBoxDestination.Controls.Add(newLabel)
  End Sub

  Private Sub radioButton_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
    Dim button As RadioButton = TryCast(sender, RadioButton)
    If button Is Nothing Then Return

    If button.Checked Then
      SelectedRadioButton = button
      SelectedPort = ports(CType(SelectedRadioButton.Tag, Integer))
    End If
  End Sub

End Class
