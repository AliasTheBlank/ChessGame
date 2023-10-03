using System;
using System.Collections.Generic;
using ChessGame.Entities.Board;
using ChessGame.Enums;
using Nez;

namespace ChessGame.Entities.Pieces;

public class CGMovementManager
{
    public static CGTile[,] Board;

    private static bool _isTileFocus;
    private static CGTile _selectedTile;

    private static List<CGTile> _possibleMoves;

    public CGMovementManager(CGTile[,] board)
    {
        Board = board;
        _isTileFocus = false;
    }

    public static void ManageMovement(CGTile clickTile)
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
            }
        }
        
        else
        {
            Console.WriteLine(clickTile.BoardPosition + " Was selected from movement manager");
            if (!clickTile.IsEmpty) {
                _isTileFocus = true;
                _selectedTile = clickTile;
                _possibleMoves = _selectedTile.CurrentPiece.GetMoves(_selectedTile, _selectedTile.CurrentPiece.Team, Board);
            }
        }
    }

    public static void UnSelectTile(bool rightClick)
    {
        if (rightClick && _isTileFocus)
        {
            Console.WriteLine( _selectedTile.BoardPosition + " Was unselected from movement manager no specific tile");
            _isTileFocus = false;
            _selectedTile = null;
        }
    }


}