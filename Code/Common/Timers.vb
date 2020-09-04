Module Timers
  ' Set this at the beginning of each control code run so we only call the date once a scan
  '   date.UtcNow takes longer than you would expect
  Private UtcDateNow_ As Date = Date.UtcNow
  Public Property UtcDateNow As Date
    Get
      Return UtcDateNow_
    End Get
    Set(value As Date)
      UtcDateNow_ = value
      LocalDateNow_ = value.ToLocalTime
    End Set
  End Property

  Private LocalDateNow_ As Date = Date.Now
  Public ReadOnly Property LocalDateNow As Date
    Get
      Return LocalDateNow_
    End Get
  End Property
End Module

Public Class Timer : Inherits MarshalByRefObject

  Private Property startTimeUtc As Date = Date.UtcNow
  Private Property endTimeUtc As Date = Date.UtcNow
  Private Property pauseTimeUtc As Date
  Private Property pauseTimeRemaining As TimeSpan

  'For compatibility
  Public Property TimeRemaining() As Integer
    Get
      Return Seconds
    End Get
    Set(ByVal value As Integer)
      Seconds = value
    End Set
  End Property

  Public Property Seconds() As Integer
    Get
      Dim returnValue As Integer
      If pauseTimeUtc = Nothing Then
        returnValue = Convert.ToInt32(endTimeUtc.Subtract(Timers.UtcDateNow).TotalSeconds)
        If returnValue > 0 Then Return returnValue
      Else
        returnValue = Convert.ToInt32(pauseTimeRemaining.TotalSeconds)
        If returnValue > 0 Then Return returnValue
      End If
      Return 0
    End Get
    Set(ByVal value As Integer)
      startTimeUtc = Timers.UtcDateNow
      endTimeUtc = startTimeUtc.AddSeconds(value)
    End Set
  End Property

  'Declared friend so we don't seen all the millisecond count down in the histories
  Friend Property Milliseconds() As Integer
    Get
      Dim returnValue As Integer
      If pauseTimeUtc = Nothing Then
        returnValue = Convert.ToInt32(endTimeUtc.Subtract(Timers.UtcDateNow).TotalMilliseconds)
        If returnValue > 0 Then Return returnValue
      Else
        returnValue = Convert.ToInt32(pauseTimeRemaining.TotalMilliseconds)
        If returnValue > 0 Then Return returnValue
      End If
      Return 0
    End Get
    Set(ByVal value As Integer)
      startTimeUtc = Timers.UtcDateNow
      endTimeUtc = startTimeUtc.AddMilliseconds(value)
    End Set
  End Property

  Public ReadOnly Property Finished() As Boolean
    Get
      If pauseTimeUtc = Nothing Then Return (Timers.UtcDateNow >= endTimeUtc)
      Return False
    End Get
  End Property

  Public ReadOnly Property Paused() As Boolean
    Get
      Return (pauseTimeUtc <> Nothing)
    End Get
  End Property

  Public Sub Pause()
    If pauseTimeUtc = Nothing Then
      pauseTimeUtc = Timers.UtcDateNow
      pauseTimeRemaining = endTimeUtc.Subtract(pauseTimeUtc)
    End If
  End Sub

  Public Sub Restart()
    endTimeUtc.Add(pauseTimeRemaining)
    pauseTimeUtc = Nothing
  End Sub

  Public Overloads Function ToString(padSpaces As Integer) As String
    If padSpaces > 0 Then
      Dim timerString As String = Me.ToString
      Return timerString.PadLeft(timerString.Length + padSpaces)
    End If
    Return Me.ToString
  End Function

  Public Overrides Function ToString() As String
    Try
      Dim s As Integer = Me.Seconds
      Dim ts As New TimeSpan(0, 0, s)
      Select Case ts.TotalSeconds
        Case Is >= 86400
          Return ts.Days.ToString("00") & ":" & ts.Hours.ToString("00") & "h"
        Case 3600 To 86399
          Return ts.Hours.ToString("00") & ":" & ts.Minutes.ToString("00") & "m"
        Case 1 To 3599
          Return ts.Minutes.ToString("00") & ":" & ts.Seconds.ToString("00") & "s"
        Case Else
          Return "00:00s"
      End Select
    Catch ex As Exception
      'TODO Log Error
    End Try
    Return Nothing
  End Function

  Friend Function ToStringMs(padSpaces As Integer) As String
    If padSpaces > 0 Then
      Dim timerString As String = Me.ToStringMs
      Return timerString.PadLeft(timerString.Length + padSpaces)
    End If
    Return Me.ToString
  End Function

  Friend Function ToStringMs() As String
    Try
      Dim ms As Integer = Me.Milliseconds

      'If there is more than 10 seconds left then just return the normal formatting
      If ms > 9999 Then Return Me.ToString

      'Return the time left formatted for milliseonds
      If ms > 0 Then
        Return ms.ToString & "ms"
      Else
        Return "00:00s"
      End If

    Catch ex As Exception
      'Ignore errors
    End Try
    Return Nothing
  End Function

End Class

Public Class TimerUp

  Public Property StartTimeUtc As Date
  Public Property StartTimeLocal As Date

  Public Sub Start()
    Me.StartTimeUtc = UtcDateNow
    Me.StartTimeLocal = LocalDateNow
  End Sub

  Public Sub [Stop]()
    Me.StartTimeUtc = Nothing
  End Sub

  Friend ReadOnly Property Milliseconds As Integer
    Get
      If StartTimeUtc = Nothing Then Return 0
      Return CInt(UtcDateNow.Subtract(StartTimeUtc).TotalMilliseconds)
    End Get
  End Property

  Public ReadOnly Property Seconds As Integer
    Get
      If StartTimeUtc = Nothing Then Return 0
      Return CInt(UtcDateNow.Subtract(StartTimeUtc).TotalSeconds)
    End Get
  End Property

  Public ReadOnly Property Running() As Boolean
    Get
      Return Not Stopped
    End Get
  End Property

  Public ReadOnly Property Stopped As Boolean
    Get
      Return (StartTimeUtc = Nothing)
    End Get
  End Property
End Class