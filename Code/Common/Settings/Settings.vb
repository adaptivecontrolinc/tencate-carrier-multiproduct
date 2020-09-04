Partial Public NotInheritable Class Settings

  Public Shared ReadOnly Property DefaultCulture() As System.Globalization.CultureInfo
    Get
      Return System.Globalization.CultureInfo.InvariantCulture
    End Get
  End Property

  Public Shared Property ConnectionStringBDC As String = "data source=10.1.5.20;initial catalog=BatchDyeingCentral;user id=Adaptive;password=zliUDc*14"
  Public Shared Property ConnectionStringEAS As String = "data source=10.1.5.40;initial catalog=ProductFile;user id=sa;password="
  Public Shared Property DemoMode As Integer = 0

  Public Shared Property SettingsLoaded As Boolean

  Public Shared Sub Load()
    Try
      'Get application and file path
      Dim appPath As String = My.Application.Info.DirectoryPath
      Dim filePath As String = appPath & "\Settings.xml"

      'If the file doses not exist then just use defaults...
      If Not My.Computer.FileSystem.FileExists(filePath) Then Exit Sub

      'Read the settings into a dataset
      Dim dsSettings As New System.Data.DataSet
      dsSettings.ReadXml(filePath)

      With dsSettings
        If .Tables.Contains("settings") Then
          With .Tables("settings")
            For Each dr As System.Data.DataRow In .Rows
              Select Case dr("name").ToString.ToLower

                Case "ConnectionStringBDC".ToLower
                  If Not dr.IsNull("value") Then ConnectionStringBDC = dr("value").ToString

                Case "ConnectionStringEAS".ToLower
                  If Not dr.IsNull("value") Then ConnectionStringEAS = dr("value").ToString

                Case "DemoMode".ToLower
                  If Not dr.IsNull("value") Then DemoMode = CInt(dr("Value"))
              End Select
            Next
          End With
        End If
      End With

      SettingsLoaded = True
    Catch ex As Exception
      'TODO - log error ?
    End Try
  End Sub

End Class
