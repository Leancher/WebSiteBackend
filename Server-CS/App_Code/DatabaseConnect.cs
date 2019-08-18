using System.Diagnostics;
using System.Data.SQLite;

public class DatabaseConnect
{
    SQLiteConnection Database = new SQLiteConnection();
    SQLiteCommand Command = new SQLiteCommand();
    public DatabaseConnect()
    {

    }
    void DatabaseOpen()
    {
        string DBPath = Utilites.GetAppPath() + @"\" + "Server-CS" + @"\" + "Database.db";        
        Database = new SQLiteConnection("Data Source=" + DBPath + "; Version=3;");
        Database.Open();
    }
    public int GetCountItems(string NameTable)
    {
        DatabaseOpen();
        Command = Database.CreateCommand();
        Command.CommandText = "SELECT Count (*) From " + NameTable;
        SQLiteDataReader ReadItem = Command.ExecuteReader();
        ReadItem.Read();
        int Item = 0;
        int.TryParse(ReadItem[0].ToString(), out Item);
        ReadItem.Close();
        Database.Dispose();
        return Item;
    }
    public string GetItemByID(string NameTable, int ItemID, string Prop)
    {
        DatabaseOpen();
        Command = Database.CreateCommand();
        Command.CommandText = "SELECT * FROM " + NameTable + " WHERE ID=" + ItemID.ToString();
        SQLiteDataReader ReadItem = Command.ExecuteReader();
        while(ReadItem.Read())
        {
            int NumberColumn = ReadItem.GetOrdinal(Prop);
            string Item = ReadItem.GetValue(NumberColumn).ToString();
            ReadItem.Close();
            Database.Dispose();
            return Item;
        }
        return "";
    }
}