using System.Collections.Generic;
using ChessGame.Entities.Board;
using ChessGame.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChessGame.Entities.Pieces;

public class CGRook : CGPiece
{
/*public CGRook(Vector2 position, Texture2D pieceTexture, CGTeam team, float scale) : base(position, pieceTexture, team, scale)
{

}*/

public  List<CGTile> GetMoves(CGTile selectedTile, CGTeam team, CGTile[,] board)
{
    return CGPossibleMoves.GetRookMoves(selectedTile,team,board);
}
}