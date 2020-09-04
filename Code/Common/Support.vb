Public Module TickCountModule
  Friend TickCount As UInt32
End Module


Public Class Flasher
  Private startTime_ As UInt32

  Public Sub Flash(ByRef variable As Boolean, ByVal onMilliSeconds As Integer)
    Flash(variable, onMilliSeconds, onMilliSeconds)
  End Sub

  Public Sub Flash(ByRef variable As Boolean, ByVal onMilliSeconds As Integer, ByVal offMilliSeconds As Integer)
    ' Initialise StartTime
    Dim elapsed As UInt32
    If startTime_ = 0 Then
      startTime_ = TickCount
    Else
      elapsed = TickCount - startTime_
    End If

    ' Generate a number that ranges from 0 to the sum
    Dim x As UInt32 = elapsed Mod CType(onMilliSeconds + offMilliSeconds, UInt32)
    variable = (x < onMilliSeconds)
  End Sub
End Class

