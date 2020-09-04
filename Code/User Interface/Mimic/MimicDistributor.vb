Public Class MimicDistributor

  Public Property ControlCode As ControlCode

  Private ReadOnly Property Remoted() As Boolean
    Get
      Return Runtime.Remoting.RemotingServices.IsTransparentProxy(ControlCode)
    End Get
  End Property

  Public Sub New()
    ' This call is required by the designer.
    InitializeComponent()

    Me.DoubleBuffered = True
    Me.AutoScaleMode = Windows.Forms.AutoScaleMode.None

    ' Add any initialization after the InitializeComponent() call.
  End Sub


  Public Sub UpdateMimic()
    Try
      With ControlCode
        TextBox1.Text = "1"
        labelStatus1.Text = .DistributorHead1.Status
        labelStatus2.Text = .DistributorHead2.Status
        labelProduct1.Text = .Head1Dispenser.ProductCodeDisplay
        labelProduct2.Text = .Head2Dispenser.ProductCodeDisplay

        TextBox1.Text = "2"
        If .DistributorHead1.TargetPort Is Nothing Then
          labelEncoder1.Text = .Head1Encoder.Position.ToString
        Else
          labelEncoder1.Text = .Head1Encoder.Position.ToString & " / " & .DistributorHead1.TargetPort.Head.ToString
        End If
        TextBox1.Text = "3"
        If .DistributorHead2.TargetPort Is Nothing Then
          labelEncoder2.Text = .Head2Encoder.Position.ToString
        Else
          labelEncoder2.Text = .Head2Encoder.Position.ToString & " / " & .DistributorHead2.TargetPort.Head.ToString
        End If
        TextBox1.Text = "4"
        controlDistributor.UpdateMimic(ControlCode)
        If .Head1Dispenser.Active Then
          labelFlow1.Text = .Head1Flowmeter.Gallons.ToString("#0.0") & " / " & .Head1Dispenser.GallonsRequested.ToString("#0.0") & " Gallons"
        Else
          labelFlow1.Text = "Available"
        End If
        TextBox1.Text = "5"
        If .Head2Dispenser.Active Then
          labelFlow2.Text = .Head2Flowmeter.Gallons.ToString("#0.0") & " / " & .Head2Dispenser.GallonsRequested.ToString("#0.0") & " Gallons"
        Else
          labelFlow2.Text = "Available"
        End If
        TextBox1.Text = "6"
        If .DistributorHead1.Enabled Then
          buttonEnableH1.Text = "Enabled"
          buttonEnableH1.BackColor = Color.Green
        Else
          labelFlow1.Text = "Head 1 Disabled"
          buttonEnableH1.Text = "Disabled"
          buttonEnableH1.BackColor = Color.Yellow
        End If
        If .DistributorHead2.Enabled Then
          buttonEnableH2.Text = "Enabled"
          buttonEnableH2.BackColor = Color.Green
        Else
          labelFlow2.Text = "Head 2 Disabled"
          buttonEnableH2.Text = "Disabled"
          buttonEnableH2.BackColor = Color.Yellow
        End If
        'labelEncoder1.Text = ""
        'labelEncoder2.Text = ""
        TextBox1.Text = "7"
        buttonSetHome1.Enabled = .IO.Head1Home
        buttonSetHome2.Enabled = .IO.Head2Home
        buttonH1Cancel.Enabled = .DistributorHead1.Busy
        buttonH2Cancel.Enabled = .DistributorHead2.Busy
        TextBox1.Text = "8"
        labelPositionH1X.Text = "Position X: " & .DistributorHead1.Head1Position.ToString
        labelPositionH1Y.Text = "Position Y: " & .DistributorHead1.Head1TraversePosition.ToString
        labelPositionH1Z.Text = "Position Z: " & .DistributorHead1.Pistons.ConnectorPosition.ToString
        TextBox1.Text = "9"
        labelPositionH2X.Text = "Position X: " & .DistributorHead2.Head2Position.ToString
        labelPositionH2Y.Text = "Position Y: " & .DistributorHead2.Head2TraversePosition.ToString
        labelPositionH2Z.Text = "Position Z: " & .DistributorHead2.Pistons.ConnectorPosition.ToString
        TextBox1.Text = "10"
      End With
    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try




  End Sub


  Private Sub MoveToHome(head As Integer)
    If head = 1 Then
      Dim question = "Are you sure you want to home Head 1?"
      If MessageBox.Show(question, "Adaptive Distributor", MessageBoxButtons.YesNo) = DialogResult.Yes Then
        ControlCode.DistributorHead1.MoveHome(1)
      End If
    ElseIf head = 2 Then
      Dim question = "Are you sure you want to home Head 2?"
      If MessageBox.Show(question, "Adaptive Distributor", MessageBoxButtons.YesNo) = DialogResult.Yes Then
        ControlCode.DistributorHead2.MoveHome(2)
      End If
    End If

  End Sub

  Private Sub MoveToPort(head As Integer)
    With ControlCode
      If head = 1 Then
        Using newForm As New FormMoveDistributor(ControlCode)
          If newForm.ShowDialog(Me) = DialogResult.OK Then
            .DistributorHead1.MovePort(newForm.SelectedPort)
          End If
        End Using
      ElseIf head = 2 Then
        Using newForm As New FormMoveDistributor(ControlCode)
          If newForm.ShowDialog(Me) = DialogResult.OK Then
            .DistributorHead2.MovePort(newForm.SelectedPort)
          End If
        End Using
      End If

    End With
  End Sub

  Private Sub MoveToTravel(head As Integer)
    With ControlCode
      If head = 1 Then
        Dim question = "Are you sure you want to move Head 1 to the travel position?"
        If MessageBox.Show(question, "Adaptive Distributor", MessageBoxButtons.YesNo) = DialogResult.Yes Then
          ControlCode.DistributorHead1.MoveTravel() '???
        End If
      ElseIf head = 2 Then
        Dim question = "Are you sure you want to move Head 2 to the travel position?"
        If MessageBox.Show(question, "Adaptive Distributor", MessageBoxButtons.YesNo) = DialogResult.Yes Then
          ControlCode.DistributorHead2.MoveTravel() '???
        End If
      End If

    End With
  End Sub

  Private Sub SetHomePosition(head As Integer)
    If head = 1 Then
      Dim question = "Are you sure you want to set the home position for Head 1?"
      If MessageBox.Show(question, "Adaptive Distributor", MessageBoxButtons.YesNo) = DialogResult.Yes Then
        ControlCode.Head1Encoder.SetHome()
      End If
    ElseIf head = 2 Then
      Dim question = "Are you sure you want to set the home position for Head 2?"
      If MessageBox.Show(question, "Adaptive Distributor", MessageBoxButtons.YesNo) = DialogResult.Yes Then
        ControlCode.Head2Encoder.SetHome()
      End If
    End If

  End Sub
  Private Sub Head2NewDispense()
    Using newForm As New FormAddSaltDispense(ControlCode)
      If newForm.ShowDialog(Me) = DialogResult.OK Then
        Dim gallons = newForm.gallons
        Dim port = newForm.SelectedPort
        Dim machine = port.Machine
        Dim tank = port.Tank

        If port IsNot Nothing Then ControlCode.Head1Dispenser.Start(gallons, machine, tank, 2)
      End If
    End Using
  End Sub

  Private Sub Head1NewDispense()
    Using newForm As New FormAddSaltDispense(ControlCode)
      If newForm.ShowDialog(Me) = DialogResult.OK Then
        Dim gallons = newForm.gallons
        Dim port = newForm.SelectedPort
        Dim machine = port.Machine
        Dim tank = port.Tank

        If port IsNot Nothing Then ControlCode.Head1Dispenser.Start(gallons, machine, tank, 1)
      End If
    End Using
  End Sub
  Private Sub buttonHead1Dispense_Click(sender As Object, e As EventArgs) Handles buttonHead1Dispense.Click
    Head1NewDispense()
  End Sub
  Private Sub buttonHead2Dispense_Click(sender As Object, e As EventArgs) Handles buttonHead2Dispense.Click
    Head2NewDispense()
  End Sub
  Private Sub buttonMoveHome1_Click(sender As Object, e As EventArgs) Handles buttonMove1Home.Click
    MoveToHome(1)
  End Sub

  Private Sub buttonMovePort1_Click(sender As Object, e As EventArgs) Handles buttonMove1Port.Click
    MoveToPort(1)
  End Sub

  Private Sub ButtonMoveTravel1_Click(sender As Object, e As EventArgs) Handles ButtonMove1Travel.Click
    MoveToTravel(1)
  End Sub

  Private Sub buttonMoveHome2_Click(sender As Object, e As EventArgs) Handles buttonMove2Home.Click
    MoveToHome(2)
  End Sub

  Private Sub buttonMovePort2_Click(sender As Object, e As EventArgs) Handles buttonMove2Port.Click
    MoveToPort(2)
  End Sub

  Private Sub ButtonMoveTravel2_Click(sender As Object, e As EventArgs) Handles ButtonMove2Travel.Click
    MoveToTravel(2)
  End Sub

 

  Private Sub buttonEnableH1_Click(sender As Object, e As EventArgs) Handles buttonEnableH1.Click
    ControlCode.DistributorHead1.Enabled = Not ControlCode.DistributorHead1.Enabled
  End Sub
  Private Sub buttonEnableH2_Click(sender As Object, e As EventArgs) Handles buttonEnableH2.Click
    ControlCode.DistributorHead2.Enabled = Not ControlCode.DistributorHead2.Enabled
  End Sub

  Private Sub buttonH1Cancel_Click(sender As Object, e As EventArgs) Handles buttonH1Cancel.Click
    ControlCode.Head1Dispenser.Abort()
  End Sub

  Private Sub buttonH2Cancel_Click(sender As Object, e As EventArgs) Handles buttonH2Cancel.Click
    ControlCode.Head2Dispenser.Abort()
  End Sub

  
  Private Sub buttonSetHome1_Click_1(sender As Object, e As EventArgs) Handles buttonSetHome1.Click
    SetHomePosition(1)
  End Sub

  Private Sub buttonSetHome2_Click_1(sender As Object, e As EventArgs) Handles buttonSetHome2.Click
    SetHomePosition(2)
  End Sub

End Class
