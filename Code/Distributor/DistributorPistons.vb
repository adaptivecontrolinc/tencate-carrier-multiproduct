Public Class DistributorPistons : Inherits MarshalByRefObject
  Property ConnectorPosition As Integer = -1
  Property TraversePosition As Integer = -1

  Public Sub MoveConnector(position As Integer)
    ConnectorPosition = position
  End Sub

  Public Sub MoveConnectorUp()
    ConnectorPosition = 0
  End Sub

  Public Sub MoveConnectorDown()
    ConnectorPosition = 1
  End Sub

  Public Sub MoveTraverse(position As Integer)
    TraversePosition = position
  End Sub

  Public Sub MoveTraverseRetract()
    TraversePosition = 0
  End Sub

  Public ReadOnly Property ConnectorUp As Boolean
    Get
      Return ConnectorPosition = 0
    End Get
  End Property

  Public ReadOnly Property ConnectorDown As Boolean
    Get
      Return ConnectorPosition = 1
    End Get
  End Property

  Public ReadOnly Property Traverse1Rev As Boolean
    Get
      Return TraversePosition = 0 OrElse TraversePosition = 1
    End Get
  End Property

  Public ReadOnly Property Traverse1Fwd As Boolean
    Get
      Return TraversePosition = 2
    End Get
  End Property

  Public ReadOnly Property Traverse2Rev As Boolean
    Get
      Return TraversePosition = 0
    End Get
  End Property

  Public ReadOnly Property Traverse2Fwd As Boolean
    Get
      Return TraversePosition = 1 OrElse TraversePosition = 2
    End Get
  End Property
  Public ReadOnly Property H2Traverse1Rev As Boolean
    Get
      Return TraversePosition = 0 OrElse TraversePosition = 2
    End Get
  End Property
  Public ReadOnly Property H2Traverse1Fwd As Boolean
    Get
      Return TraversePosition = 1
    End Get
  End Property
  Public ReadOnly Property H2Traverse2Rev As Boolean
    Get
      Return TraversePosition = 0
    End Get
  End Property
  Public ReadOnly Property H2Traverse2Fwd As Boolean
    Get
      Return TraversePosition = 1 OrElse TraversePosition = 2
    End Get
  End Property
End Class
