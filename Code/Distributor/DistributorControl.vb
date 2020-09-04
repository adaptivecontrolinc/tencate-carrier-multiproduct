Public Class DistributorControl : Inherits MarshalByRefObject
  Private controlCode As ControlCode

  Public Enum EState
    Off
    CheckHome

    StartMove
    ConnectorUp
    TraverseRetract

    HeadForward
    HeadForwardJog
    HeadReverse
    HeadReverseJog
    CheckPosition

    TraverseExtend
    ConnectorDown

    AtPortPosition
    AtHomePosition
    AtTravelPosition

    [Error]
  End Enum
  Property State As EState
  Property Timer As New Timer With {.Seconds = 16}
  Property AlarmTimer As New Timer
  Property CreepTimer As New Timer
  Property Status As String
  Property Enabled As Boolean
  Property Busy As Boolean

  Property Pistons As DistributorPistons

  Property Ports As DistributorPorts
  Property TargetPort As DistributorPort

  Public Sub New(controlCode As ControlCode)
    Me.controlCode = controlCode
    Pistons = New DistributorPistons
    Ports = New DistributorPorts
  End Sub

  Public Sub MoveHome(HeadNumber As Integer)
    With controlCode
      TargetPort = Ports.Home(HeadNumber)
      If TargetPort IsNot Nothing Then
        State = EState.StartMove
        Timer.Seconds = .Parameters.PistonDelay
      End If
    End With
  End Sub

  Public Sub MoveTravel()
    With controlCode
      TargetPort = Ports.Travel
      If TargetPort IsNot Nothing Then
        State = EState.StartMove
        Timer.Seconds = .Parameters.PistonDelay
      End If
    End With
  End Sub

  Public Sub MoveDestination(machine As Integer, tank As Integer)
    With controlCode
      TargetPort = Ports(machine, tank)
      If TargetPort IsNot Nothing Then
        State = EState.StartMove
        Timer.Seconds = .Parameters.PistonDelay
      End If
    End With
  End Sub

  Public Sub MovePort(portNumber As Integer)
    With controlCode
      TargetPort = Ports(portNumber)
      If TargetPort IsNot Nothing Then
        State = EState.StartMove
        Timer.Seconds = .Parameters.PistonDelay
      End If
    End With
  End Sub

  Public Sub MovePort(port As DistributorPort)
    With controlCode
      TargetPort = port
      If TargetPort IsNot Nothing Then
        State = EState.StartMove
        Timer.Seconds = .Parameters.PistonDelay
      End If
    End With
  End Sub

  Public Sub Run(HeadNumber As Integer)

    With controlCode
      ' Check parameters each scan
      Ports.UpdatePortPositions(.Parameters.PortPosition)

      ' For convenience
      Dim target As Integer
      Dim delta As Integer

      ' Loop until state = start state so we have completed all state transitions 
      ' - stops possible IO flicker
      Static StartState As EState
      Do
        StartState = State
        Select Case State

          Case EState.Off
            If Timer.Finished Then
              State = EState.CheckHome
              Timer.Seconds = .Parameters.PistonDelay
            End If
            Status = "Startup:" & Timer.ToString(1)

          Case EState.CheckHome
            If Timer.Finished Then
              If HeadNumber = 1 Then
                If .IO.Head1Home Then
                  .Head1Encoder.SetHome()
                  MoveHome(1)
                End If
              ElseIf HeadNumber = 2 Then
                If .IO.Head2Home Then
                  .Head2Encoder.SetHome()
                  MoveHome(2)
                End If
              End If
            End If
            Status = "Check Home Position: " & Timer.ToString

          Case EState.StartMove
            If Timer.Finished Then
              Pistons.MoveConnectorUp()
              State = EState.ConnectorUp
              Timer.Seconds = .Parameters.PistonDelay
              AlarmTimer.Seconds = .Parameters.PositionAlarmTime
            End If
            Status = "Start move to travel position: " & Timer.ToString

          Case EState.ConnectorUp
            If Timer.Finished Then
              If HeadNumber = 1 Then
                If Head1ConnectorPosition = 0 Then
                  Pistons.MoveTraverseRetract()
                  State = EState.TraverseRetract
                  Timer.Seconds = .Parameters.PistonDelay
                End If

              ElseIf HeadNumber = 2 Then
                If Head2ConnectorPosition = 0 Then
                  Pistons.MoveTraverseRetract()
                  State = EState.TraverseRetract
                  Timer.Seconds = .Parameters.PistonDelay
                End If

              End If
            End If
            Status = "Move connector to travel position: " & Timer.ToString

          Case EState.TraverseRetract
            If Timer.Finished Then
              If HeadNumber = 1 Then
                Status = "H1 Move piston to travel position: " & Timer.ToString
                If Head1TravelPosition Then
                  If TargetPort.Travel Then
                    State = EState.AtTravelPosition
                  ElseIf Not .DistributorHead2.IoHeadMotor Then
                    State = EState.HeadForward
                    If TargetPort.Head < Head1Position Then State = EState.HeadReverse
                  End If
                End If
              ElseIf HeadNumber = 2 Then
                Status = "H2 Move piston to travel position: " & Timer.ToString
                If Head2TravelPosition Then
                  If TargetPort.Travel Then
                    State = EState.AtTravelPosition
                  ElseIf Not .DistributorHead1.IoHeadMotor Then
                    State = EState.HeadForward
                    If TargetPort.Head < Head2Position Then State = EState.HeadReverse
                  End If
                End If
              End If
            End If



          Case EState.HeadForward
            target = TargetPort.Head - .Parameters.CreepRange
            If HeadNumber = 1 Then
              Status = "H1 forward: " & Head1Position.ToString & " / " & TargetPort.Head.ToString
              If Head1Position >= target Then
                State = EState.CheckPosition
                Timer.Seconds = .Parameters.ValveDelay
              End If
            ElseIf HeadNumber = 2 Then
              Status = "H2 forward: " & Head2Position.ToString & " / " & TargetPort.Head.ToString
              If Head2Position >= target Then
                State = EState.CheckPosition
                Timer.Seconds = .Parameters.ValveDelay
              End If
            End If

          Case EState.HeadForwardJog

            target = TargetPort.Head - .Parameters.CreepRange
            If HeadNumber = 1 Then
              delta = Math.Abs(TargetPort.Head - Head1Position)
              Status = "H1 jog forward: " & Head1Position.ToString & " / " & TargetPort.Head.ToString
              If (delta <= .Parameters.CountTolerance) OrElse CreepTimer.Finished Then
                State = EState.CheckPosition
                Timer.Seconds = .Parameters.ValveDelay
              End If
            ElseIf HeadNumber = 2 Then
              delta = Math.Abs(TargetPort.Head - Head2Position)
              Status = "H2 jog forward: " & Head2Position.ToString & " / " & TargetPort.Head.ToString
              If (delta <= .Parameters.CountTolerance) OrElse CreepTimer.Finished Then
                State = EState.CheckPosition
                Timer.Seconds = .Parameters.ValveDelay
              End If
            End If



          Case EState.HeadReverse
            target = TargetPort.Head + .Parameters.CreepRange
            If HeadNumber = 1 Then
              Status = "H1 reverse: " & Head1Position.ToString & " / " & TargetPort.Head.ToString
              If Head1Position <= target Then
                State = EState.CheckPosition
                Timer.Seconds = .Parameters.ValveDelay
              End If
            ElseIf HeadNumber = 2 Then
              Status = "H2 reverse: " & Head2Position.ToString & " / " & TargetPort.Head.ToString
              If Head2Position <= target Then
                State = EState.CheckPosition
                Timer.Seconds = .Parameters.ValveDelay
              End If
            End If



          Case EState.HeadReverseJog

            'target = TargetPort.Head + .Parameters.CreepRange
            If HeadNumber = 1 Then
              delta = Math.Abs(TargetPort.Head - Head1Position)
              Status = "H1 jog reverse: " & Head1Position.ToString & " / " & TargetPort.Head.ToString
              If (delta <= .Parameters.CountTolerance) OrElse CreepTimer.Finished Then
                State = EState.CheckPosition
                Timer.Seconds = .Parameters.ValveDelay
              End If
            ElseIf HeadNumber = 2 Then
              delta = Math.Abs(TargetPort.Head - Head2Position)
              Status = "H2 jog reverse: " & Head2Position.ToString & " / " & TargetPort.Head.ToString
              If (delta <= .Parameters.CountTolerance) OrElse CreepTimer.Finished Then
                State = EState.CheckPosition
                Timer.Seconds = .Parameters.ValveDelay
              End If
            End If


          Case EState.CheckPosition
            If HeadNumber = 1 Then
              Status = "H1 Check position: " & Head1Position.ToString & " / " & TargetPort.Head.ToString
              delta = Math.Abs(TargetPort.Head - Head1Position)
              If Timer.Finished Then
                If delta <= .Parameters.CountTolerance Then
                  Pistons.MoveTraverse(TargetPort.Traverse)
                  State = EState.TraverseExtend
                  Timer.Seconds = .Parameters.PistonDelay
                Else
                  'CreepTimer.Milliseconds = Math.Max(.Parameters.CreepTime, 2000)
                  CreepTimer.Milliseconds = .Parameters.CreepTime
                  State = EState.HeadForwardJog
                  If TargetPort.Head < Head1Position Then State = EState.HeadReverseJog
                End If
              End If
            ElseIf HeadNumber = 2 Then
              Status = "H2 Check position: " & Head2Position.ToString & " / " & TargetPort.Head.ToString
              delta = Math.Abs(TargetPort.Head - Head2Position)
              If Timer.Finished Then
                If delta <= .Parameters.CountTolerance Then
                  Pistons.MoveTraverse(TargetPort.Traverse)
                  State = EState.TraverseExtend
                  Timer.Seconds = .Parameters.PistonDelay
                Else
                  'CreepTimer.Milliseconds = Math.Max(.Parameters.CreepTime, 2000)
                  CreepTimer.Milliseconds = .Parameters.CreepTime
                  State = EState.HeadForwardJog
                  If TargetPort.Head < Head2Position Then State = EState.HeadReverseJog
                End If
              End If
            End If



          Case EState.TraverseExtend
            If Timer.Finished Then
              If HeadNumber = 1 Then
                Status = "H1 move piston to target port: " & Timer.ToString
                If Head1TraversePosition = TargetPort.Traverse Then
                  Pistons.MoveConnector(TargetPort.Connector)
                  State = EState.ConnectorDown
                  Timer.Seconds = .Parameters.PistonDelay
                End If
              ElseIf HeadNumber = 2 Then
                Status = "H2 move piston to target port: " & Timer.ToString
                If Head2TraversePosition = TargetPort.Traverse Then
                  Pistons.MoveConnector(TargetPort.Connector)
                  State = EState.ConnectorDown
                  Timer.Seconds = .Parameters.PistonDelay
                End If
              End If
            End If

          Case EState.ConnectorDown
            If Timer.Finished Then
              If HeadNumber = 1 Then
                If Head1ConnectorPosition = TargetPort.Connector Then
                  State = EState.AtPortPosition
                  Timer.Seconds = .Parameters.PistonDelay
                End If
              ElseIf HeadNumber = 2 Then
                If Head2ConnectorPosition = TargetPort.Connector Then
                  State = EState.AtPortPosition
                  Timer.Seconds = .Parameters.PistonDelay
                End If
              End If
            End If
            Status = "Move connector down: " & Timer.ToString



          Case EState.AtPortPosition
            If HeadNumber = 1 Then
              If TargetPort.PortNumber = 0 Then State = EState.AtTravelPosition
              If TargetPort.PortNumber = 1 Then State = EState.AtHomePosition
              Status = "At Position " & TargetPort.PortNumber.ToString
            End If
            If HeadNumber = 2 Then
              If TargetPort.PortNumber = 0 Then State = EState.AtTravelPosition
              If TargetPort.PortNumber = 2 Then State = EState.AtHomePosition
              Status = "At Position " & TargetPort.PortNumber.ToString
            End If

          Case EState.AtHomePosition
            Status = "At Home"

          Case EState.AtTravelPosition
            Status = "At Travel Position"

        End Select
      Loop Until State = StartState ' Loop until state changes have completed - stops potential IO flicker

    End With
  End Sub

  Public ReadOnly Property Head1TravelPosition() As Boolean
    Get
      Return Head1ConnectorPosition = 0 AndAlso Head1TraversePosition = 0
    End Get
  End Property
  Public ReadOnly Property Head2TravelPosition() As Boolean
    Get
      Return Head2ConnectorPosition = 0 AndAlso Head2TraversePosition = 0
    End Get
  End Property

  ' Travel position is 0
  Public ReadOnly Property Head1ConnectorPosition() As Integer
    Get
      With controlCode
        If .IO.Head1ConnectorDown AndAlso Not .IO.Head1ConnectorUp Then Return 1 ' these are feedbacks
        If .IO.Head1ConnectorUp AndAlso Not .IO.Head1ConnectorDown Then Return 0
        Return -1 ' Error
      End With
    End Get
  End Property
  ' Travel position is 0
  Public ReadOnly Property Head2ConnectorPosition() As Integer
    Get
      With controlCode
        If .IO.Head2ConnectorDown AndAlso Not .IO.Head2ConnectorUp Then Return 1 ' these are feedbacks
        If .IO.Head2ConnectorUp AndAlso Not .IO.Head2ConnectorDown Then Return 0
        Return -1 ' Error
      End With
    End Get
  End Property
  ' Travel position is 0
  Public ReadOnly Property Head1TraversePosition() As Integer
    Get
      With controlCode
        If .IO.Head1Piston1Fwd AndAlso .IO.Head1Piston2Fwd Then Return 2 ' these are feedbacks
        If .IO.Head1Piston1Fwd AndAlso .IO.Head1Piston2Rev Then Return 1
        If .IO.Head1Piston1Rev AndAlso .IO.Head1Piston2Fwd Then Return 1
        If .IO.Head1Piston1Rev AndAlso .IO.Head1Piston2Rev Then Return 0
        Return -1
      End With
    End Get
  End Property
  Public ReadOnly Property Head2TraversePosition() As Integer
    Get
      With controlCode
        If .IO.Head2Piston1Fwd AndAlso .IO.Head2Piston2Fwd Then Return 1 ' these are feedbacks
        If .IO.Head2Piston1Fwd AndAlso .IO.Head2Piston2Rev Then Return 2
        If .IO.Head2Piston1Rev AndAlso .IO.Head2Piston2Fwd Then Return 2
        If .IO.Head2Piston1Rev AndAlso .IO.Head2Piston2Rev Then Return 0
        Return -1
      End With
    End Get
  End Property
  ' 0 = Home
  Public ReadOnly Property Head1Position As Integer
    Get
      With controlCode
        Return .Head1Encoder.Position
      End With
    End Get
  End Property
  ' 0 = Home
  Public ReadOnly Property Head2Position As Integer
    Get
      With controlCode
        Return .Head2Encoder.Position
      End With
    End Get
  End Property

  Public ReadOnly Property Available As Boolean
    Get
      Return Enabled And Not Busy
    End Get
  End Property
  Public ReadOnly Property AtPortPosition As Boolean
    Get
      Return State = EState.AtPortPosition
    End Get
  End Property

  Public ReadOnly Property AtHomePosition As Boolean
    Get
      Return State = EState.AtHomePosition
    End Get
  End Property

  Public ReadOnly Property AtTravelPosition As Boolean
    Get
      Return State = EState.AtTravelPosition
    End Get
  End Property

  Public ReadOnly Property Position As String
    Get
      If AtHomePosition Then Return "Home,0-0"
      If AtPortPosition Then Return "Position," & TargetPort.Machine.ToString & "-" & TargetPort.Tank.ToString
      If AtTravelPosition Then Return "Travel"

      Return "Travel"
    End Get
  End Property

  Public ReadOnly Property AlarmPosition As Boolean
    Get
      Return (State > EState.StartMove) AndAlso (State <= EState.ConnectorDown) AndAlso AlarmTimer.Finished
    End Get
  End Property

