using System;
using System.Collections.Generic;
using ChessGame.Entities.Board;
using ChessGame.Entities.Sprites;
using ChessGame.Enums;
using ChessGame.UI;
using Nez;
using System.ComponentModel;

namespace ChessGame.Entities.Pieces;

public class CGMovementManager
{
    private static CGMovementManager _instMovementManager;

    private bool _isTileFocus;
    private CGTile _selectedTile;

    private List<CGTile> _possibleMoves;
    private List<CGTile> _possibleCastleOptions;

    private CGTeam _activePlayer;
    private CGTeam _inactivePlayer;
    private Scene _mainScene;

    private bool _promotionInProgress;

    public static CGTile[,] Board;
    public static int NumOfMove=0;
    public string MoveRecords="";


    private CGMovementManager(CGTile[,] board, Scene mainScene)
    {
        Board = board;
        _isTileFocus = false;
        _activePlayer = CGTeam.White;
        _inactivePlayer = CGTeam.Black;
        _mainScene = mainScene;
    }
    public string GetPlayer()
    {
        return _activePlayer.ToString();
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
        if (_isTileFocus)
        {
            if (_possibleMoves.Contains(clickTile))
            {
                if (!clickTile.IsEmpty)
                    clickTile.CurrentPiece.Destroy();

                MovePiece(_selectedTile, clickTile);

                if (clickTile.CurrentPiece.Type == CGPieceType.Pawn)
                {
                    if ((_activePlayer == CGTeam.White && clickTile.BoardPosition.GetRankValue() == 8)
                        || _activePlayer == CGTeam.Black && clickTile.BoardPosition.GetRankValue() == 1)
                    {
                        _promotionInProgress = true;
                        _mainScene.CreateEntity("pawn-promotion").AddComponent<CGPawnPromotionUI>();
                        // char fname = char.ToLowerInvariant(clickTile.BoardPosition.GetFileName()).ToString() + clickTile.BoardPosition.GetRankValue().ToString() + ;
                        MoveRecords += "=";

                        //test for check

                       
                        return;
                    }
                }

                SwitchTurn();
            }
            //castle
            else if (_selectedTile.CurrentPiece.Type == CGPieceType.King && _possibleCastleOptions.Contains(clickTile))
            {
                int file = clickTile.BoardPosition.GetFileName() - 65;
                int rank = 8 - clickTile.BoardPosition.GetRankValue();

                CGTile rookStartTile;
                CGTile rookEndTile;
                // Long castle case
                if (file == 2)
                {
                    rookStartTile = Board[0, rank];
                    rookEndTile = Board[3, rank];
                    MoveRecords += "O-O-O ,";
                }
                else
                {
                    rookStartTile = Board[7, rank];
                    rookEndTile = Board[5, rank];
                    MoveRecords += "O-O ,";

                }

                MovePiece(rookStartTile, rookEndTile);
                MovePiece(_selectedTile, clickTile);

                MoveRecords = MoveRecords.Split(',')[0];


                SwitchTurn();
            }
        }
        
        else if (!clickTile.IsEmpty && clickTile.CurrentPiece.Team == _activePlayer)
        {
            if (!clickTile.IsEmpty) {
                _isTileFocus = true;
                _selectedTile = clickTile;
                _possibleMoves = _selectedTile.CurrentPiece.GetMoves(_selectedTile, _selectedTile.CurrentPiece.Team, Board);

                if (clickTile.CurrentPiece.Type == CGPieceType.King)
                {
                    _possibleCastleOptions =
                        CGPossibleMoves.GetCastleMoves(_selectedTile, _selectedTile.CurrentPiece.Team, Board);
                    foreach (var VARIABLE in _possibleCastleOptions)
                    {
                        Console.WriteLine(VARIABLE.BoardPosition);
                    }
                }
            }
        }

    }

