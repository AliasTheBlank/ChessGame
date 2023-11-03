using System;
using System.Collections.Generic;
using ChessGame.Actors;
using ChessGame.Entities.Board;
using ChessGame.Entities.Pieces;
using ChessGame.Enums;
using ChessGame.Structs;
using Microsoft.Xna.Framework.Graphics;
using Nez;

namespace ChessGame.Entities.Sprites;

public class CGTextureManager
{
    private static CGTextureManager _inst;
    
    private Scene OwningScene;
    
    public Texture2D OddTileTexture { get; set; }

    public Dictionary<CGPieceType, CGPieceTexture> TextureDictionary;

    public int TileScale;

    public float PieceScale;
    
    private CGTextureManager(Scene scene)
    {
        OwningScene = scene;
        TextureDictionary = new Dictionary<CGPieceType, CGPieceTexture>();
        
        this.SetBoardTexture()
            .SetPawnTexture()
            .SetBishopTexture()
            .SetKingTexture()
            .SetQueenTexture()
            .SetRookTexture()
            .SetKnightTexture();
    }

    public static CGTextureManager GetInstance(Scene scene)
    {
        _inst = new CGTextureManager(scene);

        return _inst;
    }

    public static CGTextureManager GetInstance()
    {
        return _inst;
    }
    
    public CGTextureManager SetBoardTexture()
    {
        TextureDictionary.Add(CGPieceType.Board, new CGPieceTexture());
        
        var result = TextureDictionary[CGPieceType.Board];
        result.WhiteTexture = OwningScene.Content.Load<Texture2D>("../Content/Sprites/WhiteTile");
        result.BlackTexture = OwningScene.Content.Load<Texture2D>("../Content/Sprites/BlackTile");
            
        return this;
    }
    public CGTextureManager SetPawnTexture()
    {
        TextureDictionary.Add(CGPieceType.Pawn, new CGPieceTexture());
        
        var result = TextureDictionary[CGPieceType.Pawn];
        result.WhiteTexture = OwningScene.Content.Load<Texture2D>("../Content/Sprites/chess-pawn-white");
        result.BlackTexture = OwningScene.Content.Load<Texture2D>("../Content/Sprites/chess-pawn-black");
            
        return this;
    }
    
    public CGTextureManager SetBishopTexture()
    {
        TextureDictionary.Add(CGPieceType.Bishop, new CGPieceTexture());
        
        var result = TextureDictionary[CGPieceType.Bishop];
        result.WhiteTexture = OwningScene.Content.Load<Texture2D>("../Content/Sprites/chess-bishop-white");
        result.BlackTexture = OwningScene.Content.Load<Texture2D>("../Content/Sprites/chess-bishop-black");
            
        return this;
    }
    
    public CGTextureManager SetKnightTexture()
    {
        TextureDictionary.Add(CGPieceType.Knight, new CGPieceTexture());
        
        var result = TextureDictionary[CGPieceType.Knight];
        result.WhiteTexture = OwningScene.Content.Load<Texture2D>("../Content/Sprites/chess-knight-white");
        result.BlackTexture = OwningScene.Content.Load<Texture2D>("../Content/Sprites/chess-knight-black");
            
        return this;
    }
    
    public CGTextureManager SetKingTexture()
    {
        TextureDictionary.Add(CGPieceType.King, new CGPieceTexture());
        
        var result = TextureDictionary[CGPieceType.King];
        result.WhiteTexture = OwningScene.Content.Load<Texture2D>("../Content/Sprites/chess-king-white");
        result.BlackTexture = OwningScene.Content.Load<Texture2D>("../Content/Sprites/chess-king-black");
            
        return this;
    }
    
    public CGTextureManager SetQueenTexture()
    {
        TextureDictionary.Add(CGPieceType.Queen, new CGPieceTexture());
        
        var result = TextureDictionary[CGPieceType.Queen];
        result.WhiteTexture = OwningScene.Content.Load<Texture2D>("../Content/Sprites/chess-queen-white");
        result.BlackTexture = OwningScene.Content.Load<Texture2D>("../Content/Sprites/chess-queen-black");
            
        return this;
    }
    
    public CGTextureManager SetRookTexture()
    {
        TextureDictionary.Add(CGPieceType.Rook, new CGPieceTexture());
        
        var result = TextureDictionary[CGPieceType.Rook];
        result.WhiteTexture = OwningScene.Content.Load<Texture2D>("../Content/Sprites/chess-rook-white");
        result.BlackTexture = OwningScene.Content.Load<Texture2D>("../Content/Sprites/chess-rook-black");
            
        return this;
    }
}