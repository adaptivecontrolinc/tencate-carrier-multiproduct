<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MimicTest
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
    Me.MimicDeviceDistributor1 = New TencateCarrier.MimicControlDistributor()
    Me.SuspendLayout()
    '
    'MimicDeviceDistributor1
    '
    Me.MimicDeviceDistributor1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.MimicDeviceDistributor1.Location = New System.Drawing.Point(124, 73)
    Me.MimicDeviceDistributor1.Name = "MimicDeviceDistributor1"
    Me.MimicDeviceDistributor1.Size = New System.Drawing.Size(698, 194)
    Me.MimicDeviceDistributor1.TabIndex = 0
    '
    'MimicTest
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.MimicDeviceDistributor1)
    Me.Name = "MimicTest"
    Me.Size = New System.Drawing.Size(926, 529)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents MimicDeviceDistributor1 As TencateCarrier.MimicControlDistributor

End Class
