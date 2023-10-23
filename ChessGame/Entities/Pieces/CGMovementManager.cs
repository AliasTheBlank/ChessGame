using System;
using System.Collections.Generic;
using ChessGame.Entities.Board;
using ChessGame.Entities.Sprites;
using ChessGame.Enums;
using ChessGame.UI;
using Nez;

namespace ChessGame.Entities.Pieces;

public class CGMovementManager
{
    private static CGMovementManager _instMovementManager;

    private bool _isTileFocus;
    private CGTile _selectedTile;

    private List<CGTile> _possibleMoves;

    private CGTeam _activePlayer;
    private Scene _mainScene;

    private bool _promotionInProgress;

    public static CGTile[,] Board;
    private CGMovementManager(CGTile[,] board, Scene mainScene)
    {
        Board = board;
        _isTileFocus = false;
        _activePlayer = CGTeam.White;
        _mainScene = mainScene;
    }

    public static CGMovementManager GetInstance(CGTile[,] board, Scene mainScene)
    {
        if (_instMovementManager == null)
            _instMovementManager = new CGMovementManager(board, mainScene);

        return _instMovementManager;

    }
    
    public static CGMovementManager GetInstance()
    {
        if (_instMovementManager != null)
            return _instMovementManager;
        return null;
    }

    public void ManageMovement(CGTile clickTile)
    {
        if (_promotionInProgress)
            return;
        //Console.WriteLine(clickTile.BoardPosition + " Was selected from movement manager");
        if (_isTileFocus)
        {
            //Console.Write(clickTile.BoardPosition + " second tile was selected from movement manager");

            if (_possibleMoves.Contains(clickTile))
            {
                //Console.WriteLine(clickTile.BoardPosition + " is was move");
                if (!clickTile.IsEmpty)
                    clickTile.CurrentPiece.Destroy();

                clickTile.CurrentPiece = _selectedTile.CurrentPiece;
                clickTile.CurrentPiece.Position = clickTile.Position;
                clickTile.CurrentPiece.Moved = true;

                if (clickTile.CurrentPiece.Type == CGPieceType.Pawn)
                {
                    if ((_activePlayer == CGTeam.White && clickTile.BoardPosition.GetRankValue() == 8)
                        || _activePlayer == CGTeam.Black && clickTile.BoardPosition.GetRankValue() == 1)
                    {
                        _promotionInProgress = true;
                        _mainScene.CreateEntity("pawn-promotion").AddComponent<CGPawnPromotionUI>();
                        return;
                    }
                    
                }
                SwitchTurn();
            }
        }
        
        else if (!clickTile.IsEmpty && clickTile.CurrentPiece.Team == _activePlayer)
        {
            //Console.WriteLine(clickTile.BoardPosition + " Was selected from movement manager");
            if (!clickTile.IsEmpty) {
                _isTileFocus = true;
                _selectedTile = clickTile;
                _possibleMoves = _selectedTile.CurrentPiece.GetMoves(_selectedTile, _selectedTile.CurrentPiece.Team, Board);
            }
        }
    }

    public void UnSelectTile()
    {
        if (_isTileFocus)
        {
            Console.WriteLine( _selectedTile.BoardPosition + " Was unselected from movement manager no specific tile");
            _isTileFocus = false;
            _selectedTile = null;
        }
    }

    public void SwitchTurn()
    {
        _possibleMoves = null;
        _selectedTile.CurrentPiece = null;
        _isTileFocus = false;
        _selectedTile = null;
        _activePlayer = _activePlayer == CGTeam.White ? CGTeam.Black : CGTeam.White;
        _promotionInProgress = false;
    }

    public void PromotePawn(CGPieceType type)
    {
        var textManager = CGTextureManager.GetInstance();
        if (!textManager.TextureDictionary.TryGetValue(type, out var textures))
        {
            throw new Exception("Dictionary shouldn't be empty");
        }


        _selectedTile.CurrentPiece.SetUpPiece(type, textures.GetTexture(_selectedTile.CurrentPiece.Team));


        var promotionUI = _mainScene.FindEntity("pawn-promotion");
        
        promotionUI.Destroy();
        
        SwitchTurn();
    }
}