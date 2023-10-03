using ChessGame.Entities.Board;
using ChessGame.Entities.Pieces;
using Nez;

namespace ChessGame.Entities.Components;

public class ClickableComponent : Component, IUpdatable
{
    public void Update()
    {
        var bounds = Entity.GetComponent<BoxCollider>().Bounds;
        var mousePoint = Input.MousePosition;

        if (bounds.Contains(mousePoint))
        {
            if (Input.LeftMouseButtonPressed)
            {
                CGMovementManager.ManageMovement((CGTile)Entity);
            }
        }

        else if (Input.RightMouseButtonPressed)
        {
            CGMovementManager.UnSelectTile(true);
        }
    }
}