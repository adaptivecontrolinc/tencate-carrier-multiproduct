Public Class MimicQueue

  Public Property ControlCode As ControlCode

  Public CarrierTableXml As String
  Private CarrierTable As System.Data.DataTable

  Private ReadOnly Property Remoted() As Boolean
    Get
      Return Runtime.Remoting.RemotingServices.IsTransparentProxy(ControlCode)
    End Get
  End Property

  Public Sub New()
    ' This call is required by the designer.
    InitializeComponent()

    Me.DoubleBuffered = True
    Me.AutoScaleMode = Windows.Forms.AutoScaleMode.None

    ' Add any initialization after the InitializeComponent() call.
    InitializeControl()
  End Sub


  Private Sub InitializeControl()
    ' Setup datagrids
    SetupDataGridColumns(dataGridViewXRunning)
    SetupDataGridColumns(dataGridViewXScheduled)
  End Sub

  Public Sub UpdateMimic()
    If UpdateCarrierTable() Then
      UpdateRunning()
      UpdateScheduled()
    End If
  End Sub

  Private Function UpdateCarrierTable() As Boolean
    Try
      Dim xml = ControlCode.CarrierTableXML

      ' If data has not changed do not update
      If String.Compare(CarrierTableXml, xml) = 0 Then Return False

      ' Remember the new data for next time
      CarrierTableXml = xml

      ' Make the datatable
      If String.IsNullOrEmpty(xml) Then
        CarrierTable = Nothing
      Else
        CarrierTable = New System.Data.DataTable
        CarrierTable.ReadXml(New System.IO.StringReader(xml))
        Utilities.Sql.ConvertDateTimeToLocalTime(CarrierTable)
      End If
      Return True
    Catch ex As Exception
      'Ignore errors
    End Try
    Return False
  End Function

  Private Sub UpdateRunning()
    With dataGridViewXRunning
      If CarrierTable Is Nothing Then
        .DataSource = CarrierTable
      Else
        Dim dataView = New System.Data.DataView(CarrierTable, "DropProcessing=1", "Created DESC", DataViewRowState.CurrentRows)
        .DataSource = dataView
      End If
    End With
  End Sub

  Private Sub UpdateScheduled()
    With dataGridViewXScheduled
      If CarrierTable Is Nothing Then
        .DataSource = CarrierTable
      Else
        Dim dataView = New System.Data.DataView(CarrierTable, "DropProcessing=0", "Created DESC", DataViewRowState.CurrentRows)
        .DataSource = dataView
      End If
    End With
  End Sub

  Private Sub SetupDataGridColumns(dataGrid As DataGridViewX)
    With dataGrid
      .RowHeadersVisible = False
      .XAddTextColumn("MachineID", "Machine", 96)
      .XAddTextColumn("BatchNumber", "Batch", 128)
      .XAddTextColumn("MaterialCode", "Material", 128)
      .XAddTextColumn("AmountProjected", "Amount", 128)
      .XAddTextColumn("Destination", "Destination", 128)
      .XAddTextColumn("Created", "Created", 128)
      .XAutosizeColumn = "Created"
      .XSetupReadOnly()
      .XHideSelectionBar()
    End With
  End Sub

End Class
