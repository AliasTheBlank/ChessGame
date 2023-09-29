
using Nez;
using ChessGame.UI;

namespace ChessGame.Scenes
{
    
    public class MenuScene:Scene
    {
        public const int ScreenSpaceRenderLayer = 999;
        public UICanvas Canvas;


        public override void Initialize()
        {
            SetDesignResolution(1280, 720, SceneResolutionPolicy.None);
            Screen.SetSize(1280, 720);
            CreateEntity("menu-ui").AddComponent<MenuUI>();
        }


        public MenuScene(){}


    }

}


