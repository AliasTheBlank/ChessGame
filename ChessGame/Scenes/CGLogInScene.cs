using System.IO;
using ChessGame.DAL;
using ChessGame.UI;
using Nez;

namespace ChessGame.Scenes;

public class CGLogInScene : Scene
{
    private LogInUI _ui;
        
    public const int ScreenSpaceRenderLayer = 999;
    public UICanvas Canvas;


    public override void Initialize()
    {
        SetDesignResolution(1280, 720, SceneResolutionPolicy.None);
        Screen.SetSize(1280, 720);
        _ui = CreateEntity("menu-ui").AddComponent<LogInUI>();
    }


    public CGLogInScene(int playerToRegister = 1)
    {
        _ui.PlayerToRegister = playerToRegister;
    }

}