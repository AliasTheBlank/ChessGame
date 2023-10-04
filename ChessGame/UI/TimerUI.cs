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
        private int time;
        private Label timeLabel;

        private int time2;
        private Label timeLabel2;
        Label debug;
        string db="";
        //private Table table;
        /*public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            
        }*/

       

        public override void Update()
        {
            base.Update();
            Core.Schedule(0f, false, timer =>
            {
                //time += 1;

                Stopwatch sw = new Stopwatch();
                sw.Start();
                if (sw.Elapsed.TotalMilliseconds%1000 == 0)
                {
                    time += 1;
                }

                //Task.Delay(new TimeSpan(0, 0, 1)).ContinueWith(t => { time += 1; });

            });
          /*  addTime1s();
            addTime2s();*/

            timeLabel.SetText(time.ToString());
            //timeLabel2.SetText(time2.ToString());
        }


        async void addTime1s()
        {
            await Task.Delay(1000);
            time++;
        }
        async void addTime2s()
        {
            await Task.Delay(2000);
            time2++;
        }

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();

            var table = Stage.AddElement(new Table());
            table.SetFillParent(true).Top().Center();
            var lblStyle = new LabelStyle(Color.White);
            lblStyle.FontScale = 1.5f;
            lblStyle.Background = new PrimitiveDrawable(Color.Black);
            time = 0;
            time2 = 0;
            timeLabel = new Label(time.ToString(), lblStyle);

            table.Add(timeLabel.SetStyle(lblStyle)).SetMinWidth(120).SetMinHeight(30).SetPadLeft(12);

            timeLabel2 = new Label(time2.ToString(), lblStyle);

            table.Add(timeLabel2.SetStyle(lblStyle)).SetMinWidth(120).SetMinHeight(30).SetPadLeft(12);


            debug = new Label(db.ToString(), lblStyle);
            table.Add(debug.SetStyle(lblStyle)).SetMinWidth(120).SetMinHeight(30).SetPadLeft(12);

        }
        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
