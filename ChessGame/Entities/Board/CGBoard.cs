using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace ChessGame.Actors;

public class CGBoard : Entity
{
    private int _dimensions;
    
    public CGTile[,] TileBoard;

    private Rectangle limits;


    public CGBoard(Scene scene, int dimensions = 8)
    {
        limits = new Rectangle(0, 0, 700, 700);
        
        
        
        _dimensions = dimensions;
        TileBoard = new CGTile[_dimensions, _dimensions];
        Scene = scene;

        int column = 65;
        int row = 8;

        int X = 60;
        int Y = 60;
        for (int i = 0; i < TileBoard.GetLength(0); i++)
        {
            for (int j = 0; j < TileBoard.GetLength(1); j++)
            {

                Texture2D texture;
                if ((j + i) % 2 == 0)
                {
                    texture = Scene.Content.LoadTexture(@"Content/Sprites/WhiteTile.png");
                }
                else
                {
                    texture = Scene.Content.LoadTexture(@"Content/Sprites/BlackTile.png");
                }
                TileBoard[i, j] = new ((char)column, row, new Sprite(texture));
                var tile = Scene.AddEntity(TileBoard[i, j]);
                tile.Transform.SetPosition(new Vector2(X, Y));
                X += 110;
                column++;
            }
            
            column = 65;
            row--;
            X = 60;
            Y += 110;
        }
    }

    public CGTile this[int x, int y]
    {
        get
        {
            if (x >= 0 && x < TileBoard.GetLength(0) && y >= 0 && y < TileBoard.GetLength(1))
            {
                return TileBoard[x, y];
            }
            return null;
        }
    }
}