using System;

namespace ChessGame.Structs;

public readonly record struct BoardPosition
{
    private char FileName { get; }
    private int RankValue { get; }
    
    public BoardPosition(char File, int Rank)
    {
        RankValue = Rank;
        FileName = File;
    }
     
    public BoardPosition(string RankFile)
    {
        if (RankFile.Length != 2)
        {
            throw new InvalidOperationException("The heck man");
        }
        FileName = RankFile[0];
        RankValue = RankFile[1];
    }

    public override string ToString() => $"{FileName}{RankValue}";
} 