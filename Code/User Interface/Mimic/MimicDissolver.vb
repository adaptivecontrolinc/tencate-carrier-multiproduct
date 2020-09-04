Public Class MimicDissolver

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
    With ControlCode
    

      buttonDispense.Enabled = (.Parameters.DispenseButtonEnable = 1)
    End With
  End Sub

  Private Sub NewDispense()
    Using newForm As New FormAddSaltDispense(ControlCode)
      If newForm.ShowDialog(Me) = DialogResult.OK Then
        Dim pounds = newForm.Pounds
        Dim port = newForm.SelectedPort
        '??? dc
      End If
    End Using
  End Sub

  Private Sub buttonDispense_Click(sender As Object, e As EventArgs) Handles buttonDispense.Click
    NewDispense()
  End Sub

  Private Sub buttonTransfer_Click(sender As Object, e As EventArgs) Handles buttonTransfer.Click
    Dim question = "Are you sure you want to transfer the dissolver tank?"
    If MessageBox.Show(question, "Adaptive Dissolver", MessageBoxButtons.YesNo) = DialogResult.Yes Then
      'ControlCode.Dissolver.Transfer()
    End If
  End Sub

  Private Sub buttonDrain_Click(sender As Object, e As EventArgs) Handles buttonDrain.Click
    Dim question = "Are you sure you want to drain the dissolver tank?"
    If MessageBox.Show(question, "Adaptive Dissolver", MessageBoxButtons.YesNo) = DialogResult.Yes Then
      'ControlCode.Dissolver.Drain()
    End If
  End Sub

  Private Sub buttonDrainRinse_Click(sender As Object, e As EventArgs) Handles buttonDrainRinse.Click
    Dim question = "Are you sure you want to drain and rinse the dissolver tank?"
    If MessageBox.Show(question, "Adaptive Dissolver", MessageBoxButtons.YesNo) = DialogResult.Yes Then
      'ControlCode.Dissolver.DrainRinse()
    End If
  End Sub


End Class
