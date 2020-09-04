Public Class MimicControlDistributor
  Private Property controlcode As ControlCode

  Private Property ports1 As DistributorPorts
  Private Property targetPort1 As DistributorPort
  Private Property ports2 As DistributorPorts
  Private Property targetPort2 As DistributorPort

  Private portControls As New List(Of MimicControlDistributorPort)

  Public Sub New()
    ' This call is required by the designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.DoubleBuffered = True
    InitializeControl()
  End Sub

  Private Sub InitializeControl()
    AddPorts()
  End Sub

  Private Sub AddPorts()
    Dim x As Integer = 45   ' starting x position
    Dim y As Integer = 24  ' starting y position

    Dim xSpacing As Integer = 36
    Dim ySpacing As Integer = 64

    For i = 1 To 31 Step 2
      AddPort(i, x, y + ySpacing)
      AddPort(i + 1, x, y)
      x += xSpacing
      If i = 15 Then x += xSpacing
    Next
  End Sub

  Private Sub AddPort(index As Integer, x As Integer, y As Integer)
    Dim newPort As New MimicControlDistributorPort
    With newPort
      .Location = New Point(x, y)
      .Size = New Size(32, 48)
      .State = MimicControlDistributorPort.EState.Off
      .Tag = index
      .PortNumber = index
    End With
    Me.Controls.Add(newPort)
    Me.portControls.Add(newPort) ' for convenience
  End Sub

  Public Sub UpdateMimic(controlcode As ControlCode)
    Me.controlcode = controlcode
    Me.ports1 = controlcode.DistributorHead1.Ports '???dc
    Me.targetPort1 = controlcode.DistributorHead1.TargetPort
    Me.targetPort2 = controlcode.DistributorHead2.TargetPort '???
    UpdateDestinations()
    UpdatePorts()
  End Sub

  Private Sub UpdateDestinations()
    For Each item As MimicControlDistributorPort In portControls
      If item.PortNumber >= 1 AndAlso item.PortNumber <= 32 Then
        item.Destination = ports1(item.PortNumber).Code
      End If
    Next
  End Sub

  Private Sub UpdatePorts()
    With controlcode.DistributorHead1 '???dc
      If .AtHomePosition Then
        SetHome(1)
      ElseIf .AtPortPosition Then
        SetPort(1)
      Else
        SetTarget(1)
      End If
    End With
    With controlcode.DistributorHead2 '???dc
      If .AtHomePosition Then
        SetHome(2)
      ElseIf .AtPortPosition Then
        SetPort(2)
      Else
        SetTarget(2)
      End If
    End With
  End Sub

  Private Sub SetHome(head As Integer)
    If head = 1 Then
      For Each item As MimicControlDistributorPort In portControls
        If item.PortNumber = 1 Then item.State = MimicControlDistributorPort.EState.Connected1
        If item.PortNumber > 2 AndAlso item.PortNumber <= 32 AndAlso Not (item.State = MimicControlDistributorPort.EState.Target2 OrElse _
                                                                          item.State = MimicControlDistributorPort.EState.Connected2) Then
          item.State = MimicControlDistributorPort.EState.Off
        End If
      Next
    ElseIf head = 2 Then
      For Each item As MimicControlDistributorPort In portControls
        If item.PortNumber = 2 Then item.State = MimicControlDistributorPort.EState.Connected2
        If item.PortNumber > 2 AndAlso item.PortNumber <= 32 AndAlso Not (item.State = MimicControlDistributorPort.EState.Target1 OrElse _
                                                                          item.State = MimicControlDistributorPort.EState.Connected1) Then
          item.State = MimicControlDistributorPort.EState.Off
        End If
      Next
    End If

  End Sub

  Private Sub SetPort(head As Integer)
    If head = 1 Then
      If targetPort1 Is Nothing Then Return

      For Each item As MimicControlDistributorPort In portControls
        If item.PortNumber = targetPort1.PortNumber Then
          item.State = MimicControlDistributorPort.EState.Connected1
        ElseIf Not (item.State = MimicControlDistributorPort.EState.Connected2 OrElse item.State = MimicControlDistributorPort.EState.Target2) Then
          item.State = MimicControlDistributorPort.EState.Off
        End If
      Next
    ElseIf head = 2 Then
      If targetPort2 Is Nothing Then Return

      For Each item As MimicControlDistributorPort In portControls
        If item.PortNumber = targetPort2.PortNumber Then
          item.State = MimicControlDistributorPort.EState.Connected2
        ElseIf Not (item.State = MimicControlDistributorPort.EState.Connected1 OrElse item.State = MimicControlDistributorPort.EState.Target1) Then
          item.State = MimicControlDistributorPort.EState.Off
        End If
      Next
    End If

  End Sub

  Private Sub SetTarget(head As Integer)
    If head = 1 Then
      If targetPort1 Is Nothing Then Return
      For Each item As MimicControlDistributorPort In portControls
        If item.PortNumber = targetPort1.PortNumber Then
          item.State = MimicControlDistributorPort.EState.Target1
        ElseIf Not (item.State = MimicControlDistributorPort.EState.Connected2 OrElse item.State = MimicControlDistributorPort.EState.Target2) Then
          item.State = MimicControlDistributorPort.EState.Off
        End If
      Next
    ElseIf head = 2 Then
      If targetPort2 Is Nothing Then Return
      For Each item As MimicControlDistributorPort In portControls
        If item.PortNumber = targetPort2.PortNumber Then
          item.State = MimicControlDistributorPort.EState.Target2
        ElseIf Not (item.State = MimicControlDistributorPort.EState.Connected1 OrElse item.State = MimicControlDistributorPort.EState.Target1) Then
          item.State = MimicControlDistributorPort.EState.Off
        End If
      Next
    End If

  End Sub

End Class
