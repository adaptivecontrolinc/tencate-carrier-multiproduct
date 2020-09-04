Public NotInheritable Class Parameters : Inherits MarshalByRefObject

  'Save a local reference for convenience
  Private controlCode As ControlCode

  Public Sub New(controlCode As ControlCode)
    Me.controlCode = controlCode
  End Sub


#Region " Calibration "

  <Parameter(0, 10000), Category("Calibration"), Description("Head 1 flowmeter pulses per gallon")>
  Public Head1PulsesPerGallon As Integer

  <Parameter(0, 10000), Category("Calibration"), Description("Head 2 flowmeter pulses per gallon")>
  Public Head2PulsesPerGallon As Integer

#End Region

#Region " Header 1 Product Setup"
  Private Const section_Head1Setup As String = "Head 1 Setup"

  <Parameter(0, 99999), Category(section_Head1Setup), Description("Head 1 product code")>
  Public Head1ProductCode As Integer

  <Parameter(0, 1), Category(section_Head1Setup), Description("Set to '1' to enable head 1 product dispensing.")>
  Public Head1ProductEnable As Integer

  <Parameter(0, 1000), Category(section_Head1Setup), Description("Minimum Head 1 dispense allowed (Gallons).")>
  Public Head1DispenseMin As Integer

  <Parameter(0, 1000), Category(section_Head1Setup), Description("Maximum  Head 1 dispense allowed (Gallons).")>
  Public Head1DispenseMax As Integer

  <Parameter(0, 600), Category(section_Head1Setup), Description("Head 1 time to purge after a Carrier dispense and rinse (seconds).")>
  Public Head1PurgeTime As Integer

  <Parameter(0, 100), Category(section_Head1Setup), Description("If a Head 1 dispense takes longer than thie raise an alarm")>
  Public Head1AlarmTime As Integer

  <Parameter(0, 100), Category(section_Head1Setup), Description("Raise an alarm if the Head 1 distributor does not move to the desired position in this time, seconds")>
  Public Head1DistributorAlarmTime As Integer



#End Region


#Region " Header 2 Product Setup"
  Private Const section_Head2Setup As String = "Head 2 Setup"

  <Parameter(0, 99999), Category(section_Head2Setup), Description("Head 2 product code")>
  Public Head2ProductCode As Integer

  <Parameter(0, 1), Category(section_Head2Setup), Description("Set to '1' to enable head 2 product dispensing.")>
  Public Head2ProductEnable As Integer

  <Parameter(0, 1000), Category(section_Head1Setup), Description("Minimum Head 2 dispense allowed (Gallons).")>
  Public Head2DispenseMin As Integer

  <Parameter(0, 1000), Category(section_Head1Setup), Description("Maximum  Head 2 dispense allowed (Gallons).")>
  Public Head2DispenseMax As Integer

  <Parameter(0, 600), Category(section_Head1Setup), Description("Head 2 time to purge after a Carrier dispense and rinse (seconds).")>
  Public Head2PurgeTime As Integer

  <Parameter(0, 100), Category(section_Head1Setup), Description("If a Head 2 dispense takes longer than thie raise an alarm")>
  Public Head2AlarmTime As Integer

  <Parameter(0, 100), Category(section_Head1Setup), Description("Raise an alarm if the Head 2 distributor does not move to the desired position in this time, seconds")>
  Public Head2DistributorAlarmTime As Integer


#End Region


#Region " System "

  <Parameter(0, 1), Category("System"), Description("Run in demo / simulation mode")>
  Public DemoMode As Integer

  <Parameter(0, 10000), Category("System"), Description("Maximum number of histories to keep in the database (0 = unlimited)")>
  Public NumberOfHistories As Integer

  <Parameter(4, 16), Category("System"), Description("Time to pause to allow valves to close")>
  Public ValveDelay As Integer

  <Parameter(0, 1000), Category("System"), Description("Smoothing to apply to analog level inputs")>
  Public Smoothing As Integer

  <Parameter(0, 1), Category("System"), Description("Enable or disable writes to PLC1, Dissolver PLC (0 = disable, 1 = enable)")>
  Public WriteEnablePLC1 As Integer

  <Parameter(0, 1), Category("System"), Description("Enable or disable writes to PLC2, Distributor PLC (0 = disable, 1 = enable)")>
  Public WriteEnablePLC2 As Integer

  <Parameter(0, 1), Category("System"), Description("Enable or disable dispense button on dissolver mimic screen (0 = disable, 1 = enable)")>
  Public DispenseButtonEnable As Integer

#End Region

End Class
