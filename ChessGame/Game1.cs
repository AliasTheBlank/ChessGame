
using ChessGame.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;


namespace ChessGame;

public class Game1 : Nez.Core
{
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
        
        Scene = new CGGameScene();
    }
}