using System;
using ChessGame.Enums;

namespace ChessGame.DAL;

public class CGPlayerManager
{
    private static CGPlayerManager _inst;

    private Player _player1;
    private Player _player2;

    public bool player1IsLoggedIn { get; private set; }
    public bool player2IsLoggedIn { get; private set; }

    public static CGPlayerManager GetInstance()
    {
        if (_inst == null)
        {
            _inst = new CGPlayerManager();
        }

        return _inst;
    }


    private CGPlayerManager()
    {
        player1IsLoggedIn = false;
        player2IsLoggedIn = false;
    }

    public void AssignPlayer(Player player, int playerIndex)
    {
        switch (playerIndex)
        {
            case 1:
                if (player2IsLoggedIn && _player2.GetUsername() == player.GetUsername())
                    throw new Exception("This user is already logged in");
                _player1 = player;
                player1IsLoggedIn = true;
                break;
            case 2:
                if (player1IsLoggedIn && _player1.GetUsername() == player.GetUsername())
                    throw new Exception("This user is already logged in"); 
                _player2 = player;
                player2IsLoggedIn = true;
                break;
            default:
                throw new Exception("There's no space for the player to be assigned");
        }
    }

    public void LogOutPlayer(int playerIndex)
    {
        switch (playerIndex)
        {
            case 1:
                _player1 = null;
                player1IsLoggedIn = false;
                break;
            case 2:
                _player2 = null;
                player2IsLoggedIn = false;
                break;
            default:
                throw new Exception("There cannot be more than two players");
        }
    }

    public void CalculatePlayerWin(CGTeam winner)
    {
        if (winner == CGTeam.White)
        {
            UtilityDB.ChangePlayerElo(_player1, 10);
            if (_player2.Elo > 0)
                UtilityDB.ChangePlayerElo(_player2, -10);
        }
        else
        {
            UtilityDB.ChangePlayerElo(_player2, 10);
            if (_player1.Elo > 0)
                UtilityDB.ChangePlayerElo(_player1, -10);
        }
    }
}