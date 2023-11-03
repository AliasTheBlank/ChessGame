using System;
using System.Collections.Generic;
using ChessGame.Entities.Board;
using ChessGame.Enums;

namespace ChessGame.Entities.Pieces;

public class CGPossibleMoves
{
    #region Rook movement
    
    /// <summary>
    ///  Calculate the movements that a rook or queen can do vertically up, when calling it for the first time
    /// </summary>
    /// <param name="selectedTile">the tile above the piece</param>
    /// <param name="team"></param>
    /// <returns></returns>
    public static List<CGTile> GetUpMovement(CGTile selectedTile, CGTeam team, CGTile[,] board)
    {
        var list = new List<CGTile>();

        var positionX = GetPositionX(selectedTile);
        var positionY = GetPositionY(selectedTile);
        
        if (selectedTile.IsEmpty && positionY == 0 || !selectedTile.IsEmpty && selectedTile.CurrentPiece.Team != team)
        {
            list.Add(selectedTile);
            return list;
        }

        if (!selectedTile.IsEmpty)
        {
            return list;
        }

        if (!board[positionX , positionY - 1].IsEmpty && board[positionX, positionY - 1].CurrentPiece.Team == team)
        {
            list.Add(selectedTile);
            return list;
        }

        list.AddRange(GetUpMovement(board[positionX, positionY - 1], team, board));
        list.Add(selectedTile);
        
        return list;
    }
    
    public static List<CGTile> GetDownMovement(CGTile selectedTile, CGTeam team, CGTile[,] board)
    {
        var list = new List<CGTile>();

        var positionX = GetPositionX(selectedTile);
        var positionY = GetPositionY(selectedTile);
        
        if (selectedTile.IsEmpty && positionY == 7 || !selectedTile.IsEmpty && selectedTile.CurrentPiece.Team != team)
        {
            list.Add(selectedTile);
            return list;
        }
        
        if (!selectedTile.IsEmpty)
        {
            return list;
        }

        if (!board[positionX, positionY + 1].IsEmpty && board[positionX, positionY + 1].CurrentPiece.Team == team)
        {
            list.Add(selectedTile);
            return list;
        }

        list.AddRange(GetDownMovement(board[positionX, positionY + 1], team, board));
        list.Add(selectedTile);
        
        return list;
    }
    
    public static List<CGTile> GetLeftMovement(CGTile selectedTile, CGTeam team, CGTile[,] board)
    {
        var list = new List<CGTile>();

        var positionX = GetPositionX(selectedTile);
        var positionY = GetPositionY(selectedTile);
        
        if (selectedTile.IsEmpty && positionX == 0 || !selectedTile.IsEmpty && selectedTile.CurrentPiece.Team != team)
        {
            list.Add(selectedTile);
            return list;
        }
        
        if (!selectedTile.IsEmpty)
        {
            return list;
        }

        if (!board[positionX - 1, positionY].IsEmpty && board[positionX - 1, positionY].CurrentPiece.Team == team)
        {
            list.Add(selectedTile);
            return list;
        }

        list.AddRange(GetLeftMovement(board[positionX - 1, positionY], team, board));
        list.Add(selectedTile);
        
        return list;
    }
    
    public static List<CGTile> GetRightMovement(CGTile selectedTile, CGTeam team, CGTile[,] board)
    {
        var list = new List<CGTile>();

        var positionX = GetPositionX(selectedTile);
        var positionY = GetPositionY(selectedTile);
        
        if (selectedTile.IsEmpty && positionX == 7 || !selectedTile.IsEmpty && selectedTile.CurrentPiece.Team != team)
        {
            list.Add(selectedTile);
            return list;
        }
        
        if (!selectedTile.IsEmpty)
        {
            return list;
        }

        if (!board[positionX + 1, positionY].IsEmpty && board[positionX + 1, positionY].CurrentPiece.Team == team)
        {
            list.Add(selectedTile);
            return list;
        }

        list.AddRange(GetRightMovement(board[positionX + 1, positionY], team, board));
        list.Add(selectedTile);
        
        return list;
    }
    
    #endregion

