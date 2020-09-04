<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMoveDistributor
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
    Me.controlMain = New TencateCarrier.ControlMoveDistributor()
    Me.SuspendLayout()
    '
    'buttonOK
    '
    Me.buttonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.buttonOK.Location = New System.Drawing.Point(8, 153)
    Me.buttonOK.Name = "buttonOK"
    Me.buttonOK.Size = New System.Drawing.Size(108, 32)
    Me.buttonOK.TabIndex = 1
    Me.buttonOK.Text = "OK"
    Me.buttonOK.UseVisualStyleBackColor = True
    '
    'buttonCancel
    '
    Me.buttonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.buttonCancel.Location = New System.Drawing.Point(418, 153)
    Me.buttonCancel.Name = "buttonCancel"
    Me.buttonCancel.Size = New System.Drawing.Size(108, 32)
    Me.buttonCancel.TabIndex = 2
    Me.buttonCancel.Text = "Cancel"
    Me.buttonCancel.UseVisualStyleBackColor = True
    '
    'controlMain
    '
    Me.controlMain.Location = New System.Drawing.Point(2, 2)
    Me.controlMain.Name = "controlMain"
    Me.controlMain.Size = New System.Drawing.Size(530, 150)
    Me.controlMain.TabIndex = 0
    '
    'FormMoveDistributor
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(534, 193)
    Me.ControlBox = False
    Me.Controls.Add(Me.buttonCancel)
    Me.Controls.Add(Me.buttonOK)
    Me.Controls.Add(Me.controlMain)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FormMoveDistributor"
    Me.ShowIcon = False
    Me.ShowInTaskbar = False
    Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Move Distributor"
    Me.TopMost = True
    Me.ResumeLayout(False)

  End Sub
  Private WithEvents buttonOK As System.Windows.Forms.Button
  Private WithEvents buttonCancel As System.Windows.Forms.Button
  Private WithEvents controlMain As TencateCarrier.ControlMoveDistributor
End Class
