' Modbus protocol
Namespace Ports
  Public Class ModbusOptimass : Inherits Base

    Private Structure WorkData
      Dim FirstRegister, SlaveAddress As Integer, IsRead As Boolean, Func As Func,
          Values As Array, WriteMode As WriteMode
    End Structure
    Private work_ As WorkData
    ReadOnly writeOptimisation_ As New WriteOptimisation

    Sub New(stm As System.IO.Stream)
      MyBase.New(stm, 200) ' should allow for greatest possible time to receive data, for example 100 bytes @ 19200 -> 50ms plus some delay
    End Sub

    Protected Sub BeginWriteAndReadStoreCrc(tx() As Byte, rxCount As Integer)
      Dim txCount As Integer = tx.Length

      ' The CRC should go in the last 2 bytes
      Dim crcBytes() As Byte = Hash(tx, 0, txCount - 2)
      tx(txCount - 2) = crcBytes(0)
      tx(txCount - 1) = crcBytes(1)

      BeginWriteAndRead1(tx, rxCount)
    End Sub

    Protected Overrides Function CheckRxFormat(rx() As Byte, rxCount As Integer) As Base.Result
      ' Check the incoming CRC as well - unless there are two zeroes in the data.  This is
      ' just a lazy exception for modbus tcp/ip
      Dim crc0 As Byte = rx(rxCount - 2), crc1 As Byte = rx(rxCount - 1)
      If crc0 = 0 AndAlso crc1 = 0 Then
        Return Result.OK
      Else
        Dim crcBytes() As Byte = Hash(rx, 0, rxCount - 2)
        If crcBytes(0) <> crc0 OrElse crcBytes(1) <> crc1 Then
          Return Result.Fault
        Else
          Return Result.OK
        End If
      End If
    End Function

    Private Sub RunStateMachine(slaveAddress As Integer, firstRegister As Integer, isRead As Boolean)
      If RunStateMachine0() Then
        ' Maybe it's someone else's job - TODO: timeout if no-one comes for it for a long time
        If work_.SlaveAddress = slaveAddress AndAlso work_.FirstRegister = firstRegister AndAlso
          work_.IsRead = isRead Then SetIdle() ' the end of this job
      End If
    End Sub

    Private Shared Function Hash(array() As Byte, ibStart As Integer, cbSize As Integer) As Byte()
      Dim crc As Integer = &HFFFF  ' do it in 32 bits instead of 16 bit unsigned
      Do While cbSize > 0
        crc = crc Xor array(ibStart)
        For shiftCount As Integer = 0 To 7
          If (crc And 1) <> 0 Then
            crc = (crc >> 1) Xor &HA001
          Else
            crc >>= 1
          End If
        Next shiftCount
        ibStart += 1 : cbSize -= 1
      Loop
      Dim all4Bytes() As Byte = System.BitConverter.GetBytes(crc)
      Return New Byte() {all4Bytes(0), all4Bytes(1)}
    End Function

    Private Shared Function GetBitCount(typ As Type) As Integer
      If typ Is GetType(Boolean) Then Return 1
      If typ Is GetType(Int16) OrElse typ Is GetType(UInt16) Then Return 16
      If typ Is GetType(Int32) OrElse typ Is GetType(UInt32) OrElse typ Is GetType(Single) Then Return 32
      Return 0
    End Function

    ' We always ignore element 0 of the 'values' array - this is to make it easier for the engineer writing
    ' the control-code who calls this.
    Function Read(slaveAddress As Integer, firstRegister As Integer, values As Array) As Result
      ' In modbus, the high bytes comes first

#If 0 Then
      ' One less because we ignore element 0
      Dim bitCount As Integer = GetBitCount(values.GetType.GetElementType), _
          count As Integer = values.Length - 1, _
          totalBits As Integer = count * bitCount, _
          shortCount As Integer = totalBits \ 16 : If totalBits Mod 16 <> 0 Then shortCount += 1
#End If

      ' Start a completely new task
      If IsIdle Then
        work_.SlaveAddress = slaveAddress : work_.FirstRegister = firstRegister : work_.IsRead = True
        work_.Values = Nothing

#If 1 Then
        ' One less because we ignore element 0
        Dim bitCount As Integer = GetBitCount(values.GetType.GetElementType), _
            count As Integer = values.Length - 1, _
            totalBits As Integer = count * bitCount, _
            shortCount As Integer = totalBits \ 16 : If totalBits Mod 16 <> 0 Then shortCount += 1
