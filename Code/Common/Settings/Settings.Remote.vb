Partial Public NotInheritable Class Settings

  Public NotInheritable Class Remote

    Public Shared Property DisableMimicButtons As Integer = 0

    Public Shared Property SettingsLoaded As Boolean

    Public Shared Sub Load()
      Try
        SettingsLoaded = True

        'Get application and file path
        Dim appPath As String = My.Application.Info.DirectoryPath
        Dim filePath As String = appPath & "\Settings.Remote.xml"

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

                  Case "DisableMimicButtons".ToLower
                    If Not dr.IsNull("value") Then DisableMimicButtons = Integer.Parse(dr("value").ToString)

                End Select
              Next
            End With
          End If
        End With

      Catch ex As Exception
        'TODO - log error ?
      End Try
    End Sub

  End Class

End Class
