Imports TencateCarrier.Utilities.Sql

Public Class DistributorPorts : Inherits MarshalByRefObject
  Private portsArray(32) As DistributorPort   ' Distributor Ports 1 - 32, 0 = travel Position

  Public Sub New()
    For i As Integer = 0 To portsArray.GetUpperBound(0)
      portsArray(i) = New DistributorPort(i)
    Next i
   
    ReadPortData()
  End Sub

  Default Public ReadOnly Property Port(index As Integer) As DistributorPort
    Get
      If index >= 1 AndAlso index <= portsArray.GetUpperBound(0) Then
        Return portsArray(index)
      End If
      Return Nothing
    End Get
  End Property

  Default Public ReadOnly Property Port(machine As Integer, tank As Integer) As DistributorPort
    Get
      For i As Integer = 1 To portsArray.GetUpperBound(0)
        If portsArray(i).Machine = machine AndAlso portsArray(i).Tank = tank Then
          Return portsArray(i)
        End If
      Next
      Return Nothing
    End Get
  End Property

  ReadOnly Property Travel As DistributorPort
    Get
      Return portsArray(0)
    End Get
  End Property

  ReadOnly Property Home(HeadNumber As Integer) As DistributorPort
    Get
      Return portsArray(HeadNumber)
    End Get
  End Property


  Public Sub Requery()
    ReadPortData()
  End Sub

  Private Sub ReadPortData()
    Try
      'Get application and file path
      Dim appPath As String = My.Application.Info.DirectoryPath
      Dim filePath As String = appPath & "\PortMap.xml"

      'If the file doses not exist then just use defaults...
      If Not My.Computer.FileSystem.FileExists(filePath) Then Return

      'Read the settings into a dataset
      Dim dataSet As New System.Data.DataSet
      dataSet.ReadXml(filePath)

      ' Check to see if we have the table
      If dataSet Is Nothing OrElse Not dataSet.Tables.Contains("PortMap") Then Return

      ' Add port data
      AddPortData(dataSet.Tables("PortMap"))
    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try
  End Sub

  Private Sub AddPortData(portMap As System.Data.DataTable)
    Try
      If portMap Is Nothing OrElse portMap.Rows.Count <= 0 Then Return

      ' Loop through the table and fill in 
      For Each row As System.Data.DataRow In portMap.Rows
        Dim port = NullToZeroInteger(row("PortNumber").ToString)
        If port >= 1 AndAlso port <= portsArray.GetUpperBound(0) Then
          With portsArray(port)
            .Machine = NullToZeroInteger(row("Machine").ToString)
            .Tank = NullToZeroInteger(row("Tank").ToString)

            .PositionX = NullToZeroInteger(row("PositionX").ToString)
            .PositionY = 1 : If port Mod 2 = 0 Then .PositionY = 2
            .PositionZ = 1

          End With
        End If
      Next
    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try
  End Sub

  Public Sub UpdatePortPositions(positions() As Integer)
    For i As Integer = 1 To positions.GetUpperBound(0)
      If positions(i) > 0 Then
        portsArray(i).PositionX = positions(i)
      End If
    Next
  End Sub

End Class