#End If

        ' ReadInputTable
        If firstRegister >= 10001 AndAlso firstRegister <= 19999 Then
          If bitCount <> 1 Then Throw New ArgumentOutOfRangeException
          Dim tx(8 - 1) As Byte : tx(0) = CType(slaveAddress, Byte)
          work_.Func = Func.ReadInputTable : tx(1) = work_.Func
          BitConverterLittleEndian.GetBytes(CType(firstRegister - 10001, Short)).CopyTo(tx, 2)
          BitConverterLittleEndian.GetBytes(CType(count, Short)).CopyTo(tx, 4)
          Dim byteCount As Integer = (count + 7) \ 8
          BeginWriteAndReadStoreCrc(tx, 5 + byteCount)

          ' ReadInputRegisters
        ElseIf firstRegister >= 30000 AndAlso firstRegister <= 39999 Then
          Dim tx(8 - 1) As Byte : tx(0) = CType(slaveAddress, Byte)
          work_.Func = Func.ReadInputRegisters : tx(1) = work_.Func
          BitConverterLittleEndian.GetBytes(CType(firstRegister, Short)).CopyTo(tx, 2)
          BitConverterLittleEndian.GetBytes(CType(shortCount, Short)).CopyTo(tx, 4)
          BeginWriteAndReadStoreCrc(tx, 5 + 2 * shortCount)

          ' ReadHoldingRegisters
        ElseIf firstRegister >= 40001 Then  ' read holding registers
          Dim tx(8 - 1) As Byte : tx(0) = CType(slaveAddress, Byte)
          work_.Func = Func.ReadHoldingRegisters : tx(1) = work_.Func
          BitConverterLittleEndian.GetBytes(CType(firstRegister, UShort)).CopyTo(tx, 2)
          BitConverterLittleEndian.GetBytes(CType(shortCount, Short)).CopyTo(tx, 4)
          BeginWriteAndReadStoreCrc(tx, 5 + 2 * shortCount)
        Else
          Throw New ArgumentOutOfRangeException("firstRegister")
        End If
      End If

      ' See if we're finished
      RunStateMachine(slaveAddress, firstRegister, True)
      Dim ret = GetIdleResult()
      If ret = Result.OK Then
#If 1 Then
        ' One less because we ignore element 0
        Dim bitCount As Integer = GetBitCount(values.GetType.GetElementType), _
            count As Integer = values.Length - 1, _
            totalBits As Integer = count * bitCount, _
            shortCount As Integer = totalBits \ 16 : If totalBits Mod 16 <> 0 Then shortCount += 1
