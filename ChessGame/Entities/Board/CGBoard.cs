using ChessGame.Entities.Board;
using ChessGame.Entities.Pieces;
using ChessGame.Entities.Sprites;
using ChessGame.Enums;
using ChessGame.Structs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;

namespace ChessGame.Actors;

public class CGBoard
{
    private Scene OwningScene { get; }
    private int TileSpawnOffsetX { get; set; }
    private int TileSpawnOffsetY { get; set; }
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
    
    
    
    public CGBoard SetOffsetX(int offset)
    {
        TileSpawnOffsetX = offset;
        return this;
    }

    public CGBoard SetOffsetY(int offset)
    {
        TileSpawnOffsetY = offset;
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
        var texturesManager = CGTextureManager.GetInstance(OwningScene);
        texturesManager.PieceScale = scale;
        
        var texture = texturesManager.TextureDictionary[CGPieceType.Board];
        
        var board = new CGTile[size, size];
        
        var y = (texture.BlackTexture.Height + TileSpacing) * scale;
        var x = (texture.BlackTexture.Width + TileSpacing) * scale;
        
        for (int row = 0; row < size; row++)
        {
            var tileXPosition = (x * row) + TileSpawnOffsetX;
            for (int column = 0; column < size; column++)
            {
                var tileYPosition = (y * column) + TileSpawnOffsetY;
                
                var tileTexture = (column + row) % 2 == 0 ? texture.WhiteTexture : texture.BlackTexture;
                
                BoardPosition boardPos = new((char)(row + 65), size - column);
                var tile = OwningScene.AddEntity(new CGTile(boardPos, tileTexture, scale));
                tile.Transform.SetPosition(new Vector2(tileXPosition, tileYPosition));
                board[row, column] = tile;
            }
        }

        return board;
    }

    public CGBoard PopulateBoard(CGTile[,] board, float scale)
    {
        var texturesManager = CGTextureManager.GetInstance(OwningScene);
        texturesManager.PieceScale = scale;
        
        var texture = texturesManager.TextureDictionary[CGPieceType.Pawn];
        
        for (int i = 0; i < 8; i++)
        {
            board[i, 1].CurrentPiece = OwningScene.AddEntity(new CGPiece(board[i, 1].Position, texture.GetTexture(CGTeam.Black), CGTeam.Black, scale, CGPieceType.Pawn));
            board[i, 6].CurrentPiece = OwningScene.AddEntity(new CGPiece(board[i, 6].Position, texture.GetTexture(CGTeam.White), CGTeam.White, scale, CGPieceType.Pawn));
        }
        
        texture = texturesManager.TextureDictionary[CGPieceType.Rook];
        board[0, 0].CurrentPiece = OwningScene.AddEntity(new CGPiece(board[0, 0].Position, texture.GetTexture(CGTeam.Black), CGTeam.Black, scale, CGPieceType.Rook));
        board[7, 0].CurrentPiece = OwningScene.AddEntity(new CGPiece(board[7, 0].Position, texture.GetTexture(CGTeam.Black), CGTeam.Black, scale, CGPieceType.Rook));
        board[0, 7].CurrentPiece =  OwningScene.AddEntity(new CGPiece(board[0, 7].Position, texture.GetTexture(CGTeam.White), CGTeam.White, scale, CGPieceType.Rook));
        board[7, 7].CurrentPiece = OwningScene.AddEntity(new CGPiece(board[7, 7].Position, texture.GetTexture(CGTeam.White), CGTeam.White, scale, CGPieceType.Rook));
        
        texture = texturesManager.TextureDictionary[CGPieceType.Knight];
        board[1, 0].CurrentPiece = OwningScene.AddEntity(new CGPiece(board[1, 0].Position, texture.GetTexture(CGTeam.Black), CGTeam.Black, scale, CGPieceType.Knight));
        board[6, 0].CurrentPiece = OwningScene.AddEntity(new CGPiece(board[6, 0].Position, texture.GetTexture(CGTeam.Black), CGTeam.Black, scale, CGPieceType.Knight));
        board[1, 7].CurrentPiece = OwningScene.AddEntity(new CGPiece(board[1, 7].Position, texture.GetTexture(CGTeam.White), CGTeam.White, scale, CGPieceType.Knight));
        board[6, 7].CurrentPiece = OwningScene.AddEntity(new CGPiece(board[6, 7].Position, texture.GetTexture(CGTeam.White), CGTeam.White, scale, CGPieceType.Knight));
        
        texture = texturesManager.TextureDictionary[CGPieceType.Bishop];
        board[2, 0].CurrentPiece = OwningScene.AddEntity(new CGPiece(board[2, 0].Position, texture.GetTexture(CGTeam.Black), CGTeam.Black, scale,CGPieceType.Bishop));
        board[5, 0].CurrentPiece = OwningScene.AddEntity(new CGPiece(board[5, 0].Position, texture.GetTexture(CGTeam.Black), CGTeam.Black, scale, CGPieceType.Bishop));
        board[2, 7].CurrentPiece = OwningScene.AddEntity(new CGPiece(board[2, 7].Position, texture.GetTexture(CGTeam.White), CGTeam.White, scale, CGPieceType.Bishop));
        board[5, 7].CurrentPiece = OwningScene.AddEntity(new CGPiece(board[5, 7].Position, texture.GetTexture(CGTeam.White), CGTeam.White, scale, CGPieceType.Bishop));
        
        texture = texturesManager.TextureDictionary[CGPieceType.Queen];
        board[3, 0].CurrentPiece = OwningScene.AddEntity(new CGPiece(board[3, 0].Position, texture.GetTexture(CGTeam.Black), CGTeam.Black, scale, CGPieceType.Queen));
        board[3, 7].CurrentPiece = OwningScene.AddEntity(new CGPiece(board[3, 7].Position, texture.GetTexture(CGTeam.White), CGTeam.White, scale, CGPieceType.Queen));
        
        texture = texturesManager.TextureDictionary[CGPieceType.King];
        board[4, 0].CurrentPiece = OwningScene.AddEntity(new CGPiece(board[4, 0].Position, texture.GetTexture(CGTeam.Black), CGTeam.Black, scale, CGPieceType.King));
        board[4, 7].CurrentPiece = OwningScene.AddEntity(new CGPiece(board[4, 7].Position, texture.GetTexture(CGTeam.White), CGTeam.White, scale, CGPieceType.King));
        
        return this;
    }
}