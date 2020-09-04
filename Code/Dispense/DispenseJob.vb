Imports TencateCarrier.Utilities.Sql

' Version 1.102 [2020-09-04] Update for header specific product code.  
' RequestedCarrier table fields:
'   (MaterialCode,varchar), (DropeDone,int), (DropProcessing,int), (Created, datetime),
'   (MachineID, int), (Destination, varchar), (BatchNumber, nvarchar), (SequenceNumber, int), 
'   (DropNumber, int), (AmountProjected, int) "Pounds"

Public Class DispenseJob
  Property Machine As Integer
  Property Tank As Integer
  Property Head As Integer
  Property Batch As String
  Property MaterialCode As String
  Property ProductCode As Integer
  ReadOnly Property ProductCodeDisplay As String
    Get
      If ProductCode < 1000 Then
        Return "0" & ProductCode.ToString
      Else
        Return ProductCode.ToString
      End If
    End Get
  End Property

  Property Gallons As Double
  Property FinalValue As Double

  Property Manual As Boolean

  Friend Property row As System.Data.DataRow

  Public Function LoadRow(row As System.Data.DataRow) As Boolean
    Try
      If row Is Nothing Then Return False

      Me.row = row

      Machine = NullToZeroInteger(row("MachineID"))
      Tank = GetTankNumber(row("Destination").ToString)
      Batch = row("BatchNumber").ToString
      Gallons = CInt(row("AmountProjected")) / 8.34           ' Pounds to Gallons
      MaterialCode = row("MaterialCode").ToString
      ProductCode = NullToZeroInteger(row("MaterialCode"))

      If Machine <= 0 OrElse Machine >= 15 Then Return False
      If Tank <= 0 OrElse Tank >= 3 Then Return False
      If Batch.Length <= 0 OrElse Batch.ToLower = "???" Then Return False
      If Gallons <= 0 OrElse Gallons >= 300 Then Return False

      Return True
    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try
    Return False
  End Function

  Private Function GetTankNumber(destination As String) As Integer
    ' Destination is the tank number in this format #1, #2
    If destination.Length = 2 Then
      Dim tryInteger As Integer
      If Integer.TryParse(destination.Substring(1, 1), tryInteger) Then Return tryInteger
    End If
    Return -1
  End Function

End Class
