Partial Class ControlCode
  'Various display properties for mimics etc.

  Public ReadOnly Property RunMode() As String
    Get
      Return Parent.Mode.ToString
    End Get
  End Property

 
  Public ReadOnly Property Status() As String
    Get
      If String.IsNullOrEmpty(Parent.Signal) Then
        If SlowFlash Then
          Return "H1- " & DistributorHead1.Status
        Else
          Return "H2- " & DistributorHead2.Status
        End If

      Else
        Return Parent.Signal
      End If
        Return Nothing
    End Get
  End Property

End Class
