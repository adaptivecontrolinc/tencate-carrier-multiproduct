<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MimicControlDistributorPort
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MimicControlDistributorPort))
    Me.labelPort = New System.Windows.Forms.Label()
    Me.labelDestination = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'labelPort
    '
    Me.labelPort.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelPort.Location = New System.Drawing.Point(0, 9)
    Me.labelPort.Name = "labelPort"
    Me.labelPort.Size = New System.Drawing.Size(34, 14)
    Me.labelPort.TabIndex = 0
    Me.labelPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'labelDestination
    '
    Me.labelDestination.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelDestination.Location = New System.Drawing.Point(0, 34)
    Me.labelDestination.Name = "labelDestination"
    Me.labelDestination.Size = New System.Drawing.Size(34, 14)
    Me.labelDestination.TabIndex = 1
    Me.labelDestination.TextAlign = System.Drawing.ContentAlignment.TopCenter
    '
    'MimicControlDistributorPort
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.Color.Transparent
    Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
    Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
    Me.Controls.Add(Me.labelDestination)
    Me.Controls.Add(Me.labelPort)
    Me.DoubleBuffered = True
    Me.Name = "MimicControlDistributorPort"
    Me.Size = New System.Drawing.Size(32, 48)
    Me.ResumeLayout(False)

  End Sub
  Private WithEvents labelPort As System.Windows.Forms.Label
  Private WithEvents labelDestination As System.Windows.Forms.Label

End Class
