Imports System.Data.SQLite
Public Class DatabaseConnect
    Dim Database As SQLiteConnection
    Dim Command As New SQLiteCommand
    Public Sub DatabaseOpen()
        Database = New SQLiteConnection("Data Source=" + Config.GetAppPath() + "\Server\Database.db; Version=3;")
        Database.Open()
    End Sub

    Public Sub UpdateViewValue(TableName As String, ItemID As String, Count As String)
        Command = Database.CreateCommand()
        Command.CommandText = "UPDATE " + TableName + " SET Viewed='" + Count + "' WHERE ID=" + ItemID
        Command.ExecuteNonQuery()
    End Sub

    Public Function GetCountItems(NameTable As String) As Integer
        DatabaseOpen()
        Command = Database.CreateCommand()
        Command.CommandText = "SELECT Count (*) From " + NameTable
        Dim ReadItem = Command.ExecuteReader()
        ReadItem.Read()
        Dim Item = ReadItem(0)
        ReadItem.Close()
        Database.Dispose()
        Return Item
    End Function
    Public Function GetItemID(NameTable As String, ItemName As String) As String
        Command = Database.CreateCommand()
        Command.CommandText = "SELECT * FROM " + Config.CategoryTable + " WHERE Name LIKE '" + ItemName + "'"
        Dim ReadItem = Command.ExecuteReader()
        While ReadItem.Read()
            Dim Item = ReadItem.Item("ID").ToString
            ReadItem.Close()
            Return Item
        End While
        Return ""
    End Function
    Public Function GetItemByID(NameTable As String, ItemID As String, ItemProperty As String) As String
        DatabaseOpen()
        Command = Database.CreateCommand()
        Command.CommandText = "SELECT * FROM " + NameTable + " WHERE ID=" + ItemID
        Dim ReadItem = Command.ExecuteReader()
        While ReadItem.Read()
            Dim Item = ReadItem.Item(ItemProperty).ToString
            ReadItem.Close()
            Database.Dispose()
            Return Item
        End While
        Return ""
    End Function

    Public Sub DatabaseClose()
        Database.Dispose()
    End Sub
End Class