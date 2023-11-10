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
        public int playtime;
        public string player;

        public override void Update()
        {

           

        }

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

            topButtonStyle.FontScale = 1.5f;





            var menuTab = new Table().Center();



            var lblStyle = new LabelStyle(Color.White);
            lblStyle.FontScale = 5f;
            lblStyle.Background = (new PrimitiveDrawable(Color.Black));


            var lblTitle = new Nez.UI.Label("Game Over");
            lblTitle.SetFontScale(5);
            lblTitle.SetBackground(new PrimitiveDrawable(Color.Black));
            _table.Add(lblTitle);
            _table.Row().SetPadTop(20);
            _table.Row().SetPadBottom(10);

            var lblContent = new Nez.UI.Label(player+" Win!!!");
            lblTitle.SetFontScale(5);
            _table.Add(lblTitle);
            _table.Row().SetPadTop(20);
            _table.Row().SetPadBottom(10);

            var lblConten2 = new Nez.UI.Label("Total play time: "+playtime+" seconds");
            lblTitle.SetFontScale(5);
            _table.Add(lblTitle);
            _table.Row().SetPadTop(20);
            _table.Row().SetPadBottom(10);

            _table.Add(new TextButton("Quit", topButtonStyle)).SetFillX().SetMinHeight(50)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   Environment.Exit(0);
               };


        }
    }
}
