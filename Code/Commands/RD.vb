' Main dispense command
<Command("Run", "", "", "", "'1*60"), Description("Run Dissolver Job"), Category("System Commands")>
Public Class RD : Implements ACCommand

  ' Keep a local copy for convenience
  Private ReadOnly controlCode As ControlCode

  Public Enum EState
    Off
    Start
    Run
    Abort
    Done
  End Enum

  Public Sub New(ByVal controlCode As ControlCode)
    Me.controlCode = controlCode
  End Sub

  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
    ' Some code
  End Sub

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With Me.controlCode
      ' Reset timer just in case
      Timer.Seconds = 0

      ' Start by trying to load the current job
      State = EState.Start
    End With
    Return False
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With Me.controlCode
      Select Case State

        Case EState.Off

        Case EState.Start

        Case EState.Run

        Case EState.Done

      End Select
    End With
    Return False
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    State = EState.Off
  End Sub

  Public Sub Abort()
    If (State <> EState.Off) Then State = EState.Abort
  End Sub


  Public ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return (State <> EState.Off)
    End Get
  End Property

  Public ReadOnly Property IsOverrun() As Boolean
    Get
      Return (State = EState.Run) AndAlso Timer.Finished
    End Get
  End Property

  Private state_ As EState
  Public Property State() As EState
    Get
      Return state_
    End Get
    Private Set(ByVal value As EState)
      state_ = value
    End Set
  End Property

  Private timer_ As New Timer
  Public ReadOnly Property Timer() As Timer
    Get
      Return timer_
    End Get
  End Property
End Class

Partial Public Class ControlCode
  Public DP As New RD(Me)
End Class
