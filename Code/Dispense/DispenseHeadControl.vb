Public Class DispenseHeadControl : Inherits MarshalByRefObject
  Private controlcode As ControlCode

  Public Enum EState
    Off
    Disabled
    Idle
    Start
    DistributorWaitHome
    DistributorSetPosition
    DistributorWaitPosition
    CarrierStart
    Carrier
    RinseStart
    Rinse
    PurgeStart
    Purge
    FillStart
    Fill
    CompleteJob
    CancelJob
    DistributorSetHome
    Finished
    ErrorFlowmeter
    ErrorVolumeTooLow
    ErrorVolumeTooHigh
  End Enum
  Public Property State As EState
  Public Property Status As String
  Public Property Timer As New Timer With {.Seconds = 16}
  Public Property AlarmTimer As New Timer With {.Seconds = 16}

  Public Property DispenseJob As DispenseJob         ' 

  Public Property Machine As Integer             ' Destination Machine
  Public Property Tank As Integer                ' Destination Tank
  Public Property Head As Integer                ' Head to use
  Public Property Manual As Boolean              ' Manual or automatic dispense

  'Public Property CarrierGrams As Integer           ' grams of carrier to dispense
  'Public Property CarrierLiters As Double          ' liters of Carrier to dispense to get desired carrier grams
  Public Property GallonsRequested As Double         ' for display
  Public Property GallonsDispensed As Double

  Public Sub New(controlCode As ControlCode)
    Me.controlcode = controlCode
  End Sub

  Public Sub Start(Gallons As Double, machine As Integer, tank As Integer, headnumber As Integer)
    Dim newjob = New DispenseJob
    With newjob
      .Batch = "Manual"
      .Manual = True
      .Head = headnumber
      .Gallons = Gallons
      .Machine = machine
      .Tank = tank
    End With
    Start(newjob)
  End Sub

  Public Sub Start(newJob As DispenseJob)
    Me.DispenseJob = newJob
    With controlcode
      GallonsRequested = DispenseJob.Gallons
      Me.Machine = DispenseJob.Machine
      Me.Tank = DispenseJob.Tank
      Me.Head = DispenseJob.Head
      Me.Manual = DispenseJob.Manual

      Timer.Seconds = .Parameters.ValveDelay
      State = EState.Start

      ' Probably need to do more than this if this is an auto dispense
      If GallonsRequested < DispenseMin Then State = EState.ErrorVolumeTooLow
      If GallonsRequested > DispenseMax Then State = EState.ErrorVolumeTooHigh
    End With
  End Sub

  Public Sub Run(head As Integer)
    With controlcode

      Static StartState As EState
      Do
        StartState = State  ' loop until all state changes have completed
        Select Case State

          Case EState.Off
            If Timer.Finished Then State = EState.Idle
            Status = "Startup: " & Timer.ToString

          Case EState.Disabled
            Status = "Disabled"

          Case EState.Idle
            Status = "Idle"

          Case EState.Start
            If head = 1 Then
              .Head1Flowmeter.Reset()
            ElseIf head = 2 Then
              .Head2Flowmeter.Reset()
            End If
            If Timer.Finished Then
              State = EState.DistributorWaitHome
              Timer.Seconds = .Parameters.ValveDelay
              AlarmTimer.Seconds = DistributorAlarmTime
            End If
            Status = "Start: " & Timer.ToString

          Case EState.DistributorWaitHome
            If Timer.Finished Then
              If head = 1 Then
                If .DistributorHead1.AtHomePosition Then State = EState.DistributorSetPosition
              ElseIf head = 2 Then
                If .DistributorHead2.AtHomePosition Then State = EState.DistributorSetPosition
              End If
            End If
            Status = "Wait for distributor home: " & Timer.ToString


          Case EState.DistributorSetPosition
            If head = 1 Then .DistributorHead1.MoveDestination(Machine, Tank)
            If head = 2 Then .DistributorHead2.MoveDestination(Machine, Tank)

            Timer.Seconds = 30
            State = EState.DistributorWaitPosition
            Status = "Set distributor position: " & Timer.ToString


          Case EState.DistributorWaitPosition
            If Timer.Finished Then
              If (head = 1 AndAlso .DistributorHead1.AtPortPosition) OrElse (head = 2 AndAlso .DistributorHead2.AtPortPosition) Then
                State = EState.CarrierStart
                Timer.Seconds = .Parameters.ValveDelay
              End If
            End If
            Status = "Wait for distributor position: " & Timer.ToString

          Case EState.CarrierStart
            If Timer.Finished Then
              If head = 1 Then
                .Head1Flowmeter.Reset()
              ElseIf head = 2 Then
                .Head2Flowmeter.Reset()
              End If

              State = EState.Carrier
            End If
            Status = "Start dispense: " & Timer.ToString

          Case EState.Carrier
            If head = 1 Then
              Status = "Dispense " & ProductCodeDisplay & ":  " & .Head1Flowmeter.Gallons.ToString("#0") & " / " & GallonsRequested.ToString("#0") & " Gallons"
              If .Head1Flowmeter.Gallons >= GallonsRequested Then
                DispenseJob.FinalValue = (.Head1Flowmeter.Gallons * 8.34)
                Timer.Seconds = .Parameters.ValveDelay
                State = EState.PurgeStart
              End If
            ElseIf head = 2 Then
              Status = "Dispense " & ProductCodeDisplay & ":  " & .Head2Flowmeter.Gallons.ToString("#0") & " / " & GallonsRequested.ToString("#0") & " Gallons"
              If .Head2Flowmeter.Gallons >= GallonsRequested Then
                DispenseJob.FinalValue = (.Head2Flowmeter.Gallons * 8.34)
                Timer.Seconds = .Parameters.ValveDelay
                State = EState.PurgeStart
              End If
            End If


          Case EState.PurgeStart
            If Timer.Finished Then
              Timer.Seconds = PurgeTime
              State = EState.Purge
            End If
            Status = "Start purge: " & Timer.ToString

          Case EState.Purge
            If Timer.Finished Then
              Timer.Seconds = .Parameters.ValveDelay
              State = EState.CompleteJob
            End If
            Status = "Purge: " & Timer.ToString


          Case EState.CompleteJob
            CompleteCarrierJobAsync()
            Timer.Seconds = .Parameters.ValveDelay
            State = EState.DistributorSetHome

          Case EState.CancelJob
            CancelCarrierJobAsync()
            Timer.Seconds = .Parameters.ValveDelay
            State = EState.DistributorSetHome

          Case EState.DistributorSetHome
            If Timer.Finished Then
              If head = 1 Then
                .DistributorHead1.MoveHome(1)
              ElseIf head = 2 Then
                .DistributorHead2.MoveHome(2)
              End If
              Timer.Seconds = .Parameters.ValveDelay
              State = EState.Finished
            End If
            Status = "Home the distributor: " & Timer.ToString

          Case EState.Finished
            If Timer.Finished Then
              If head = 1 Then
                .DistributorHead1.Busy = False
              ElseIf head = 2 Then
                .DistributorHead2.Busy = False
              End If
              State = EState.Idle
            End If
            Status = "Finished: " & Timer.ToString


          Case EState.ErrorFlowmeter
            Status = "Flowmeter Error"

          Case EState.ErrorVolumeTooLow
            Status = "Dispense volume too low"

          Case EState.ErrorVolumeTooHigh
            Status = "Dispense volume too high"
        End Select
      Loop Until State = StartState


      If Not Enabled Then
        State = EState.Disabled
      Else
        If State = EState.Disabled Then State = EState.Idle
      End If
    End With
  End Sub

  Public Sub Cancel()
    State = EState.Idle
  End Sub

  Public Sub Abort()
    With controlcode
      State = EState.CancelJob
      Timer.Seconds = Math.Max(.Parameters.ValveDelay, 4) ' at least four seconds
    End With
  End Sub

  Private Sub CompleteCarrierJobAsync()
    Dim completedCarrierJob = DispenseJob
    If completedCarrierJob.Manual Then Return
    Threading.ThreadPool.QueueUserWorkItem(AddressOf CompleteJob, completedCarrierJob)
  End Sub

  Private Sub CompleteJob(stateInfo As Object)
    Dim completedCarrierJob = CType(stateInfo, DispenseJob)
    If completedCarrierJob.Manual Then Return
    controlcode.DispenseScheduler.SetCarrierJobComplete(completedCarrierJob)
  End Sub

  Private Sub CancelCarrierJobAsync()
    Dim cancelledCarrierJob = DispenseJob
    If cancelledCarrierJob.Manual Then Return
    Threading.ThreadPool.QueueUserWorkItem(AddressOf CancelJob, cancelledCarrierJob)
  End Sub

  Private Sub CancelJob(stateInfo As Object)
    Dim cancelledCarrierJob = CType(stateInfo, DispenseJob)
    If cancelledCarrierJob.Manual Then Return
    controlcode.DispenseScheduler.SetCarrierJobManual(cancelledCarrierJob)
  End Sub

  Public ReadOnly Property Alarm As Boolean
    Get
      Return State = EState.ErrorFlowmeter OrElse State = EState.ErrorVolumeTooLow OrElse State = EState.ErrorVolumeTooHigh
    End Get
  End Property

  Public ReadOnly Property AlarmDistributor As Boolean
    Get
      Return (State >= EState.DistributorWaitHome) AndAlso (State <= EState.DistributorWaitPosition) AndAlso AlarmTimer.Finished
    End Get
  End Property

  Public ReadOnly Property Idle As Boolean
    Get
      Return (State = EState.Idle)
    End Get
  End Property

  Public ReadOnly Property Active As Boolean
    Get
      Return (State > EState.Idle)
    End Get
  End Property

  Public ReadOnly Property Finished As Boolean
    Get
      Return (State = EState.Finished)
    End Get
  End Property

  Public ReadOnly Property [Error] As Boolean
    Get
      Return (State = EState.ErrorFlowmeter)
    End Get
  End Property

  Public ReadOnly Property Dispensing As Boolean
    Get
      Return State > EState.Off AndAlso State <= EState.Carrier
    End Get
  End Property


  Public ReadOnly Property IoDispense As Boolean
    Get
      Return (State >= EState.CarrierStart AndAlso State <= EState.Fill)
    End Get
  End Property

  Public ReadOnly Property IoCarrier As Boolean
    Get
      Return (State = EState.Carrier)
    End Get
  End Property

  Public ReadOnly Property IoRinse As Boolean
    Get
      Return (State = EState.Rinse) OrElse (State = EState.Fill)
    End Get
  End Property

  Public ReadOnly Property IoPurge As Boolean
    Get
      Return (State = EState.Purge)
    End Get
  End Property

#Region " PROPERTIES "

  Private enabled_ As Boolean
  Public Property Enabled As Boolean
    Get
      Return enabled_
    End Get
    Set(value As Boolean)
      enabled_ = value
    End Set
  End Property

  Private productCode_ As Integer
  Public Property ProductCode As Integer
    Get
      Return productCode_
    End Get
    Set(value As Integer)
      productCode_ = value
    End Set
  End Property

  Public ReadOnly Property ProductCodeDisplay As String
    Get
      ' TODO - validate product code possibilities (Carrier Code = 0701)
      If ProductCode < 1000 Then
        Return "0" & ProductCode.ToString
      Else
        Return ProductCode.ToString
      End If
    End Get
  End Property


  Public Property DispenseMin As Integer
  Public Property DispenseMax As Integer
  Public Property PurgeTime As Integer
  Public Property DispenseAlarmTime As Integer
  Public Property DistributorAlarmTime As Integer

#End Region

End Class