#Region " IO Properties "

  Public ReadOnly Property IoConnectorUp As Boolean
    Get
      Return (State > EState.CheckHome) AndAlso (State < EState.Error) AndAlso Pistons.ConnectorUp
    End Get
  End Property

  Public ReadOnly Property IoConnectorDown As Boolean
    Get
      Return (State > EState.CheckHome) AndAlso (State < EState.Error) AndAlso Pistons.ConnectorDown
    End Get
  End Property

  Public ReadOnly Property IoPiston1Rev As Boolean
    Get
      Return (State > EState.CheckHome) AndAlso (State < EState.Error) AndAlso Pistons.Traverse1Rev
    End Get
  End Property

  Public ReadOnly Property IoPiston1Fwd As Boolean
    Get
      Return (State > EState.CheckHome) AndAlso (State < EState.Error) AndAlso Pistons.Traverse1Fwd
    End Get
  End Property

  Public ReadOnly Property IoPiston2Rev As Boolean
    Get
      Return (State > EState.CheckHome) AndAlso (State < EState.Error) AndAlso Pistons.Traverse2Rev
    End Get
  End Property

  Public ReadOnly Property IoPiston2Fwd As Boolean
    Get
      Return (State > EState.CheckHome) AndAlso (State < EState.Error) AndAlso Pistons.Traverse2Fwd
    End Get
  End Property
  Public ReadOnly Property IoH2Piston1Fwd As Boolean
    Get
      Return (State > EState.CheckHome) AndAlso (State < EState.Error) AndAlso Pistons.H2Traverse1Fwd
    End Get
  End Property
  Public ReadOnly Property IoH2Piston1Rev As Boolean
    Get
      Return (State > EState.CheckHome) AndAlso (State < EState.Error) AndAlso Pistons.H2Traverse1Rev
    End Get
  End Property
  Public ReadOnly Property IoH2Piston2Fwd As Boolean
    Get
      Return (State > EState.CheckHome) AndAlso (State < EState.Error) AndAlso Pistons.H2Traverse2Fwd
    End Get
  End Property
  Public ReadOnly Property IoH2Piston2Rev As Boolean
    Get
      Return (State > EState.CheckHome) AndAlso (State < EState.Error) AndAlso Pistons.H2Traverse2Rev
    End Get
  End Property
  Public ReadOnly Property IoHeadMotor As Boolean
    Get
      Return (State = EState.CheckPosition) OrElse IoHeadMotorFwd OrElse IoHeadMotorRev
    End Get
  End Property

  Public ReadOnly Property IoHeadMotorFwd As Boolean
    Get
      Return State = EState.HeadForward OrElse State = EState.HeadForwardJog
    End Get
  End Property

  Public ReadOnly Property IoHeadMotorRev As Boolean
    Get
      Return State = EState.HeadReverse OrElse State = EState.HeadReverseJog
    End Get
  End Property

#End Region

End Class

#Region " Parameters "

Partial Public Class Parameters

  <Parameter(0, 32768), Category("Distributor"), Description("Delay time to check piston actuation, seconds")> _
  Public PistonDelay As Integer

  <Parameter(0, 32768), Category("Distributor"), Description("Raise an alarm if it takes longer than this to move to a new distributor position, seconds")> _
  Public PositionAlarmTime As Integer

  <Parameter(0, 50), Category("Distributor"), Description("If the head is within this many counts of the target port, jog the motor.")> _
  Public CreepRange As Integer
 
  <Parameter(0, 1000), Category("Distributor"), Description("If the head is close, jog the motor for this many milliseconds.")> _
  Public CreepTime As Integer

  <Parameter(0, 50), Category("Distributor"), Description("If the head is within this many counts of the target port, it is close enough.")> _
  Public CountTolerance As Integer

  <Parameter(0, 1), Category("Distributor"), Description("Enable or disable dispense button on dissolver mimic screen (0 = disable, 1 = enable)")> _
  Public PortPosition(32) As Integer

End Class

#End Region