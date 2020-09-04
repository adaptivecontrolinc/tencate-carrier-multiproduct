Public Class FlowmeterAnalog : Inherits MarshalByRefObject

  Public Property SampleRate As Integer = 100          ' How often we sample in milliseconds
  Friend Property SampleSum As Integer                 ' Running sum (sum every sample milliseconds)
  Friend Property SampleCount As Integer               ' Number of samples we've taken

  Public Grams As Integer
  Public GramsPerSecond As Integer

  Public Property Timer As New TimerUp

  Public ReadOnly Property Liters As Double
    Get
      Return Grams / 1000
    End Get
  End Property

  Public ReadOnly Property Gallons As Double
    Get
      Return Liters / Utilities.Conversions.LitersToGallonsUS
    End Get
  End Property

  Public Sub Start()
    Me.SampleSum = 0
    Me.SampleCount = 0
    Me.Grams = 0
    Me.GramsPerSecond = 0
    Me.Timer.Start()
  End Sub

  Public Sub [Stop]()
    Me.Timer.Stop()
  End Sub

  Public Sub Run(input As Integer, inputMin As Integer, inputMax As Integer, flowRateMax As Integer)
    Dim range As Integer = inputMax - inputMin
    Dim flowRate As Integer = CInt(((input - inputMin) / range) * flowRateMax)

    ' Run the totalizer
    RunTotalizer(Math.Max(flowRate, 0))
  End Sub

  Private Sub RunTotalizer(gramsPerSecond As Integer)
    Me.GramsPerSecond = gramsPerSecond

    ' Not running
    If Timer.Stopped Then Exit Sub

    If ((SampleCount + 1) * SampleRate) <= Timer.Milliseconds Then
      SampleCount += 1
      SampleSum += gramsPerSecond

      Grams = CInt((SampleSum * SampleRate) / 1000)
    End If
  End Sub

End Class
