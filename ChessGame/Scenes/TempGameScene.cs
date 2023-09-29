using Nez;
using Nez.UI;
using ChessGame.UI;

namespace ChessGame.Scenes
{
    public class TempGameScene:Scene
    {
        public const int ScreenSpaceRenderLayer = 999;
        public UICanvas Canvas;

        Table _table;


        public TempGameScene() { }
        
        public override void Initialize()
        {
            SetDesignResolution(1280, 720, SceneResolutionPolicy.None);
            Screen.SetSize(1280, 720);
            /*Canvas = CreateEntity("ui").AddComponent(new UICanvas());
            Canvas.IsFullScreen = false;
            Canvas.RenderLayer = ScreenSpaceRenderLayer;
            SetUpUI();*/
            CreateEntity("game-ui").AddComponent<GameUI>();

        }
    }
}
