
using ChessGame.Scenes;


namespace ChessGame;

public class Game1 : Nez.Core
{
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
        
        Window.AllowUserResizing=true;
        Scene = new MenuScene();
        
        //Scene = new CGGameScene();
    }
}