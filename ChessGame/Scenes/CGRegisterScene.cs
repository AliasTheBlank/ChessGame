using ChessGame.UI;
using Nez;

namespace ChessGame.Scenes;

public class CGRegisterScene : Scene
{
    public override void Initialize()
    {
        SetDesignResolution(1280, 720, SceneResolutionPolicy.None);
        Screen.SetSize(1280, 720);
        CreateEntity("menu-ui").AddComponent<RegisterUI>();
    }

    public CGRegisterScene()
    {

    }
}