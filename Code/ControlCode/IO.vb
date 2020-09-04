Public NotInheritable Class IO : Inherits MarshalByRefObject

  'Save a local reference for convenience
  Private controlCode As ControlCode

  Private Const plcTimeoutSeconds As Integer = 4

  ' Distributor PLC
  Public Plc1 As Ports.Modbus
  Public Plc1Timer As New Timer With {.Seconds = 16}


#Region " IO List "


  ' Distributor PLC1 Dinp 1-32

  ' Distributor Inputs
  <IO(IOType.Dinp, 2), Description("Distributor Auto Selected")> Public DistributorAutoSelected As Boolean
  <IO(IOType.Dinp, 4), Description("Distributor Emergency Stop OK")> Public DistributorEmergencyStopOK As Boolean
  <IO(IOType.Dinp, 7), Description("Distributor Air Supply OK")> Public DistributorAirOK As Boolean
  <IO(IOType.Dinp, 10), Description("Head 1 Reverse Limit")> Public Head1ReverseLimit As Boolean
  <IO(IOType.Dinp, 12), Description("Head 2 Home Switch")> Public Head2Home As Boolean
  <IO(IOType.Dinp, 13), Description("Head 1 Home Switch")> Public Head1Home As Boolean
  <IO(IOType.Dinp, 14), Description("Head 2 Forward Limit")> Public Head2ForwardLimit As Boolean
  <IO(IOType.Dinp, 15), Description("Head 1 Forward Limit")> Public Head1ForwardLimit As Boolean
  <IO(IOType.Dinp, 16), Description("Head 2 Reverse Limit")> Public Head2ReverseLimit As Boolean
  <IO(IOType.Dinp, 17), Description("Manual Step Up")> Public ManualStepUp As Boolean
  <IO(IOType.Dinp, 18), Description("Head 1 Traverse Piston 1 Forward")> Public Head1Piston1Fwd As Boolean
  <IO(IOType.Dinp, 19), Description("Manual Step Down")> Public ManualStepDown As Boolean
  <IO(IOType.Dinp, 20), Description("Head 1 Traverse Piston 1 Reverse")> Public Head1Piston1Rev As Boolean
  <IO(IOType.Dinp, 21), Description("Head 1 Connector Down")> Public Head1ConnectorDown As Boolean
  <IO(IOType.Dinp, 22), Description("Head 1 Traverse Piston 2 Forward")> Public Head1Piston2Fwd As Boolean
  <IO(IOType.Dinp, 23), Description("Head 1 Connector Up")> Public Head1ConnectorUp As Boolean
  <IO(IOType.Dinp, 24), Description("Head 1 Traverse Piston 2 Reverse")> Public Head1Piston2Rev As Boolean
  <IO(IOType.Dinp, 25), Description("Distributor Inverter Fault")> Public DistributorInverterFault As Boolean
  <IO(IOType.Dinp, 26), Description("Head 2 Traverse Piston 1 Forward")> Public Head2Piston1Fwd As Boolean
  <IO(IOType.Dinp, 27), Description("Distributor Motor Fault")> Public DistributorMotorFault As Boolean
  <IO(IOType.Dinp, 28), Description("Head 2 Traverse Piston 1 Reverse")> Public Head2Piston1Rev As Boolean
  <IO(IOType.Dinp, 29), Description("Head 2 Connector Down")> Public Head2ConnectorDown As Boolean
  <IO(IOType.Dinp, 30), Description("Head 2 Traverse Piston 2 Forward")> Public Head2Piston2Fwd As Boolean
  <IO(IOType.Dinp, 31), Description("Head 2 Connector Up")> Public Head2ConnectorUp As Boolean
  <IO(IOType.Dinp, 32), Description("Head 2 Traverse Piston 2 Reverse")> Public Head2Piston2Rev As Boolean



  ' Distributor PLC1 Dout 1-32
  ' Distributor outputs
  <IO(IOType.Dout, 1, Override.Allow), Description("Head 1 Traverse Piston 1 Forward Solenoid Valve")> Public Head1Piston1FwdSv As Boolean
  <IO(IOType.Dout, 2, Override.Allow), Description("Head 1 Connector Down  Solenoid Valve")> Public Head1ConnectorDownSv As Boolean
  <IO(IOType.Dout, 3, Override.Allow), Description("Head 1 Traverse Piston 1 Reverse  Solenoid Valve")> Public Head1Piston1RevSv As Boolean
  <IO(IOType.Dout, 4, Override.Allow), Description("Head 1 Connector Up  Solenoid Valve")> Public Head1ConnectorUpSv As Boolean
  <IO(IOType.Dout, 5, Override.Allow), Description("Head 1 Traverse Piston 2 Forward Solenoid Valve")> Public Head1Piston2FwdSv As Boolean
  <IO(IOType.Dout, 6, Override.Allow), Description("Head 2 Traverse Piston 1 Forward Solenoid Valve")> Public Head2Piston1FwdSv As Boolean
  <IO(IOType.Dout, 7, Override.Allow), Description("Head 1 Traverse Piston 2 Reverse Solenoid Valve")> Public Head1Piston2RevSv As Boolean
  <IO(IOType.Dout, 8, Override.Allow), Description("Head 2 Traverse Piston 1 Reverse  Solenoid Valve")> Public Head2Piston1RevSv As Boolean
  <IO(IOType.Dout, 9, Override.Allow), Description("Head 2 Traverse Piston 2 Forward Solenoid Valve")> Public Head2Piston2FwdSv As Boolean
  <IO(IOType.Dout, 10, Override.Allow), Description("Head 1 Add Solenoid Valve")> Public Head1AddSv As Boolean
  <IO(IOType.Dout, 11, Override.Allow), Description("Head 2 Traverse Piston 2 Reverse Solenoid Valve")> Public Head2Piston2RevSv As Boolean
  <IO(IOType.Dout, 12, Override.Allow), Description("Head 2 Add Solenoid Valve")> Public Head2AddSv As Boolean
  <IO(IOType.Dout, 13, Override.Allow), Description("Head 2 Connector Down  Solenoid Valve")> Public Head2ConnectorDownSv As Boolean
  <IO(IOType.Dout, 14, Override.Allow), Description("Head 1 Air Blowing Solenoid Valve")> Public Head1AirBlowingSv As Boolean
  <IO(IOType.Dout, 15, Override.Allow), Description("Head 2 Connector Up  Solenoid Valve")> Public Head2ConnectorUpSv As Boolean
  <IO(IOType.Dout, 16, Override.Allow), Description("Head 2 Air Blowing Solenoid Valve")> Public Head2AirBlowingSv As Boolean

  <IO(IOType.Dout, 17, Override.Allow), Description("Head 1 Motor Starter")> Public Head1Motor As Boolean
  <IO(IOType.Dout, 18, Override.Allow), Description("Head Motor Speed 1")> Public HeadMotorSpeed1 As Boolean
  <IO(IOType.Dout, 19, Override.Allow), Description("Head 2 Motor Starter")> Public Head2Motor As Boolean
  <IO(IOType.Dout, 20, Override.Allow), Description("Head Motor Speed 2")> Public HeadMotorSpeed2 As Boolean
  <IO(IOType.Dout, 21, Override.Allow), Description("Head Motor Forward")> Public HeadMotorFwd As Boolean
  <IO(IOType.Dout, 22, Override.Allow), Description("Head Motor Speed 3")> Public HeadMotorSpeed3 As Boolean
  <IO(IOType.Dout, 23, Override.Allow), Description("Head Motor Reverse")> Public HeadMotorRev As Boolean
  <IO(IOType.Dout, 25, Override.Allow), Description("Carrier Pump")> Public CarrierPump As Boolean
  <IO(IOType.Dout, 29, Override.Allow), Description("Head 1 Flowmeter Reset")> Public Head1MeterReset As Boolean
  <IO(IOType.Dout, 31, Override.Allow), Description("Head 2 Flowmeter Reset")> Public Head2MeterReset As Boolean
  <IO(IOType.Dout, 32, Override.Allow), Description("Distributor Alarm Light")> Public DistributorAlarmLight As Boolean

  <IO(IOType.Anout, 1, Override.Allow), Description("Head 1 flowmeter configuration")> Public Head1FlowmeterConfig As Short
  <IO(IOType.Anout, 2, Override.Allow), Description("Head 2 flowmeter configuration")> Public Head2FlowmeterConfig As Short


  <IO(IOType.Counter, 1, Override.Prevent, "", "%d"), Description("Head 1 Encoder Count")> Public Head1EncoderCount As Short
  <IO(IOType.Counter, 2, Override.Prevent, "", "%d"), Description("Head 2 Encoder Count")> Public Head2EncoderCount As Short
  <IO(IOType.Counter, 3, Override.Prevent, "", "%d"), Description("Head 1 Flowmeter Count")> Public Head1FlowmeterCount As Integer
  <IO(IOType.Counter, 4, Override.Prevent, "", "%d"), Description("Head 2 Flowmeter Count")> Public Head2FlowmeterCount As Integer


