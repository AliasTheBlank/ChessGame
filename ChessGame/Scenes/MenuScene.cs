using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Nez;
using Microsoft.Xna.Framework.Graphics;
using Nez.Samples;
using Nez.UI;
using Nez.ImGuiTools;
using Nez.Tweens;
using Nez.Console;



namespace ChessGame.Scenes
{
   // [SampleScene("Menu Scene", 9999, "Scene with a single Entity. The minimum to have something to show")]
    public class MenuScene:Scene
    {
        public const int ScreenSpaceRenderLayer = 999;
        public UICanvas Canvas;

        Table _table;

        ScreenSpaceRenderer _screenSpaceRenderer;
        static bool _needsFullRenderSizeForUi;
        NezSpriteFont _font;
        SpriteFont _spriteFont;
        public void Initualize()
        {
            SetDesignResolution(1280, 720, SceneResolutionPolicy.None);
            Screen.SetSize(1280, 720);
            //AddRenderer(new DefaultRenderer());
            CreateEntity("menu-ui").AddComponent<MenuUI>();
        }


        public MenuScene()
        {
            this.Initialize();
            // create our canvas and put it on the screen space render layer
            Canvas = CreateEntity("ui").AddComponent(new UICanvas());
            Canvas.IsFullScreen = false;
            Canvas.RenderLayer = ScreenSpaceRenderLayer;
            SetUpUI();
        }

        void SetUpUI()
        {
            //_spriteFont = Content.Load<SpriteFont>("Minecraft.ttf");
            //_font = new NezSpriteFont(_spriteFont);

            _table = Canvas.Stage.AddElement(new Table());
            var UI = new Table();
            _table.SetFillParent(true).Center();

            var topButtonStyle = new TextButtonStyle(new PrimitiveDrawable(Color.Black, 10f),
                new PrimitiveDrawable(Color.Yellow), new PrimitiveDrawable(Color.DarkSlateBlue))
            {
                DownFontColor = Color.Black
            };
            
            topButtonStyle.FontScale = 1.5f;
            // topButtonStyle.Font = _font;

            var skin = Skin.CreateDefaultSkin();
            //skin.GetFont();


            _table.Add(new TextButton("Single Player", topButtonStyle)).SetFillX().SetMinHeight(50)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   //Go to scene
               };

            _table.Row().SetPadTop(20);
            _table.Add(new TextButton("Multi Player Offline", topButtonStyle)).SetFillX().SetMinHeight(50)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   //Go to scene
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
        }
    }

}


