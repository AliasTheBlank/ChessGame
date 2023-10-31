using Nez;
using Nez.UI;
using Microsoft.Xna.Framework;
using System.Threading;
using System.Timers;
using System.Diagnostics;
using System.Threading.Tasks;
using System;

namespace ChessGame.UI
{
    public class TimerUI:UICanvas
    {
        private Label timeLabel;
        private DateTime curr_time;
        private DateTime old_time;
        private int delta;


        public override void Update()
        {
            
            curr_time = DateTime.Now;

            delta = Convert.ToInt32((curr_time - old_time).TotalSeconds);

            timeLabel.SetText(delta.ToString());
            base.Update();
            
        }



        public override void OnAddedToEntity()
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



        }
        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
