using System.Collections.Generic;
using ChessGame.Entities.Board;
using ChessGame.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace ChessGame.Entities.Pieces;

public abstract class CGPiece : Entity
{
    private bool _moved;
    private Texture2D _pieceTexture;

    public CGTeam Team;
    public List<CGTile> PosibleMoves;
    public bool Moved
    {
        get => _moved;
        set => _moved = value;
    }

    public CGPiece(Vector2 position, Texture2D pieceTexture, CGTeam team, float scale)
    {
        this.Position = position;
        _moved = false;
        _pieceTexture = pieceTexture;
        this.AddComponent(new SpriteRenderer(_pieceTexture))
            .SetLayerDepth(0);
        Team = team;
        this.Transform.SetScale(scale);
    }

    public CGPiece()
    {
        
    }

    public abstract List<CGTile> GetMoves(CGTile selectedTile, CGTeam team, CGTile[,] board);
}