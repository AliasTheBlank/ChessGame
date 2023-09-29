using ChessGame.Entities.Components;
using ChessGame.Structs;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace ChessGame.Entities.Board;

/// <summary>
/// 
/// </summary>
public class CGTile : Entity
{
    //private Point _coordinate;
    public BoardPosition BoardPosition { get; }

    //public CGPiece CurrentPiece;
    //public (char column, int row) Position; // make struct

    public Texture2D sprite;
    
    //public CGPiece Piece { get; set; }

    /*
    public Point Coordinate
    {
        get => _coordinate;
        set
        {
            _coordinate = value;
            Position = ((char)_coordinate.X, _coordinate.Y);
        }
    }
    */
    
    /*
    public CGTile(char column, int row, Texture2D texture)
    {
        Position.column = column;
        Position.row = row;
        this.sprite = texture;
        this.AddComponent(new SpriteRenderer(sprite));
        Transform.SetScale(new Vector2(.75f, .75f));
        
        
        Console.WriteLine("Create tile in " + column + row);
    }
    */

    public CGTile(BoardPosition boardPosition, Texture2D texture)
    {
        BoardPosition = boardPosition;
        
        sprite = texture;
        AddComponent(new SpriteRenderer(sprite));
        AddComponent<ClickableComponent>();
        
        var col = AddComponent<BoxCollider>();
        col.SetSize(sprite.Width, sprite.Height);
    }

    public override void Update()
    {
        base.Update();
    }

    //public bool IsEmpty => Piece == null;

}