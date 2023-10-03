using ChessGame.Entities.Board;
using ChessGame.Entities.Pieces;
using ChessGame.Enums;
using ChessGame.Structs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;

namespace ChessGame.Actors;

public class CGBoard
{
    private Scene OwningScene { get; }

    #region Textures

    
    /// <summary>
    /// 
    /// </summary>
    private Texture2D OddTileTexture { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    private Texture2D EvenTileTexture { get; set; }
    
    private Texture2D WhitePawnTexture { get; set; }
    private Texture2D BlackPawnTexture { get; set; }

    private Texture2D WhiteBishopTexture { get; set; }
    private Texture2D BlackBishopTexture { get; set; }
    
    private Texture2D WhiteKnightTexture { get; set; }
    private Texture2D BlackKnightTexture { get; set; }
    
    private Texture2D WhiteQueenTexture { get; set; }
    private Texture2D BlackQueenTexture { get; set; }
    
    private Texture2D WhiteKingTexture { get; set; }
    private Texture2D BlackKingTexture { get; set; }
    
    private Texture2D WhiteRookTexture { get; set; }
    private Texture2D BlackRookTexture { get; set; }


    #endregion

    private int TileSpawnOffset { get; set; }

    private int TileSpacing { get; set; }
    
    public enum TileType
    {
        Odd,
        Even
    }
    
    public CGBoard(Scene owningScene)
    {
        OwningScene = owningScene;
    }
    
    
    
    public CGBoard SetOffset(int offset)
    {
        TileSpawnOffset = offset;
        return this;
    }
    
    public CGBoard SetSpacing(int spacing)
    {
        TileSpacing = spacing;
        return this;
    }


    // TODO: Change to return the tiles in the most convenient way
    public CGTile[,] Generate(float scale = 1,int size = 8)
    {
        var board = new CGTile[size, size];
        
        var y = (OddTileTexture.Height + TileSpacing) * scale;
        var x = (OddTileTexture.Width + TileSpacing) * scale;
        
        for (int row = 0; row < size; row++)
        {
            var tileXPosition = (x * row) + TileSpawnOffset;
            for (int column = 0; column < size; column++)
            {
                var tileYPosition = (y * column) + TileSpawnOffset;
                
                var texture = (column + row) % 2 == 0 ? EvenTileTexture : OddTileTexture;
                
                BoardPosition boardPos = new((char)(row + 65), size - column);
                var tile = OwningScene.AddEntity(new CGTile(boardPos, texture, scale));
                tile.Transform.SetPosition(new Vector2(tileXPosition, tileYPosition));
                board[row, column] = tile;
            }
        }

        return board;
    }

    public CGBoard PopulateBoard(CGTile[,] board, float scale)
    {
        for (int i = 0; i < 8; i++)
        {
            board[i, 1].CurrentPiece = OwningScene.AddEntity(new CGPawn(board[i, 1].Position, BlackPawnTexture, CGTeam.Black, scale));
            board[i, 6].CurrentPiece = OwningScene.AddEntity(new CGPawn(board[i, 6].Position, WhitePawnTexture, CGTeam.White, scale));
        }
        
        board[0, 0].CurrentPiece = OwningScene.AddEntity(new CGRook(board[0, 0].Position, BlackRookTexture, CGTeam.Black, scale));
        board[7, 0].CurrentPiece = OwningScene.AddEntity(new CGRook(board[7, 0].Position, BlackRookTexture, CGTeam.Black, scale));
        board[0, 7].CurrentPiece =  OwningScene.AddEntity(new CGRook(board[0, 7].Position, WhiteRookTexture, CGTeam.White, scale));
        board[7, 7].CurrentPiece = OwningScene.AddEntity(new CGRook(board[7, 7].Position, WhiteRookTexture, CGTeam.White, scale));
        
        board[1, 0].CurrentPiece = OwningScene.AddEntity(new CGKnight(board[1, 0].Position, BlackKnightTexture, CGTeam.Black, scale));
        board[6, 0].CurrentPiece = OwningScene.AddEntity(new CGKnight(board[6, 0].Position, BlackKnightTexture, CGTeam.Black, scale));
        board[1, 7].CurrentPiece = OwningScene.AddEntity(new CGKnight(board[1, 7].Position, WhiteKnightTexture, CGTeam.White, scale));
        board[6, 7].CurrentPiece = OwningScene.AddEntity(new CGKnight(board[6, 7].Position, WhiteKnightTexture, CGTeam.White, scale));
        
        board[2, 0].CurrentPiece = OwningScene.AddEntity(new CGBishop(board[2, 0].Position, BlackBishopTexture, CGTeam.Black, scale));
        board[5, 0].CurrentPiece = OwningScene.AddEntity(new CGBishop(board[5, 0].Position, BlackBishopTexture, CGTeam.Black, scale));
        board[2, 7].CurrentPiece = OwningScene.AddEntity(new CGBishop(board[2, 7].Position, WhiteBishopTexture, CGTeam.White, scale));
        board[5, 7].CurrentPiece = OwningScene.AddEntity(new CGBishop(board[5, 7].Position, WhiteBishopTexture, CGTeam.White, scale));
        
        board[3, 0].CurrentPiece = OwningScene.AddEntity(new CGQueen(board[3, 0].Position, BlackQueenTexture, CGTeam.Black, scale));
        board[3, 7].CurrentPiece = OwningScene.AddEntity(new CGQueen(board[3, 7].Position, WhiteQueenTexture, CGTeam.White, scale));
        
        board[4, 0].CurrentPiece = OwningScene.AddEntity(new CGKing(board[4, 0].Position, BlackKingTexture, CGTeam.Black, scale));
        board[4, 7].CurrentPiece = OwningScene.AddEntity(new CGKing(board[4, 7].Position, WhiteKingTexture, CGTeam.White, scale));
        
        return this;
    }

    #region Texture setters

    public CGBoard SetBoardTexture(TileType tileType, string texturePath)
    {
        switch (tileType)
        {
            case TileType.Odd:
                OddTileTexture = OwningScene.Content.Load<Texture2D>(texturePath);
                break;
            case TileType.Even:
                EvenTileTexture = OwningScene.Content.Load<Texture2D>(texturePath);
                break;
        }

        return this;
    }
    public CGBoard SetPawnTexture(CGTeam team, string texturePath)
    {
        switch (team)
        {
            case CGTeam.White:
                WhitePawnTexture = OwningScene.Content.Load<Texture2D>(texturePath);
                break;
            case CGTeam.Black:
                BlackPawnTexture = OwningScene.Content.Load<Texture2D>(texturePath);
                break;
        }

        return this;
    }
    
    public CGBoard SetBishopTexture(CGTeam team, string texturePath)
    {
        switch (team)
        {
            case CGTeam.White:
                WhiteBishopTexture = OwningScene.Content.Load<Texture2D>(texturePath);
                break;
            case CGTeam.Black:
                BlackBishopTexture = OwningScene.Content.Load<Texture2D>(texturePath);
                break;
        }

        return this;
    }
    
    public CGBoard SetKnightTexture(CGTeam team, string texturePath)
    {
        switch (team)
        {
            case CGTeam.White:
                WhiteKnightTexture = OwningScene.Content.Load<Texture2D>(texturePath);
                break;
            case CGTeam.Black:
                BlackKnightTexture = OwningScene.Content.Load<Texture2D>(texturePath);
                break;
        }

        return this;
    }
    
    public CGBoard SetKingTexture(CGTeam team, string texturePath)
    {
        switch (team)
        {
            case CGTeam.White:
                WhiteKingTexture = OwningScene.Content.Load<Texture2D>(texturePath);
                break;
            case CGTeam.Black:
                BlackKingTexture = OwningScene.Content.Load<Texture2D>(texturePath);
                break;
        }

        return this;
    }
    
    public CGBoard SetQueenTexture(CGTeam team, string texturePath)
    {
        switch (team)
        {
            case CGTeam.White:
                WhiteQueenTexture = OwningScene.Content.Load<Texture2D>(texturePath);
                break;
            case CGTeam.Black:
                BlackQueenTexture = OwningScene.Content.Load<Texture2D>(texturePath);
                break;
        }

        return this;
    }
    
    public CGBoard SetRookTexture(CGTeam team, string texturePath)
    {
        switch (team)
        {
            case CGTeam.White:
                WhiteRookTexture = OwningScene.Content.Load<Texture2D>(texturePath);
                break;
            case CGTeam.Black:
                BlackRookTexture = OwningScene.Content.Load<Texture2D>(texturePath);
                break;
        }

        return this;
    }

    public CGBoard LoadTextures()
    {
        
        this.SetBoardTexture(CGBoard.TileType.Even, "../Content/Sprites/WhiteTile")
            .SetBoardTexture(CGBoard.TileType.Odd, "../Content/Sprites/BlackTile")
            .SetPawnTexture(CGTeam.White, "../Content/Sprites/chess-pawn-white")
            .SetPawnTexture(CGTeam.Black, "../Content/Sprites/chess-pawn-black")
            .SetBishopTexture(CGTeam.White, "../Content/Sprites/chess-bishop-white")
            .SetBishopTexture(CGTeam.Black, "../Content/Sprites/chess-bishop-black")
            .SetKingTexture(CGTeam.White, "../Content/Sprites/chess-king-white")
            .SetKingTexture(CGTeam.Black, "../Content/Sprites/chess-king-black")
            .SetQueenTexture(CGTeam.White, "../Content/Sprites/chess-queen-white")
            .SetQueenTexture(CGTeam.Black, "../Content/Sprites/chess-queen-black")
            .SetRookTexture(CGTeam.White, "../Content/Sprites/chess-rook-white")
            .SetRookTexture(CGTeam.Black, "../Content/Sprites/chess-rook-black")
            .SetKnightTexture(CGTeam.White, "../Content/Sprites/chess-knight-white")
            .SetKnightTexture(CGTeam.Black, "../Content/Sprites/chess-knight-black");
        return this;
    }
    
    #endregion
}