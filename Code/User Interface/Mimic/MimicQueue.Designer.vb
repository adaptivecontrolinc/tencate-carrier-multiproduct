<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MimicQueue
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
    Me.groupBoxRunning = New System.Windows.Forms.GroupBox()
    Me.dataGridViewXRunning = New TencateCarrier.DataGridViewX()
    Me.groupBoxScheduled = New System.Windows.Forms.GroupBox()
    Me.dataGridViewXScheduled = New TencateCarrier.DataGridViewX()
    Me.timerMain = New System.Windows.Forms.Timer(Me.components)
    Me.groupBoxRunning.SuspendLayout()
    CType(Me.dataGridViewXRunning, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.groupBoxScheduled.SuspendLayout()
    CType(Me.dataGridViewXScheduled, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'groupBoxRunning
    '
    Me.groupBoxRunning.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.groupBoxRunning.Controls.Add(Me.dataGridViewXRunning)
    Me.groupBoxRunning.Location = New System.Drawing.Point(12, 12)
    Me.groupBoxRunning.Name = "groupBoxRunning"
    Me.groupBoxRunning.Size = New System.Drawing.Size(776, 128)
    Me.groupBoxRunning.TabIndex = 0
    Me.groupBoxRunning.TabStop = False
    Me.groupBoxRunning.Text = "Running"
    '
    'dataGridViewXRunning
    '
    Me.dataGridViewXRunning.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dataGridViewXRunning.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dataGridViewXRunning.Location = New System.Drawing.Point(12, 24)
    Me.dataGridViewXRunning.Name = "dataGridViewXRunning"
    Me.dataGridViewXRunning.Size = New System.Drawing.Size(752, 92)
    Me.dataGridViewXRunning.TabIndex = 0
    Me.dataGridViewXRunning.XAutosizeColumn = Nothing
    Me.dataGridViewXRunning.XBorderColor = System.Drawing.Color.DarkGray
    '
    'groupBoxScheduled
    '
    Me.groupBoxScheduled.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.groupBoxScheduled.Controls.Add(Me.dataGridViewXScheduled)
    Me.groupBoxScheduled.Location = New System.Drawing.Point(12, 150)
    Me.groupBoxScheduled.Name = "groupBoxScheduled"
    Me.groupBoxScheduled.Size = New System.Drawing.Size(776, 308)
    Me.groupBoxScheduled.TabIndex = 1
    Me.groupBoxScheduled.TabStop = False
    Me.groupBoxScheduled.Text = "Scheduled"
    '
    'dataGridViewXScheduled
    '
    Me.dataGridViewXScheduled.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dataGridViewXScheduled.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dataGridViewXScheduled.Location = New System.Drawing.Point(12, 24)
    Me.dataGridViewXScheduled.Name = "dataGridViewXScheduled"
    Me.dataGridViewXScheduled.Size = New System.Drawing.Size(752, 272)
    Me.dataGridViewXScheduled.TabIndex = 1
    Me.dataGridViewXScheduled.XAutosizeColumn = Nothing
    Me.dataGridViewXScheduled.XBorderColor = System.Drawing.Color.DarkGray
    '
    'timerMain
    '
    Me.timerMain.Interval = 1000
    '
    'MimicQueue
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
    Me.BackColor = System.Drawing.SystemColors.ControlLightLight
    Me.Controls.Add(Me.groupBoxScheduled)
    Me.Controls.Add(Me.groupBoxRunning)
    Me.Name = "MimicQueue"
    Me.Size = New System.Drawing.Size(800, 472)
    Me.groupBoxRunning.ResumeLayout(False)
    CType(Me.dataGridViewXRunning, System.ComponentModel.ISupportInitialize).EndInit()
    Me.groupBoxScheduled.ResumeLayout(False)
    CType(Me.dataGridViewXScheduled, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Private WithEvents groupBoxRunning As System.Windows.Forms.GroupBox
  Private WithEvents groupBoxScheduled As System.Windows.Forms.GroupBox
  Private WithEvents dataGridViewXRunning As TencateCarrier.DataGridViewX
  Private WithEvents dataGridViewXScheduled As TencateCarrier.DataGridViewX
  Private WithEvents timerMain As System.Windows.Forms.Timer

End Class
