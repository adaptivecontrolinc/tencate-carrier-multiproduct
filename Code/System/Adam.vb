Imports System.Net.Sockets

Namespace Ports
  ''' <summary>A TCP stream for an Adam 4577 device that keeps re-connecting, and doesn't throw exceptions.</summary>
  ''' <remarks>Instead of exceptions, it returns 0 bytes from Read's.</remarks>
  Public Class AdamTcpStream : Inherits System.IO.Stream
    ReadOnly computer_ As String, port_ As Integer
    Private socket_ As Socket, receiveTimeout_ As Integer

    Sub New(computer As String, Optional port As Integer = 5201)
      computer_ = computer : port_ = port
    End Sub

    Overrides Sub Close()
      If socket_ IsNot Nothing Then socket_.Close() : socket_ = Nothing
    End Sub

    Overrides Function ToString() As String
      Return computer_ & ":" & port_.ToString(InvariantCulture)
    End Function

    Overrides Property ReadTimeout() As Integer
      Get
        Return receiveTimeout_
      End Get
      Set(value As Integer)
        If receiveTimeout_ = value Then Exit Property
        receiveTimeout_ = value
        If socket_ IsNot Nothing Then socket_.ReceiveTimeout = value
      End Set
    End Property

    Overrides Function Read(buffer() As Byte, offset As Integer, count As Integer) As Integer
      Try
#If 0 Then
          Return socket_.Receive(buffer, offset, count, SocketFlags.None)
#Else
        Return If(ReceiveAll(socket_, buffer, offset, count) = SocketError.Success, count, 0)
#End If
      Catch
        Close()
        Return 0
      End Try
    End Function

    Private Function ReceiveAll(socket As Socket, buffer() As Byte, offset As Integer, size As Integer) As SocketError
      Do While size > 0
        Dim errorCode As SocketError
        Dim red = socket.Receive(buffer, offset, size, SocketFlags.None, errorCode)
        If red = 0 Then Return SocketError.TimedOut
        If errorCode <> 0 Then Return errorCode
        offset += red : size -= red
      Loop
      Return SocketError.Success
    End Function


    Overrides Sub Write(buffer() As Byte, offset As Integer, count As Integer)
      If socket_ Is Nothing Then
        ' Opens the socket as TCP
        socket_ = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        socket_.ReceiveTimeout = receiveTimeout_
        Try
          socket_.Connect(computer_, port_)
        Catch
          Close()
        End Try
      End If
      Try
        socket_.Send(buffer, offset, count, SocketFlags.None)
      Catch ex As SocketException
        Close()
      End Try
    End Sub

    ' These are necessary, but un-interesting
    Overrides ReadOnly Property CanRead() As Boolean
      Get
        Return True
      End Get
    End Property
    Overrides ReadOnly Property CanSeek() As Boolean
      Get
        Return False
      End Get
    End Property
    Overrides ReadOnly Property CanWrite() As Boolean
      Get
        Return True
      End Get
    End Property
    Overrides Sub Flush()
    End Sub

    Overrides ReadOnly Property Length() As Long
      Get
        Throw New NotSupportedException
      End Get
    End Property
    Overrides Property Position() As Long
      Get
        Throw New NotSupportedException
      End Get
      Set(value As Long)
        Throw New NotSupportedException
      End Set
    End Property
    Overrides Function Seek(offset As Long, origin As System.IO.SeekOrigin) As Long
      Throw New NotSupportedException
    End Function
    Overrides Sub SetLength(value As Long)
      Throw New NotSupportedException
    End Sub
  End Class

  ''' <summary>A UDP stream for an Adam 4577 device that keeps re-connecting, and doesn't throw exceptions.</summary>
  ''' <remarks>Instead of exceptions, it returns 0 bytes from Read's.</remarks>
  Public Class AdamUdpStream : Inherits System.IO.Stream
    ReadOnly computer_ As String, port_ As Integer
    Private endPoint_ As Net.IPEndPoint
    Private socket_ As Socket, receiveTimeout_ As Integer

    Sub New(computer As String, Optional port As Integer = 5201)
      computer_ = computer : port_ = port
    End Sub

    Overrides Sub Close()
      If socket_ IsNot Nothing Then socket_.Close() : socket_ = Nothing
    End Sub

    Overrides Function ToString() As String
      Return computer_ & ":" & port_.ToString(InvariantCulture)
    End Function

    Overrides Property ReadTimeout() As Integer
      Get
        Return receiveTimeout_
      End Get
      Set(value As Integer)
        If receiveTimeout_ = value Then Exit Property
        receiveTimeout_ = value
        If socket_ IsNot Nothing Then socket_.ReceiveTimeout = value
      End Set
    End Property

    Overrides Function Read(buffer() As Byte, offset As Integer, count As Integer) As Integer
      If socket_ Is Nothing Then Return Nothing ' should have been opened by a preceding Write
      Dim remoteEp As Net.EndPoint = endPoint_
      Dim ret = socket_.ReceiveFrom(buffer, offset, count, SocketFlags.None, remoteEp)
      Return ret
    End Function

    Overrides Sub Write(buffer() As Byte, offset As Integer, count As Integer)
      If endPoint_ Is Nothing Then
        Try
          Dim address As Net.IPAddress
          If Not Net.IPAddress.TryParse(computer_, address) Then address = Net.Dns.GetHostEntry(computer_).AddressList(0)
          endPoint_ = New Net.IPEndPoint(address, port_)
        Catch
          Exit Sub
        End Try
      End If

      If socket_ Is Nothing Then
        ' Opens the socket as UDP
        socket_ = New Socket(endPoint_.AddressFamily, SocketType.Dgram, ProtocolType.Udp)
        If endPoint_.AddressFamily = AddressFamily.InterNetworkV6 Then
          Try
            socket_.SetSocketOption(SocketOptionLevel.IPv6, CType(27, SocketOptionName), 0)
          Catch : End Try
        End If
        socket_.ReceiveTimeout = receiveTimeout_
      End If
      Try
        socket_.SendTo(buffer, offset, count, SocketFlags.None, endPoint_)
      Catch ex As SocketException
        Close()
      End Try
    End Sub

    ' These are necessary, but un-interesting
    Overrides ReadOnly Property CanRead() As Boolean
      Get
        Return True
      End Get
    End Property
    Overrides ReadOnly Property CanSeek() As Boolean
      Get
        Return False
      End Get
    End Property
    Overrides ReadOnly Property CanWrite() As Boolean
      Get
        Return True
      End Get
    End Property
    Overrides Sub Flush()
    End Sub

    Overrides ReadOnly Property Length() As Long
      Get
        Throw New NotSupportedException
      End Get
    End Property
    Overrides Property Position() As Long
      Get
        Throw New NotSupportedException
      End Get
      Set(value As Long)
        Throw New NotSupportedException
      End Set
    End Property
    Overrides Function Seek(offset As Long, origin As System.IO.SeekOrigin) As Long
      Throw New NotSupportedException
    End Function
    Overrides Sub SetLength(value As Long)
      Throw New NotSupportedException
    End Sub
  End Class
End Namespace
