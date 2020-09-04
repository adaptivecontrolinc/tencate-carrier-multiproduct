<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAddSaltDispense
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
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
    Me.buttonOK = New System.Windows.Forms.Button()
    Me.buttonCancel = New System.Windows.Forms.Button()
    Me.ControlMain = New TencateCarrier.ControlAddSaltDispense()
    Me.SuspendLayout()
    '
    'buttonOK
    '
    Me.buttonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.buttonOK.Location = New System.Drawing.Point(8, 229)
    Me.buttonOK.Name = "buttonOK"
    Me.buttonOK.Size = New System.Drawing.Size(108, 32)
    Me.buttonOK.TabIndex = 1
    Me.buttonOK.Text = "OK"
    Me.buttonOK.UseVisualStyleBackColor = True
    '
    'buttonCancel
    '
    Me.buttonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.buttonCancel.Location = New System.Drawing.Point(502, 229)
    Me.buttonCancel.Name = "buttonCancel"
    Me.buttonCancel.Size = New System.Drawing.Size(108, 32)
    Me.buttonCancel.TabIndex = 2
    Me.buttonCancel.Text = "Cancel"
    Me.buttonCancel.UseVisualStyleBackColor = True
    '
    'ControlMain
    '
    Me.ControlMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ControlMain.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ControlMain.Location = New System.Drawing.Point(8, 8)
    Me.ControlMain.Name = "ControlMain"
    Me.ControlMain.Size = New System.Drawing.Size(602, 206)
    Me.ControlMain.TabIndex = 0
    '
    'FormAddSaltDispense
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(618, 269)
    Me.ControlBox = False
    Me.Controls.Add(Me.buttonCancel)
    Me.Controls.Add(Me.buttonOK)
    Me.Controls.Add(Me.ControlMain)
    Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FormAddSaltDispense"
    Me.ShowIcon = False
    Me.ShowInTaskbar = False
    Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Add Salt Dispense"
    Me.TopMost = True
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ControlMain As TencateCarrier.ControlAddSaltDispense
  Private WithEvents buttonOK As System.Windows.Forms.Button
  Private WithEvents buttonCancel As System.Windows.Forms.Button
End Class