#End Region

  Public Sub New(ByVal controlCode As ControlCode)
    ' Save local reference
    Me.controlCode = controlCode

    ' Load any IO remaps from the Settings.IO.xml file
    RemapIO()

    With controlCode
      ' Initialize PLC comms
      Dim Plc1Address As String = .Parent.Setting("Plc1Address")
      If String.IsNullOrEmpty(Plc1Address) Then Plc1Address = "192.168.1.191"
      Plc1 = New Ports.Modbus(New Ports.ModbusTcp(Plc1Address, 502))
      Plc1Timer.Seconds = plcTimeoutSeconds
    End With

  End Sub

  Private Sub RemapIO()
    Try
      ' Get the file path
      Dim filePath As String = My.Application.Info.DirectoryPath & "\Settings.IO.xml"

      ' If the file does not exist quit
      If Not My.Computer.FileSystem.FileExists(filePath) Then Exit Sub

      ' Read the settings into a dataset
      Dim dsSettings As New System.Data.DataSet : dsSettings.ReadXml(filePath)

      If dsSettings.Tables.Contains("Settings") Then
        For Each row As System.Data.DataRow In dsSettings.Tables("Settings").Rows
          Dim name As String = row("Name").ToString
          Dim channel As Integer = Utilities.Sql.NullToZeroInteger(row("Channel").ToString)
          controlCode.Parent.SetIOChannel(name, channel)
        Next
      End If

    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try
  End Sub

  Public Function ReadInputs(ByVal dinp() As Boolean, ByVal aninp() As Short, ByVal temp() As Short) As Boolean
    Dim returnValue As Boolean
    If ReadInputsPLC1(dinp, aninp, temp) Then returnValue = True
    Return returnValue
  End Function

  Private Function ReadInputsPLC1(ByVal dinp() As Boolean, ByVal aninp() As Short, ByVal temp() As Short) As Boolean
    If Plc1 Is Nothing Then Return False

    ' True if we get a response from the PLC
    Dim returnValue As Boolean = False

    ' Read digital inputs PLC1
    Dim dinpRead1(32) As Boolean
    Select Case Plc1.Read(1, 10065, dinpRead1)
      Case Ports.Modbus.Result.OK
        For i As Integer = 1 To 16
          If i <= dinp.GetUpperBound(0) Then dinp(i) = dinpRead1(i + 16)
        Next
        For i As Integer = 17 To 32
          If i <= dinp.GetUpperBound(0) Then dinp(i) = dinpRead1(i - 16)
        Next
        returnValue = True
        Plc1Timer.Seconds = plcTimeoutSeconds
      Case Ports.Modbus.Result.Fault
      Case Ports.Modbus.Result.HwFault
    End Select

    With controlCode
      Dim Status1Read(4) As Short

      Select Case Plc1.Read(1, 40001, Status1Read)
        Case Ports.Modbus.Result.OK
          If Status1Read(1) < 0 Then
            .Head1FlowmeterReady = False
          Else
            Head1FlowmeterCount = Status1Read(2)
            .Head1FlowmeterReady = True
          End If

      End Select
      Dim Status2Read(4) As Short

      Select Case Plc1.Read(1, 40003, Status2Read)
        Case Ports.Modbus.Result.OK
          If Status2Read(1) < 0 Then
            .Head2FlowmeterReady = False
          Else
            Head2FlowmeterCount = Status2Read(2)
            .Head2FlowmeterReady = True
          End If

      End Select
    End With

    Dim CountRead(4) As Short
    Select Case Plc1.Read(1, 40193, CountRead)
      Case Ports.Modbus.Result.OK
        Head1EncoderCount = CountRead(2)
        Head2EncoderCount = CountRead(4)

      Case Ports.Modbus.Result.Fault
      Case Ports.Modbus.Result.HwFault
    End Select

    Return returnValue
  End Function
  Public Sub WriteOutputs(ByVal dout() As Boolean, ByVal anout() As Short)
    With controlCode
      If .Parameters.WriteEnablePLC1 = 1 Then
        WriteConfigPLC1()
        WriteOutputsPLC1(dout, anout)
      End If
    End With
  End Sub

  Private Sub WriteConfigPLC1()
    If Plc1 Is Nothing Then Exit Sub

    ' Set watch dog timeout - send always
    Dim watchdogConfigAddress As Integer = 42001
    Dim watchdogConfigValues() As Short = {0, 2000}   ' 2000 milliseconds

    Select Case Plc1.Write(1, watchdogConfigAddress, watchdogConfigValues, Ports.WriteMode.Always)
      Case Ports.Modbus.Result.OK
      Case Ports.Modbus.Result.Fault
      Case Ports.Modbus.Result.HwFault
    End Select
  End Sub

  Private Sub WriteOutputsPLC1(ByVal dout() As Boolean, ByVal anout() As Short)
    If Plc1 Is Nothing Then Exit Sub

    ' If the system is stopping or watchdog has timed out then turn all outputs off.
    If controlCode.SystemShutdown Then
      For i = 1 To dout.GetUpperBound(0) : dout(i) = False : Next
      For i = 1 To anout.GetUpperBound(0) : anout(i) = 0 : Next
    End If

    ' Remap the IO 'cos it's a little odd 
    Dim doutWrite1(32) As Boolean
    For i = 1 To 16
      If i <= dout.GetUpperBound(0) Then doutWrite1((i - 0) + 16) = dout(i)
    Next i
    For i = 17 To 32
      If i <= dout.GetUpperBound(0) Then doutWrite1((i - 0) - 16) = dout(i)
    Next

    ' Write douts 1-32 to PLC1
    Select Case Plc1.Write(1, 40001 + 388, doutWrite1, Ports.WriteMode.Always)
      Case Ports.Modbus.Result.OK
        Plc1Timer.Seconds = plcTimeoutSeconds  ' Raise an alarm if we do not see a write at least every plcTimeoutSeconds
      Case Ports.Modbus.Result.Fault
      Case Ports.Modbus.Result.HwFault
    End Select

    ' Set counter configuration - send always
    Dim counterConfigAddress1 As Integer = 40001 + 576
    Dim counterConfigAddress2 As Integer = 40001 + 578
    Dim counterConfigValues() As Short = {0, 15360, 14}

    Select Case Plc1.Write(1, counterConfigAddress1, counterConfigValues, Ports.WriteMode.Always)
      Case Ports.Modbus.Result.OK
      Case Ports.Modbus.Result.Fault
      Case Ports.Modbus.Result.HwFault
    End Select

    Select Case Plc1.Write(1, counterConfigAddress2, counterConfigValues, Ports.WriteMode.Always)
      Case Ports.Modbus.Result.OK
      Case Ports.Modbus.Result.Fault
      Case Ports.Modbus.Result.HwFault
    End Select


    With controlCode
      Dim FlowmeterCmd(1) As Short
      FlowmeterCmd(1) = anout(1)
      If Not .Head1FlowmeterReady Then
        FlowmeterCmd(1) = 5260
      End If
      'If Not .Head1FlowmeterDef Then
      '  FlowmeterCmd(1) = 10240
      'End If

      Select Case Plc1.Write(1, 40385, FlowmeterCmd, Ports.WriteMode.Optimised)
        Case Ports.Modbus.Result.Fault
        Case Ports.Modbus.Result.OK
          FlowmeterCmd(1) = FlowmeterCmd(1)
      End Select

      Dim Flowmeter2Cmd(1) As Short
      Flowmeter2Cmd(1) = anout(2)
      If Not .Head2FlowmeterReady Then
        Flowmeter2Cmd(1) = 5260
      End If
     
      Select Case Plc1.Write(1, 40387, Flowmeter2Cmd, Ports.WriteMode.Optimised)
        Case Ports.Modbus.Result.Fault
        Case Ports.Modbus.Result.OK
          Flowmeter2Cmd(1) = Flowmeter2Cmd(1)
      End Select


    End With

    Dim ResetNetFail(1) As Short
    ResetNetFail(1) = 2
    Select Case Plc1.Write(1, 40768, ResetNetFail, Ports.WriteMode.Optimised)
      Case Ports.Modbus.Result.Fault
      Case Ports.Modbus.Result.OK

    End Select
  End Sub
  Public ReadOnly Property Plc1Timeout As Boolean
    Get
      If controlCode.SystemShutdown Then Return False
      If controlCode.Parent.Mode = Mode.Debug Then Plc1Timer.Seconds = plcTimeoutSeconds
      If Plc1Timer.Finished Then Return True
      Return False
    End Get
  End Property
End Class
'Phoenix IO Watchdog operation:
'  If the watchdog has timed out enabling or disabling the watchdog or changing the watchdog time won’t work.  You must cycle power or reset the fault as outlined below. 
'
'  The watchdog must be enabled from the web interface by entering the Bus Coupler IP address in a web browser, and then navigating to the Device Configuration - Watchdog page.  
'  Select the Enable radio button, then enter private in the password box, then click Apply and Reboot.
'
'  The watchdog time in milliseconds should be written to 42001.  After the time is written, it begins counting down on the bus coupler.  
'  If the count reaches 0 a NetFail error is set and maintained until power is cycled or a reset bit is set.  Writing a value of 0 disables the watchdog, provided the watchdog is not timed out.  
'
'  The 16 bit value at 40192 should be read to determine fault status.  A value of 1 indicates normal operation, a value of 2 indicated the watchdog has timed out.  
'  If the watchdog has timed out, it must be reset by writing a 2 to the 16 bit register at 40768. 
'
'  The memory addresses above are independent of IO configuration, so they should be the same on every controller of this type.
