using System.Data.SQLite;

public class AuthenticationService
{
    private readonly DatabaseContext _dbContext;

    public AuthenticationService(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Authenticate(string username, string password)
    {
        _dbContext.OpenConnection();
        string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
        using (var cmd = new SQLiteCommand(query, _dbContext.Connection))
        {
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            _dbContext.CloseConnection();
            return count > 0;
        }
    }
}
