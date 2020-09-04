<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MimicDissolver
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
    Me.labelSilo2 = New System.Windows.Forms.Label()
    Me.labelSilo1 = New System.Windows.Forms.Label()
    Me.labelStatus = New System.Windows.Forms.Label()
    Me.labelTitle = New System.Windows.Forms.Label()
    Me.buttonDispense = New System.Windows.Forms.Button()
    Me.buttonDrainRinse = New System.Windows.Forms.Button()
    Me.buttonDrain = New System.Windows.Forms.Button()
    Me.groupBoxActions = New System.Windows.Forms.GroupBox()
    Me.labelScale = New System.Windows.Forms.Label()
    Me.LabelTemperature = New System.Windows.Forms.Label()
    Me.labelFlowrate = New System.Windows.Forms.Label()
    Me.buttonTransfer = New System.Windows.Forms.Button()
    '  Me.IO_Mixer = New TencateCarrier.MimicDeviceMixer()
    Me.levelIndicator = New TencateCarrier.MimicLevelIndicator()
    Me.IO_Silo2Auger = New TencateCarrier.MimicDeviceAuger()
    Me.IO_Silo1Auger = New TencateCarrier.MimicDeviceAuger()
    Me.IO_Silo2Vibrate = New TencateCarrier.MimicDeviceVibrate()
    Me.IO_Silo1Vibrate = New TencateCarrier.MimicDeviceVibrate()
    Me.pump = New TencateCarrier.MimicDevicePump()
    Me.IO_Vent = New TencateCarrier.MimicDeviceValve()
    Me.IO_Purge = New TencateCarrier.MimicDeviceValve()
    Me.IO_Drain = New TencateCarrier.MimicDeviceValve()
    Me.IO_FillCold = New TencateCarrier.MimicDeviceValve()
    Me.IO_Transfer = New TencateCarrier.MimicDeviceValve()
    Me.IO_Steam = New TencateCarrier.MimicDeviceValve()
    Me.IO_CircRight = New TencateCarrier.MimicDeviceValve()
    Me.IO_FillHot = New TencateCarrier.MimicDeviceValve()
    Me.IO_CircLeft = New TencateCarrier.MimicDeviceValve()
    Me.MimicDissolverDrawing = New TencateCarrier.MimicDissolverDrawing()
    Me.groupBoxActions.SuspendLayout()
    Me.SuspendLayout()
    '
    'labelSilo2
    '
    Me.labelSilo2.BackColor = System.Drawing.Color.Transparent
    Me.labelSilo2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelSilo2.Location = New System.Drawing.Point(591, 40)
    Me.labelSilo2.Name = "labelSilo2"
    Me.labelSilo2.Size = New System.Drawing.Size(99, 22)
    Me.labelSilo2.TabIndex = 14
    Me.labelSilo2.Text = "Silo 2"
    Me.labelSilo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'labelSilo1
    '
    Me.labelSilo1.BackColor = System.Drawing.Color.Transparent
    Me.labelSilo1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelSilo1.Location = New System.Drawing.Point(110, 40)
    Me.labelSilo1.Name = "labelSilo1"
    Me.labelSilo1.Size = New System.Drawing.Size(99, 22)
    Me.labelSilo1.TabIndex = 13
    Me.labelSilo1.Text = "Silo 1"
    Me.labelSilo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'labelStatus
    '
    Me.labelStatus.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelStatus.Location = New System.Drawing.Point(247, 32)
    Me.labelStatus.Name = "labelStatus"
    Me.labelStatus.Size = New System.Drawing.Size(304, 22)
    Me.labelStatus.TabIndex = 11
    Me.labelStatus.Text = "Status"
    Me.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'labelTitle
    '
    Me.labelTitle.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelTitle.Location = New System.Drawing.Point(247, 0)
    Me.labelTitle.Name = "labelTitle"
    Me.labelTitle.Size = New System.Drawing.Size(304, 22)
    Me.labelTitle.TabIndex = 10
    Me.labelTitle.Text = "Salt Dissolver"
    Me.labelTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter
    '
    'buttonDispense
    '
    Me.buttonDispense.BackColor = System.Drawing.Color.Gainsboro
    Me.buttonDispense.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.buttonDispense.Location = New System.Drawing.Point(6, 18)
    Me.buttonDispense.Name = "buttonDispense"
    Me.buttonDispense.Size = New System.Drawing.Size(148, 44)
    Me.buttonDispense.TabIndex = 0
    Me.buttonDispense.Text = "Dispense"
    Me.buttonDispense.UseVisualStyleBackColor = False
    '
    'buttonDrainRinse
    '
    Me.buttonDrainRinse.BackColor = System.Drawing.Color.Gainsboro
    Me.buttonDrainRinse.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.buttonDrainRinse.Location = New System.Drawing.Point(6, 166)
    Me.buttonDrainRinse.Name = "buttonDrainRinse"
    Me.buttonDrainRinse.Size = New System.Drawing.Size(148, 44)
    Me.buttonDrainRinse.TabIndex = 16
    Me.buttonDrainRinse.Text = "Drain && Rinse"
    Me.buttonDrainRinse.UseVisualStyleBackColor = False
    '
    'buttonDrain
    '
    Me.buttonDrain.BackColor = System.Drawing.Color.Gainsboro
    Me.buttonDrain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.buttonDrain.Location = New System.Drawing.Point(6, 120)
    Me.buttonDrain.Name = "buttonDrain"
    Me.buttonDrain.Size = New System.Drawing.Size(148, 44)
    Me.buttonDrain.TabIndex = 17
    Me.buttonDrain.Text = "Drain"
    Me.buttonDrain.UseVisualStyleBackColor = False
    '
    'groupBoxActions
    '
    Me.groupBoxActions.Controls.Add(Me.buttonTransfer)
    Me.groupBoxActions.Controls.Add(Me.buttonDispense)
    Me.groupBoxActions.Controls.Add(Me.buttonDrainRinse)
    Me.groupBoxActions.Controls.Add(Me.buttonDrain)
    Me.groupBoxActions.Location = New System.Drawing.Point(8, 255)
    Me.groupBoxActions.Name = "groupBoxActions"
    Me.groupBoxActions.Size = New System.Drawing.Size(160, 216)
    Me.groupBoxActions.TabIndex = 18
    Me.groupBoxActions.TabStop = False
    Me.groupBoxActions.Text = "Actions - Dissolver"
    '
    'labelScale
    '
    Me.labelScale.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelScale.Location = New System.Drawing.Point(247, 130)
    Me.labelScale.Name = "labelScale"
    Me.labelScale.Size = New System.Drawing.Size(304, 22)
    Me.labelScale.TabIndex = 25
    Me.labelScale.Text = "0.0 Kg"
    Me.labelScale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'LabelTemperature
    '
    Me.LabelTemperature.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LabelTemperature.ForeColor = System.Drawing.Color.Red
    Me.LabelTemperature.Location = New System.Drawing.Point(404, 256)
    Me.LabelTemperature.Name = "LabelTemperature"
    Me.LabelTemperature.Size = New System.Drawing.Size(40, 15)
    Me.LabelTemperature.TabIndex = 26
    Me.LabelTemperature.Text = "120F"
    Me.LabelTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'labelFlowrate
    '
    Me.labelFlowrate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelFlowrate.Location = New System.Drawing.Point(247, 70)
    Me.labelFlowrate.Name = "labelFlowrate"
    Me.labelFlowrate.Size = New System.Drawing.Size(304, 22)
    Me.labelFlowrate.TabIndex = 27
    Me.labelFlowrate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'buttonTransfer
    '
    Me.buttonTransfer.BackColor = System.Drawing.Color.Gainsboro
    Me.buttonTransfer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.buttonTransfer.Location = New System.Drawing.Point(6, 74)
    Me.buttonTransfer.Name = "buttonTransfer"
    Me.buttonTransfer.Size = New System.Drawing.Size(148, 44)
    Me.buttonTransfer.TabIndex = 18
    Me.buttonTransfer.Text = "Transfer"
    Me.buttonTransfer.UseVisualStyleBackColor = False
    '
    'IO_Mixer
    '
    Me.IO_Mixer.Location = New System.Drawing.Point(382, 169)
    Me.IO_Mixer.Name = "IO_Mixer"
    Me.IO_Mixer.NormallyOn = False
    Me.IO_Mixer.OffFeedback = False
    Me.IO_Mixer.OffFeedbackEnabled = False
    Me.IO_Mixer.OnFeedback = False
    Me.IO_Mixer.OnFeedbackEnabled = False
    Me.IO_Mixer.Orientation = TencateCarrier.MimicDevice.EOrientation.Horizontal
    Me.IO_Mixer.Overridden = False
    Me.IO_Mixer.Size = New System.Drawing.Size(35, 37)
    Me.IO_Mixer.TabIndex = 9
    Me.IO_Mixer.Text = "MimicDeviceMixer1"
    Me.IO_Mixer.UIEnabled = False
    '
    'levelIndicator
    '
    Me.levelIndicator.BarColorMain = System.Drawing.Color.Green
    Me.levelIndicator.Location = New System.Drawing.Point(354, 256)
    Me.levelIndicator.Maximum = 1000
    Me.levelIndicator.Name = "levelIndicator"
    Me.levelIndicator.Size = New System.Drawing.Size(12, 76)
    Me.levelIndicator.TabIndex = 24
    '
    'IO_Silo2Auger
    '
    Me.IO_Silo2Auger.Location = New System.Drawing.Point(686, 211)
    Me.IO_Silo2Auger.Name = "IO_Silo2Auger"
    Me.IO_Silo2Auger.NormallyOn = False
    Me.IO_Silo2Auger.OffFeedback = False
    Me.IO_Silo2Auger.OffFeedbackEnabled = False
    Me.IO_Silo2Auger.OnFeedback = False
    Me.IO_Silo2Auger.OnFeedbackEnabled = False
    Me.IO_Silo2Auger.Orientation = TencateCarrier.MimicDeviceLeftRight.EOrientation.Right
    Me.IO_Silo2Auger.Overridden = False
    Me.IO_Silo2Auger.Size = New System.Drawing.Size(51, 26)
    Me.IO_Silo2Auger.TabIndex = 23
    Me.IO_Silo2Auger.Text = "Auger Silo 2"
    Me.IO_Silo2Auger.UIEnabled = False
    '
    'IO_Silo1Auger
    '
    Me.IO_Silo1Auger.Location = New System.Drawing.Point(62, 211)
    Me.IO_Silo1Auger.Name = "IO_Silo1Auger"
    Me.IO_Silo1Auger.NormallyOn = False
    Me.IO_Silo1Auger.OffFeedback = False
    Me.IO_Silo1Auger.OffFeedbackEnabled = False
    Me.IO_Silo1Auger.OnFeedback = False
    Me.IO_Silo1Auger.OnFeedbackEnabled = False
    Me.IO_Silo1Auger.Orientation = TencateCarrier.MimicDeviceLeftRight.EOrientation.Left
    Me.IO_Silo1Auger.Overridden = False
    Me.IO_Silo1Auger.Size = New System.Drawing.Size(51, 26)
    Me.IO_Silo1Auger.TabIndex = 22
    Me.IO_Silo1Auger.Text = "Auger Silo 1"
    Me.IO_Silo1Auger.UIEnabled = False
    '
    'IO_Silo2Vibrate
    '
    Me.IO_Silo2Vibrate.Location = New System.Drawing.Point(643, 151)
    Me.IO_Silo2Vibrate.Name = "IO_Silo2Vibrate"
    Me.IO_Silo2Vibrate.NormallyOn = False
    Me.IO_Silo2Vibrate.OffFeedback = False
    Me.IO_Silo2Vibrate.OffFeedbackEnabled = False
    Me.IO_Silo2Vibrate.OnFeedback = False
    Me.IO_Silo2Vibrate.OnFeedbackEnabled = False
    Me.IO_Silo2Vibrate.Orientation = TencateCarrier.MimicDeviceLeftRight.EOrientation.Right
    Me.IO_Silo2Vibrate.Overridden = False
    Me.IO_Silo2Vibrate.Size = New System.Drawing.Size(24, 13)
    Me.IO_Silo2Vibrate.TabIndex = 21
    Me.IO_Silo2Vibrate.Text = "Vibrate Silo 2"
    Me.IO_Silo2Vibrate.UIEnabled = False
    '
    'IO_Silo1Vibrate
    '
    Me.IO_Silo1Vibrate.Location = New System.Drawing.Point(132, 151)
    Me.IO_Silo1Vibrate.Name = "IO_Silo1Vibrate"
    Me.IO_Silo1Vibrate.NormallyOn = False
    Me.IO_Silo1Vibrate.OffFeedback = False
    Me.IO_Silo1Vibrate.OffFeedbackEnabled = False
    Me.IO_Silo1Vibrate.OnFeedback = False
    Me.IO_Silo1Vibrate.OnFeedbackEnabled = False
    Me.IO_Silo1Vibrate.Orientation = TencateCarrier.MimicDeviceLeftRight.EOrientation.Left
    Me.IO_Silo1Vibrate.Overridden = False
    Me.IO_Silo1Vibrate.Size = New System.Drawing.Size(24, 13)
    Me.IO_Silo1Vibrate.TabIndex = 20
    Me.IO_Silo1Vibrate.Text = "Vibrate Silo 1"
    Me.IO_Silo1Vibrate.UIEnabled = False
    '
    'pump
    '
    Me.pump.Location = New System.Drawing.Point(432, 374)
    Me.pump.Name = "pump"
    Me.pump.NormallyOn = False
    Me.pump.OffFeedback = False
    Me.pump.OffFeedbackEnabled = False
    Me.pump.OnFeedback = False
    Me.pump.OnFeedbackEnabled = False
    Me.pump.Orientation = TencateCarrier.MimicDevice.EOrientation.Horizontal
    Me.pump.Overridden = False
    Me.pump.Size = New System.Drawing.Size(31, 28)
    Me.pump.TabIndex = 19
    Me.pump.Text = "MimicDevicePump1"
    Me.pump.UIEnabled = False
    '
    'IO_Vent
    '
    Me.IO_Vent.Location = New System.Drawing.Point(573, 395)
    Me.IO_Vent.Name = "IO_Vent"
    Me.IO_Vent.NormallyOn = False
    Me.IO_Vent.OffFeedback = False
    Me.IO_Vent.OffFeedbackEnabled = False
    Me.IO_Vent.OnFeedback = False
    Me.IO_Vent.OnFeedbackEnabled = False
    Me.IO_Vent.Orientation = TencateCarrier.MimicDevice.EOrientation.Vertical
    Me.IO_Vent.Overridden = False
    Me.IO_Vent.Size = New System.Drawing.Size(13, 25)
    Me.IO_Vent.TabIndex = 6
    Me.IO_Vent.Text = "MimicDeviceValve1"
    Me.IO_Vent.UIEnabled = False
    '
    'IO_Purge
    '
    Me.IO_Purge.Location = New System.Drawing.Point(549, 395)
    Me.IO_Purge.Name = "IO_Purge"
    Me.IO_Purge.NormallyOn = False
    Me.IO_Purge.OffFeedback = False
    Me.IO_Purge.OffFeedbackEnabled = False
    Me.IO_Purge.OnFeedback = False
    Me.IO_Purge.OnFeedbackEnabled = False
    Me.IO_Purge.Orientation = TencateCarrier.MimicDevice.EOrientation.Vertical
    Me.IO_Purge.Overridden = False
    Me.IO_Purge.Size = New System.Drawing.Size(13, 25)
    Me.IO_Purge.TabIndex = 5
    Me.IO_Purge.Text = "MimicDeviceValve1"
    Me.IO_Purge.UIEnabled = False
    '
    'IO_Drain
    '
    Me.IO_Drain.Location = New System.Drawing.Point(615, 380)
    Me.IO_Drain.Name = "IO_Drain"
    Me.IO_Drain.NormallyOn = False
    Me.IO_Drain.OffFeedback = False
    Me.IO_Drain.OffFeedbackEnabled = False
    Me.IO_Drain.OnFeedback = False
    Me.IO_Drain.OnFeedbackEnabled = False
    Me.IO_Drain.Orientation = TencateCarrier.MimicDevice.EOrientation.Horizontal
    Me.IO_Drain.Overridden = False
    Me.IO_Drain.Size = New System.Drawing.Size(25, 13)
    Me.IO_Drain.TabIndex = 0
    Me.IO_Drain.Text = "MimicDeviceValve1"
    Me.IO_Drain.UIEnabled = False
    '
    'IO_FillCold
    '
    Me.IO_FillCold.Location = New System.Drawing.Point(297, 296)
    Me.IO_FillCold.Name = "IO_FillCold"
    Me.IO_FillCold.NormallyOn = False
    Me.IO_FillCold.OffFeedback = False
    Me.IO_FillCold.OffFeedbackEnabled = False
    Me.IO_FillCold.OnFeedback = False
    Me.IO_FillCold.OnFeedbackEnabled = False
    Me.IO_FillCold.Orientation = TencateCarrier.MimicDevice.EOrientation.Vertical
    Me.IO_FillCold.Overridden = False
    Me.IO_FillCold.Size = New System.Drawing.Size(13, 25)
    Me.IO_FillCold.TabIndex = 8
    Me.IO_FillCold.Text = "MimicDeviceValve1"
    Me.IO_FillCold.UIEnabled = False
    '
    'IO_Transfer
    '
    Me.IO_Transfer.Location = New System.Drawing.Point(507, 380)
    Me.IO_Transfer.Name = "IO_Transfer"
    Me.IO_Transfer.NormallyOn = False
    Me.IO_Transfer.OffFeedback = False
    Me.IO_Transfer.OffFeedbackEnabled = False
    Me.IO_Transfer.OnFeedback = False
    Me.IO_Transfer.OnFeedbackEnabled = False
    Me.IO_Transfer.Orientation = TencateCarrier.MimicDevice.EOrientation.Horizontal
    Me.IO_Transfer.Overridden = False
    Me.IO_Transfer.Size = New System.Drawing.Size(25, 13)
    Me.IO_Transfer.TabIndex = 1
    Me.IO_Transfer.Text = "MimicDeviceValve1"
    Me.IO_Transfer.UIEnabled = False
    '
    'IO_Steam
    '
    Me.IO_Steam.Location = New System.Drawing.Point(507, 338)
    Me.IO_Steam.Name = "IO_Steam"
    Me.IO_Steam.NormallyOn = False
    Me.IO_Steam.OffFeedback = False
    Me.IO_Steam.OffFeedbackEnabled = False
    Me.IO_Steam.OnFeedback = False
    Me.IO_Steam.OnFeedbackEnabled = False
    Me.IO_Steam.Orientation = TencateCarrier.MimicDevice.EOrientation.Horizontal
    Me.IO_Steam.Overridden = False
    Me.IO_Steam.Size = New System.Drawing.Size(25, 13)
    Me.IO_Steam.TabIndex = 2
    Me.IO_Steam.Text = "MimicDeviceValve1"
    Me.IO_Steam.UIEnabled = False
    '
    'IO_CircRight
    '
    Me.IO_CircRight.Location = New System.Drawing.Point(453, 308)
    Me.IO_CircRight.Name = "IO_CircRight"
    Me.IO_CircRight.NormallyOn = False
    Me.IO_CircRight.OffFeedback = False
    Me.IO_CircRight.OffFeedbackEnabled = False
    Me.IO_CircRight.OnFeedback = False
    Me.IO_CircRight.OnFeedbackEnabled = False
    Me.IO_CircRight.Orientation = TencateCarrier.MimicDevice.EOrientation.Horizontal
    Me.IO_CircRight.Overridden = False
    Me.IO_CircRight.Size = New System.Drawing.Size(25, 13)
    Me.IO_CircRight.TabIndex = 4
    Me.IO_CircRight.Text = "MimicDeviceValve1"
    Me.IO_CircRight.UIEnabled = False
    '
    'IO_FillHot
    '
    Me.IO_FillHot.Location = New System.Drawing.Point(273, 296)
    Me.IO_FillHot.Name = "IO_FillHot"
    Me.IO_FillHot.NormallyOn = False
    Me.IO_FillHot.OffFeedback = False
    Me.IO_FillHot.OffFeedbackEnabled = False
    Me.IO_FillHot.OnFeedback = False
    Me.IO_FillHot.OnFeedbackEnabled = False
    Me.IO_FillHot.Orientation = TencateCarrier.MimicDevice.EOrientation.Vertical
    Me.IO_FillHot.Overridden = False
    Me.IO_FillHot.Size = New System.Drawing.Size(13, 25)
    Me.IO_FillHot.TabIndex = 7
    Me.IO_FillHot.Text = "MimicDeviceValve1"
    Me.IO_FillHot.UIEnabled = False
    '
    'IO_CircLeft
    '
    Me.IO_CircLeft.Location = New System.Drawing.Point(453, 272)
    Me.IO_CircLeft.Name = "IO_CircLeft"
    Me.IO_CircLeft.NormallyOn = False
    Me.IO_CircLeft.OffFeedback = False
    Me.IO_CircLeft.OffFeedbackEnabled = False
    Me.IO_CircLeft.OnFeedback = False
    Me.IO_CircLeft.OnFeedbackEnabled = False
    Me.IO_CircLeft.Orientation = TencateCarrier.MimicDevice.EOrientation.Horizontal
    Me.IO_CircLeft.Overridden = False
    Me.IO_CircLeft.Size = New System.Drawing.Size(25, 13)
    Me.IO_CircLeft.TabIndex = 3
    Me.IO_CircLeft.Text = "MimicDeviceValve1"
    Me.IO_CircLeft.UIEnabled = False
    '
    'MimicDissolverDrawing
    '
    Me.MimicDissolverDrawing.BackColor = System.Drawing.SystemColors.ControlLightLight
    Me.MimicDissolverDrawing.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.MimicDissolverDrawing.Location = New System.Drawing.Point(0, 0)
    Me.MimicDissolverDrawing.Name = "MimicDissolverDrawing"
    Me.MimicDissolverDrawing.Size = New System.Drawing.Size(800, 472)
    Me.MimicDissolverDrawing.TabIndex = 15
    '
    'MimicDissolver
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
    Me.BackColor = System.Drawing.SystemColors.ControlLightLight
    Me.Controls.Add(Me.labelFlowrate)
    Me.Controls.Add(Me.IO_Mixer)
    Me.Controls.Add(Me.LabelTemperature)
    Me.Controls.Add(Me.labelScale)
    Me.Controls.Add(Me.levelIndicator)
    Me.Controls.Add(Me.IO_Silo2Auger)
    Me.Controls.Add(Me.IO_Silo1Auger)
    Me.Controls.Add(Me.IO_Silo2Vibrate)
    Me.Controls.Add(Me.IO_Silo1Vibrate)
    Me.Controls.Add(Me.pump)
    Me.Controls.Add(Me.groupBoxActions)
    Me.Controls.Add(Me.IO_Vent)
    Me.Controls.Add(Me.IO_Purge)
    Me.Controls.Add(Me.labelStatus)
    Me.Controls.Add(Me.IO_Drain)
    Me.Controls.Add(Me.IO_FillCold)
    Me.Controls.Add(Me.labelSilo2)
    Me.Controls.Add(Me.IO_Transfer)
    Me.Controls.Add(Me.IO_Steam)
    Me.Controls.Add(Me.IO_CircRight)
    Me.Controls.Add(Me.IO_FillHot)
    Me.Controls.Add(Me.IO_CircLeft)
    Me.Controls.Add(Me.labelTitle)
    Me.Controls.Add(Me.labelSilo1)
    Me.Controls.Add(Me.MimicDissolverDrawing)
    Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Name = "MimicDissolver"
    Me.Size = New System.Drawing.Size(800, 472)
    Me.groupBoxActions.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents IO_Transfer As TencateCarrier.MimicDeviceValve
  Friend WithEvents IO_Drain As TencateCarrier.MimicDeviceValve
  Friend WithEvents IO_FillCold As TencateCarrier.MimicDeviceValve
  Friend WithEvents IO_FillHot As tencatecarrier.MimicDeviceValve
  Friend WithEvents IO_Vent As TencateCarrier.MimicDeviceValve
  Friend WithEvents IO_Purge As TencateCarrier.MimicDeviceValve
  Friend WithEvents IO_CircRight As TencateCarrier.MimicDeviceValve
  Friend WithEvents IO_CircLeft As TencateCarrier.MimicDeviceValve
  Friend WithEvents IO_Steam As TencateCarrier.MimicDeviceValve
  Friend WithEvents IO_Mixer As TencateCarrier.MimicDeviceMixer
  Friend WithEvents MimicDissolverDrawing As tencatecarrier.MimicDissolverDrawing
  Private WithEvents buttonDispense As System.Windows.Forms.Button
  Private WithEvents buttonDrainRinse As System.Windows.Forms.Button
  Private WithEvents buttonDrain As System.Windows.Forms.Button
  Private WithEvents pump As TencateCarrier.MimicDevicePump
  Private WithEvents labelSilo1 As System.Windows.Forms.Label
  Private WithEvents labelSilo2 As System.Windows.Forms.Label
  Private WithEvents labelStatus As System.Windows.Forms.Label
  Private WithEvents labelTitle As System.Windows.Forms.Label
  Private WithEvents groupBoxActions As System.Windows.Forms.GroupBox
  Private WithEvents IO_Silo2Vibrate As TencateCarrier.MimicDeviceVibrate
  Private WithEvents IO_Silo1Vibrate As TencateCarrier.MimicDeviceVibrate
  Private WithEvents IO_Silo1Auger As TencateCarrier.MimicDeviceAuger
  Private WithEvents IO_Silo2Auger As TencateCarrier.MimicDeviceAuger
  Private WithEvents levelIndicator As TencateCarrier.MimicLevelIndicator
  Private WithEvents labelScale As System.Windows.Forms.Label
  Friend WithEvents LabelTemperature As System.Windows.Forms.Label
  Private WithEvents labelFlowrate As System.Windows.Forms.Label
  Private WithEvents buttonTransfer As System.Windows.Forms.Button

End Class
