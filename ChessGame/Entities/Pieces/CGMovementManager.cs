using System;
using System.Collections.Generic;
using ChessGame.Entities.Board;
using ChessGame.Entities.Sprites;
using ChessGame.Enums;
using ChessGame.UI;
using Nez;
using System.ComponentModel;
using ChessGame.Structs;
using ChessGame.Scenes;

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
    public static int NumOfMove = 0;
    public string MoveRecords = "";


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

                if(clickTile.CurrentPiece == null)
                {
                    return;
                }
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
    bool GameRunning = true;
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

        var gameUI = _mainScene.FindEntity("game-ui").GetComponent<GameUI>();
        gameUI.player = _activePlayer.ToString();
        gameUI.ResetTimer();

        if (FindOpportnentKing(_inactivePlayer,Board) == null)
        {
            GameOver();
        }

        if (GameStateCanCheckOpportnent(_inactivePlayer, Board))
        {
            MoveRecords += "+";
            if (GameStateCanCheckMateOpponent(_inactivePlayer)){
                MoveRecords += "#";
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        //var gameUI = _mainScene.FindEntity("game-ui").GetComponent<GameUI>();

        Core.StartSceneTransition(new FadeTransition(() => new CGGameOverScene(_inactivePlayer.ToString(),MoveRecords)));
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
        if (IsMoveIllegal(start,end))
        {
            return;
        }

        if (_activePlayer == CGTeam.White)
        {
            NumOfMove++;
            MoveRecords += " " + NumOfMove + ".";
        }
        else
        {
            MoveRecords += " ";
        }
        string isCaptured = null;


        if (end.CurrentPiece != null && EnumHelper.GetDescription(start.CurrentPiece.Type) == "")
        {
            isCaptured = char.ToLower(start.BoardPosition.GetFileName()) + "x";
        }
        else if (end.CurrentPiece != null)
        {
            isCaptured = EnumHelper.GetDescription(start.CurrentPiece.Type) + "x";
        }
        MoveRecords += isCaptured + char.ToLowerInvariant(end.BoardPosition.GetFileName()) + end.BoardPosition.GetRankValue();

        end.CurrentPiece = start.CurrentPiece;
        end.CurrentPiece.Position = end.Position;
        end.CurrentPiece.Moved = true;

    }
    public bool IsMoveIllegal(CGTile start, CGTile end)
    {
        var board = MovementBoard(Board,start, end);
        var curTeam = start.CurrentPiece.Team;
        var oppTeam = start.CurrentPiece.Team == CGTeam.White ? CGTeam.Black : CGTeam.White;

        CGTile curKing = FindOpportnentKing(oppTeam, board);
        var kpos = curKing.BoardPosition.ToString();
        foreach (var piece in board)
        {
            if (piece.CurrentPiece == null) continue;
            if (piece.CurrentPiece.Team == curTeam) continue;
            var p = piece;
            
            if (CanCaptureTile(piece, curKing, board))
            {
                return true;
            }
        }
        return false;
    }
    private CGTile FindOpportnentKing(CGTeam team, CGTile[,] board)
    {
        CGTile oppKing = null;
        foreach (var p in board)
        {
            if (p.CurrentPiece == null) continue;
            if (p.CurrentPiece.Team == team) continue;
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
        CGTile oppKing = FindOpportnentKing(team,board);

        foreach (var piece in board)
        {
            if (piece.CurrentPiece == null) continue;
            if (piece.CurrentPiece.Team != team) continue;

            if (CanCaptureTile(piece, oppKing, board))
            {
                return true;
            }

        }
        return false;
    }

    public CGTile[,] MovementBoard(CGTile[,] board, CGTile start, CGTile end)
    {

        CGTile[,] replicatedBoards = new CGTile[board.GetLength(0), board.GetLength(1)];

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                replicatedBoards[i, j] = new CGTile(board[i, j]);
                if (board[i, j].BoardPosition == start.BoardPosition)
                    replicatedBoards[i, j] = new CGTile(end, start.BoardPosition);

                if (board[i, j].BoardPosition == end.BoardPosition)
                    replicatedBoards[i, j] = new CGTile(start, end.BoardPosition);
                
            }
        }
        return replicatedBoards;
    }

    public bool GameStateCanCheckMateOpponent(CGTeam team)
    {

        CGTile oppKing = FindOpportnentKing(team,Board);
        //find oppornent king moves
        List<CGTile> kingMoves = oppKing.CurrentPiece.GetMoves(oppKing, oppKing.CurrentPiece.Team, Board);
        List<BoardPosition>kingPotentialPos = new List<BoardPosition>(kingMoves.Count);

        for (int i = 0; i < kingMoves.Count; i++)
        {
            kingPotentialPos.Add(kingMoves[i].BoardPosition);
        }

        bool[] movesInCheck = new bool[kingMoves.Count];

        for (int i = 0; i < kingMoves.Count; i++)
        {
            CGTile[,] replicatedBoards = MovementBoard(Board, oppKing, kingMoves[i]);

            foreach (var piece in replicatedBoards)
            {
                if (piece.CurrentPiece == null) continue;
                if (piece.CurrentPiece.Team == oppKing.CurrentPiece.Team) continue;

                List<CGTile> currPossibleMove = piece.CurrentPiece.GetMoves(piece, piece.CurrentPiece.Team, replicatedBoards);

                foreach (var move in currPossibleMove)
                {
                    if (move.BoardPosition == kingPotentialPos[i])
                    {
                        //check for counter move

                        movesInCheck[i] = true;
                        break;
                    }
                }
            }
        }

        bool isCheckMate = true;

        foreach (var move in movesInCheck)
        {
            if (move == false)
            {
                isCheckMate = false;
                break;
            }
        }

        if (isCheckMate) {
            return true;
        }
        return false;

    }
    public bool CanCaptureTile(CGTile currPiece, CGTile targetTile, CGTile[,] board)
    {
        List<CGTile> currPossibleMove = currPiece.CurrentPiece.GetMoves(currPiece, currPiece.CurrentPiece.Team, board);
        foreach (var move in currPossibleMove)
        {
            if (move.CurrentPiece == null) continue;
            if (move == targetTile)
                return true;
            
        }
        return false;
    }
    
}

  
