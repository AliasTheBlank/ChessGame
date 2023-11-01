using ChessGame.Enums;
using Microsoft.Xna.Framework.Graphics;

namespace ChessGame.Structs;

public class CGPieceTexture
{
    public Texture2D WhiteTexture;
    public Texture2D BlackTexture;
    
    public CGPieceTexture()
    {
        
    }

    public Texture2D GetTexture(CGTeam team)
    {
        return team == CGTeam.White ? WhiteTexture : BlackTexture;
    }
}