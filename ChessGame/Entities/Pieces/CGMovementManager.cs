using System;
using System.Collections.Generic;
using ChessGame.Entities.Board;
using ChessGame.Enums;
using Nez;

namespace ChessGame.Entities.Pieces;

public class CGMovementManager
{
    private static CGMovementManager _instMovementManager;
    
    public static CGTile[,] Board;

    private bool _isTileFocus;
    private CGTile _selectedTile;

    private List<CGTile> _possibleMoves;

    private CGTeam _activePlayer;

    private CGMovementManager(CGTile[,] board)
    {
        Board = board;
        _isTileFocus = false;
        _activePlayer = CGTeam.White;
    }

    public static CGMovementManager GetInstance(CGTile[,] board)
    {
        if (_instMovementManager == null)
            _instMovementManager = new CGMovementManager(board);

        return _instMovementManager;

    }
    
    public static CGMovementManager GetInstance()
    {
        if (_instMovementManager != null)
            return _instMovementManager;
        return null;
    }

    public void ManageMovement(CGTile clickTile)
    {
        if (_isTileFocus)
        {
            Console.Write(clickTile.BoardPosition + " second tile was selected from movement manager");

            if (_possibleMoves.Contains(clickTile))
            {
                Console.WriteLine(clickTile.BoardPosition + " is was move");
                if (!clickTile.IsEmpty)
                    clickTile.CurrentPiece.Destroy();

                clickTile.CurrentPiece = _selectedTile.CurrentPiece;

                clickTile.CurrentPiece.Position = clickTile.Position;
                clickTile.CurrentPiece.Moved = true;

                _possibleMoves = null;
                _selectedTile.CurrentPiece = null;
                _isTileFocus = false;
                _selectedTile = null;
                _activePlayer = _activePlayer == CGTeam.White ? CGTeam.Black : CGTeam.White;
            }
        }
        
        else if (!clickTile.IsEmpty && clickTile.CurrentPiece.Team == _activePlayer)
        {
            Console.WriteLine(clickTile.BoardPosition + " Was selected from movement manager");
            if (!clickTile.IsEmpty) {
                _isTileFocus = true;
                _selectedTile = clickTile;
                _possibleMoves = _selectedTile.CurrentPiece.GetMoves(_selectedTile, _selectedTile.CurrentPiece.Team, Board);
            }
        }
    }

    public void UnSelectTile(bool rightClick)
    {
        if (rightClick && _isTileFocus)
        {
            Console.WriteLine( _selectedTile.BoardPosition + " Was unselected from movement manager no specific tile");
            _isTileFocus = false;
            _selectedTile = null;
        }
    }


}