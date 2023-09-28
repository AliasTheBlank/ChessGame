using System;
using ChessGame.Entities.Pieces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace ChessGame.Actors;

public class CGTile : Entity
{
    private Point _coordinate;


    public CGPiece CurrentPiece;
    public (char column, int row) Position;
    public Texture2D sprite;
    public CGPiece Piece { get; set; }

    public Point Coordinate
    {
        get => _coordinate;
        set
        {
            _coordinate = value;
            Position = ((char)_coordinate.X, _coordinate.Y);
        }
    }
    
    public CGTile(char column, int row, Texture2D texture)
    {
        Position.column = column;
        Position.row = row;
        this.sprite = texture;
        this.AddComponent(new SpriteRenderer(sprite));
        Transform.SetScale(new Vector2(.75f, .75f));
        Console.WriteLine("Create tile in " + column + row);
    }

    public bool IsEmpty => Piece == null;

}