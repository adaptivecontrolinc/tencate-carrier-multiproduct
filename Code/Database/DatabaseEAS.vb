Imports TencateCarrier.Utilities.Sql

Public Class DatabaseEAS : Inherits MarshalByRefObject
  Private controlCode As ControlCode
  Private connectionEAS As String

  Public Property CarrierTable As System.Data.DataTable
  Public Property CarrierTableXML As String

  Public Property Sleeping As Boolean

  Public Sub New(controlCode As ControlCode)
    Me.controlCode = controlCode
    Me.connectionEAS = Settings.ConnectionStringEAS
  End Sub

  Public Sub Start()
    With New System.Threading.Thread(AddressOf Run)
      .Name = "DatabaseReads"
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
        Dim enabled = (.Parameters.DBSleepTimeEAS > 0)
        Dim sleepTimeMs = MinMax(.Parameters.DBSleepTimeEAS, 8, 64) * 1000
        Try
          If enabled Then
            UpdateCarrierTableXML()
          End If
        Catch ex As Exception
          Utilities.Log.LogError(ex)
        End Try
        Sleeping = True
        Threading.Thread.Sleep(sleepTimeMs)
        Sleeping = False
      End With
    Loop
  End Sub

  Private Sub UpdateCarrierTableXML()
    Dim sql As String = Nothing
    Try
      sql = "SELECT TOP 100 * FROM RequestedCarrier WHERE DropDone=0 ORDER BY Created"
      CarrierTable = Utilities.Sql.GetDataTable(connectionEAS, sql, "RequestedCarrier")

      If CarrierTable Is Nothing OrElse CarrierTable.Rows.Count <= 0 Then
        CarrierTableXML = ""  ' use an empty string Dave's remoting does not like nothing
        Exit Sub
      End If

      ' Use a string writer to convert xml stream to a string
      Dim writer As New System.IO.StringWriter
      CarrierTable.WriteXml(writer, System.Data.XmlWriteMode.WriteSchema)
      CarrierTableXML = writer.ToString

    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
  End Sub

End Class

Partial Public Class Parameters

  <Parameter(0, 1000), Category("System"), Description("Sleep time in seconds between SQL read / writes to smieasyweb")> _
  Public DBSleepTimeEAS As Integer

End Class