Public Class MimicControlDistributorPort

  Public Enum EState
    Off        ' white
    Target1     ' amber with "H1"
    Target2     ' amber with "H2"
    Connected1  ' green with "H1"
    Connected2     ' green with "H2"
  End Enum

  Private _State As EState
  <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
  Public Property State As EState
    Get
      Return _State
    End Get
    Set(value As EState)
      If value <> _State Then StateChanged(value)
      _State = value
    End Set
  End Property

  Private Sub StateChanged(newState As EState)
    Select Case newState
      Case EState.Off
        Me.BackgroundImage = My.Resources.MimicPort
        PortNumber = _PortNumber
        Invalidate()

      Case EState.Target1
        Me.BackgroundImage = My.Resources.MimicPortTarget1
        PortNumber = 0
        Invalidate()

      Case EState.Target2
        Me.BackgroundImage = My.Resources.MimicPortTarget2
        PortNumber = 0
        Invalidate()

      Case EState.Connected1
        Me.BackgroundImage = My.Resources.MimicPortConnected1
        PortNumber = 0
        Invalidate()

      Case EState.Connected2
        Me.BackgroundImage = My.Resources.MimicPortConnected2
        PortNumber = 0
        Invalidate()
    End Select
  End Sub

  Private _PortNumber As Integer
  <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
  Public Property PortNumber As Integer
    Get
      Return _PortNumber
    End Get
    Set(value As Integer)
      If value <> _PortNumber OrElse labelPort.Visible = False Then
        If value = 0 Then
          labelPort.Visible = False
        Else
          labelPort.Visible = True
          labelPort.Text = value.ToString
          _PortNumber = value
        End If
      End If


    End Set
  End Property

  Private _Destination As String
  <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
  Public Property Destination As String
    Get
      Return _Destination
    End Get
    Set(value As String)
      If value <> _Destination Then labelDestination.Text = value
      _Destination = value
    End Set
  End Property

  Public Sub New()
    ' This call is required by the designer.
    InitializeComponent()
  
    ' Add any initialization after the InitializeComponent() call.
    Me.DoubleBuffered = True
    Me.Size = New Size(32, 48)
  End Sub

End Class
