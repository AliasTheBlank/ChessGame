using Nez;
using Nez.UI;
using System;
using ChessGame.DAL;
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
                Core.StartSceneTransition(new FadeTransition(() => new CGGameScene()));
            };
            _table.Row().SetPadTop(20);

            var playerManager = CGPlayerManager.GetInstance();
            var playerLoggedIn = playerManager.player1IsLoggedIn ? "Log In player 1" : "Log out Player 1";
            
            _table.Add(new TextButton(playerLoggedIn, topButtonStyle)).SetFillX().SetMinHeight(50)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   if (playerManager.player1IsLoggedIn)
                   {
                       playerManager.LogOutPlayer(1);
                   }
                   else
                   {
                       Core.StartSceneTransition(new FadeTransition(() => new CGLogInScene(1)));
                   }
               };
            
            playerLoggedIn = playerManager.player2IsLoggedIn ? "Log In player 2" : "Log out Player 2";
            _table.Row().SetPadTop(20);
            _table.Add(new TextButton(playerLoggedIn, topButtonStyle)).SetFillX().SetMinHeight(50)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   if (playerManager.player1IsLoggedIn)
                   {
                       playerManager.LogOutPlayer(2);
                   }
                   else
                   {
                       Core.StartSceneTransition(new FadeTransition(() => new CGLogInScene(2)));
                   }
               };
            
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