    #region Knight Movement
    /// <summary>
    /// This method calculates the possible movement that knight might commit in a game
    /// </summary>
    /// <param name="selectedTile">The tile the knight is in</param>
    /// <param name="team"></param>
    /// <param name="board"></param>
    /// <returns></returns>
    public static List<CGTile> GetKnightMoves(CGTile selectedTile, CGTeam team, CGTile[,] board)
    {
        var list = new List<CGTile>();

        var positionX = GetPositionX(selectedTile);
        var positionY = GetPositionY(selectedTile);

        if (positionX > 1)
        {
            if (positionY < 7 && CanAddTile(board[positionX - 2, positionY + 1], team))
                
                list.Add(board[positionX - 2, positionY + 1]);
            
            if (positionY > 0 && CanAddTile(board[positionX - 2, positionY - 1], team))
                list.Add(board[positionX - 2, positionY - 1]);
        }
        
        if (positionX < 6)
        {
            if (positionY < 7 && CanAddTile(board[positionX + 2, positionY + 1], team))
                list.Add(board[positionX + 2, positionY + 1]);
            if (positionY > 0 && CanAddTile(board[positionX + 2, positionY - 1], team))
                list.Add(board[positionX + 2, positionY - 1]);
        }
        
        if (positionY > 1)
        {
            if (positionX < 7 && CanAddTile(board[positionX + 1, positionY - 2], team))
                list.Add(board[positionX + 1, positionY - 2]);
            if (positionX > 0 && CanAddTile(board[positionX - 1, positionY - 2], team))
                list.Add(board[positionX - 1, positionY - 2]);
        }
        
        if (positionY < 6)
        {
            if (positionX < 7 && CanAddTile(board[positionX + 1, positionY + 2], team))
                list.Add(board[positionX + 1, positionY + 2]);
            if (positionX > 0 && CanAddTile(board[positionX - 1, positionY + 2], team))
                list.Add(board[positionX - 1, positionY + 2]);
        }

        return list;
    }
    
    #endregion

    private static bool CanAddTile(CGTile tile, CGTeam team)
    {
        return tile.IsEmpty || tile.CurrentPiece.Team != team;
    }
    
    #region bishop moves
    /// <summary>
    /// 
    /// </summary>
    /// <param name="selectedTile">The tile where the piece is</param>
    /// <param name="team"></param>
    /// <param name="board"></param>
    /// <returns></returns>
    public static List<CGTile> CalculateUpperLeftTile(CGTile selectedTile, CGTeam team, CGTile[,] board)
    {
        var list = new List<CGTile>();
        

        var positionX = GetPositionX(selectedTile);
        var positionY = GetPositionY(selectedTile);

        while (positionX > 0 && positionY > 0)
        {
            positionX--;
            positionY--;
            
            if (!CanAddTile(board[positionX, positionY], team))
                return list;
            
            list.Add(board[positionX, positionY]);
        }

        return list;
    }
    
    public static List<CGTile> CalculateUpperRightTile(CGTile selectedTile, CGTeam team, CGTile[,] board)
    {
        var list = new List<CGTile>();
        

        var positionX = GetPositionX(selectedTile);
        var positionY = GetPositionY(selectedTile);

        while (positionX < 7 && positionY > 0)
        {
            positionX++;
            positionY--;
            
            if (!CanAddTile(board[positionX, positionY], team))
                return list;
            
            list.Add(board[positionX, positionY]);
        }

        return list;
    }
    
    public static List<CGTile> CalculateLowerLeftTile(CGTile selectedTile, CGTeam team, CGTile[,] board)
    {
        var list = new List<CGTile>();
        

        var positionX = GetPositionX(selectedTile);
        var positionY = GetPositionY(selectedTile);

        while (positionX > 0 && positionY < 7)
        {
            positionX--;
            positionY++;
            
            if (!CanAddTile(board[positionX, positionY], team))
                return list;
            
            list.Add(board[positionX, positionY]);
        }

        return list;
    }
    
    public static List<CGTile> CalculateLowerRightTile(CGTile selectedTile, CGTeam team, CGTile[,] board)
    {
        var list = new List<CGTile>();
        

        var positionX = GetPositionX(selectedTile);
        var positionY = GetPositionY(selectedTile);

        while (positionX < 7 && positionY < 7)
        {
            positionX++;
            positionY++;
            
            if (!CanAddTile(board[positionX, positionY], team))
                return list;
            
            list.Add(board[positionX, positionY]);
        }

        return list;
    }
    
    
    #endregion

    private static int GetPositionY(CGTile selectedTile)
    {
        var temp = Char.GetNumericValue(selectedTile.BoardPosition.ToString()![1]);
        var rank = Convert.ToInt32(selectedTile.BoardPosition.ToString()![1]);
        return 8 - (int)temp;
    }

