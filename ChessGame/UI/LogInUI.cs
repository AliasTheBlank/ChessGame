using System.Windows.Forms;
using ChessGame.DAL;
using ChessGame.Scenes;
using Microsoft.Xna.Framework;
using Nez;
using Nez.ImGuiTools;
using Nez.Tweens;
using Nez.UI;


namespace ChessGame.UI;

public class LogInUI : UICanvas
{
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

        var _lblUserName = new Nez.UI.Label("Username");
        _lblUserName.SetFontScale(2f);
        _table.Add(_lblUserName);
        
        _table.Row().SetPadTop(20);
        
        var _userName = new TextField("Username", skin);
        _table.Add(_userName);
        _table.Row().SetPadTop(20);
        
        var _lblLastName = new Nez.UI.Label("LastName");
        _lblLastName.SetFontScale(2f);
        _table.Add(_lblLastName);
        _table.Row().SetPadTop(20);

        var _lastName = new TextField("LastName", skin);
        _table.Add(_lastName);
        _table.Row().SetPadTop(20);

        _table.Add(new TextButton("Log in", topButtonStyle)).SetFillX().SetMinHeight(50)
            .GetElement<TextButton>().OnClicked += butt =>
        {
            if (!UtilityDB.ValidatePlayer(_userName.GetText(), _lastName.GetText()))
            {
                lblError.SetText("Invalid user or password");
                return;
            }
            
            TweenManager.StopAllTweens();
            Core.GetGlobalManager<ImGuiManager>()?.SetEnabled(true);
            Core.StartSceneTransition(new FadeTransition(() => new MenuScene()));
        };
    }
}