<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ControlAddSaltDispense
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
    Me.GroupBoxAmount = New System.Windows.Forms.GroupBox()
    Me.numberPad = New TencateCarrier.ControlNumberPad()
    Me.GroupBoxDestination = New System.Windows.Forms.GroupBox()
    Me.GroupBoxAmount.SuspendLayout()
    Me.SuspendLayout()
    '
    'GroupBoxAmount
    '
    Me.GroupBoxAmount.Controls.Add(Me.numberPad)
    Me.GroupBoxAmount.Location = New System.Drawing.Point(4, 2)
    Me.GroupBoxAmount.Name = "GroupBoxAmount"
    Me.GroupBoxAmount.Size = New System.Drawing.Size(140, 200)
    Me.GroupBoxAmount.TabIndex = 2
    Me.GroupBoxAmount.TabStop = False
    Me.GroupBoxAmount.Text = "Dispense Amount (Gal)"
    '
    'numberPad
    '
    Me.numberPad.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.numberPad.KeySize = New System.Drawing.Size(36, 32)
    Me.numberPad.Location = New System.Drawing.Point(16, 28)
    Me.numberPad.Name = "numberPad"
    Me.numberPad.ShowAmount = True
    Me.numberPad.Size = New System.Drawing.Size(108, 154)
    Me.numberPad.TabIndex = 0
    '
    'GroupBoxDestination
    '
    Me.GroupBoxDestination.Location = New System.Drawing.Point(150, 2)
    Me.GroupBoxDestination.Name = "GroupBoxDestination"
    Me.GroupBoxDestination.Size = New System.Drawing.Size(450, 200)
    Me.GroupBoxDestination.TabIndex = 3
    Me.GroupBoxDestination.TabStop = False
    Me.GroupBoxDestination.Text = "Dispense Destination"
    '
    'ControlAddSaltDispense
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.GroupBoxDestination)
    Me.Controls.Add(Me.GroupBoxAmount)
    Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Name = "ControlAddSaltDispense"
    Me.Size = New System.Drawing.Size(602, 206)
    Me.GroupBoxAmount.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents numberPad As TencateCarrier.ControlNumberPad
  Friend WithEvents GroupBoxAmount As System.Windows.Forms.GroupBox
  Friend WithEvents GroupBoxDestination As System.Windows.Forms.GroupBox

End Class
