using System;
using System.ComponentModel;
using System.Reflection;

namespace ChessGame.Enums;

public enum CGPieceType
{
    [Description("")]
    Pawn,

    [Description("N")]
    Knight,

    [Description("R")]
    Rook,

    [Description("B")]
    Bishop,

    [Description("Q")]
    Queen,

    [Description("K")]
    King,

    [Description("")]
    Board
}

public static class EnumHelper
{
    public static string GetDescription(CGPieceType piece)
    {

        Type type = piece.GetType();

        MemberInfo[] memInfo = type.GetMember(piece.ToString());
        if (memInfo != null && memInfo.Length > 0)
        {
            object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attrs != null && attrs.Length > 0)
                return ((DescriptionAttribute)attrs[0]).Description;
        }
        return piece.ToString();
    }
}