Partial Class ControlCode

  '/ doesn't round off the tenth while \ does => used to display strings on drawscreen
  <ScreenButton("Main", 1, ButtonImage.Vessel)> _
  Public Sub DrawScreen(ByVal screen As Integer, ByVal row() As String) Implements ACControlCode.DrawScreen

  End Sub

End Class
