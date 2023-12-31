﻿using System.Windows.Forms;
using ChessGame.DAL;
using ChessGame.Scenes;
using Microsoft.Xna.Framework;
using Nez;
using Nez.ImGuiTools;
using Nez.Tweens;
using Nez.UI;


namespace ChessGame.UI;

public class RegisterUI : UICanvas
{
    public int PlayerSession;
    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();
        
        var _table = Stage.AddElement(new Table());
        _table.SetFillParent(true).Center();
        
        var skin = Skin.CreateDefaultSkin();
        
        var topButtonStyle = new TextButtonStyle(new PrimitiveDrawable(Color.Black, 10f),
            new PrimitiveDrawable(Color.Yellow), new PrimitiveDrawable(Color.DarkSlateBlue))
        {
            DownFontColor = Color.Black
        };

        topButtonStyle.FontScale = 1.5f;
        
        var lblTitle = new Nez.UI.Label("Welcome to our chess game");
        lblTitle.SetFontScale(3);
        _table.Add(lblTitle);
        _table.Row().SetPadTop(20);
        
        var lblError = new Nez.UI.Label("");
        lblError.SetFontScale(2.5f);
        _table.Add(lblError);
        _table.Row().SetPadTop(20);

        var lblUserName = new Nez.UI.Label("Username");
        lblUserName.SetFontScale(2f);
        _table.Add(lblUserName);
        
        _table.Row().SetPadTop(20);
        
        var userName = new TextField("", skin);
        _table.Add(userName);
        _table.Row().SetPadTop(20);
        
        var lblPassword = new Nez.UI.Label("Password");
        lblPassword.SetFontScale(2f);
        _table.Add(lblPassword);
        _table.Row().SetPadTop(20);

        var password = new TextField("", skin);
        _table.Add(password);
        _table.Row().SetPadTop(20);
        
        var lblConfirmPassword = new Nez.UI.Label("Confirm password");
        lblConfirmPassword.SetFontScale(2f);
        _table.Add(lblConfirmPassword);
        _table.Row().SetPadTop(20);

        var confirmPassword = new TextField("", skin);
        _table.Add(confirmPassword);
        _table.Row().SetPadTop(20);

        _table.Add(new TextButton("Register", topButtonStyle)).SetFillX().SetMinHeight(50)
            .GetElement<TextButton>().OnClicked += butt =>
        {
            try
            {
                if (UtilityDB.UserExist(userName.GetText()))
                {
                    lblError.SetText("User already exist");
                    return;
                }

                if (password.GetText() != confirmPassword.GetText())
                {
                    lblError.SetText("Password doesn't match");
                    return;
                }

                UtilityDB.CreateUser(userName.GetText(), password.GetText());
                lblError.SetText("User successfully created");
                userName.SetText("");
                password.SetText("");
                confirmPassword.SetText("");
            }
            catch
            {
                lblError.SetText("Servers are currently unavailable");
            }
        };

        _table.Row().SetPadTop(20);
        _table.Add(new TextButton("Already have an account? Log in!", topButtonStyle)).SetFillX().SetMinHeight(50)
            .GetElement<TextButton>().OnClicked += butt =>
        {
            TweenManager.StopAllTweens();
            Core.GetGlobalManager<ImGuiManager>()?.SetEnabled(true);
            Core.StartSceneTransition(new FadeTransition(() => new CGLogInScene(PlayerSession)));
        };
    }
}