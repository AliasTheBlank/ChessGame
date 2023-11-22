using ChessGame.UI;
using Nez;

namespace ChessGame.Scenes;

public class CGRegisterScene : Scene
{
    private int _playerSession;
    public override void Initialize()
    {
        SetDesignResolution(1280, 720, SceneResolutionPolicy.None);
        Screen.SetSize(1280, 720);
        var ui = CreateEntity("menu-ui").AddComponent<RegisterUI>();
        ui.PlayerSession = _playerSession;
    }

    public CGRegisterScene(int playerSession = 1)
    {
        _playerSession = playerSession;
    }
}