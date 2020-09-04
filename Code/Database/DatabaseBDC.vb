Imports TencateCarrier.Utilities.Sql

Public Class DatabaseBDC : Inherits MarshalByRefObject
  Private controlCode As ControlCode
  Private connectionBDC As String

  

  Property CarrierData As New MachineData
  'Property DissolverData As New MachineData

  Public Sub New(controlCode As ControlCode)
    Me.controlCode = controlCode
    Me.connectionBDC = Settings.ConnectionStringBDC
  End Sub

  Public Sub Start()
    With New System.Threading.Thread(AddressOf Run)
      .Name = "DatabaseBDC"
      .Priority = Threading.ThreadPriority.BelowNormal
      .Start()
    End With
  End Sub

  Private Sub Run()
    ' Wait at least thirty two seconds before starting the loop 
    '    Batch Control seems to take a while to fully wake up so lets not overtax the dear
    Threading.Thread.Sleep(32000)

    ' Run constantly with a little sleep time between loops
    Do
      With controlCode
        Dim enabled = (.Parameters.DBSleepTimeBDC > 0)
        Dim sleepTimeMs = MinMax(.Parameters.DBSleepTimeBDC, 4, 64) * 500 ' / 2 basically to give a bit of time between the write & read
        Try
          If enabled Then
            UpdateMachinesTable()
            Threading.Thread.Sleep(sleepTimeMs)
            ReadMachinesTable()
          End If
        Catch ex As Exception
          Utilities.Log.LogError(ex)
        End Try
        Threading.Thread.Sleep(sleepTimeMs)
      End With
    Loop
  End Sub

  Private Sub UpdateMachinesTable()
    Dim sql As String = Nothing
    Try
      sql = "UPDATE Machines SET " & _
            " State=" & SqlString(controlCode.DistributorHead1.Position) & _
            ",LastUpdate=" & Utilities.Sql.SqlDateString(Date.UtcNow) & _
            " WHERE Name='Carrier'"
      SqlUpdate(connectionBDC, sql)
    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
  End Sub

  Private Sub ReadMachinesTable()
    Try
      Dim machines = GetDataTable(connectionBDC, "SELECT * FROM Machines", "Machines")
      If machines Is Nothing OrElse machines.Rows.Count <= 0 Then Exit Sub

      For Each row As System.Data.DataRow In machines.Rows
        If row("Name").ToString.Equals("Carrier", StringComparison.InvariantCultureIgnoreCase) Then
          CarrierData.Update(row)
        End If
      Next

      CheckCommands()
    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try
  End Sub

  Private Sub CheckCommands()
    Try
      With CarrierData
        If .Request.Code Is Nothing Then Return
        Select Case .Request.Code.ToLower
          Case "move" : Move(.Request.HeadNumber, .Request.Machine, .Request.Tank)
        End Select
      End With
    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try
  End Sub

  Private Sub Move(headnumber As Integer, machine As Integer, tank As Integer)
    If headnumber = 1 Then
      If machine = 0 AndAlso tank = 0 Then
        controlCode.DistributorHead1.MoveHome(1)
        ClearCommand()
      ElseIf machine > 0 Then
        controlCode.DistributorHead1.MoveDestination(machine, tank)
        ClearCommand()
      End If
    ElseIf headnumber = 2 Then
      If machine = 0 AndAlso tank = 0 Then
        controlCode.DistributorHead2.MoveHome(2)
        ClearCommand()
      ElseIf machine > 0 Then
        controlCode.DistributorHead2.MoveDestination(machine, tank)
        ClearCommand()
      End If
    End If

  End Sub

  Private Sub ClearCommand()
    Dim sql As String = Nothing
    Try
      sql = "UPDATE Machines SET Request=Null WHERE Name='Carrier'"
      SqlUpdate(connectionBDC, sql)

    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
  End Sub

  Public Class MachineData
    Property RequestString As String
    Property StateString As String
    Property LastUpdate As Date

    Property Request As New Details
    Property State As New Details

    Property DataRow As System.Data.DataRow

    Public Sub Update(newDataRow As System.Data.DataRow)
      If newDataRow Is Nothing Then Return

      DataRow = newDataRow
      RequestString = DataRow("Request").ToString
      StateString = DataRow("State").ToString
      LastUpdate = NullToNothingDate(DataRow("LastUpdate"))

      Request.Update(RequestString)
      State.Update(StateString)
    End Sub

    Public Class Details
      Property Code As String
      Property Parameters As String
      Property HeadNumber As Integer
      Property Machine As Integer
      Property Tank As Integer

      Public Sub Update(value As String)
        Code = GetCode(value)
        Parameters = GetParameters(value)
        Machine = GetMachine(Parameters)
        Tank = GetTank(Parameters)
      End Sub

      Private Function GetCode(value As String) As String
        If value Is Nothing Then Return Nothing

        Dim data = value.Split(",".ToCharArray)
        If data Is Nothing OrElse data.Length <> 2 Then Return value

        Return data(0)
      End Function

      Private Function GetParameters(value As String) As String
        If value Is Nothing Then Return Nothing

        Dim data = value.Split(",".ToCharArray)
        If data Is Nothing OrElse data.Length <> 2 Then Return Nothing

        Return data(1)
      End Function

      Private Function GetMachine(value As String) As Integer
        If value Is Nothing OrElse value.Length <= 0 Then Return -1

        Dim machineString = value.Substring(0, value.Length - 2)
        Dim tryInteger As Integer : If Integer.TryParse(machineString, tryInteger) Then Return tryInteger

        Return -1
      End Function

      Private Function GetTank(value As String) As Integer
        If value Is Nothing OrElse value.Length <= 0 Then Return -1

        Dim tankString = value.Substring(value.Length - 1, 1)
        Dim tryInteger As Integer : If Integer.TryParse(tankString, tryInteger) Then Return tryInteger

        Return -1
      End Function
    End Class

  End Class

End Class

Partial Public Class Parameters

  <Parameter(0, 1000), Category("System"), Description("Sleep time in seconds between SQL read / writes to BatchDyeingCentral")> _
  Public DBSleepTimeBDC As Integer

End Class