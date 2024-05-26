using System.Data.SQLite;

public class DatabaseContext
{
    private const string ConnectionString = "Data Source=BookstoreDB.db;Version=3;";

    public SQLiteConnection Connection { get; }

    public DatabaseContext()
    {
        Connection = new SQLiteConnection(ConnectionString);
    }

    public void OpenConnection()
    {
        if (Connection.State != System.Data.ConnectionState.Open)
        {
            Connection.Open();
        }
    }

    public void CloseConnection()
    {
        if (Connection.State != System.Data.ConnectionState.Closed)
        {
            Connection.Close();
        }
    }
}
