
using ChessGame.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Nez;
using Microsoft.Xna.Framework.Graphics;

using Nez.UI;
using Nez.ImGuiTools;
using Nez.Tweens;
using Nez.Console;


namespace ChessGame;

public class Game1 : Nez.Core
{
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
        
        Window.AllowUserResizing=true;
        Scene = new MenuScene();
    }
}