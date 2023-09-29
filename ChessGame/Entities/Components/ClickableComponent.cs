using System;
using ChessGame.Entities.Board;
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
                Console.WriteLine(((CGTile)Entity).BoardPosition);
            }
        }
    }
}