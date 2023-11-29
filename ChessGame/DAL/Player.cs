namespace ChessGame.DAL;

public class Player
{
    private string _username;
    public int Elo { get; private set;  }

    public Player(string username, int elo)
    {
        _username = username;
        Elo = elo;
    }

    public void ChangeEloPoints(int points)
    {
        Elo += points;
    }

    public string GetUsername()
    {
        return _username;
    }
}