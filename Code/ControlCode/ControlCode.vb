Public Class ControlCode : Inherits MarshalByRefObject
  Implements ACControlCode

  Public Parent As ACParent
  Public Parameters As Parameters
  Public Alarms As Alarms
  Public IO As IO
  Public RD As RD

  Private HeadInverterDelay As New Timer
  Public Head1FlowmeterDef As Boolean
  Public Head1FlowmeterReady As Boolean
  Public Head2FlowmeterReady As Boolean
  Public Head1FlowmeterStatus As Integer
  Public Head2FlowmeterStatus As Integer
  Public SlowFlash As Boolean
  Public Head1Encoder As Encoder
  Public Head2Encoder As Encoder
  Public DistributorHead1 As DistributorControl
  Public DistributorHead2 As DistributorControl
  Public Head1FwdOK As Boolean
  Public Head2FwdOK As Boolean
  Public Head1RevOK As Boolean
  Public Head2RevOK As Boolean
  Public Head1TravelOK As Boolean
  Public Head2TravelOK As Boolean
  Public DatabaseBDC As DatabaseBDC  ' read writes to Adaptive database
  Public DatabaseEAS As DatabaseEAS  ' read writes to EAS smieasyweb database
  Public Head1Flowmeter As Flowmeter
  Public Head2Flowmeter As Flowmeter
  Public Head1Dispenser As DispenseHeadControl
  Public Head2Dispenser As DispenseHeadControl
  Public DispenseScheduler As DispenseScheduler

  Public Sub New(ByVal parent As ACParent)
    ' Load dispenser settings first so we can override anything (settings.xml)
    Settings.Load()

    ' Store parent reference locally
    Me.Parent = parent

    ' Make a few changes to the button layout
    UpdateInterface()

    ' Initialize parameters. alarms, IO and commands
    Parameters = New Parameters(Me)
    Alarms = New Alarms(Me)
    IO = New IO(Me)
    RD = New RD(Me)

    Head1Flowmeter = New Flowmeter
    Head2Flowmeter = New Flowmeter

    Head1Encoder = New Encoder(Me)
    Head2Encoder = New Encoder(Me)
    Head1Dispenser = New DispenseHeadControl(Me)
    Head2Dispenser = New DispenseHeadControl(Me)
    DistributorHead1 = New DistributorControl(Me)
    DistributorHead2 = New DistributorControl(Me)
    DispenseScheduler = New DispenseScheduler(Me) : DispenseScheduler.Start()
    DatabaseBDC = New DatabaseBDC(Me) : DatabaseBDC.Start()
    DatabaseEAS = New DatabaseEAS(Me) : DatabaseEAS.Start()
  End Sub

  Public Sub Run() Implements ACControlCode.Run
    ' Set a global date now at the beginning of each scan so timers don't constantly call system time
    Timers.UtcDateNow = Date.UtcNow

    Static SlowFlasher As New Flasher

    SlowFlasher.Flash(SlowFlash, 1200)


    If Parameters.ValveDelay < 4 Then Parameters.ValveDelay = 4

    ' For convenience
    Dim Halted As Boolean = Parent.IsPaused OrElse Not IO.DistributorAutoSelected
    Dim NHalted As Boolean = Not Halted

    ' Run Encoder control
    Head1Encoder.Run(IO.Head1EncoderCount)
    Head2Encoder.Run(IO.Head2EncoderCount)

    ' Run distributor control code
    DistributorHead1.Run(1)
    DistributorHead2.Run(2)

    ' Confirm Product Code setpoint for first use - if set to 0, set to default Carrier code 701 ("0701")
    If Parameters.Head1ProductCode = 0 Then
      ' TODO - maybe signal or alarm instead
      Parameters.Head1ProductCode = 701
    End If
    If Parameters.Head2ProductCode = 0 Then
      ' TODO - maybe signal or alarm instead
      Parameters.Head2ProductCode = 701
    End If

    ' Head 1 specific parameters & Run
    With Head1Dispenser
      ' Load Parameters
      .DispenseMax = Parameters.Head1DispenseMax
      .DispenseMin = Parameters.Head1DispenseMin
      .Enabled = Parameters.Head1ProductEnable = 1
      .ProductCode = Parameters.Head1ProductCode
      .PurgeTime = Parameters.Head1PurgeTime
      .DispenseAlarmTime = Parameters.Head1AlarmTime
      .DistributorAlarmTime = Parameters.Head1DistributorAlarmTime
      ' Run control
      .Run(1)
    End With

    ' Head 2 specific parameters & Run
    With Head2Dispenser
      ' Load Parameters
      .DispenseMax = Parameters.Head2DispenseMax
      .DispenseMin = Parameters.Head2DispenseMin
      .Enabled = Parameters.Head2ProductEnable = 1
      .ProductCode = Parameters.Head2ProductCode
      .PurgeTime = Parameters.Head2PurgeTime
      .DispenseAlarmTime = Parameters.Head2AlarmTime
      .DistributorAlarmTime = Parameters.Head2DistributorAlarmTime
      ' Run control
      .Run(2)
    End With

    ' If in demo mode simulate dissolver to help debugging
    If DemoMode Then
      Me.Parent.Mode = Mode.Debug
      RunSimulations()
    Else
      Head1Flowmeter.CountsPerGallon = Parameters.Head1PulsesPerGallon
      If Head1FlowmeterReady Then Head1Flowmeter.Counter = IO.Head1FlowmeterCount
      Head2Flowmeter.CountsPerGallon = Parameters.Head2PulsesPerGallon
      If Head2FlowmeterReady Then Head2Flowmeter.Counter = IO.Head2FlowmeterCount
    End If

    Head1FwdOK = IO.Head1ForwardLimit AndAlso HeadInverterDelay.Finished AndAlso IO.Head1ConnectorUp AndAlso IO.Head1Piston1Rev AndAlso
      IO.Head1Piston2Rev AndAlso (Not (IO.Head2Piston1Fwd AndAlso IO.Head2Piston2Fwd) OrElse (DistributorHead2.Head2Position > (DistributorHead1.Head1Position + 300)))
    Head1RevOK = IO.Head1ReverseLimit AndAlso HeadInverterDelay.Finished AndAlso IO.Head1ConnectorUp AndAlso IO.Head1Piston1Rev AndAlso
      IO.Head1Piston2Rev AndAlso (Not (IO.Head2Piston1Fwd AndAlso IO.Head2Piston2Fwd) OrElse (DistributorHead2.Head2Position > (DistributorHead1.Head1Position)))
    Head2FwdOK = IO.Head2ForwardLimit AndAlso HeadInverterDelay.Finished AndAlso IO.Head2ConnectorUp AndAlso IO.Head2Piston1Rev AndAlso
      IO.Head2Piston2Rev AndAlso (Not (IO.Head1Piston1Fwd AndAlso IO.Head1Piston2Fwd) OrElse (DistributorHead1.Head1Position > (DistributorHead2.Head2Position + 300)))
    Head2RevOK = IO.Head2ReverseLimit AndAlso HeadInverterDelay.Finished AndAlso IO.Head2ConnectorUp AndAlso IO.Head2Piston1Rev AndAlso
      IO.Head2Piston2Rev AndAlso (Not (IO.Head1Piston1Fwd AndAlso IO.Head1Piston2Fwd) OrElse (DistributorHead1.Head1Position > (DistributorHead2.Head2Position)))

    If Not IO.Head1Motor AndAlso Not IO.Head2Motor Then HeadInverterDelay.Seconds = 2 ' delay for head inverter to make starter pulls in first
    ' A bit belts & braces
    IO.HeadMotorFwd = NHalted AndAlso (Head1FwdOK AndAlso DistributorHead1.IoHeadMotorFwd) OrElse (Head2FwdOK AndAlso DistributorHead2.IoHeadMotorFwd)
    IO.HeadMotorRev = NHalted AndAlso (Head1RevOK AndAlso DistributorHead1.IoHeadMotorRev) OrElse (Head2RevOK AndAlso DistributorHead2.IoHeadMotorRev)
    IO.HeadMotorSpeed1 = NHalted AndAlso (IO.Head1Motor OrElse IO.Head2Motor)
    IO.HeadMotorSpeed2 = NHalted And False
    IO.HeadMotorSpeed3 = NHalted And False
    IO.DistributorAlarmLight = Parent.IsAlarmUnacknowledged OrElse Parent.IsSignalUnacknowledged


    ' Distributor IO
    With DistributorHead1
      IO.Head1Motor = NHalted AndAlso .IoHeadMotor ' andalso Head1FwdOK
      IO.Head1ConnectorUpSv = NHalted AndAlso .IoConnectorUp
      IO.Head1ConnectorDownSv = .IoConnectorDown AndAlso (IO.Head1Piston1Fwd OrElse IO.Head1Piston2Fwd)

      IO.Head1Piston1RevSv = NHalted AndAlso .IoPiston1Rev AndAlso IO.Head1ConnectorUp
      IO.Head1Piston1FwdSv = NHalted AndAlso .IoPiston1Fwd AndAlso IO.Head1ConnectorUp

      IO.Head1Piston2RevSv = NHalted AndAlso .IoPiston2Rev AndAlso IO.Head1ConnectorUp
      IO.Head1Piston2FwdSv = NHalted AndAlso .IoPiston2Fwd AndAlso IO.Head1ConnectorUp
      IO.Head1AddSv = NHalted AndAlso Head1Dispenser.IoCarrier AndAlso IO.Head1ConnectorDown
      IO.Head1AirBlowingSv = NHalted AndAlso Head1Dispenser.IoPurge AndAlso IO.Head1ConnectorDown
    End With

    With DistributorHead2
      IO.Head2Motor = NHalted AndAlso .IoHeadMotor
      IO.Head2ConnectorUpSv = NHalted AndAlso .IoConnectorUp
      IO.Head2ConnectorDownSv = NHalted AndAlso .IoConnectorDown AndAlso (IO.Head2Piston1Fwd OrElse IO.Head2Piston2Fwd)

      IO.Head2Piston1RevSv = NHalted AndAlso .IoH2Piston1Rev AndAlso IO.Head2ConnectorUp
      IO.Head2Piston1FwdSv = NHalted AndAlso .IoH2Piston1Fwd AndAlso IO.Head2ConnectorUp

      IO.Head2Piston2RevSv = NHalted AndAlso .IoH2Piston2Rev AndAlso IO.Head2ConnectorUp
      IO.Head2Piston2FwdSv = NHalted AndAlso .IoH2Piston2Fwd AndAlso IO.Head2ConnectorUp
      IO.Head2AddSv = NHalted AndAlso Head2Dispenser.IoCarrier AndAlso IO.Head2ConnectorDown
      IO.Head2AirBlowingSv = NHalted AndAlso Head2Dispenser.IoPurge AndAlso IO.Head2ConnectorDown
    End With
    IO.CarrierPump = NHalted AndAlso ((Head1Dispenser.IoCarrier AndAlso IO.Head1ConnectorDown) OrElse (Head2Dispenser.IoCarrier AndAlso IO.Head2ConnectorDown))
    ' Run alarms and set alarm light
    Alarms.Run()  ' TODO Move this

    ' Run remote function code - for mimics mainly
    'RunRemoteFunctions()

    ' Process remote pushbuttons
    ' Parent.PressButtons(IO.RemoteRun, IO.RemoteHalt, IO.EmergencyStop, False, False)
    ' Parent.PressButtons(False, False, IO.EmergencyStop, False, False)
    'Parent.PressButtons(False, False, False, False, False)
  End Sub
  Public Sub AutoScheduleProgram(Dyelot As String)
    Dim sql As String = Nothing
    Dyelot = Dyelot & "-PR"
    Try
      sql = "Insert Into Dyelots(Dyelot,ReDye,Machine,Program) VALUES ('" & Dyelot & "', 0, 'Local', 1)" ' SET Dyelot=" & "hi" & ",ReDye = 0,  Machine = Local,Program=1"
      Parent.DbExecute(sql)

    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
  End Sub
  Public ReadOnly Property CarrierTableXML As String
    Get
      Return DatabaseEAS.CarrierTableXML
    End Get
  End Property

  Private systemStartupTimer As New Timer With {.Seconds = 8}
  Public ReadOnly Property SystemStartup As Boolean
    Get
      Return Not systemStartupTimer.Finished
    End Get
  End Property
  Public Property SystemShutdown As Boolean

