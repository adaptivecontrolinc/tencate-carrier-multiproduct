Partial Public Class Utilities
  Public Class Email

    Private Shared smtpHost_ As String   'usually something like smtp.companyname.com
    Public Shared Property SmtpHost() As String
      Get
        Return smtpHost_
      End Get
      Set(ByVal value As String)
        smtpHost_ = value
      End Set
    End Property

    Private Shared smtpFrom_ As String
    Public Shared Property SmtpFrom() As String
      Get
        Return smtpFrom_
      End Get
      Set(ByVal value As String)
        smtpFrom_ = value
      End Set
    End Property

    Private Shared smtpTo_ As String
    Public Shared Property SmtpTo() As String
      Get
        Return smtpTo_
      End Get
      Set(ByVal value As String)
        smtpTo_ = value
      End Set
    End Property

    Private Shared smtpSubject_ As String
    Public Shared Property SmtpSubject() As String
      Get
        Return smtpSubject_
      End Get
      Set(ByVal value As String)
        smtpSubject_ = value
      End Set
    End Property

    Private Shared smtpMessage_ As String
    Public Shared Property SmtpMessage() As String
      Get
        Return smtpMessage_
      End Get
      Set(ByVal value As String)
        smtpMessage_ = value
      End Set
    End Property

    Private Shared lastEmail_ As Date = Date.Now
    Public Shared Property LastEmail() As Date
      Get
        Return lastEmail_
      End Get
      Set(ByVal value As Date)
        lastEmail_ = value
      End Set
    End Property

    Public Shared Sub Setup(ByVal smtpHost As String, ByVal smtpFrom As String, ByVal smtpTo As String)
      Email.SmtpHost = smtpHost
      Email.SmtpFrom = smtpFrom
      Email.SmtpTo = smtpTo
    End Sub

    Public Shared Sub SendSync(ByVal subject As String, ByVal message As String)
      Try
        'Make sure we have the smtp info we need
        If smtpHost Is Nothing OrElse SmtpFrom Is Nothing OrElse SmtpTo Is Nothing Then Exit Sub

        'Don't send more than one email every two minutes
        If Timers.LocalDateNow.Subtract(LastEmail).Minutes < 2 Then Exit Sub

        SmtpSubject = subject
        SmtpMessage = message
        LastEmail = Timers.LocalDateNow

        SendEmail()
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    Public Shared Sub SendAsync(ByVal subject As String, ByVal message As String)
      Try
        'Make sure we have the smtp info we need
        If smtpHost Is Nothing OrElse SmtpFrom Is Nothing OrElse SmtpTo Is Nothing Then Exit Sub

        'Don't send more than one email every two minutes
        If Timers.LocalDateNow.Subtract(LastEmail).Minutes < 2 Then Exit Sub

        SmtpSubject = subject
        SmtpMessage = message
        LastEmail = Timers.LocalDateNow

        Dim backgroundThread As New System.Threading.Thread(AddressOf SendEmail)
        backgroundThread.Start()
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    Private Shared Sub SendEmail()
      Try
        Dim email As New System.Net.Mail.MailMessage
        With email
          .From = New System.Net.Mail.MailAddress(SmtpFrom)
          .To.Add(SmtpTo)
          .To.Add("service@adaptivecontrol.com")
          .Subject = SmtpSubject
          .Body = SmtpMessage
        End With

        Dim smtp As New System.Net.Mail.SmtpClient
        With smtp
          .Host = smtpHost
          .Send(email)
        End With
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

  End Class
End Class