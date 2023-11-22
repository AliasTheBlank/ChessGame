namespace ChessGame.DAL;

public class Player
{
    private string _username;
    private int _elo;

    public Player(string username, int elo)
    {
        _username = username;
        _elo = elo;
    }

    public void ChangeEloPoints(int points)
    {
        _elo += points;
    }

    public string GetUsername()
    {
        return "";
    }
}