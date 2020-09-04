Public Class DistributorPort : Inherits MarshalByRefObject

  Property PortNumber As Integer
  Property Machine As Integer
  Property Tank As Integer

  Property PositionX As Integer   ' head 
  Property PositionY As Integer   ' traverse
  Property PositionZ As Integer   ' connector

  ReadOnly Property Head As Integer
    Get
      Return PositionX
    End Get
  End Property

  ReadOnly Property Traverse As Integer
    Get
      Return PositionY
    End Get
  End Property

  ReadOnly Property Connector As Integer
    Get
      Return PositionZ
    End Get
  End Property

  ReadOnly Property Code As String
    Get
      If PortNumber = 1 OrElse PortNumber = 2 Then Return "Home"
      If Machine >= 0 AndAlso Tank >= 0 Then Return Machine.ToString & "-" & Tank.ToString

      Return " "
    End Get
  End Property

  ReadOnly Property Travel As Boolean
    Get
      Return PortNumber = 0
    End Get
  End Property

  ReadOnly Property Home As Boolean
    Get
      Return PortNumber = 1
    End Get
  End Property
  ReadOnly Property PortIsEven As Boolean
    Get
      Return PortNumber Mod 2 = 0
    End Get
  End Property

  Public Sub New(portNumber As Integer)
    Me.PortNumber = portNumber
  End Sub

  Public Sub New()

    Me.PortNumber = 0
  End Sub
End Class
