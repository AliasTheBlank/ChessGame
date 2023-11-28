using Nez;
using Nez.UI;
using System;
using ChessGame.DAL;
using ChessGame.Enums;
using Microsoft.Xna.Framework;
using Nez.ImGuiTools;
using Nez.Tweens;
using ChessGame.Scenes;


namespace ChessGame.UI
{
    public class MenuUI : UICanvas
    {

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();

            var _table = Stage.AddElement(new Table());
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
                Core.StartSceneTransition(new FadeTransition(() => new CGGameScene(EMatchType.Casual)));
            };
            _table.Row().SetPadTop(20);

            var playerManager = CGPlayerManager.GetInstance();
            var playerLoggedIn = playerManager.player1IsLoggedIn ? "Log out Player 1" : "Log In player 1";

            TextButton textButton = new TextButton(playerLoggedIn, topButtonStyle);
            
            _table.Add(textButton).SetFillX().SetMinHeight(50)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   if (playerManager.player1IsLoggedIn)
                   {
                       playerManager.LogOutPlayer(1);
                       textButton.SetText("Log in player 1");
                       Core.StartSceneTransition(new FadeTransition(() => new MenuScene()));
                   }
                   else
                   {
                       Core.StartSceneTransition(new FadeTransition(() => new CGLogInScene(1)));
                   }
               };
            
            playerLoggedIn = playerManager.player2IsLoggedIn ? "Log out Player 2" : "Log In player 2";
            textButton = new TextButton(playerLoggedIn, topButtonStyle);
            
            _table.Row().SetPadTop(20);
            _table.Add(textButton).SetFillX().SetMinHeight(50)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   if (playerManager.player2IsLoggedIn)
                   {
                       playerManager.LogOutPlayer(2);
                       textButton.SetText("Log in player 2");
                       Core.StartSceneTransition(new FadeTransition(() => new MenuScene()));
                   }
                   else
                   {
                       Core.StartSceneTransition(new FadeTransition(() => new CGLogInScene(2)));
                   }
               };

            if (playerManager.player1IsLoggedIn && playerManager.player2IsLoggedIn)
            {
                _table.Row().SetPadTop(20);
                _table.Add(new TextButton("Competitive match", topButtonStyle)).SetFillX().SetMinHeight(50)
                    .GetElement<TextButton>().OnClicked += butt =>
                {
                    TweenManager.StopAllTweens();
                    Core.GetGlobalManager<ImGuiManager>()?.SetEnabled(true);
                    Core.StartSceneTransition(new FadeTransition(() => new CGGameScene(EMatchType.Competitive)));
                };
            }
            
            _table.Row().SetPadTop(20);
            _table.Add(new TextButton("Log out", topButtonStyle)).SetFillX().SetMinHeight(50)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   //Go to scene
                   Core.StartSceneTransition(new FadeTransition(() => new CGLogInScene()));
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

