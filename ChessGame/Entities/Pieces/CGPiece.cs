using System;
using System.Collections.Generic;
using ChessGame.Entities.Board;
using ChessGame.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace ChessGame.Entities.Pieces;

public class CGPiece : Entity
{
    private bool _moved;
    public Texture2D _pieceTexture;

    public CGTeam Team;
    public List<CGTile> PosibleMoves;

    public CGPieceType Type;
    
    private SpriteRenderer SpriteRenderer { get; set; }
    
    public bool Moved
    {
        get => _moved;
        set => _moved = value;
    }

    public CGPiece(Vector2 position, Texture2D pieceTexture, CGTeam team, float scale, CGPieceType type)
    {
        this.Position = position;
        _moved = false;
        _pieceTexture = pieceTexture;
        Team = team;
        SpriteRenderer = new SpriteRenderer(_pieceTexture);
        this.AddComponent(SpriteRenderer)
            .SetLayerDepth(0);
        Type = type;
        Transform.SetScale(scale);
        
    }

    public CGPiece SetUpPiece(CGPieceType type, Texture2D newTexture)
    {
        Type = type;
        SpriteRenderer.SetTexture(newTexture);

        return this;
    }
    
    public CGPiece()
    {
        
    }

    public List<CGTile> GetMoves(CGTile selectedTile, CGTeam team, CGTile[,] board)
    {
        switch (Type)
        {
            case CGPieceType.Pawn:
                return CGPossibleMoves.GetPawnMoves(selectedTile, team, board);
                break;
            
            case CGPieceType.Bishop:
                return CGPossibleMoves.GetBishopMoves(selectedTile, team, board);
                break;
            
            case CGPieceType.Knight:
                return CGPossibleMoves.GetKnightMoves(selectedTile,team,board);
                break;
            
            case CGPieceType.Rook:
                return CGPossibleMoves.GetRookMoves(selectedTile,team,board);
                break;
            
            case CGPieceType.Queen:
                return CGPossibleMoves.GetQueenMoves(selectedTile,team,board);
                break;
            
            case CGPieceType.King:
                return CGPossibleMoves.GetKingMoves(selectedTile,team,board);
                break;
            
            
            default:
                throw new Exception("Unexpected piece type, cannot handle it's movement");
                break;
        }
    }
}