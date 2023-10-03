using ChessGame.Actors;
using ChessGame.Entities.Board;
using ChessGame.Entities.Components;
using ChessGame.Entities.Pieces;
using ChessGame.Enums;
using Nez;
namespace ChessGame.Scenes;

public class CGGameScene : Scene
{
    public const int ScreenSpaceRenderLayer = 999;
    
    ScreenSpaceRenderer _screenSpaceRenderer;
    static bool _needsFullRenderSizeForUi;
    private CGTile Board;
    public CGTile[,] TileBoard;
    
    public UICanvas Canvas;

    
    public CGGameScene()
    {
        
        int column = 65;
        int row = 1;
        TileBoard = new CGTile[8, 8];
        
        
        Screen.SetSize(660*2, 660*2);
    
        // create our canvas and put it on the screen space render layer
        Canvas = CreateEntity("ui").AddComponent(new UICanvas());
        Canvas.IsFullScreen = true;
        Canvas.RenderLayer = ScreenSpaceRenderLayer;
        
        var cbGenerator = new CGBoard(this);

        cbGenerator
            .LoadTextures()
            .SetOffset(100);

        var board = cbGenerator.Generate(0.8f);
        var movementManager = new CGMovementManager(board);
        cbGenerator.PopulateBoard(board, 0.4f);
    }
}