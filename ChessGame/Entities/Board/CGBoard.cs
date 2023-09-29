using ChessGame.Entities.Board;
using ChessGame.Structs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace ChessGame.Actors;

public class CGBoard
{
    private Scene OwningScene { get; }
    
    /// <summary>
    /// 
    /// </summary>
    private Texture2D OddTileTexture { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    private Texture2D EvenTileTexture { get; set; }

    private int TileSpawnOffset { get; set; }

    private int TileSpacing { get; set; }
    
    public enum TileType
    {
        Odd,
        Even
    }
    
    public CGBoard(Scene owningScene)
    {
        OwningScene = owningScene;
    }
    
    public CGBoard SetTexture(TileType tileType, string texturePath)
    {
        switch (tileType)
        {
            case TileType.Odd:
                OddTileTexture = OwningScene.Content.LoadTexture(texturePath);
                break;
            case TileType.Even:
                EvenTileTexture = OwningScene.Content.LoadTexture(texturePath);;
                break;
        }

        return this;
    }
    
    public CGBoard SetOffset(int offset)
    {
        TileSpawnOffset = offset;
        return this;
    }
    
    public CGBoard SetSpacing(int spacing)
    {
        TileSpacing = spacing;
        return this;
    }


    // TODO: Change to return the tiles in the most convenient way
    public void Generate(int size = 8)
    {
        var y = OddTileTexture.Height + TileSpacing;
        var x = OddTileTexture.Width + TileSpacing;
        
        for (int row = 0; row < size; row++)
        {
            var tileXPosition = (x * row) + TileSpawnOffset;
            for (int column = 0; column < size; column++)
            {
                var tileYPosition = (y * column) + TileSpawnOffset;
                
                var texture = (column + row) % 2 == 0 ? EvenTileTexture : OddTileTexture;
                
                BoardPosition boardPos = new((char)(row + 65), size - column);
                var tile = OwningScene.AddEntity(new CGTile(boardPos, texture));
                tile.Transform.SetPosition(new Vector2(tileXPosition, tileYPosition));
            }
        }
    }
}