Imports System.Data
Imports System.Globalization.CultureInfo

Partial Public NotInheritable Class Utilities
  Public NotInheritable Class Sql

#Region " Select "

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataRow(ByVal connectionString As String, ByVal selectString As String) As DataRow
      'Get a data row from the passed connection string and select statement
      '  return nothing if no row found, more than one row found or an exception is thrown
      Try
        Dim dt As New DataTable
        Dim da As New SqlClient.SqlDataAdapter

        da.SelectCommand = New SqlClient.SqlCommand
        With da.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : da.Fill(dt) : .Connection.Close()
          If dt.Rows.Count = 1 Then Return dt.Rows(0)
        End With

      Catch ex As Exception
        'Just return nothing if we get an error
        'Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataRow(ByVal connectionString As String, ByVal selectString As String, ByVal tableName As String) As DataRow
      'Get a data row from the passed connection string and select statement 
      '  set the table name so this row can be used with the update and insert functions here...
      '  return nothing if no row found, more than one row found or an exception is thrown
      Try
        Dim dt As New DataTable(tableName)
        Dim da As New SqlClient.SqlDataAdapter

        da.SelectCommand = New SqlClient.SqlCommand
        With da.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : da.Fill(dt) : .Connection.Close()
          If dt.Rows.Count = 1 Then Return dt.Rows(0)
        End With

      Catch ex As Exception
        'Just return nothing if we get an error
        'Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataRow(ByVal connectionString As String, ByVal tableName As String, ByVal id As Integer) As DataRow
      'Get a data row from the passed connection string table name and id
      '  set the table name so this row can be used with the update and insert functions here...
      '  return nothing if no row found, more than one row found or an exception is thrown
      Try
        Dim dt As New DataTable(tableName)
        Dim da As New SqlClient.SqlDataAdapter

        da.SelectCommand = New SqlClient.SqlCommand
        With da.SelectCommand
          .CommandText = "SELECT * FROM " & tableName & " WHERE ID=" & id.ToString(InvariantCulture)
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : da.Fill(dt) : .Connection.Close()
          If dt.Rows.Count = 1 Then Return dt.Rows(0)
        End With

      Catch Ex As Exception
        'Just return nothing if we get an error
        'Utilities.Log.LogError(Ex)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataTable(ByVal connectionString As String, ByVal selectString As String) As DataTable
      Try
        'Get a data table from the passed connection string and select string
        '  return nothing if no table found or an exception is thrown
        Dim dt As New DataTable
        Dim da As New SqlClient.SqlDataAdapter

        da.SelectCommand = New SqlClient.SqlCommand
        With da.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : da.Fill(dt) : .Connection.Close()
          Return dt
        End With

      Catch ex As Exception
        'Just return nothing if we get an error
        'Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataTable(ByVal connectionString As String, ByVal selectString As String, ByVal tableName As String) As DataTable
      'Get a data table from the passed connection string and select string
      '  set the table name so this table can be used with the update and insert functions here...
      '  return nothing if no table found or an exception is thrown
      Try
        Dim dt As New DataTable(tableName)
        Dim da As New SqlClient.SqlDataAdapter

        da.SelectCommand = New SqlClient.SqlCommand
        With da.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : da.Fill(dt) : .Connection.Close()
          Return dt
        End With

      Catch ex As Exception
        'Just return nothing if we get an error
        'Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataTableSchema(ByVal connectionString As String, ByVal tableName As String) As DataTable
      'Get the Table Schema for the target table
      '  this will give the structure of table so we can use DataTable.NewRow
      '  assumes the table has an ID column that is AutoIncrement - not sure this has much effect
      '  return nothing if no table found or an exception is thrown
      Try
        Dim dt As New DataTable(tableName)
        Dim da As New SqlClient.SqlDataAdapter

        da.SelectCommand = New SqlClient.SqlCommand
        With da.SelectCommand
          .CommandText = "SELECT * FROM " & tableName
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : da.FillSchema(dt, SchemaType.Source) : .Connection.Close()
        End With

        'Set the AutoIncrement properties - this is not imported with the schema so we must manually set it
        dt.Columns("ID").AutoIncrement = True
        dt.Columns("ID").AutoIncrementSeed = 1
        dt.Columns("ID").AutoIncrementStep = 1

        Return dt
      Catch ex As Exception
        'Just return nothing if we get an error
        'Utilities.Log.LogError(ex)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataTableSchema(ByVal connectionString As String, ByVal tableName As String, ByVal primaryKey As String) As DataTable
      Try
        'Get the Schema for the target table
        '  this will give the structure of table so we can use DataTable.NewRow
        '  doing this here and passing a reference to reduce the number of calls to the database
        Dim dt As New DataTable(tableName)
        Dim da As New SqlClient.SqlDataAdapter

        da.SelectCommand = New SqlClient.SqlCommand
        With da.SelectCommand
          .CommandText = "SELECT * FROM " & tableName
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : da.FillSchema(dt, SchemaType.Source) : .Connection.Close()
        End With

        'Set primary key if it has been passed
        dt.PrimaryKey = New DataColumn() {dt.Columns(primaryKey)}

        Return dt
      Catch ex As Exception
        'Just return nothing if we get an error
        'Utilities.Log.LogError(ex)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlSelect(ByVal connectionString As String, ByVal selectString As String) As DataTable
      Return GetDataTable(connectionString, selectString)
    End Function

