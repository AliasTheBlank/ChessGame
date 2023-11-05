namespace ChessGame;

public class DBConfig
{
    public string Host;
    public string Username;
    public string Password;
    public string Database;

    public override string ToString()
    {
        return $"Host={Host};Username={Username};Password={Password};Database={Database}";
    }
}