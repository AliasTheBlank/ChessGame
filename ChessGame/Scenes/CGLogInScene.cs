using System.IO;
using ChessGame.DAL;
using ChessGame.UI;
using Nez;

namespace ChessGame.Scenes;

public class CGLogInScene : Scene
{
    public const int ScreenSpaceRenderLayer = 999;
    public UICanvas Canvas;


    public override void Initialize()
    {
        SetDesignResolution(1280, 720, SceneResolutionPolicy.None);
        Screen.SetSize(1280, 720);
        CreateEntity("menu-ui").AddComponent<LogInUI>();
    }


    public CGLogInScene(){}

}