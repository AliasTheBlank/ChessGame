using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Nez;
using Microsoft.Xna.Framework.Graphics;
using Nez.UI;
using Nez.ImGuiTools;
using Nez.Tweens;
using Nez.Console;
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

       /* void SetUpUI()
        {

            _table = Canvas.Stage.AddElement(new Table());
            var UI = new Table();
            _table.SetFillParent(true).Center().Top();

            var topButtonStyle = new TextButtonStyle(new PrimitiveDrawable(Color.Black, 10f),
                new PrimitiveDrawable(Color.Yellow), new PrimitiveDrawable(Color.DarkSlateBlue))
            {
                DownFontColor = Color.Black
            };

            topButtonStyle.FontScale = 1.5f;


            var menuTab = new Table().Center();
            
            var lblStyle = new LabelStyle(Color.Black);
            lblStyle.FontScale = 2.5f;

            _table.Add(new TextButton("Toggle Menu List", topButtonStyle)).SetFillX().SetMinHeight(30)
               .GetElement<Button>().OnClicked += butt =>
               {
                   menuTab.SetIsVisible(!menuTab.IsVisible());
               };
            _table.Add(new TextButton("Start Game", topButtonStyle)).SetFillX().SetMinHeight(30)
              .GetElement<Button>().OnClicked += butt =>
              {
                  
                  //menuTab.SetIsVisible(!menuTab.IsVisible());
              };
            _table.Add(new Label("Player _", lblStyle));
            _table.Add(new Label("Timer _", lblStyle));

            _table.Row();

            _table.Add(menuTab).SetAlign(Align.Top);

            menuTab.Add(new TextButton("Back to Main menu", topButtonStyle)).SetFillX().SetMinHeight(30)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   //add reminder if user want to resign
                   TweenManager.StopAllTweens();
                   Core.GetGlobalManager<ImGuiManager>()?.SetEnabled(true);
                   Core.StartSceneTransition(new FadeTransition(() => new MenuScene()));
               };
            menuTab.Row();

            menuTab.Add(new TextButton("Quit", topButtonStyle)).SetFillX().SetMinHeight(30)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   //add reminder if user want to resign

                   Environment.Exit(0);
               };

            
             
            Skin skin = Skin.CreateDefaultSkin();
            
            _table.Row();
            SelectBox<Button> gameModes = new SelectBox<Button>(skin);
            
            _table.Add(gameModes);
            _table.Row();


        }*/

    }
}