#Region " Control Methods "

  Public Sub StartUp() Implements ACControlCode.StartUp
    systemStartupTimer.Seconds = 8
  End Sub

  Public Sub ShutDown() Implements ACControlCode.ShutDown
    SystemShutdown = True
  End Sub

  Public Sub ProgramStart() Implements ACControlCode.ProgramStart
  End Sub

  Public Sub ProgramStop() Implements ACControlCode.ProgramStop
  End Sub

  Public Function ReadInputs(ByVal dinp() As Boolean, ByVal aninp() As Short, ByVal temp() As Short) As Boolean Implements ACControlCode.ReadInputs
    Return IO.ReadInputs(dinp, aninp, temp)
  End Function

  Public Sub WriteOutputs(ByVal dout() As Boolean, ByVal anout() As Short) Implements ACControlCode.WriteOutputs
    IO.WriteOutputs(dout, anout)
  End Sub

#End Region

#Region " Interface changes "

  Private Sub UpdateInterface()
    'The new buttons
    'Dim buttonIOLog As New ToolStripButton("IO Log", My.Resources.IO16x16)

    With Parent
      'Add buttons to Operator tool strip
      Parent.AddStandardButton(StandardButton.Mimic, ButtonPosition.Operator)
      Parent.AddStandardButton(StandardButton.Graph, ButtonPosition.Operator)
      Parent.AddStandardButton(StandardButton.History, ButtonPosition.Operator)
      Parent.AddStandardButton(StandardButton.WorkList, ButtonPosition.Operator)
      Parent.AddStandardButton(StandardButton.Program, ButtonPosition.Operator)
      'Add buttons to Expert tool strip
      .AddStandardButton(StandardButton.IO, ButtonPosition.Expert)
      .AddStandardButton(StandardButton.Variables, ButtonPosition.Expert)
      .AddStandardButton(StandardButton.Parameters, ButtonPosition.Expert)
      .AddStandardButton(StandardButton.Programs, ButtonPosition.Expert)
      'Add custom buttons to Expert tool strip
      '.AddButton(buttonIOLog, ButtonPosition.Expert, New ControlIOLog)
    End With
  End Sub

