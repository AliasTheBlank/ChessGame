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
    // [SampleScene("Menu Scene", 9999, "Scene with a single Entity. The minimum to have something to show")]
    public class MenuScene:Scene
    {
        public const int ScreenSpaceRenderLayer = 999;
        public UICanvas Canvas;


        public override void Initialize()
        {
            SetDesignResolution(1280, 720, SceneResolutionPolicy.None);
            Screen.SetSize(1280, 720);
            CreateEntity("menu-ui").AddComponent<MenuUI>();


            //AddRenderer(new DefaultRenderer());

            /*Canvas = CreateEntity("ui").AddComponent(new UICanvas());
            Canvas.IsFullScreen = false;
            Canvas.RenderLayer = ScreenSpaceRenderLayer;
            SetUpUI();*/
        }


        public MenuScene(){}

     /*   void SetUpUI()
        {
           
            _table = Canvas.Stage.AddElement(new Table());
            var UI = new Table();
            _table.SetFillParent(true).Center();

            var topButtonStyle = new TextButtonStyle(new PrimitiveDrawable(Color.Black, 10f),
                new PrimitiveDrawable(Color.Yellow), new PrimitiveDrawable(Color.DarkSlateBlue))
            {
                DownFontColor = Color.Black
            };
            
            topButtonStyle.FontScale = 1.5f;
          
            
        

            _table.Add(new TextButton("Single Player", topButtonStyle)).SetFillX().SetMinHeight(50)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   
                   TweenManager.StopAllTweens();
                   Core.GetGlobalManager<ImGuiManager>()?.SetEnabled(true);
                   Core.StartSceneTransition(new FadeTransition(() => new BasicScene()));
               };
            _table.Row().SetPadTop(20);



            _table.Add(new TextButton("Multi Player Offline", topButtonStyle)).SetFillX().SetMinHeight(50)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   //Go to scene
                 
                   TweenManager.StopAllTweens();
                   Core.GetGlobalManager<ImGuiManager>()?.SetEnabled(true);
                   Core.StartSceneTransition(new FadeTransition(() => new TempGameScene()));
               };
            _table.Row().SetPadTop(20);
            _table.Add(new TextButton("Multi Player LAN", topButtonStyle)).SetFillX().SetMinHeight(50)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   //Go to scene
               };
            _table.Row().SetPadTop(20);
            _table.Add(new TextButton("Multi Player WLAN", topButtonStyle)).SetFillX().SetMinHeight(50)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   //Go to scene
               };
            _table.Row().SetPadTop(20);
            _table.Add(new TextButton("Quit", topButtonStyle)).SetFillX().SetMinHeight(50)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   Environment.Exit(0);
               };
        }*/
    }

}


