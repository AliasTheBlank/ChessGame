using ChessGame.Entities.Pieces;
using Nez;
using Nez.Textures;

namespace ChessGame.Actors;

public class CGTile : SceneComponent
{
    public CGPiece CurrentPiece;
    public (char column, int row) Position;
    private Sprite _texture;
    
    public CGTile(char column, int row, Sprite texture)
    {
        Position.column = column;
        Position.row = row;
        _texture = texture;
    }


}