#End Region

#Region " Update "

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function UpdateDataRow(ByVal connectionString As String, ByVal dr As DataRow) As Boolean
      'Write changes in a data row back to the database
      '  return false if write fails or an exception is thrown
      Try
        'Make sure the data row is not empty
        If dr Is Nothing Then Return False
        Dim dt As DataTable = dr.Table

        Dim da As New SqlClient.SqlDataAdapter
        da.UpdateCommand = New SqlClient.SqlCommand

        With da.UpdateCommand
          'Add parameters and build insert string based on table schema
          AddSqlParameters(dt, .Parameters)
          BuildSqlUpdateString(dt, .CommandText)

          'Set values of all parameters from the data row
          For Each dc As DataColumn In dt.Columns
            .Parameters("@" & dc.ColumnName).Value = dr(dc.ColumnName)
          Next

          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open()
          Dim rows As Integer = .ExecuteNonQuery()
          .Connection.Close()
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Sub BuildSqlUpdateString(ByVal dt As DataTable, ByRef commandText As String)
      'Build a sql update string based on the table schema
      Dim sql As String = Nothing
      Try
        'Make sure we have a DataTable to work with
        If dt Is Nothing Then Exit Sub

        'Make the sql string
        For Each dc As DataColumn In dt.Columns
          If dc.ColumnName.ToLower <> "id" Then
            sql &= "[" & dc.ColumnName.ToString & "]=@" & dc.ColumnName.ToString & ","
          End If
        Next
        'remove the last comma
        sql = sql.Substring(0, sql.Length - 1)

        commandText = "UPDATE " & dt.TableName & " SET " & sql & " WHERE ID=@ID"
      Catch ex As Exception
        Utilities.Log.LogError(ex, sql)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlUpdate(ByVal connectionString As String, ByVal updateString As String) As Integer
      Try
        Dim rows As Integer
        Dim da As New SqlClient.SqlDataAdapter
        da.UpdateCommand = New SqlClient.SqlCommand
        With da.UpdateCommand
          .CommandText = updateString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : rows = .ExecuteNonQuery() : .Connection.Close()
        End With

        Return rows
      Catch ex As Exception
        Utilities.Log.LogError(ex, updateString)
      End Try
      Return -1
    End Function

#End Region

#Region " Delete "

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlDelete(ByVal connectionString As String, ByVal deleteString As String) As Integer
      Try
        Dim rows As Integer
        Dim da As New SqlClient.SqlDataAdapter
        da.DeleteCommand = New SqlClient.SqlCommand
        With da.DeleteCommand
          .CommandText = deleteString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : rows = .ExecuteNonQuery() : .Connection.Close()
        End With

        Return rows
      Catch ex As Exception
        Utilities.Log.LogError(ex, deleteString)
      End Try
      Return -1
    End Function

