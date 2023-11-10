using ChessGame.Actors;
using ChessGame.Entities.Board;
using ChessGame.Entities.Components;
using ChessGame.Entities.Pieces;
using ChessGame.Enums;
using ChessGame.UI;
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

        GameUI gameUI = CreateEntity("game-ui").AddComponent<GameUI>();

        Canvas = CreateEntity("ui").AddComponent(new UICanvas());
        Canvas.IsFullScreen = true;
        Canvas.RenderLayer = ScreenSpaceRenderLayer;
        
        var cbGenerator = new CGBoard(this);

        cbGenerator
            .SetOffsetX(300)
            .SetOffsetY(140);

        var board = cbGenerator.Generate(0.8f);
        var movementManager = CGMovementManager.GetInstance(board, this);


        string cGTeam = movementManager.GetPlayer();
        gameUI.player = cGTeam;

        cbGenerator.PopulateBoard(board, 0.4f);
    }
}