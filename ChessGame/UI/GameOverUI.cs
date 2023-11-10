using Nez;
using Nez.UI;
using System;
using Microsoft.Xna.Framework;
using Nez.ImGuiTools;
using Nez.Tweens;
using ChessGame.Scenes;
using ChessGame.Enums;

namespace ChessGame.UI
{
    public class GameOverUI: UICanvas
    {
        public string player;
        public string moveRecords;



        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
            var _table = Stage.AddElement(new Table());
            var UI = new Table();
            _table.SetFillParent(true).Center();
            _table.SetBackground(new PrimitiveDrawable(Color.DarkSlateBlue));

            var topButtonStyle = new TextButtonStyle(new PrimitiveDrawable(Color.Black, 10f),
                 new PrimitiveDrawable(Color.Yellow), new PrimitiveDrawable(Color.DarkSlateBlue))
            {
                DownFontColor = Color.Black
            };

            topButtonStyle.FontScale = 2f;



            var lblStyle = new LabelStyle(Color.White);
            lblStyle.FontScale = 4f;
            //lblStyle.Background = (new PrimitiveDrawable(Color.Black));






            var lblTitle = new Nez.UI.Label("Game Over");
            lblTitle.SetFontScale(3);

            _table.Add(lblTitle).SetMinWidth(60).SetMinHeight(60);
            _table.Row().SetPadTop(20);
            _table.Row().SetPadBottom(10);

            var lblContent = new Nez.UI.Label(player + " Win!!!",lblStyle) ;
            _table.Add(lblContent).SetMinWidth(60).SetMinHeight(60);
            _table.Row().SetPadTop(20);
            _table.Row().SetPadBottom(10);


            var lblMoveRecord = new Label("Move records:");
            lblMoveRecord.SetFontScale(3f);
            _table.Add(lblMoveRecord);
            _table.Row().SetPadTop(20);
            _table.Row().SetPadBottom(50);


            var txtMoveRecord = new Label(moveRecords);
            txtMoveRecord.SetFontScale(2f);
            _table.Add(txtMoveRecord);
            _table.Row().SetPadTop(20);
            _table.Row().SetPadBottom(10);

            _table.Add(new TextButton("Back to main menu", topButtonStyle)).SetMinHeight(50)
                .GetElement<TextButton>().OnClicked += butt =>
                {

                    TweenManager.StopAllTweens();
                    Core.GetGlobalManager<ImGuiManager>()?.SetEnabled(true);
                    Core.StartSceneTransition(new FadeTransition(() => new MenuScene()));
                };
            _table.Row().SetPadTop(20);

            _table.Add(new TextButton("Quit", topButtonStyle)).SetMinHeight(50)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   Environment.Exit(0);
               };


        }
    }
}
