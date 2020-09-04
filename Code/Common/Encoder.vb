Public Class Encoder : Inherits MarshalByRefObject
  Private controlCode As ControlCode

  Property Encoder As Short
  Property Count As Integer

  Property Home As Integer
  Property Position As Short

  Private MaxCount As Integer = CInt(2 ^ 16)
  Private startup As Boolean = True

  Public Sub New(controlcode As ControlCode)
    Me.controlCode = controlcode
  End Sub

  Public Sub Run(encoder As Short)
    If startup Then FirstRun() ' load initial values

    Me.Encoder = encoder
    Me.Count = CType(encoder, UShort)

    If Count >= Home Then
      Position = CType(Count - Home, Short)
    Else
      Position = CType((MaxCount - Home) + Count, Short)   ' counter has wrapped
    End If

    ' Always set this so they can't accidentally overwrite it
    controlCode.Parameters.EncoderHome = Home
  End Sub

  Public Sub SetHome()
    Me.Home = Me.Count
    controlCode.Parameters.EncoderHome = Me.Home
  End Sub

  Public Sub SetHome(counter As Integer)
    Me.Count = counter
    Me.Home = counter
  End Sub

  Private Sub FirstRun()
    startup = False
    Home = controlCode.Parameters.EncoderHome
  End Sub

End Class

#Region " Parameters "

Partial Public Class Parameters

  <Parameter(0, 32768), Category("Encoder"), Description("Encoder value at home position)")> _
  Public EncoderHome As Integer

End Class

#End Region