    public void UnSelectTile()
    {
        if (_isTileFocus)
        {
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
        _inactivePlayer = _activePlayer;
        _activePlayer = _activePlayer == CGTeam.White ? CGTeam.Black : CGTeam.White;
        _promotionInProgress = false;
        _possibleCastleOptions = null;

        if (GameStateCanCheckOpportnent(_inactivePlayer,Board))
            GameStateCanCheckMateOpponent(_inactivePlayer);
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
        MoveRecords += EnumHelper.GetDescription(type);

        SwitchTurn();

        //
    }

    private void MovePiece(CGTile start, CGTile end)
    {
        if (_activePlayer == CGTeam.White)
        {
            NumOfMove++;
            MoveRecords += " "+NumOfMove + ".";
        }
        else
        {
            MoveRecords += " ";
        }
        char? isCaptured = null;
        if (end.CurrentPiece != null)
        {
            isCaptured = 'x' ;
        }

        //TODO: Add checkmate to notation
        MoveRecords +=  EnumHelper.GetDescription(start.CurrentPiece.Type) + isCaptured + char.ToLowerInvariant(end.BoardPosition.GetFileName()) + end.BoardPosition.GetRankValue();
        end.CurrentPiece = start.CurrentPiece;
        end.CurrentPiece.Position = end.Position;
        end.CurrentPiece.Moved = true;
       
    }
    #region working check
    private CGTile FindOpportnentKing(CGTeam team)
    {
        CGTile oppKing = null;
        foreach (var p in Board)
        {
            if(p.CurrentPiece==null) continue;
            if (p.CurrentPiece.Team == team) continue;
            //find oppornent king
            if (p.CurrentPiece.Type == CGPieceType.King)
            {
                oppKing = p;
                break;
            }

        }
        return oppKing;
    }
    private bool GameStateCanCheckOpportnent(CGTeam team, CGTile[,] board)
    {
        CGTile oppKing = FindOpportnentKing(team);

        foreach (var piece in board)
        {
            if (piece.CurrentPiece == null) continue;
            if (piece.CurrentPiece.Team != team) continue;

            if (CanCaptureTile(piece,oppKing))
            {
                MoveRecords += "+";                
                return true;
            }

        }
        return false;
    }

    public CGTile[,] KingMoveBoard(CGTile[,] board, CGTile king, CGTile piece) 
    {
        CGTile[,] replicatedBoards = board;
        //king.BoardPositio = piece.BoardPosition;



        return replicatedBoards;
    }
    public void GameStateCanCheckMateOpponent(CGTeam team)
    {


        CGTile oppKing = FindOpportnentKing(team);
        //find oppornent king moves
        List<CGTile> kingMoves = oppKing.CurrentPiece.GetMoves(oppKing, oppKing.CurrentPiece.Team, Board);

        bool IsCheck = false;

        bool[] movesInCheck = new bool[kingMoves.Count];

        Array.Fill(movesInCheck, IsCheck);

        //Check Mate part
        //check each piece potential move if they can check the king potential moves
        //foreach (var moveK in kingMoves)

        CGTile[,] replicatedBoards = Board;

        for(int i = 0; i < kingMoves.Count; i++)
        {

            //move king to potential move in the replicated board

           /* rookStartTile = Board[0, rank];
            rookEndTile = Board[3, rank];
            MoveRecords += "O-O-O ,";*/

            CGTile temp = kingMoves[i];
            kingMoves[i] = oppKing;
            oppKing = temp;

            

            kingMoves[i].CurrentPiece.Position = kingMoves[i].Position;
            kingMoves[i].CurrentPiece = oppKing.CurrentPiece;
            kingMoves[i].CurrentPiece.Moved = true;


            foreach (var piece in replicatedBoards)
            {
                if (piece.CurrentPiece == null) continue;
                if (piece.CurrentPiece.Team != team) continue;

                //find our piece and check if our piece can check opportnent king potential move

                if(CanCaptureTile(piece, kingMoves[i]))
                {
                    movesInCheck[i] = true;
                    break;
                }
            }
        }

        bool isCheckMate = true;

        foreach(var move in movesInCheck)
        {
            //king can move and not check
            if (move ==false)
            {
                isCheckMate = false;
                break;
            } 
        }


        if (isCheckMate) { MoveRecords += "#"; }
        
    }
    public bool CanCaptureTile(CGTile currPiece, CGTile targetTile)
    {
        List<CGTile> currPossibleMove = currPiece.CurrentPiece.GetMoves(currPiece, currPiece.CurrentPiece.Team, Board);


        foreach (var move in currPossibleMove)
        {
            if (move.CurrentPiece == null) continue;

            if (move == targetTile)
            {
                return true;
            }
        }
        return false;
    }

    /*public bool CanCaptureKing(CGTile currPiece)
    {


        _possibleMoves = currPiece.CurrentPiece.GetMoves(currPiece, currPiece.CurrentPiece.Team, Board);

        bool res = false;
        foreach (var move in _possibleMoves)
        {
            if (move.CurrentPiece == null) continue;

            if (move.CurrentPiece.Type == CGPieceType.King && move.CurrentPiece.Team != currPiece.CurrentPiece.Team)
            {

                res = true;
                break;
            }
        }

        _possibleMoves = null;
        return res;
    }*/
    #endregion


    #region new check and checkmate
    /*private bool IsCheckMate(CGTile piece)
    {
        if (!CanCaptureKing(piece)) return false;

        _possibleMoves = piece.CurrentPiece.GetMoves(piece, piece.CurrentPiece.Team, Board);

        CGTile KingPiece;

        //find King and its possible moves
        foreach (var move in _possibleMoves)
        {
            if (move.CurrentPiece == null) continue;

            if (move.CurrentPiece.Type == CGPieceType.King && move.CurrentPiece.Team != piece.CurrentPiece.Team)
            {
                KingPiece = move;
                //king pos move
                _possibleMoves = KingPiece.CurrentPiece.GetMoves(KingPiece, KingPiece.CurrentPiece.Team, Board);
                break;

            }
        }

        //check if kings possible moves is in check or not,
        //
        //if all is in check then check mate 

        foreach (var currPiece in Board)
        {
            foreach (var move in _possibleMoves)
            {
                if (!CanCaptureTile(currPiece, move))
                {
                    return false;
                }
            }
        }

        return true;
    }*/

    
    #endregion
}
  
