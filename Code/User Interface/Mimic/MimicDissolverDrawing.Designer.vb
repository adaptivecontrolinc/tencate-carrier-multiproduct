<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MimicDissolverDrawing
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
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.SuspendLayout()
    '
    'Panel1
    '
    Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight
    ' Me.Panel1.BackgroundImage = Global.TencateCarrier.My.Resources.Resources.MimicDissolver
    Me.Panel1.Location = New System.Drawing.Point(62, 25)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(675, 422)
    Me.Panel1.TabIndex = 0
    '
    'MimicDissolverDrawing
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
    Me.BackColor = System.Drawing.SystemColors.ControlLightLight
    Me.Controls.Add(Me.Panel1)
    Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Name = "MimicDissolverDrawing"
    Me.Size = New System.Drawing.Size(800, 472)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents Panel1 As System.Windows.Forms.Panel

End Class
