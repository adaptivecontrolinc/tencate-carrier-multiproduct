Imports TencateCarrier.Utilities.Sql

' Version 1.102 [2020-09-02] - Update for header specific product code.  
' RequestedCarrier table fields:
'   (MaterialCode,varchar), (DropeDone,int), (DropProcessing,int), (Created, datetime),
'   (MachineID, int), (Destination, varchar), (BatchNumber, nvarchar), (SequenceNumber, int), 
'   (DropNumber, int), (AmountProjected, int) "Pounds"

Public Class DispenseScheduler : Inherits MarshalByRefObject
  Private controlCode As ControlCode
  Private connectionEAS As String

  Public Sub New(controlCode As ControlCode)
    Me.controlCode = controlCode
    Me.connectionEAS = Settings.ConnectionStringEAS
  End Sub

  Public Sub Start()
    With New System.Threading.Thread(AddressOf Run)
      .Name = "DispenseScheduler"
      .Priority = Threading.ThreadPriority.BelowNormal
      .Start()
    End With
  End Sub

  Private Sub Run()
    ' Wait at least thirty two seconds before starting the loop 
    '    Batch Control seems to take a while to fully wake up so lets not overtax the dear
    'Threading.Thread.Sleep(32000)

    ' Run constantly with a little sleep time between loops
    Do
      With controlCode
        Dim enabled = (.Parameters.CarrierSchedulerEnabled = 1)
        Dim sleepTimeMs = 4000
        Try
          If enabled AndAlso .IO.DistributorAutoSelected Then
            If .Head1Dispenser.Idle OrElse .Head2Dispenser.Idle Then CheckCarrierTable()
          End If
        Catch ex As Exception
          Utilities.Log.LogError(ex)
        End Try
        Threading.Thread.Sleep(sleepTimeMs)
      End With
    Loop
  End Sub

  Private Sub CheckCarrierTable()
    Dim sql As String = Nothing
    Try
      Dim head1Product As String = controlCode.Head1Dispenser.ProductCodeDisplay
      Dim head2Product As String = controlCode.Head2Dispenser.ProductCodeDisplay

      ' sql = "SELECT TOP 16 * FROM RequestedCarrier WHERE MaterialCode='0701' AND DropDone=0 AND DropProcessing=0 ORDER BY Created"
      sql = "SELECT TOP 16 * FROM RequestedCarrier WHERE MaterialCode='" & head1Product & "' OR MaterialCode='" & head2Product & "' AND DropDone=0 AND DropProcessing=0 ORDER BY Created"
      Dim CarrierTable = Utilities.Sql.GetDataTable(connectionEAS, sql, "RequestedCarrier")

      If CarrierTable Is Nothing OrElse CarrierTable.Rows.Count <= 0 Then Return

      For Each row As System.Data.DataRow In CarrierTable.Rows
        Dim newJob As New DispenseJob
        If newJob.LoadRow(row) Then
          '  newJob.Head = HeadCheck(newJob.Machine, newJob.Tank) 'Old method where system only dispensed product="Carrier"
          newJob.Head = HeadCheck(newJob.Machine, newJob.Tank, newJob.ProductCode)
          If newJob.Head = 1 OrElse newJob.Head = 2 Then  'wait here for a head to be available
            If StartCarrierJob(newJob) Then
              Return
            Else
              SetCarrierJobManual(newJob)
            End If
          End If
        Else
          SetCarrierJobManual(row)
        End If
      Next

    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
  End Sub

  Private Function StartCarrierJob(CarrierJob As DispenseJob) As Boolean
    Try
      If CarrierJob Is Nothing OrElse (2 < CarrierJob.Head) OrElse (CarrierJob.Head < 1) Then Return False
      If CarrierJob.Head = 1 Then
        controlCode.Head1Dispenser.Start(CarrierJob)
        controlCode.DistributorHead1.Busy = True
        SetCarrierJobRunning(CarrierJob)
      ElseIf CarrierJob.Head = 2 Then
        controlCode.Head2Dispenser.Start(CarrierJob)
        controlCode.DistributorHead2.Busy = True
        SetCarrierJobRunning(CarrierJob)
      End If
      'controlCode.AutoScheduleProgram(CarrierJob.Batch)

      Return True
    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try
    Return False
  End Function

  Public Sub SetCarrierJobRunning(CarrierJob As DispenseJob)
    Dim sql As String = Nothing
    Try
      With CarrierJob
        sql = "UPDATE RequestedCarrier SET DropProcessing=1 WHERE " &
              "(MachineID=" & .row("MachineID").ToString & ") AND " &
              "(BatchNumber=" & SqlString(.row("BatchNumber").ToString) & ") AND " &
              "(SequenceNumber=" & .row("SequenceNumber").ToString & ") AND " &
              "(DropNumber=" & .row("DropNumber").ToString & ") AND " &
              "(Destination=" & SqlString(.row("Destination").ToString) & ")"
        SqlUpdate(connectionEAS, sql)
      End With
    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
  End Sub

  Public Sub SetCarrierJobComplete(CarrierJob As DispenseJob)
    Dim sql As String = Nothing
    Try
      With CarrierJob
        With CarrierJob
          sql = "UPDATE RequestedCarrier SET DropDone=1, AmountDispensed= " & .FinalValue.ToString & " WHERE " &
                "(MachineID=" & .row("MachineID").ToString & ") AND " &
                "(BatchNumber=" & SqlString(.row("BatchNumber").ToString) & ") AND " &
                "(SequenceNumber=" & .row("SequenceNumber").ToString & ") AND " &
                "(DropNumber=" & .row("DropNumber").ToString & ") AND " &
                "(Destination=" & SqlString(.row("Destination").ToString) & ")"
          SqlUpdate(connectionEAS, sql)
        End With
      End With
    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
    CarrierJob = Nothing  ' probably not necessary...
  End Sub

  Public Sub SetCarrierJobManual(CarrierJob As DispenseJob)
    Dim sql As String = Nothing
    Try
      With CarrierJob
        sql = "UPDATE RequestedCarrier SET DropDone=1, AmountDispensed=0, ManualLog=1 WHERE " &
              "(MachineID=" & .row("MachineID").ToString & ") AND " &
              "(BatchNumber=" & SqlString(.row("BatchNumber").ToString) & ") AND " &
              "(SequenceNumber=" & .row("SequenceNumber").ToString & ") AND " &
              "(DropNumber=" & .row("DropNumber").ToString & ") AND " &
              "(Destination=" & SqlString(.row("Destination").ToString) & ")"
        SqlUpdate(connectionEAS, sql)
      End With
    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
    If CarrierJob Is Nothing Then Return
  End Sub

  Public Sub SetCarrierJobManual(row As System.Data.DataRow)
    Dim sql As String = Nothing
    Try
      sql = "UPDATE RequestedCarrier SET DropDone=1, AmountDispensed=0, ManualLog=1 WHERE " &
              "(MachineID=" & row("MachineID").ToString & ") AND " &
              "(BatchNumber=" & SqlString(row("BatchNumber").ToString) & ") AND " &
              "(SequenceNumber=" & row("SequenceNumber").ToString & ") AND " &
              "(DropNumber=" & row("DropNumber").ToString & ") AND " &
              "(Destination=" & SqlString(row("Destination").ToString) & ")"
      SqlUpdate(connectionEAS, sql)
    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
  End Sub

  Private Function HeadCheck(machine As Integer, tank As Integer) As Integer
    Dim testports As DistributorPorts
    Dim testport As DistributorPort
    Dim HeadAssignment As Integer
    testports = New DistributorPorts
    testport = controlCode.DistributorHead1.Ports.Port(machine, tank)

    'head 1 is on the even side 
    ' TODO - confirm that the even/odd side does not matter when it comes to the product specific head dispenser
    With controlCode
      If controlCode.DistributorHead1.Available AndAlso (Not testport.PortIsEven OrElse Not controlCode.DistributorHead2.Available) Then
        HeadAssignment = 1
      ElseIf controlCode.DistributorHead2.Available AndAlso (testport.PortIsEven OrElse Not controlCode.DistributorHead1.Available) Then
        HeadAssignment = 2
      End If
    End With
    Return HeadAssignment
  End Function

  Private Function HeadCheck(machine As Integer, tank As Integer, productCode As Integer) As Integer
    Dim testports As DistributorPorts
    Dim testport As DistributorPort
    Dim HeadAssignment As Integer
    testports = New DistributorPorts
    testport = controlCode.DistributorHead1.Ports.Port(machine, tank)

    With controlCode
      ' Product is dispensed from one of the headers
      If productCode = .Head1Dispenser.ProductCode Then
        If controlCode.DistributorHead1.Available Then
          HeadAssignment = 1
        End If
      ElseIf productCode = .Head2Dispenser.ProductCode Then
        If controlCode.DistributorHead2.Available Then
          HeadAssignment = 2
        End If
      End If

    End With
    Return HeadAssignment
  End Function

End Class

Partial Public Class Parameters

  <Parameter(0, 1), Category("Carrier Control"), Description("Set to one to enable Carrier scheduling.")>
  Public CarrierSchedulerEnabled As Integer

End Class
