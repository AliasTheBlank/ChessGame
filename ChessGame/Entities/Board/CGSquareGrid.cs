using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChessGame.Actors;

public class CGSquareGrid
{
    public bool showGrid;
    public Vector2 slotDims, gridDims, physicalStartPos, totalPhysicalDims, currentHoverSlot, updateOffset;
    public Texture2D Texture2D;

    public List<GridItem> GridItems = new List<GridItem>();
    
}