<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ControlMoveDistributor
  Inherits System.Windows.Forms.UserControl

  'UserControl overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.GroupBoxPort = New System.Windows.Forms.GroupBox()
    Me.SuspendLayout()
    '
    'GroupBoxPort
    '
    Me.GroupBoxPort.Location = New System.Drawing.Point(6, 2)
    Me.GroupBoxPort.Name = "GroupBoxPort"
    Me.GroupBoxPort.Size = New System.Drawing.Size(518, 140)
    Me.GroupBoxPort.TabIndex = 0
    Me.GroupBoxPort.TabStop = False
    Me.GroupBoxPort.Text = "Select Port"
    '
    'ControlMoveDistributor
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.GroupBoxPort)
    Me.Name = "ControlMoveDistributor"
    Me.Size = New System.Drawing.Size(530, 150)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents GroupBoxPort As System.Windows.Forms.GroupBox

End Class