#End Region

#Region " Simulation Code "

  Friend ReadOnly Property DemoMode As Boolean
    Get
      Return (Settings.DemoMode = 1) OrElse (Parameters.DemoMode = 1)
    End Get
  End Property

  Private Sub RunSimulations()
    'RunDistributorSimulations()
  End Sub

  'Public Sub RunDistributorSimulations()
  '  IO.EncoderCount = CShort(SimulateDistributorEncoder(IO.EncoderCount))

  '  ' Some feedbacks
  '  IO.ConnectorDown = IO.ConnectorDownSv
  '  IO.ConnectorUp = IO.ConnectorUpSv
  '  IO.Piston1Fwd = IO.Piston1FwdSv
  '  IO.Piston1Rev = IO.Piston1RevSv
  '  IO.Piston2Fwd = IO.Piston2FwdSv
  '  IO.Piston2Rev = IO.Piston2RevSv

  'End Sub

  'Private Function SimulateDistributorEncoder(encoder As Integer) As Integer
  '  Static Timer As New Timer
  '  If Timer.Finished Then
  '    Timer.Milliseconds = 4
  '  Else
  '    Return encoder
  '  End If

  '  With Distributor
  '    If .IOHeadMotorFwd AndAlso IO.HeadMotorSpeed1 Then Return encoder + 1
  '    If .IOHeadMotorFwd AndAlso IO.HeadMotorSpeed2 Then Return encoder + 16
  '    If .IOHeadMotorFwd AndAlso IO.HeadMotorSpeed3 Then Return encoder + 64

  '    If .IOHeadMotorRev AndAlso IO.HeadMotorSpeed1 Then Return encoder - 2
  '    If .IOHeadMotorRev AndAlso IO.HeadMotorSpeed2 Then Return encoder - 16
  '    If .IOHeadMotorRev AndAlso IO.HeadMotorSpeed3 Then Return encoder - 64

  '    Return encoder
  '  End With
  'End Function

#End Region

End Class
