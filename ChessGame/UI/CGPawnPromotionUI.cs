using ChessGame.Entities.Board;
using ChessGame.Entities.Pieces;
using ChessGame.Enums;
using Nez;
using Nez.UI;
using Microsoft.Xna.Framework;

namespace ChessGame.UI;

public class CGPawnPromotionUI : UICanvas
{
    public override void OnAddedToEntity()
    {
        var _table = Stage.AddElement(new Table());
        _table.SetFillParent(true).Right();
        
        var topButtonStyle = new TextButtonStyle(new PrimitiveDrawable(Color.Black, 10f),
            new PrimitiveDrawable(Color.Yellow), new PrimitiveDrawable(Color.DarkSlateBlue))
        {
            DownFontColor = Color.Black
        };

        topButtonStyle.FontScale = 1.5f;

        var momvementManager = CGMovementManager.GetInstance();
        
        _table.Add(new TextButton("Bishop", topButtonStyle)).SetFillX().SetMinHeight(50)
            .GetElement<TextButton>().OnClicked += butt =>
        {
            momvementManager.PromotePawn(CGPieceType.Bishop);
            //momvementManager.MoveRecords += EnumHelper.GetDescription(CGPieceType.Bishop);
        };
        
        _table.Add(new TextButton("Knight", topButtonStyle)).SetFillX().SetMinHeight(50)
            .GetElement<TextButton>().OnClicked += butt =>
        {
            momvementManager.PromotePawn(CGPieceType.Knight);
            //momvementManager.MoveRecords += EnumHelper.GetDescription(CGPieceType.Knight);

        };
        
        _table.Add(new TextButton("Rook", topButtonStyle)).SetFillX().SetMinHeight(50)
            .GetElement<TextButton>().OnClicked += butt =>
        {
            momvementManager.PromotePawn(CGPieceType.Rook);
            //momvementManager.MoveRecords += EnumHelper.GetDescription(CGPieceType.Rook);

        };
        
        _table.Add(new TextButton("Queen", topButtonStyle)).SetFillX().SetMinHeight(50)
            .GetElement<TextButton>().OnClicked += butt =>
        {
            momvementManager.PromotePawn(CGPieceType.Queen);
            //momvementManager.MoveRecords += EnumHelper.GetDescription(CGPieceType.Queen);


        };
    }
}