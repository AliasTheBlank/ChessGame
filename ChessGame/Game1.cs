
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
using Nez.Samples;
using Nez.UI;
using Nez.ImGuiTools;
using Nez.Tweens;
using Nez.Console;


namespace ChessGame;

public class Game1 : Nez.Core
{
    //private GraphicsDeviceManager _graphics;
    //private SpriteBatch _spriteBatch;

    //private Texture2D bgSprite;

    

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
        
        Window.AllowUserResizing=true;
        Scene = new TempGameScene();
        //var Stage = new Stage();
    }
/*
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        bgSprite = Content.Load<Texture2D>("board");

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Beige);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();

        _spriteBatch.Draw(bgSprite,new Rectangle(728/6, 0,512,512), Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }*/
}