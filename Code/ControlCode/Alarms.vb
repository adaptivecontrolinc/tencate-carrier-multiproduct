Public NotInheritable Class Alarms : Inherits MarshalByRefObject
  ' For convenience
  Private controlCode As ControlCode

  Public SystemInDebugMode As Boolean
  Public SystemInTestMode As Boolean
  Public SystemInOverrideMode As Boolean

  Public PlcTimeout As Boolean
  Public EmergencyStop As Boolean

  Public SiloNotOpen As Boolean
  Public SiloFeedError As Boolean

  Public H1PositionError As Boolean
  Public H2PositionError As Boolean

  Public Sub New(ByVal controlCode As ControlCode)
    ' Save control code reference locally
    Me.controlCode = controlCode
  End Sub

  Public Sub Run()
    With controlCode
      If .SystemStartup Then Exit Sub
      If Not .IO.DistributorEmergencyStopOK Then
        EmergencyStop = True
      Else : EmergencyStop = False
      End If
      SystemInDebugMode = (.Parent.Mode = Mode.Debug)
      SystemInOverrideMode = (.Parent.Mode = Mode.Override)
      SystemInTestMode = (.Parent.Mode = Mode.Test)

      PlcTimeout = .IO.Plc1Timeout

      With .DistributorHead1
        H1PositionError = .AlarmPosition
      End With

      With .DistributorHead2
        H2PositionError = .AlarmPosition
      End With

    End With
  End Sub

End Class
