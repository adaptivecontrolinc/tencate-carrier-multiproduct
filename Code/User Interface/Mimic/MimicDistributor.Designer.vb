<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MimicDistributor
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
    Me.labelStatus1 = New System.Windows.Forms.Label()
    Me.labelTitle = New System.Windows.Forms.Label()
    Me.labelEncoder1 = New System.Windows.Forms.Label()
    Me.labelStatus2 = New System.Windows.Forms.Label()
    Me.buttonMove1Home = New System.Windows.Forms.Button()
    Me.buttonMove1Port = New System.Windows.Forms.Button()
    Me.ButtonMove1Travel = New System.Windows.Forms.Button()
    Me.groupBoxActions1 = New System.Windows.Forms.GroupBox()
    Me.buttonHead1Dispense = New System.Windows.Forms.Button()
    Me.buttonH1Cancel = New System.Windows.Forms.Button()
    Me.buttonEnableH1 = New System.Windows.Forms.Button()
    Me.labelFlow1 = New System.Windows.Forms.Label()
    Me.labelPositionH1Z = New System.Windows.Forms.Label()
    Me.buttonSetHome1 = New System.Windows.Forms.Button()
    Me.labelPositionH1Y = New System.Windows.Forms.Label()
    Me.labelPositionH1X = New System.Windows.Forms.Label()
    Me.GroupActions2 = New System.Windows.Forms.GroupBox()
    Me.buttonHead2Dispense = New System.Windows.Forms.Button()
    Me.buttonH2Cancel = New System.Windows.Forms.Button()
    Me.buttonEnableH2 = New System.Windows.Forms.Button()
    Me.labelFlow2 = New System.Windows.Forms.Label()
    Me.labelPositionH2Z = New System.Windows.Forms.Label()
    Me.labelEncoder2 = New System.Windows.Forms.Label()
    Me.buttonSetHome2 = New System.Windows.Forms.Button()
    Me.labelPositionH2Y = New System.Windows.Forms.Label()
    Me.labelPositionH2X = New System.Windows.Forms.Label()
    Me.ButtonMove2Travel = New System.Windows.Forms.Button()
    Me.buttonMove2Port = New System.Windows.Forms.Button()
    Me.buttonMove2Home = New System.Windows.Forms.Button()
    Me.controlDistributor = New TencateCarrier.MimicControlDistributor()
    Me.TextBox1 = New System.Windows.Forms.TextBox()
    Me.labelProduct2 = New System.Windows.Forms.Label()
    Me.labelProduct1 = New System.Windows.Forms.Label()
    Me.groupBoxActions1.SuspendLayout()
    Me.GroupActions2.SuspendLayout()
    Me.SuspendLayout()
    '
    'labelStatus1
    '
    Me.labelStatus1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelStatus1.Location = New System.Drawing.Point(2, 21)
    Me.labelStatus1.Name = "labelStatus1"
    Me.labelStatus1.Size = New System.Drawing.Size(340, 50)
    Me.labelStatus1.TabIndex = 11
    Me.labelStatus1.Text = "Status"
    Me.labelStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'labelTitle
    '
    Me.labelTitle.BackColor = System.Drawing.SystemColors.ControlLight
    Me.labelTitle.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelTitle.Location = New System.Drawing.Point(246, 4)
    Me.labelTitle.Name = "labelTitle"
    Me.labelTitle.Size = New System.Drawing.Size(304, 21)
    Me.labelTitle.TabIndex = 10
    Me.labelTitle.Text = "Carrier Distributor"
    Me.labelTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter
    '
    'labelEncoder1
    '
    Me.labelEncoder1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelEncoder1.Location = New System.Drawing.Point(165, 90)
    Me.labelEncoder1.Name = "labelEncoder1"
    Me.labelEncoder1.Size = New System.Drawing.Size(173, 22)
    Me.labelEncoder1.TabIndex = 27
    Me.labelEncoder1.Text = "Enc1"
    Me.labelEncoder1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'labelStatus2
    '
    Me.labelStatus2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelStatus2.Location = New System.Drawing.Point(3, 21)
    Me.labelStatus2.Name = "labelStatus2"
    Me.labelStatus2.Size = New System.Drawing.Size(340, 50)
    Me.labelStatus2.TabIndex = 30
    Me.labelStatus2.Text = "Status"
    Me.labelStatus2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'buttonMove1Home
    '
    Me.buttonMove1Home.BackColor = System.Drawing.Color.Gainsboro
    Me.buttonMove1Home.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.buttonMove1Home.Location = New System.Drawing.Point(6, 113)
    Me.buttonMove1Home.Name = "buttonMove1Home"
    Me.buttonMove1Home.Size = New System.Drawing.Size(148, 44)
    Me.buttonMove1Home.TabIndex = 17
    Me.buttonMove1Home.Text = "Move Home"
    Me.buttonMove1Home.UseVisualStyleBackColor = False
    '
    'buttonMove1Port
    '
    Me.buttonMove1Port.BackColor = System.Drawing.Color.Gainsboro
    Me.buttonMove1Port.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.buttonMove1Port.Location = New System.Drawing.Point(5, 166)
    Me.buttonMove1Port.Name = "buttonMove1Port"
    Me.buttonMove1Port.Size = New System.Drawing.Size(148, 44)
    Me.buttonMove1Port.TabIndex = 0
    Me.buttonMove1Port.Text = "Move To Port"
    Me.buttonMove1Port.UseVisualStyleBackColor = False
    '
    'ButtonMove1Travel
    '
    Me.ButtonMove1Travel.BackColor = System.Drawing.Color.Gainsboro
    Me.ButtonMove1Travel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ButtonMove1Travel.Location = New System.Drawing.Point(5, 218)
    Me.ButtonMove1Travel.Name = "ButtonMove1Travel"
    Me.ButtonMove1Travel.Size = New System.Drawing.Size(148, 44)
    Me.ButtonMove1Travel.TabIndex = 18
    Me.ButtonMove1Travel.Text = "Travel Position"
    Me.ButtonMove1Travel.UseVisualStyleBackColor = False
    '
    'groupBoxActions1
    '
    Me.groupBoxActions1.Controls.Add(Me.labelProduct1)
    Me.groupBoxActions1.Controls.Add(Me.buttonHead1Dispense)
    Me.groupBoxActions1.Controls.Add(Me.buttonH1Cancel)
    Me.groupBoxActions1.Controls.Add(Me.buttonEnableH1)
    Me.groupBoxActions1.Controls.Add(Me.labelFlow1)
    Me.groupBoxActions1.Controls.Add(Me.labelPositionH1Z)
    Me.groupBoxActions1.Controls.Add(Me.labelEncoder1)
    Me.groupBoxActions1.Controls.Add(Me.buttonSetHome1)
    Me.groupBoxActions1.Controls.Add(Me.labelPositionH1Y)
    Me.groupBoxActions1.Controls.Add(Me.labelPositionH1X)
    Me.groupBoxActions1.Controls.Add(Me.ButtonMove1Travel)
    Me.groupBoxActions1.Controls.Add(Me.buttonMove1Port)
    Me.groupBoxActions1.Controls.Add(Me.buttonMove1Home)
    Me.groupBoxActions1.Controls.Add(Me.labelStatus1)
    Me.groupBoxActions1.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.groupBoxActions1.Location = New System.Drawing.Point(453, 149)
    Me.groupBoxActions1.Name = "groupBoxActions1"
    Me.groupBoxActions1.Size = New System.Drawing.Size(344, 321)
    Me.groupBoxActions1.TabIndex = 18
    Me.groupBoxActions1.TabStop = False
    Me.groupBoxActions1.Text = "Head 1"
    '
    'buttonHead1Dispense
    '
    Me.buttonHead1Dispense.BackColor = System.Drawing.Color.Gainsboro
    Me.buttonHead1Dispense.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.buttonHead1Dispense.Location = New System.Drawing.Point(190, 163)
    Me.buttonHead1Dispense.Name = "buttonHead1Dispense"
    Me.buttonHead1Dispense.Size = New System.Drawing.Size(148, 44)
    Me.buttonHead1Dispense.TabIndex = 40
    Me.buttonHead1Dispense.Text = "Dispense"
    Me.buttonHead1Dispense.UseVisualStyleBackColor = False
    '
    'buttonH1Cancel
    '
    Me.buttonH1Cancel.BackColor = System.Drawing.Color.Gainsboro
    Me.buttonH1Cancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.buttonH1Cancel.Location = New System.Drawing.Point(5, 272)
    Me.buttonH1Cancel.Name = "buttonH1Cancel"
    Me.buttonH1Cancel.Size = New System.Drawing.Size(148, 44)
    Me.buttonH1Cancel.TabIndex = 40
    Me.buttonH1Cancel.Text = "Cancel Dispense"
    Me.buttonH1Cancel.UseVisualStyleBackColor = False
    '
    'buttonEnableH1
    '
    Me.buttonEnableH1.BackColor = System.Drawing.Color.Gainsboro
    Me.buttonEnableH1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.buttonEnableH1.Location = New System.Drawing.Point(190, 220)
    Me.buttonEnableH1.Name = "buttonEnableH1"
    Me.buttonEnableH1.Size = New System.Drawing.Size(148, 44)
    Me.buttonEnableH1.TabIndex = 37
    Me.buttonEnableH1.Text = "Enable"
    Me.buttonEnableH1.UseVisualStyleBackColor = False
    '
    'labelFlow1
    '
    Me.labelFlow1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelFlow1.Location = New System.Drawing.Point(165, 68)
    Me.labelFlow1.Name = "labelFlow1"
    Me.labelFlow1.Size = New System.Drawing.Size(173, 22)
    Me.labelFlow1.TabIndex = 36
    Me.labelFlow1.Text = "Flow1"
    Me.labelFlow1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'labelPositionH1Z
    '
    Me.labelPositionH1Z.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelPositionH1Z.Location = New System.Drawing.Point(200, 138)
    Me.labelPositionH1Z.Name = "labelPositionH1Z"
    Me.labelPositionH1Z.Size = New System.Drawing.Size(120, 22)
    Me.labelPositionH1Z.TabIndex = 35
    Me.labelPositionH1Z.Text = "Position Z: "
    Me.labelPositionH1Z.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'buttonSetHome1
    '
    Me.buttonSetHome1.BackColor = System.Drawing.Color.Gainsboro
    Me.buttonSetHome1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.buttonSetHome1.Location = New System.Drawing.Point(190, 272)
    Me.buttonSetHome1.Name = "buttonSetHome1"
    Me.buttonSetHome1.Size = New System.Drawing.Size(148, 44)
    Me.buttonSetHome1.TabIndex = 32
    Me.buttonSetHome1.Text = "Set Home Position"
    Me.buttonSetHome1.UseVisualStyleBackColor = False
    '
    'labelPositionH1Y
    '
    Me.labelPositionH1Y.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelPositionH1Y.Location = New System.Drawing.Point(200, 121)
    Me.labelPositionH1Y.Name = "labelPositionH1Y"
    Me.labelPositionH1Y.Size = New System.Drawing.Size(120, 22)
    Me.labelPositionH1Y.TabIndex = 34
    Me.labelPositionH1Y.Text = "Position Y: "
    Me.labelPositionH1Y.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'labelPositionH1X
    '
    Me.labelPositionH1X.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelPositionH1X.Location = New System.Drawing.Point(200, 105)
    Me.labelPositionH1X.Name = "labelPositionH1X"
    Me.labelPositionH1X.Size = New System.Drawing.Size(120, 22)
    Me.labelPositionH1X.TabIndex = 33
    Me.labelPositionH1X.Text = "Position X: "
    Me.labelPositionH1X.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'GroupActions2
    '
    Me.GroupActions2.Controls.Add(Me.labelProduct2)
    Me.GroupActions2.Controls.Add(Me.buttonHead2Dispense)
    Me.GroupActions2.Controls.Add(Me.buttonH2Cancel)
    Me.GroupActions2.Controls.Add(Me.buttonEnableH2)
    Me.GroupActions2.Controls.Add(Me.labelFlow2)
    Me.GroupActions2.Controls.Add(Me.labelPositionH2Z)
    Me.GroupActions2.Controls.Add(Me.labelEncoder2)
    Me.GroupActions2.Controls.Add(Me.buttonSetHome2)
    Me.GroupActions2.Controls.Add(Me.labelPositionH2Y)
    Me.GroupActions2.Controls.Add(Me.labelPositionH2X)
    Me.GroupActions2.Controls.Add(Me.ButtonMove2Travel)
    Me.GroupActions2.Controls.Add(Me.buttonMove2Port)
    Me.GroupActions2.Controls.Add(Me.buttonMove2Home)
    Me.GroupActions2.Controls.Add(Me.labelStatus2)
    Me.GroupActions2.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.GroupActions2.Location = New System.Drawing.Point(3, 149)
    Me.GroupActions2.Name = "GroupActions2"
    Me.GroupActions2.Size = New System.Drawing.Size(344, 321)
    Me.GroupActions2.TabIndex = 36
    Me.GroupActions2.TabStop = False
    Me.GroupActions2.Text = "Head 2"
    '
    'buttonHead2Dispense
    '
    Me.buttonHead2Dispense.BackColor = System.Drawing.Color.Gainsboro
    Me.buttonHead2Dispense.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.buttonHead2Dispense.Location = New System.Drawing.Point(190, 166)
    Me.buttonHead2Dispense.Name = "buttonHead2Dispense"
    Me.buttonHead2Dispense.Size = New System.Drawing.Size(148, 44)
    Me.buttonHead2Dispense.TabIndex = 37
    Me.buttonHead2Dispense.Text = "Dispense"
    Me.buttonHead2Dispense.UseVisualStyleBackColor = False
    '
    'buttonH2Cancel
    '
    Me.buttonH2Cancel.BackColor = System.Drawing.Color.Gainsboro
    Me.buttonH2Cancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.buttonH2Cancel.Location = New System.Drawing.Point(6, 272)
    Me.buttonH2Cancel.Name = "buttonH2Cancel"
    Me.buttonH2Cancel.Size = New System.Drawing.Size(148, 44)
    Me.buttonH2Cancel.TabIndex = 39
    Me.buttonH2Cancel.Text = "Cancel Dispense"
    Me.buttonH2Cancel.UseVisualStyleBackColor = False
    '
    'buttonEnableH2
    '
    Me.buttonEnableH2.BackColor = System.Drawing.Color.Gainsboro
    Me.buttonEnableH2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.buttonEnableH2.Location = New System.Drawing.Point(190, 220)
    Me.buttonEnableH2.Name = "buttonEnableH2"
    Me.buttonEnableH2.Size = New System.Drawing.Size(148, 44)
    Me.buttonEnableH2.TabIndex = 38
    Me.buttonEnableH2.Text = "Enable"
    Me.buttonEnableH2.UseVisualStyleBackColor = False
    '
    'labelFlow2
    '
    Me.labelFlow2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelFlow2.Location = New System.Drawing.Point(165, 68)
    Me.labelFlow2.Name = "labelFlow2"
    Me.labelFlow2.Size = New System.Drawing.Size(173, 22)
    Me.labelFlow2.TabIndex = 37
    Me.labelFlow2.Text = "Flow2"
    Me.labelFlow2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'labelPositionH2Z
    '
    Me.labelPositionH2Z.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelPositionH2Z.Location = New System.Drawing.Point(201, 138)
    Me.labelPositionH2Z.Name = "labelPositionH2Z"
    Me.labelPositionH2Z.Size = New System.Drawing.Size(120, 22)
    Me.labelPositionH2Z.TabIndex = 35
    Me.labelPositionH2Z.Text = "Position Z: "
    Me.labelPositionH2Z.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'labelEncoder2
    '
    Me.labelEncoder2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelEncoder2.Location = New System.Drawing.Point(165, 90)
    Me.labelEncoder2.Name = "labelEncoder2"
    Me.labelEncoder2.Size = New System.Drawing.Size(173, 22)
    Me.labelEncoder2.TabIndex = 27
    Me.labelEncoder2.Text = "Enc2"
    Me.labelEncoder2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'buttonSetHome2
    '
    Me.buttonSetHome2.BackColor = System.Drawing.Color.Gainsboro
    Me.buttonSetHome2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.buttonSetHome2.Location = New System.Drawing.Point(191, 272)
    Me.buttonSetHome2.Name = "buttonSetHome2"
    Me.buttonSetHome2.Size = New System.Drawing.Size(148, 44)
    Me.buttonSetHome2.TabIndex = 32
    Me.buttonSetHome2.Text = "Set Home Position"
    Me.buttonSetHome2.UseVisualStyleBackColor = False
    '
    'labelPositionH2Y
    '
    Me.labelPositionH2Y.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelPositionH2Y.Location = New System.Drawing.Point(201, 121)
    Me.labelPositionH2Y.Name = "labelPositionH2Y"
    Me.labelPositionH2Y.Size = New System.Drawing.Size(120, 22)
    Me.labelPositionH2Y.TabIndex = 34
    Me.labelPositionH2Y.Text = "Position Y: "
    Me.labelPositionH2Y.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'labelPositionH2X
    '
    Me.labelPositionH2X.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelPositionH2X.Location = New System.Drawing.Point(201, 105)
    Me.labelPositionH2X.Name = "labelPositionH2X"
    Me.labelPositionH2X.Size = New System.Drawing.Size(120, 22)
    Me.labelPositionH2X.TabIndex = 33
    Me.labelPositionH2X.Text = "Position X: "
    Me.labelPositionH2X.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'ButtonMove2Travel
    '
    Me.ButtonMove2Travel.BackColor = System.Drawing.Color.Gainsboro
    Me.ButtonMove2Travel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ButtonMove2Travel.Location = New System.Drawing.Point(6, 218)
    Me.ButtonMove2Travel.Name = "ButtonMove2Travel"
    Me.ButtonMove2Travel.Size = New System.Drawing.Size(148, 44)
    Me.ButtonMove2Travel.TabIndex = 18
    Me.ButtonMove2Travel.Text = "Travel Position"
    Me.ButtonMove2Travel.UseVisualStyleBackColor = False
    '
    'buttonMove2Port
    '
    Me.buttonMove2Port.BackColor = System.Drawing.Color.Gainsboro
    Me.buttonMove2Port.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.buttonMove2Port.Location = New System.Drawing.Point(6, 166)
    Me.buttonMove2Port.Name = "buttonMove2Port"
    Me.buttonMove2Port.Size = New System.Drawing.Size(148, 44)
    Me.buttonMove2Port.TabIndex = 0
    Me.buttonMove2Port.Text = "Move To Port"
    Me.buttonMove2Port.UseVisualStyleBackColor = False
    '
    'buttonMove2Home
    '
    Me.buttonMove2Home.BackColor = System.Drawing.Color.Gainsboro
    Me.buttonMove2Home.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.buttonMove2Home.Location = New System.Drawing.Point(6, 113)
    Me.buttonMove2Home.Name = "buttonMove2Home"
    Me.buttonMove2Home.Size = New System.Drawing.Size(148, 44)
    Me.buttonMove2Home.TabIndex = 17
    Me.buttonMove2Home.Text = "Move Home"
    Me.buttonMove2Home.UseVisualStyleBackColor = False
    '
    'controlDistributor
    '
    Me.controlDistributor.BackgroundImage = Global.TencateCarrier.My.Resources.Resources.MimicDistributorBox
    Me.controlDistributor.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.controlDistributor.Location = New System.Drawing.Point(48, 1)
    Me.controlDistributor.Name = "controlDistributor"
    Me.controlDistributor.Size = New System.Drawing.Size(698, 150)
    Me.controlDistributor.TabIndex = 29
    '
    'TextBox1
    '
    Me.TextBox1.Location = New System.Drawing.Point(353, 254)
    Me.TextBox1.Name = "TextBox1"
    Me.TextBox1.Size = New System.Drawing.Size(100, 21)
    Me.TextBox1.TabIndex = 37
    Me.TextBox1.Visible = False
    '
    'labelProduct2
    '
    Me.labelProduct2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelProduct2.Location = New System.Drawing.Point(6, 71)
    Me.labelProduct2.Name = "labelProduct2"
    Me.labelProduct2.Size = New System.Drawing.Size(148, 39)
    Me.labelProduct2.TabIndex = 40
    Me.labelProduct2.Text = "Product: 0000"
    Me.labelProduct2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'labelProduct1
    '
    Me.labelProduct1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelProduct1.Location = New System.Drawing.Point(6, 71)
    Me.labelProduct1.Name = "labelProduct1"
    Me.labelProduct1.Size = New System.Drawing.Size(148, 39)
    Me.labelProduct1.TabIndex = 41
    Me.labelProduct1.Text = "Product: 0000"
    Me.labelProduct1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'MimicDistributor
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
    Me.BackColor = System.Drawing.SystemColors.ControlLightLight
    Me.Controls.Add(Me.TextBox1)
    Me.Controls.Add(Me.GroupActions2)
    Me.Controls.Add(Me.groupBoxActions1)
    Me.Controls.Add(Me.labelTitle)
    Me.Controls.Add(Me.controlDistributor)
    Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Name = "MimicDistributor"
    Me.Size = New System.Drawing.Size(800, 472)
    Me.groupBoxActions1.ResumeLayout(False)
    Me.GroupActions2.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Private WithEvents labelStatus1 As System.Windows.Forms.Label
  Private WithEvents labelTitle As System.Windows.Forms.Label
  Private WithEvents labelEncoder1 As System.Windows.Forms.Label
  Private WithEvents controlDistributor As TencateCarrier.MimicControlDistributor
  Private WithEvents labelStatus2 As System.Windows.Forms.Label
  Private WithEvents buttonMove1Home As System.Windows.Forms.Button
  Private WithEvents buttonMove1Port As System.Windows.Forms.Button
  Private WithEvents ButtonMove1Travel As System.Windows.Forms.Button
  Private WithEvents groupBoxActions1 As System.Windows.Forms.GroupBox
  Private WithEvents labelPositionH1Z As System.Windows.Forms.Label
  Private WithEvents buttonSetHome1 As System.Windows.Forms.Button
  Private WithEvents labelPositionH1Y As System.Windows.Forms.Label
  Private WithEvents labelPositionH1X As System.Windows.Forms.Label
  Private WithEvents labelFlow1 As System.Windows.Forms.Label
  Private WithEvents GroupActions2 As System.Windows.Forms.GroupBox
  Private WithEvents labelFlow2 As System.Windows.Forms.Label
  Private WithEvents labelPositionH2Z As System.Windows.Forms.Label
  Private WithEvents labelEncoder2 As System.Windows.Forms.Label
  Private WithEvents buttonSetHome2 As System.Windows.Forms.Button
  Private WithEvents labelPositionH2Y As System.Windows.Forms.Label
  Private WithEvents labelPositionH2X As System.Windows.Forms.Label
  Private WithEvents ButtonMove2Travel As System.Windows.Forms.Button
  Private WithEvents buttonMove2Port As System.Windows.Forms.Button
  Private WithEvents buttonMove2Home As System.Windows.Forms.Button
  Private WithEvents buttonEnableH1 As System.Windows.Forms.Button
  Private WithEvents buttonEnableH2 As System.Windows.Forms.Button
  Private WithEvents buttonH1Cancel As System.Windows.Forms.Button
  Private WithEvents buttonH2Cancel As System.Windows.Forms.Button
  Private WithEvents buttonHead1Dispense As System.Windows.Forms.Button
  Private WithEvents buttonHead2Dispense As System.Windows.Forms.Button
  Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
  Private WithEvents labelProduct1 As Label
  Private WithEvents labelProduct2 As Label
End Class
