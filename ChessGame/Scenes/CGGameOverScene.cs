using ChessGame.Actors;
using ChessGame.Entities.Board;
using ChessGame.Entities.Pieces;
using ChessGame.UI;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Scenes
{

    public class CGGameOverScene : Scene
    {
        public const int ScreenSpaceRenderLayer = 999;

        ScreenSpaceRenderer _screenSpaceRenderer;
        public UICanvas Canvas;
        public string MoveRecords="";
        public override void Initialize()
        {
            SetDesignResolution(1280, 720, SceneResolutionPolicy.None);
            Screen.SetSize(1280, 720);


        }


        public CGGameOverScene(string player,string moveRecord, bool stalement)
        {
            MoveRecords = moveRecord;
            var gameoverUI = new GameOverUI();
            gameoverUI.moveRecords = MoveRecords;
            gameoverUI.player = player;
            gameoverUI.stalement = stalement;

            CreateEntity("gameover-ui").AddComponent(gameoverUI);
        }
    }
}
