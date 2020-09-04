Module Support

  Public Function MulDiv(ByVal value As Integer, ByVal multiply As Integer, ByVal divide As Integer) As Integer
    If divide = 0 Then Return 0 ' no divide by zero error please
    Return CType((CType(value, Long) * multiply) \ divide, Integer)
  End Function

  Public Function MulDiv(ByVal value As Short, ByVal multiply As Integer, ByVal divide As Integer) As Short
    If divide = 0 Then Return 0 ' no divide by zero error please
    Return CType((CType(value, Long) * multiply) \ divide, Short)
  End Function

  ''' <summary>Returns a rescaled value.</summary>
  ''' <param name="value"></param>
  ''' <param name="inMin"></param>
  ''' <param name="inMax"></param>
  ''' <param name="outMin"></param>
  ''' <param name="outMax"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function ReScale(ByVal value As Integer, ByVal inMin As Integer, ByVal inMax As Integer, ByVal outMin As Integer, ByVal outMax As Integer) As Integer
    If inMin = inMax Then Return 0 ' avoid division by zero
    If value < inMin Then value = inMin
    If value > inMax Then value = inMax
    Return MulDiv(value - inMin, outMax - outMin, inMax - inMin) + outMin
  End Function

  Public Function ReScale(ByVal value As Short, ByVal inMin As Integer, ByVal inMax As Integer, ByVal outMin As Integer, ByVal outMax As Integer) As Short
    Return CType(ReScale(CType(value, Integer), inMin, inMax, outMin, outMax), Short)
  End Function

  ''' <summary>Returns the given value constrained to lie beneath the min and the max.</summary>
  ''' <param name="value">The value to check.</param>
  ''' <param name="min">The least value that can be returned.</param>
  ''' <param name="max">The greatest value that can be returned.</param>
  Public Function MinMax(ByVal value As Integer, ByVal min As Integer, ByVal max As Integer) As Integer
    If value < min Then Return min
    If value > max Then Return max
    Return value
  End Function

  Public Function TimerString(ByVal secs As Integer) As String
    ' TODO: pre-make many of these for speed ?
    Dim hours As Integer = secs \ 3600, minutes As Integer = (secs Mod 3600) \ 60, _
        seconds As Integer = (secs Mod 60)

    ' Hours and minutes
    If hours > 0 Then Return hours.ToString("00", Globalization.CultureInfo.InvariantCulture) & ":" _
                             & minutes.ToString("00", Globalization.CultureInfo.InvariantCulture) & "m"
    ' Minutes and seconds
    Return minutes.ToString("00", Globalization.CultureInfo.InvariantCulture) & ":" _
             & seconds.ToString("00", Globalization.CultureInfo.InvariantCulture) & "s"
  End Function
End Module