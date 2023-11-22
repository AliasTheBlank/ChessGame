using System.IO;
using ChessGame.DAL;
using ChessGame.UI;
using Nez;

namespace ChessGame.Scenes;

public class CGLogInScene : Scene
{
    private int _playerToRegister;
    public const int ScreenSpaceRenderLayer = 999;
    public UICanvas Canvas;


    public override void Initialize()
    {
        SetDesignResolution(1280, 720, SceneResolutionPolicy.None);
        Screen.SetSize(1280, 720);
        var ui = CreateEntity("menu-ui").AddComponent<LogInUI>();
        ui.PlayerToRegister = _playerToRegister;
    }


    public CGLogInScene(int playerToRegister = 1)
    {
        _playerToRegister = playerToRegister;
    }

}