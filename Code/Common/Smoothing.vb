Public Class Smoothing : Inherits MarshalByRefObject

  Private values(32) As Integer
  Private nextIndexToUse As Integer
  Private numberOfValuesStored As Integer

  Public Function Smooth(ByVal value As Integer, ByVal smoothing As Integer) As Integer
    Try
      'Just return the value - no smoothing
      If smoothing < 2 Then
        If values.Length <> 1 Then Array.Resize(values, 1)
        Return value
      End If

      'Check to see if the smoothing rate has changed - we'll need to resize the array if it has
      If smoothing <> values.Length Then Return ResizeValues(value, smoothing)

      'Store the new value and increment index etc.
      StoreValue(value)

      'Calculate the sum 
      Dim sum As Integer
      For i As Integer = 0 To numberOfValuesStored - 1 : sum += values(i) : Next

      'Return the average of the stored values
      Return Convert.ToInt32(sum / numberOfValuesStored)

    Catch ex As Exception
      'Ignore errors
    End Try
    Return value
  End Function

  Public Function Smooth(ByVal value As Short, ByVal smoothing As Integer) As Short
    Return Convert.ToInt16(Smooth(Convert.ToInt32(value), smoothing))
  End Function

  Private Function ResizeValues(ByVal value As Integer, ByVal smoothing As Integer) As Integer
    Array.Resize(values, smoothing)
    values(0) = value
    nextIndexToUse = 1
    numberOfValuesStored = 1
    Return value
  End Function

  Private Sub StoreValue(ByVal value As Integer)
    'Check to see if we're wrapping 
    If nextIndexToUse > values.GetUpperBound(0) Then
      'If we're wrapping store the new value at position 0
      values(0) = value
      nextIndexToUse = 1
    Else
      values(nextIndexToUse) = value
      nextIndexToUse += 1
      numberOfValuesStored += 1
    End If

    'Limit number of values to array size
    If numberOfValuesStored > values.Length Then numberOfValuesStored = values.Length
  End Sub

End Class
