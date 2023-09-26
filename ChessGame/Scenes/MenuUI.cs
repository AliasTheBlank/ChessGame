using Nez;
using Nez.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Scenes
{
    public class MenuUI:UICanvas
    {
   



        public override void OnAddedToEntity()
        {
            Debug.Log("Here");
            base.OnAddedToEntity();

            // setup a Skin and a Table for our UI
            var skin = Skin.CreateDefaultSkin();
            var table = Stage.AddElement(new Table());
            table.Defaults().SetPadTop(10).SetMinWidth(170).SetMinHeight(30);
            table.SetFillParent(true).Center();

            // add a button for each of the actions/AI types we need
            table.Add(new TextButton("bt: new", skin)).GetElement<TextButton>();
            table.Row();

        }

    }
}