#End Region

#Region " Insert "

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function InsertDataRow(ByVal connectionString As String, ByVal dr As DataRow) As Boolean
      Try
        'Must have a valid data row
        If dr Is Nothing Then Return False

        Dim dt As DataTable = dr.Table
        Dim da As New SqlClient.SqlDataAdapter

        da.InsertCommand = New SqlClient.SqlCommand
        With da.InsertCommand

          'Add parameters and build insert string based on table schema
          AddSqlParameters(dt, .Parameters)
          BuildSqlInsertString(dt, .CommandText)

          'Set values of all parameters from the data row
          For Each dc As DataColumn In dt.Columns
            .Parameters("@" & dc.ColumnName).Value = dr(dc.ColumnName)
          Next

          Dim autoIncrement As Integer = -1
          .CommandText = .CommandText & "; SELECT CAST(scope_identity() AS int);"
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : autoIncrement = Integer.Parse(.ExecuteScalar().ToString) : .Connection.Close()
          dr("ID") = autoIncrement
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function InsertDataRowWithID(ByVal connectionString As String, ByVal dr As DataRow) As Boolean
      Try
        'Must have a valid data row
        If dr Is Nothing Then Return False

        Dim dt As DataTable = dr.Table
        Dim da As New SqlClient.SqlDataAdapter

        da.InsertCommand = New SqlClient.SqlCommand
        With da.InsertCommand

          'Add parameters and build insert string based on table schema
          AddSqlParameters(dt, .Parameters)
          BuildSqlInsertStringWithID(dt, .CommandText)

          'Set values of all parameters from the data row
          For Each dc As DataColumn In dt.Columns
            .Parameters("@" & dc.ColumnName).Value = dr(dc.ColumnName)
          Next

          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : .ExecuteNonQuery() : .Connection.Close()
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function InsertDataTable(ByVal connectionString As String, ByVal dt As DataTable) As Boolean
      Try
        'Must have a valid data table
        If dt Is Nothing Then Return False

        Dim rows As Integer
        Dim da As New SqlClient.SqlDataAdapter
        With da
          .InsertCommand = New SqlClient.SqlCommand

          With .InsertCommand
            AddSqlParameters(dt, .Parameters)
            BuildSqlInsertString(dt, .CommandText)

            .Connection = New SqlClient.SqlConnection(connectionString)
            For Each dr As DataRow In dt.Rows
              For Each dc As DataColumn In dt.Columns
                .Parameters("@" & dc.ColumnName).Value = dr(dc.ColumnName)
              Next
              .Connection.Open() : rows = .ExecuteNonQuery() : .Connection.Close()
            Next
          End With
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function InsertDataTable(ByVal connectionString As String, ByVal dt As DataTable, ByVal sqlDelete As String) As Boolean
      'Insert a datatable into the database - this is used to update detail tables using a transactiion
      '  we start the transaction, delete the original records then insert the new records
      '  the transaction will be rolled back if any errors occur
      'Create the connection and declare the command and transaction objects
      Try
        Dim connection As New SqlClient.SqlConnection(connectionString)
        Dim deleteCommand As SqlClient.SqlCommand
        Dim insertCommand As SqlClient.SqlCommand
        Dim transaction As SqlClient.SqlTransaction

        'Open the connection and make the command and transaction objects on this connection
        connection.Open()
        transaction = connection.BeginTransaction
        deleteCommand = connection.CreateCommand : deleteCommand.Transaction = transaction
        insertCommand = connection.CreateCommand : insertCommand.Transaction = transaction

        Dim sql As String = Nothing
        Try
          'Delete any existing records
          With deleteCommand
            sql = sqlDelete ' just so we get the sql statement if we throw an error
            .CommandText = sql
            .ExecuteNonQuery()
          End With

          'Insert new or modified records
          With insertCommand
            'Create the parameters we will use for the insert command
            AddSqlParameters(dt, .Parameters)

            'Insert all the datarows preserving ID where it is set
            For Each dr As DataRow In dt.Rows
              If dr.RowState <> DataRowState.Deleted Then
                'Set values of all parameters from the data row
                For Each dc As DataColumn In dt.Columns
                  insertCommand.Parameters("@" & dc.ColumnName).Value = dr(dc.ColumnName)
                Next
                'Make the insert string this will be different if ID is null (new record)
                If dr.IsNull("ID") Then
                  BuildSqlInsertString(dt, sql)
                  sql &= "; SELECT CAST(scope_identity() AS int);"
                  .CommandText = sql
                  dr("ID") = Integer.Parse(.ExecuteScalar().ToString)
                Else
                  BuildSqlInsertStringWithID(dt, sql)
                  sql = "SET IDENTITY_INSERT " & dt.TableName & " ON; " & sql & "; SET IDENTITY_INSERT " & dt.TableName & " OFF;"
                  .CommandText = sql
                  .ExecuteNonQuery()
                End If
              End If
            Next
          End With

          'Commit the changes 
          transaction.Commit()
          Return True
        Catch ex As Exception
          Utilities.Log.LogError(ex, sql)
        End Try
        transaction.Rollback()

      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Sub BuildSqlInsertString(ByVal dt As DataTable, ByRef commandText As String)
      'Make a Sql insert (using sql parameters) to insert a row into the database
      '  assumes ID is an AutoIncrement field and will be filled in by the database
      Try
        If dt Is Nothing Then Exit Sub

        Dim columns As String = "", values As String = ""
        For Each dc As DataColumn In dt.Columns
          If dc.ColumnName <> "ID" Then
            If columns.Length = 0 Then
              columns = "[" & dc.ColumnName.ToString & "]"
            Else
              columns &= ",[" & dc.ColumnName.ToString & "]"
            End If
            If values.Length = 0 Then
              values = "@" & dc.ColumnName.ToString
            Else
              values &= ",@" & dc.ColumnName.ToString
            End If
          End If
        Next
        commandText = "INSERT INTO " & dt.TableName & " (" & columns & ") VALUES(" & values & ")"
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Sub BuildSqlInsertStringWithID(ByVal dt As DataTable, ByRef commandText As String)
      'Make a Sql insert (using sql parameters) to insert a row into the database
      '  assumes ID is an AutoIncrement field but trys to insert the existing ID value 
      '  this can be used to delete and re-insert records with the original ID values
      Try
        If dt Is Nothing Then Exit Sub

        Dim columns As String = "", values As String = ""
        For Each dc As DataColumn In dt.Columns
          If columns.Length = 0 Then
            columns = "[" & dc.ColumnName.ToString & "]"
          Else
            columns &= ",[" & dc.ColumnName.ToString & "]"
          End If
          If values.Length = 0 Then
            values = "@" & dc.ColumnName.ToString
          Else
            values &= ",@" & dc.ColumnName.ToString
          End If
        Next
        commandText = "INSERT INTO " & dt.TableName & " (" & columns & ") VALUES(" & values & ")"
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlInsert(ByVal connectionString As String, ByVal insertString As String) As Integer
      Try
        Dim rows As Integer
        Dim da As New SqlClient.SqlDataAdapter
        da.InsertCommand = New SqlClient.SqlCommand
        With da.InsertCommand
          .CommandText = insertString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : rows = .ExecuteNonQuery() : .Connection.Close()
        End With

        Return rows
      Catch ex As Exception
        Utilities.Log.LogError(ex, insertString)
      End Try
      Return -1
    End Function
