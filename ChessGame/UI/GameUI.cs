using Nez;
using Nez.UI;
using System;
using Microsoft.Xna.Framework;
using Nez.ImGuiTools;
using Nez.Tweens;
using ChessGame.Scenes;



namespace ChessGame.UI
{
    public  class GameUI : UICanvas
    {
        private Label timeLabel;
        private DateTime curr_time;
        private DateTime old_time;
        private int delta;


        public override void Update()
        {

            curr_time = DateTime.Now;

            delta = Convert.ToInt32((curr_time - old_time).TotalSeconds);

            timeLabel.SetText("Timer: "+delta.ToString());
            base.Update();

        }



       /* public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();

            var table = Stage.AddElement(new Table());
            table.SetFillParent(true).Top().Center();
            var lblStyle = new LabelStyle(Color.White);
            lblStyle.FontScale = 1.5f;
            lblStyle.Background = new PrimitiveDrawable(Color.Black);

            delta = 0;
            old_time = DateTime.Now;
            curr_time = DateTime.Now;

            timeLabel = new Label(delta.ToString(), lblStyle);

            table.Add(timeLabel.SetStyle(lblStyle)).SetMinWidth(120).SetMinHeight(30).SetPadLeft(12);



        }*/
        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
            var _table = Stage.AddElement(new Table());
            var UI = new Table();
            _table.SetFillParent(true).Left().Top();

            var menuButtonStyle = new TextButtonStyle(new PrimitiveDrawable(Color.Gray, 10f),
                new PrimitiveDrawable(Color.Yellow), new PrimitiveDrawable(Color.Black))
            {
                DownFontColor = Color.Black
            };

            menuButtonStyle.FontScale = 1.5f;


            var toggleButtonStyle = new TextButtonStyle(new PrimitiveDrawable(Color.Black, 10f),
                new PrimitiveDrawable(Color.Yellow), new PrimitiveDrawable(Color.DarkSlateGray))
            {
                DownFontColor = Color.Black
            };

            toggleButtonStyle.FontScale = 1.5f;



            var menuTab = new Table().Center();
            


            var lblStyle = new LabelStyle(Color.White);
            lblStyle.FontScale = 1.5f;
            lblStyle.Background = (new PrimitiveDrawable(Color.Black));





            _table.Add(new TextButton("Toggle Menu List", toggleButtonStyle)).SetFillX().SetMinHeight(30)
               .GetElement<Button>().OnClicked += butt =>
               {
                   menuTab.SetIsVisible(!menuTab.IsVisible());
               };

           


            const string playerOneTurn = "Player 1's turn";
            const string playerTwoTurn = "Player 2's turn";

            Label turnIndicator = new Label("Player 1's turn", lblStyle);

            _table.Add(turnIndicator).SetMinWidth(120).SetMinHeight(30).SetPadLeft(12);



            delta = 0;
            old_time = DateTime.Now;
            curr_time = DateTime.Now;

            timeLabel = new Label("Timer: "+delta.ToString(), lblStyle);

            
            
            _table.Add(timeLabel).SetMinWidth(120).SetMinHeight(30).SetPadLeft(12);


            _table.Row().SetPadBottom(10);

            _table.Add(menuTab).SetPadTop(15);

            menuTab.Add(new TextButton("Back to Main menu", menuButtonStyle)).SetFillX().SetMinHeight(30)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   //add reminder if user want to resign
                   TweenManager.StopAllTweens();
                   Core.GetGlobalManager<ImGuiManager>()?.SetEnabled(true);
                   Core.StartSceneTransition(new FadeTransition(() => new MenuScene()));
               };
            menuTab.Row().SetPadTop(15);

            var startBtn = new TextButton("Start Game", menuButtonStyle);
            menuTab.Add(startBtn).SetFillX().SetMinHeight(30)
              .GetElement<Button>().OnClicked += butt =>
              {

                  //menuTab.SetIsVisible(!menuTab.IsVisible());
                  if (startBtn.GetText() == "Start Game")
                      startBtn.SetText("Pause");
                  else
                  {
                      if(startBtn.GetText() == "Pause")
                      {
                          startBtn.SetText("Continue");
                          startBtn.SetBackground(new PrimitiveDrawable(Color.Red));
                      }
                      else
                      {
                          startBtn.SetText("Pause");
                          startBtn.SetBackground(new PrimitiveDrawable(Color.Gray));
                      }
                   

                  }
                  //change game state
              };
            menuTab.Row().SetPadTop(15);

            menuTab.Add(new TextButton("Action", menuButtonStyle)).SetFillX().SetMinHeight(30).GetElement<Button>().OnClicked += butt =>
            {
                turnIndicator.SetText(turnIndicator.GetText() == playerOneTurn ? playerTwoTurn : playerOneTurn);

                //reset timer
            };
            menuTab.Row().SetPadTop(15);

            menuTab.Add(new TextButton("Quit", menuButtonStyle)).SetFillX().SetMinHeight(30)
               .GetElement<TextButton>().OnClicked += butt =>
               {
                   //add reminder if user want to resign

                   Environment.Exit(0);
               };



      
        }
    }
}