    private static int GetPositionX(CGTile selectedTile)
    {
        var file = Convert.ToInt32(selectedTile.BoardPosition.ToString()![0]);
        return 8 - (73 - file);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="selectedTile">Pass the place of the pawn</param>
    /// <param name="team"></param>
    /// <param name="board"></param>
    /// <returns></returns>
    public static List<CGTile> GetPawnMoves(CGTile selectedTile, CGTeam team, CGTile[,] board)
    {
        var list = new List<CGTile>();
        
        var positionX = GetPositionX(selectedTile);
        var positionY = GetPositionY(selectedTile);

        int movementDirection = team == CGTeam.White ? -1 : 1;

        var temp = positionY + (movementDirection * 2);
        if (!selectedTile.CurrentPiece.Moved && board[positionX, positionY + (movementDirection * 2)].IsEmpty)
        {
            list.Add(board[positionX, positionY + movementDirection * 2]);
            //Console.WriteLine(positionX + " " + (positionY + movementDirection * 2));
        }
        
        if (positionX < 7 && IsOccupiedByEnemy(board[positionX + 1, positionY + movementDirection], team))
            list.Add(board[positionX + 1, positionY + movementDirection]);
        
        if (positionX > 0 && IsOccupiedByEnemy(board[positionX - 1, positionY + movementDirection], team))
            list.Add(board[positionX - 1, positionY + movementDirection]);
        
        if (board[positionX, positionY + movementDirection].IsEmpty)
            list.Add(board[positionX, positionY + movementDirection]);
        
        return list;
    }

    private static bool IsOccupiedByEnemy(CGTile selectedTile, CGTeam team)
    {
        return !selectedTile.IsEmpty && selectedTile.CurrentPiece.Team != team;
    }

    public static List<CGTile> GetKingMoves(CGTile selectedTile, CGTeam team, CGTile[,] board)
    {
        var list = new List<CGTile>();

        var positionX = GetPositionX(selectedTile);
        var positionY = GetPositionY(selectedTile);

        bool upBorder = positionY == 0;
        bool downBorder = positionY == 7;
        bool rightBorder = positionX == 7;
        bool leftBorder = positionX == 0;
        
        if (!leftBorder && CanAddTile(board[positionX - 1, positionY], team))
            list.Add(board[positionX - 1, positionY]);
        if (!rightBorder && CanAddTile(board[positionX + 1, positionY], team))
            list.Add(board[positionX + 1, positionY]);
        if (!upBorder && CanAddTile(board[positionX, positionY - 1], team))
            list.Add(board[positionX , positionY - 1]);
        if (!downBorder && CanAddTile(board[positionX, positionY + 1], team))
            list.Add(board[positionX, positionY + 1]);
        
        if (!leftBorder && !upBorder && CanAddTile(board[positionX - 1, positionY - 1], team))
            list.Add(board[positionX - 1, positionY - 1]);
        if (!leftBorder && !downBorder && CanAddTile(board[positionX - 1, positionY + 1], team))
            list.Add(board[positionX - 1, positionY + 1]);
        if (!rightBorder && !upBorder && CanAddTile(board[positionX + 1, positionY - 1], team))
            list.Add(board[positionX + 1, positionY - 1]);
        if (!rightBorder && !downBorder && CanAddTile(board[positionX + 1, positionY + 1], team))
            list.Add(board[positionX + 1, positionY + 1]);
        
        return list;
    }

    public static List<CGTile> GetCastleMoves(CGTile selectedTile, CGTeam team, CGTile[,] board)
    {
        var availableTiles = new List<CGTile>();

        int file = selectedTile.BoardPosition.GetFileName() - 65;
        int rank = 8 -selectedTile.BoardPosition.GetRankValue();
        for (int i = file - 1; i >= 0; i--)
        {
            
            if (i != 0 && !board[i, rank].IsEmpty)
                break;

            if (i == 0 && !board[i, rank].IsEmpty && !board[i, rank].CurrentPiece.Moved &&
                board[i, rank].CurrentPiece.Type == CGPieceType.Rook)
            {
                availableTiles.Add(board[file - 2, rank]);
            }
        }

        for (int i = file + 1; i <= board.GetLength(0) - 1; i++)
        {
            if (i != board.GetLength(0) - 1  && !board[i, rank].IsEmpty)
                break;

            if (i == board.GetLength(0) - 1 && !board[i, rank].IsEmpty && !board[i, rank].CurrentPiece.Moved &&
                board[i, rank].CurrentPiece.Type == CGPieceType.Rook)
            {
                availableTiles.Add(board[file + 2, rank]);
            }
        }

        return availableTiles;
    }

    public static List<CGTile> GetRookMoves(CGTile selectedTile, CGTeam team, CGTile[,] board)
    {
        var list = new List<CGTile>();
        
        var positionX = GetPositionX(selectedTile);
        var positionY = GetPositionY(selectedTile);
        
        bool upBorder = positionY == 0;
        bool downBorder = positionY == 7;
        bool rightBorder = positionX == 7;
        bool leftBorder = positionX == 0;
        
        if (!leftBorder)
            list.AddRange(GetLeftMovement(board[positionX - 1,positionY], team, board));
        if (!rightBorder)
            list.AddRange(GetRightMovement(board[positionX + 1,positionY], team, board));
        if (!upBorder)
            list.AddRange(GetUpMovement(board[positionX,positionY - 1], team, board));
        if (!downBorder)
            list.AddRange(GetDownMovement(board[positionX,positionY + 1], team, board));

        return list;
    }

    public static List<CGTile> GetBishopMoves(CGTile selectedTile, CGTeam team, CGTile[,] board)
    {
        var list = CalculateUpperLeftTile(selectedTile, team, board);
        
        list.AddRange(CalculateUpperRightTile(selectedTile, team, board));
        list.AddRange(CalculateLowerLeftTile(selectedTile, team, board));
        list.AddRange(CalculateLowerRightTile(selectedTile,team,board));

        return list;
    }

    public static List<CGTile> GetQueenMoves(CGTile selectedTile, CGTeam team, CGTile[,] board)
    {
        var list = GetRookMoves(selectedTile, team, board);
        list.AddRange(GetBishopMoves(selectedTile, team, board));
        return list;
    }

}
