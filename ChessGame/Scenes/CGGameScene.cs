using System.Linq;
using ChessGame.Actors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;

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
        
        
        Screen.SetSize(640*2, 640*2);
    
        // create our canvas and put it on the screen space render layer
        Canvas = CreateEntity("ui").AddComponent(new UICanvas());
        Canvas.IsFullScreen = true;
        Canvas.RenderLayer = ScreenSpaceRenderLayer;


        var texture2 = Content.LoadTexture(@"Content/Sprites/WhiteTile.png");
        //var tile = AddEntity(new CGTile('F',1,texture2));
        var board = AddEntity(new CGBoard(this));

    }
}