#End If

        ' Ok, store these new values - we may have to unscramble the hi-lo order
        Select Case work_.Func
          ' Data here is in single bytes
          Case Func.ReadInputTable
            For i As Integer = 0 To count - 1
              values.SetValue((rx_(3 + i \ 8) And (1 << (i And 7))) <> 0, i + 1)
            Next i

            ' Data is returned in exactly the same way for both of these
          Case Func.ReadInputRegisters, Func.ReadHoldingRegisters
            ' Convert the words back round to low byte first
            Dim uShortArray() As UShort = TryCast(values, UShort())
            If uShortArray IsNot Nothing Then
              For i As Integer = 0 To count - 1
                uShortArray(i + 1) = BitConverterLittleEndian.ToUInt16(rx_, 3 + 2 * i)
              Next i
            Else
              Dim singleArray() As Single = TryCast(values, Single())
              If singleArray IsNot Nothing Then
                For i As Integer = 0 To count - 1
                  singleArray(i + 1) = BitConverterLittleEndian.ToSingle(rx_, 3 + 4 * i)
                Next i
              Else
                Dim data(shortCount - 1) As Short
                For i As Integer = 0 To shortCount - 1
                  data(i) = BitConverterLittleEndian.ToInt16(rx_, 3 + 2 * i)
                Next i

                Dim booleanArray() As Boolean = TryCast(values, Boolean())
                If booleanArray IsNot Nothing Then
                  For i As Integer = 0 To count - 1
                    booleanArray(i + 1) = (data(i \ 16) And (1 << (i And 15))) <> 0
                  Next i
                Else
                  Dim shortArray() As Short = DirectCast(values, Short())
                  For i As Integer = 0 To count - 1
                    shortArray(i + 1) = data(i)
                  Next i
                End If
              End If
            End If
        End Select
      End If
      Return ret
    End Function

    Function CanWrite(slaveAddress As Integer, firstRegister As Integer) As Boolean
      If IsIdle Then Return True
      RunStateMachine(slaveAddress, firstRegister, False)
      Dim ret = GetIdleResult()
      If ret = Result.OK AndAlso work_.WriteMode = Ports.WriteMode.Optimised Then
        writeOptimisation_.SuccessfulWrite(work_.Values, slaveAddress, firstRegister)
      End If
      Return False

    End Function

    Function Write(slaveAddress As Integer, firstRegister As Integer, values As Array, writeMode As WriteMode) As Result
      If IsIdle Then
        ' Start a completely new task

        ' Optionally, do write-optimisation, meaning we usually do not write the same values to the same
        ' registers in the same slave.
        If writeMode = Ports.WriteMode.Optimised AndAlso _
           writeOptimisation_.RecentlyWritten(values, slaveAddress, firstRegister) Then Return Result.OK

        work_.SlaveAddress = slaveAddress : work_.FirstRegister = firstRegister : work_.IsRead = False
        work_.Values = values : work_.WriteMode = writeMode

        Dim bitCount As Integer = GetBitCount(values.GetType.GetElementType), _
            count As Integer = values.Length - 1, _
            totalBits As Integer = count * bitCount, _
            shortCount As Integer = totalBits \ 16 : If totalBits Mod 16 <> 0 Then shortCount += 1

        ' ForceMultipleOutputs
        If firstRegister >= 10001 AndAlso firstRegister <= 19999 Then
          If bitCount <> 1 Then Throw New NotSupportedException
          Dim byteCount As Integer = count \ 8 : If count Mod 8 <> 0 Then byteCount += 1
          Dim tx(9 + byteCount - 1) As Byte
          tx(0) = CType(slaveAddress, Byte)
          work_.Func = Func.ForceMultipleOutputs : tx(1) = work_.Func
          Dim startAddressBytes() As Byte = BitConverterLittleEndian.GetBytes(CType(firstRegister - 10001, Short)), _
              pointsBytes() As Byte = BitConverterLittleEndian.GetBytes(CType(count, Short))
          tx(2) = startAddressBytes(0) : tx(3) = startAddressBytes(1)
          tx(4) = pointsBytes(0) : tx(5) = pointsBytes(1)
          tx(6) = CType(byteCount, Byte)

          Dim boolArray() As Boolean = DirectCast(values, Boolean())
          For i As Integer = 0 To count - 1
            If boolArray(i + 1) Then
              tx(7 + (i \ 8)) = CType(tx(7 + (i \ 8)) Or (1 << (i And 7)), Byte)
            End If
          Next i
          BeginWriteAndReadStoreCrc(tx, 8)

          ' PresetSingleRegister or PresetMultipleRegisters
        ElseIf firstRegister >= 40001 Then
          ' Make the request
          Dim tx() As Byte
          Dim fn As Func
          If shortCount = 1 Then
            fn = Func.PresetSingleRegister
            tx = New Byte(8 - 1) {}
          Else
            fn = Func.PresetMultipleRegisters
            tx = New Byte(7 + 2 * shortCount + 2 - 1) {}
          End If

          tx(0) = CType(slaveAddress, Byte)
          work_.Func = fn : tx(1) = work_.Func
          Dim startAddressBytes() As Byte = BitConverterLittleEndian.GetBytes(CType(firstRegister - 40001, Short))
          tx(2) = startAddressBytes(0) : tx(3) = startAddressBytes(1)

          If shortCount > 1 Then
            Dim pointsBytes() As Byte = BitConverterLittleEndian.GetBytes(CType(shortCount, Short))
            tx(4) = pointsBytes(0) : tx(5) = pointsBytes(1)
            tx(6) = CType(2 * shortCount, Byte)
          End If

          Dim data(shortCount * 2 - 1) As Byte
          Dim uShortArray() As UShort = TryCast(values, UShort())
          If uShortArray IsNot Nothing Then
            For i As Integer = 0 To count - 1
              BitConverterLittleEndian.GetBytes(uShortArray(i + 1)).CopyTo(data, i * 2)
            Next i
          Else
            Dim singleArray() As Single = TryCast(values, Single())
            If singleArray IsNot Nothing Then
              For i As Integer = 0 To count - 1
                BitConverterLittleEndian.GetBytes(singleArray(i + 1)).CopyTo(data, i * 4)
              Next i
            Else
              Dim booleanArray() As Boolean = TryCast(values, Boolean())
              If booleanArray IsNot Nothing Then
                Dim i As Integer
                Do While i * 16 < count
                  Dim startIndex As Integer = i * 16, _
                      encodeBitCount As Integer = Math.Min(count - startIndex, 16)
                  BitConverterLittleEndian.GetBytes(BitEncoding.ToInt16(booleanArray, startIndex + 1, encodeBitCount)).CopyTo(data, i * 2)
                  i += 1
                Loop
              Else
                Dim shortArray() As Short = DirectCast(values, Short())
                For i As Integer = 0 To count - 1
                  BitConverterLittleEndian.GetBytes(shortArray(i + 1)).CopyTo(data, i * 2)
                Next i
              End If
            End If
          End If

          ' Put in the data and begin the write
          If shortCount = 1 Then
            data.CopyTo(tx, 4)
          Else
            data.CopyTo(tx, 7)
          End If
          BeginWriteAndReadStoreCrc(tx, 8)
        Else
          Throw New ArgumentOutOfRangeException("firstRegister")
        End If
      End If

      ' See if we're finished
      RunStateMachine(slaveAddress, firstRegister, False)
      Dim ret = GetIdleResult()
      If ret = Result.OK AndAlso writeMode = Ports.WriteMode.Optimised Then
        writeOptimisation_.SuccessfulWrite(values, slaveAddress, firstRegister)
      End If
      Return ret
    End Function

    Private Enum Func As Byte
      ReadOutputTable = 1
      ReadInputTable
      ReadHoldingRegisters
      ReadInputRegisters
      ForceSingleOutput
      PresetSingleRegister
      ForceMultipleOutputs = 15
      PresetMultipleRegisters
    End Enum

    Private NotInheritable Class BitConverterLittleEndian
      Private Sub New()
      End Sub
      Shared Function GetBytes(value As Int16) As Byte()
        Dim ret() As Byte = System.BitConverter.GetBytes(value)
        Dim b As Byte = ret(0) : ret(0) = ret(1) : ret(1) = b
        Return ret
      End Function
      Shared Function GetBytes(value As UInt16) As Byte()
        Dim ret() As Byte = System.BitConverter.GetBytes(value)
        Dim b As Byte = ret(0) : ret(0) = ret(1) : ret(1) = b
        Return ret
      End Function
      Shared Function GetBytes(value As Int32) As Byte()
        Dim ret() As Byte = System.BitConverter.GetBytes(value)
        Return New Byte() {ret(3), ret(2), ret(1), ret(0)}
      End Function
      Shared Function GetBytes(value As Single) As Byte()
        Dim ret() As Byte = System.BitConverter.GetBytes(value)
        Return New Byte() {ret(3), ret(2), ret(1), ret(0)}
      End Function

      Shared Function ToInt16(value() As Byte, startIndex As Integer) As Short
        Return System.BitConverter.ToInt16(New Byte() {value(startIndex + 1), value(startIndex)}, 0)
      End Function
      Shared Function ToUInt16(value() As Byte, startIndex As Integer) As UShort
        Return System.BitConverter.ToUInt16(New Byte() {value(startIndex + 1), value(startIndex)}, 0)
      End Function
      Shared Function ToInt32(value() As Byte, startIndex As Integer) As Integer
        Return System.BitConverter.ToInt32(New Byte() {value(startIndex + 3), value(startIndex + 2), value(startIndex + 1), value(startIndex)}, 0)
      End Function
      Shared Function ToSingle(value() As Byte, startIndex As Integer) As Single
        Return System.BitConverter.ToSingle(New Byte() {value(startIndex + 3), value(startIndex + 2), value(startIndex + 1), value(startIndex)}, 0)
      End Function
    End Class

    Private NotInheritable Class BitEncoding
      Private Sub New()
      End Sub

      Shared Function ToByte(value() As Boolean, startIndex As Integer, length As Integer) As Byte
        Dim ret As Integer
        For i As Integer = length - 1 To 0 Step -1
          ret *= 2
          If value(startIndex + i) Then ret += 1
        Next i
        Return CType(ret, Byte)
      End Function

      Shared Function ToInt16(value() As Boolean, startIndex As Integer, length As Integer) As Short
        Dim ret As Integer
        For i As Integer = length - 1 To 0 Step -1
          ret *= 2
          If value(startIndex + i) Then ret += 1
        Next i
        If ret < 32768 Then Return CType(ret, Short)
        Return CType(ret - 65536, Short)
      End Function
    End Class
  End Class

End Namespace
