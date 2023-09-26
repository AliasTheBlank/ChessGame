namespace ChessGame.Actors;

public class CGBoard : Nez.SceneComponent
{
    private int _dimensions;
    
    public CGTile[,] TileBoard;


    public CGBoard(int dimensions = 8)
    {
        _dimensions = dimensions;
        TileBoard = new CGTile[_dimensions, _dimensions];
        
        for (int i = 0; i < TileBoard.GetLength(0); i++)
        {
            for (int j = 0; j < TileBoard.GetLength(1); j++)
            {
                if ((j + i) % 2 == 0)
                {
                    
                }
                else
                {
                    
                }
            }
        }
    }
}