#End Region

#Region " Copy "

    ' Copy matching data from one table to another
    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function CopyTableData(sourceTable As System.Data.DataTable, targetTable As System.Data.DataTable) As Boolean
      Try
        'Clear existing data
        targetTable.Clear()

        For Each row As System.Data.DataRow In sourceTable.Rows
          Try
            Dim newRow As System.Data.DataRow = targetTable.NewRow
            If Utilities.Sql.CopyRowData(row, newRow) Then
              targetTable.Rows.Add(newRow)
            End If
          Catch ex As Exception
            'Don't let one error stop the copy
          End Try
        Next
        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function

    'Copy matching data from source to target (name and data type must match)
    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function CopyRowData(sourceRow As System.Data.DataRow, targetRow As System.Data.DataRow) As Boolean
      Try
        For Each column As System.Data.DataColumn In sourceRow.Table.Columns
          Try
            Dim columnName As String = column.ColumnName
            If targetRow.Table.Columns.Contains(columnName) Then
              If column.DataType Is targetRow.Table.Columns(columnName).DataType Then
                targetRow(columnName) = sourceRow(columnName)
              End If
            End If
          Catch ex As Exception
            'Don't let one error stop the copy
          End Try
        Next
        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function

#End Region

#Region " Utilities "

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Sub AddSqlParameters(ByVal dt As DataTable, ByRef pc As SqlClient.SqlParameterCollection)
      Try
        If dt Is Nothing Then Exit Sub

        pc.Clear()
        For Each dc As DataColumn In dt.Columns
          'Create a parameter for every column in the table
          Dim p As New SqlClient.SqlParameter()
          p.ParameterName = "@" & dc.ColumnName
          If dc.DataType.Name = "Int16" Then p.SqlDbType = SqlDbType.SmallInt
          If dc.DataType.Name = "Int32" Then p.SqlDbType = SqlDbType.Int
          If dc.DataType.Name = "String" Then p.SqlDbType = SqlDbType.NVarChar
          If dc.DataType.Name = "DateTime" Then p.SqlDbType = SqlDbType.DateTime
          If dc.DataType.Name = "Double" Then p.SqlDbType = SqlDbType.Float
          If dc.DataType.Name = "Decimal" Then p.SqlDbType = SqlDbType.Decimal
          pc.Add(p)
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlExecute(ByVal connectionString As String, ByVal sqlString As String) As Boolean
      Try
        Dim da As New SqlClient.SqlDataAdapter
        da.UpdateCommand = New SqlClient.SqlCommand
        With da.UpdateCommand
          .CommandText = sqlString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : .ExecuteNonQuery() : .Connection.Close()
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex, sqlString)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function TableExists(connectionString As String, tableName As String) As Boolean
      Dim sql As String
      Try
        sql = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=" & SqlString(tableName)
        Dim dr As System.Data.DataTable = SqlSelect(connectionString, sql)

        If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then Return True
      Catch ex As Exception
        'Ignore errors 
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlString(ByVal value As Object) As String
      Try
        'If this is an empty string just return null
        If value Is Nothing Then Return "Null"

        'TODO check for null
        Return SqlString(value.ToString)

      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "Null"
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlString(ByVal value As String) As String
      Dim returnString As String = Nothing
      Try
        'If this is an empty string just return null
        If value Is Nothing OrElse value.Length = 0 Then Return "Null"

        'Look for single quotes (') and double up
        For i As Integer = 0 To value.Length - 1
          Select Case value.Substring(i, 1)
            Case "'" : returnString &= value.Substring(i, 1) & "'"
            Case Else : returnString &= value.Substring(i, 1)
          End Select
        Next i
        Return "'" & returnString & "'"
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "Null"
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlDateString(ByVal value As String) As String
      Try
        If value = Nothing OrElse value.Length <= 0 Then
          Return "Null"
        Else
          Dim valueToDate As Date
          If Date.TryParse(value, valueToDate) Then
            Return SqlDateString(valueToDate)
          Else
            Return "Null"
          End If
        End If
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "Null"
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlDateString(ByVal value As Date) As String
      Try
        If value = Nothing Then
          Return "Null"
        Else
          Return SqlString(value.ToString(InvariantCulture))
        End If
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "Null"
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function EmptyStringToNull(ByVal dr As DataRow, ByVal column As String) As String
      Try
        If dr.IsNull(column) Then Return "Null"
        If dr(column).ToString.Length <= 0 Then Return "Null"
        Return SqlString(dr(column).ToString)
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "Null"
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function NullToNothingString(ByVal value As String) As String
      Try
        'Check to see if it's null
        If String.IsNullOrEmpty(value) Then Return Nothing

        'String isn't null so just return the string
        Return value
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function NullToNothingString(ByVal value As Object) As String
      Try
        'Check to see if it's a string
        If TypeOf value Is String Then Return DirectCast(value, String)

        'If not return nothing
        Return Nothing
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function NullToZeroInteger(ByVal value As String) As Integer
      Try
        'Check to see if it's null
        If String.IsNullOrEmpty(value) Then Return 0

        'See if we can parse it
        Dim tryInteger As Integer
        If Integer.TryParse(value, tryInteger) Then Return tryInteger
      Catch ex As Exception
        'No code
      End Try
      Return 0
    End Function

    '<System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function NullToZeroInteger(ByVal value As Object) As Integer
      Try
        'Check to see if it's an integer
        If TypeOf value Is Integer Then Return DirectCast(value, Integer) ' int

        'Check to see if it's a small integer
        If TypeOf value Is Short Then Return DirectCast(value, Short) ' smallint

        'Check to see if it's a tiny integer
        If TypeOf value Is Byte Then Return DirectCast(value, Byte) ' tinyint

        'Check to see if it will convert to string and parse
        If TypeOf value Is String Then
          Dim tryInteger As Integer
          If Integer.TryParse(DirectCast(value, String), tryInteger) Then Return tryInteger
        End If

        'If not return 0
        Return 0
      Catch ex As Exception
        'No code
      End Try
      Return 0
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function NullToZeroDouble(ByVal value As String) As Double
      Try
        'Check to see if it's null
        If String.IsNullOrEmpty(value) Then Return 0

        'See if we can parse it
        Dim tryDouble As Double
        If Double.TryParse(value, tryDouble) Then Return tryDouble
      Catch ex As Exception
        'No code
      End Try
      Return 0
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function NullToZeroDouble(ByVal value As Object) As Double
      Try
        'Check to see if it's a double
        If TypeOf value Is Double Then Return DirectCast(value, Double)

        'Check to see if it is a string
        If TypeOf value Is String Then
          Dim tryDouble As Double
          If Double.TryParse(DirectCast(value, String), tryDouble) Then Return tryDouble
        End If

        'If not return 0
        Return 0
      Catch ex As Exception
        'No code
      End Try
      Return 0
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function NullToNothingDate(ByVal value As String) As Date
      Try
        'Check to see if it's null
        If String.IsNullOrEmpty(value) Then Return Nothing

        'See if we can parse it
        Dim tryDate As Date
        If Date.TryParse(value, tryDate) Then Return tryDate
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function NullToNothingDate(ByVal value As Object) As Date
      Try
        'Check to see if it's a date
        If TypeOf value Is Date Then Return DirectCast(value, Date)

        'If it's not a date return nothing
        Return Nothing
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    '<System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function DateToLocalTimeString(ByVal value As Object) As String
      Try
        'Check to see if it's empty
        If value Is Nothing Then Return Nothing

        'Check to see if it's a date
        If TypeOf value Is Date Then Return DirectCast(value, Date).ToLocalTime.ToString

        'If it's not a date return nothing
        Return Nothing
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    '<System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Sub ConvertDateTimeToLocalTime(ByVal table As System.Data.DataTable)
      Dim columnName As String
      Try
        'Make sure we have some data to work with
        If table Is Nothing OrElse table.Rows.Count <= 0 Then Exit Sub

        'Loop through the columns and convert each DateTime column to local time
        For Each column As Data.DataColumn In table.Columns
          If column.DataType Is System.Type.GetType("System.DateTime") Then
            columnName = column.ColumnName
            'Loop through the rows and convert all non-null date columns to local time
            For Each row As System.Data.DataRow In table.Rows
              If Not row.IsNull(columnName) Then
                row(columnName) = DirectCast(row(columnName), Date).ToLocalTime
              End If
            Next
          End If
        Next
      Catch ex As Exception
        'Ignore errors
      End Try
    End Sub

    Public Shared Sub ConvertDateTimeToLocalTime(ByVal row As System.Data.DataRow)
      Dim columnName As String
      Try
        'Get the table associated with this datarow
        Dim table As System.Data.DataTable = row.Table

        'Make sure we have some data to work with
        If table Is Nothing OrElse row Is Nothing Then Exit Sub

        'Loop through the columns and convert each DateTime column to local time
        For Each column As Data.DataColumn In table.Columns
          If column.DataType Is System.Type.GetType("System.DateTime") Then
            columnName = column.ColumnName
            'Only convert non-null values
            If Not row.IsNull(columnName) Then
              row(columnName) = DirectCast(row(columnName), Date).ToLocalTime
            End If
          End If
        Next
      Catch ex As Exception
        'Ignore errors
      End Try
    End Sub

    Public Shared Sub ConvertDateTimeToUniversalTime(ByVal table As System.Data.DataTable)
      Dim columnName As String
      Try
        'Make sure we have some data to work with
        If table Is Nothing OrElse table.Rows.Count <= 0 Then Exit Sub

        'Loop through the columns and convert each date column to local time
        For Each column As Data.DataColumn In table.Columns
          If column.DataType Is System.Type.GetType("System.DateTime") Then
            columnName = column.ColumnName
            'Loop through the rows and convert all non-null date columns to local time
            For Each row As System.Data.DataRow In table.Rows
              If Not row.IsNull(columnName) Then
                row(columnName) = DirectCast(row(columnName), Date).ToUniversalTime
              End If
            Next
          End If
        Next
      Catch ex As Exception
        'Ignore errors
      End Try
    End Sub

    Public Shared Sub ConvertDateTimeToUniversalTime(ByVal row As System.Data.DataRow)
      Dim columnName As String
      Try
        'Get the table associated with this datarow
        Dim table As System.Data.DataTable = row.Table

        'Make sure we have some data to work with
        If table Is Nothing OrElse row Is Nothing Then Exit Sub

        'Loop through the columns and convert each DateTime column to local time
        For Each column As Data.DataColumn In table.Columns
          If column.DataType Is System.Type.GetType("System.DateTime") Then
            columnName = column.ColumnName
            'Only convert non-null values
            If Not row.IsNull(columnName) Then
              row(columnName) = DirectCast(row(columnName), Date).ToUniversalTime
            End If
          End If
        Next
      Catch ex As Exception
        'Ignore errors
      End Try
    End Sub


    ' Compare the data in two data tables - return true if the data is the same 
    '   assumes tables have the same sort order
    Public Shared Function CompareDataTables(table1 As System.Data.DataTable, table2 As System.Data.DataTable) As Boolean
      Try
        'Make sure we have data to work with
        If table1 Is Nothing OrElse table2 Is Nothing Then Return False

        'Make sure the tables have the same number of rows
        If table1.Rows.Count <> table2.Rows.Count Then Return False

        'Check all rows
        For i = 0 To table1.Rows.Count - 1
          If Not CompareDataRow(table1.Rows(i), table2.Rows(i)) Then Return False
        Next

        'If we get this far everything must be the same
        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function

    Public Shared Function CompareDataRow(row1 As System.Data.DataRow, row2 As System.Data.DataRow) As Boolean
      Try
        'Make sure we have data 
        If row1 Is Nothing OrElse row2 Is Nothing Then Return False

        'Make sure the rows have the same number of columns
        If row1.Table.Columns.Count <> row2.Table.Columns.Count Then Return False

        'Check each column
        For i = 0 To row1.Table.Columns.Count - 1
          If row1.Item(i).ToString <> row2.Item(i).ToString Then Return False
        Next

        'If we get this far everything must be the same
        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function

#End Region

  End Class
End Class