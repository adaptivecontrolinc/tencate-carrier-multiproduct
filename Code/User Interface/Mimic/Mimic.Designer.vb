<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Mimic
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
    Me.components = New System.ComponentModel.Container()
    Me.toolStripMain = New System.Windows.Forms.ToolStrip()
    Me.toolStripButtonDistributor = New System.Windows.Forms.ToolStripButton()
    Me.toolStripButtonQueue = New System.Windows.Forms.ToolStripButton()
    Me.timerMain = New System.Windows.Forms.Timer(Me.components)
    Me.labelStatus = New System.Windows.Forms.Label()
    Me.pageDistributor = New TencateCarrier.MimicDistributor()
    Me.pageQueue = New TencateCarrier.MimicQueue()
    Me.toolStripMain.SuspendLayout()
    Me.SuspendLayout()
    '
    'toolStripMain
    '
    Me.toolStripMain.BackColor = System.Drawing.SystemColors.ButtonFace
    Me.toolStripMain.Dock = System.Windows.Forms.DockStyle.None
    Me.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
    Me.toolStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripButtonDistributor, Me.toolStripButtonQueue})
    Me.toolStripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
    Me.toolStripMain.Location = New System.Drawing.Point(6, 6)
    Me.toolStripMain.Name = "toolStripMain"
    Me.toolStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
    Me.toolStripMain.Size = New System.Drawing.Size(191, 23)
    Me.toolStripMain.TabIndex = 1
    Me.toolStripMain.Text = "ToolStripMain"
    '
    'toolStripButtonDistributor
    '
    Me.toolStripButtonDistributor.CheckOnClick = True
    Me.toolStripButtonDistributor.Image = Global.TencateCarrier.My.Resources.Resources.Bespoke16x16
    Me.toolStripButtonDistributor.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolStripButtonDistributor.Name = "toolStripButtonDistributor"
    Me.toolStripButtonDistributor.Size = New System.Drawing.Size(83, 20)
    Me.toolStripButtonDistributor.Text = "Distributor"
    Me.toolStripButtonDistributor.ToolTipText = "Distributor"
    '
    'toolStripButtonQueue
    '
    Me.toolStripButtonQueue.CheckOnClick = True
    Me.toolStripButtonQueue.Image = Global.TencateCarrier.My.Resources.Resources.WorkList16x16
    Me.toolStripButtonQueue.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolStripButtonQueue.Name = "toolStripButtonQueue"
    Me.toolStripButtonQueue.Size = New System.Drawing.Size(76, 20)
    Me.toolStripButtonQueue.Text = "Work List"
    '
    'timerMain
    '
    Me.timerMain.Enabled = True
    Me.timerMain.Interval = 1000
    '
    'labelStatus
    '
    Me.labelStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelStatus.Location = New System.Drawing.Point(240, 13)
    Me.labelStatus.Name = "labelStatus"
    Me.labelStatus.Size = New System.Drawing.Size(550, 16)
    Me.labelStatus.TabIndex = 2
    Me.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'pageDistributor
    '
    Me.pageDistributor.BackColor = System.Drawing.SystemColors.ControlLightLight
    Me.pageDistributor.ControlCode = Nothing
    Me.pageDistributor.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.pageDistributor.Location = New System.Drawing.Point(0, 32)
    Me.pageDistributor.Name = "pageDistributor"
    Me.pageDistributor.Size = New System.Drawing.Size(800, 472)
    Me.pageDistributor.TabIndex = 4
    '
    'pageQueue
    '
    Me.pageQueue.BackColor = System.Drawing.SystemColors.ControlLightLight
    Me.pageQueue.ControlCode = Nothing
    Me.pageQueue.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.pageQueue.Location = New System.Drawing.Point(0, 32)
    Me.pageQueue.Name = "pageQueue"
    Me.pageQueue.Size = New System.Drawing.Size(800, 472)
    Me.pageQueue.TabIndex = 5
    '
    'Mimic
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
    Me.BackColor = System.Drawing.SystemColors.ControlLightLight
    Me.Controls.Add(Me.labelStatus)
    Me.Controls.Add(Me.toolStripMain)
    Me.Controls.Add(Me.pageDistributor)
    Me.Controls.Add(Me.pageQueue)
    Me.Name = "Mimic"
    Me.Size = New System.Drawing.Size(800, 504)
    Me.toolStripMain.ResumeLayout(False)
    Me.toolStripMain.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  ' Private WithEvents pageDissolver As TencateCarrier.MimicDissolver
  Private WithEvents pageDistributor As TencateCarrier.MimicDistributor
  Private WithEvents pageQueue As TencateCarrier.MimicQueue
  Private WithEvents toolStripMain As System.Windows.Forms.ToolStrip
  Private WithEvents toolStripButtonDistributor As System.Windows.Forms.ToolStripButton
  Private WithEvents toolStripButtonQueue As System.Windows.Forms.ToolStripButton
  Private WithEvents labelStatus As System.Windows.Forms.Label
  Private WithEvents timerMain As System.Windows.Forms.Timer

End Class
