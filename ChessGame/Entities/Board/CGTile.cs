using ChessGame.Entities.Components;
using ChessGame.Entities.Pieces;
using ChessGame.Structs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System.ComponentModel;

namespace ChessGame.Entities.Board;

/// <summary>
/// 
/// </summary>
public class CGTile : Entity
{
    //private Point _coordinate;
    public BoardPosition BoardPosition { get; }

    public CGPiece CurrentPiece;

    public Texture2D sprite;

    public CGTile(BoardPosition boardPosition, Texture2D texture, float scale)
    {
        BoardPosition = boardPosition;
        
        sprite = texture;
        this.AddComponent(new SpriteRenderer(sprite))
            .SetLayerDepth(1);
        AddComponent<ClickableComponent>();
        this.Transform.SetScale(scale);
        
        
        var col = AddComponent<BoxCollider>();
        col.SetSize(sprite.Width, sprite.Height);
    }

    public override void Update()
    {
        base.Update();
    }
    public bool IsEmpty => CurrentPiece == null;

    public CGTile(CGTile tile) 
    { 
        this.BoardPosition = new BoardPosition(tile.BoardPosition.GetFileName(), tile.BoardPosition.GetRankValue());
        this.CurrentPiece = tile.CurrentPiece;

    }
    public CGTile(CGTile tile, BoardPosition boardPosition)
    {
        this.BoardPosition = new BoardPosition(boardPosition.GetFileName(), boardPosition.GetRankValue());
        this.CurrentPiece = tile.CurrentPiece;

    }
}