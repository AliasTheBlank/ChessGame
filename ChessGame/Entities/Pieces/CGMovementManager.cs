using System;
using System.Collections.Generic;
using ChessGame.Entities.Board;
using ChessGame.Entities.Sprites;
using ChessGame.Enums;
using ChessGame.UI;
using Nez;
using System.ComponentModel;
using ChessGame.DAL;
using ChessGame.Structs;
using ChessGame.Scenes;
using Microsoft.Xna.Framework;
using Nez.Sprites;
using ChessGame.Actors;
using System.IO;


namespace ChessGame.Entities.Pieces;

public class CGMovementManager
{
    private static CGMovementManager _instMovementManager;

    private bool _isTileFocus;
    private CGTile _selectedTile;
    private Color _selectedTileColor;
    
    private List<CGTile> _possibleMoves;
    private Dictionary<CGTile, Color> _possibleMovesColors;
    
    private List<CGTile> _possibleCastleOptions;

    private CGTeam _activePlayer;
    private CGTeam _inactivePlayer;
    private Scene _mainScene;

    private bool _promotionInProgress;

    private EMatchType _matchType;
    

    public static CGTile[,] Board;
    public static int NumOfMove = 0;
    public string MoveRecords = "";


    private CGMovementManager(CGTile[,] board, Scene mainScene, EMatchType matchType)
    {
        Board = board;
        _isTileFocus = false;
        _activePlayer = CGTeam.White;
        _inactivePlayer = CGTeam.Black;
        _mainScene = mainScene;
        _matchType = matchType;
    }
    public string GetPlayer()
    {
        return _activePlayer.ToString();
    }
    public static CGMovementManager GetInstance(CGTile[,] board, Scene mainScene, EMatchType matchType, bool generateNew = false)
    {
        if (_instMovementManager == null || generateNew)
            _instMovementManager = new CGMovementManager(board, mainScene, matchType);

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
                /*if (!clickTile.IsEmpty)
                    clickTile.CurrentPiece.Destroy();*/

                MovePiece(_selectedTile, clickTile);

                if (clickTile.CurrentPiece == null)
                {
                    UnSelectTile();
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

                MovePiece(_selectedTile, clickTile, true);

                SwitchTurn();
            }
        }

        else if (!clickTile.IsEmpty && clickTile.CurrentPiece.Team == _activePlayer)
        {
            if (!clickTile.IsEmpty) {
                _isTileFocus = true;
                _selectedTile = clickTile;
                //_possibleMoves = _selectedTile.CurrentPiece.GetMoves(_selectedTile, _selectedTile.CurrentPiece.Team, Board);
                _possibleMoves = GetLegalMoves(_selectedTile, Board);


                if (clickTile.CurrentPiece.Type == CGPieceType.King)
                {
                    _possibleCastleOptions =
                        CGPossibleMoves.GetCastleMoves(_selectedTile, _selectedTile.CurrentPiece.Team, Board);
                }
                
                if (clickTile.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
                {
                    _selectedTileColor = spriteRenderer.Color;
                    spriteRenderer.Color = Color.Blue;
                }
                
                ColorPossibleMoves();
            }
        }

    }

    public void UnSelectTile()
    {
        if (_isTileFocus)
        {
            ResetTilesColor();
            _isTileFocus = false;
            _selectedTile = null;
        }
    }
    public List<CGTile> GetLegalMoves(CGTile selectedTile, CGTile[,] board)
    {
        List<CGTile> allPossibleMoves = selectedTile.CurrentPiece.GetMoves(selectedTile, selectedTile.CurrentPiece.Team, board);

        List<CGTile> legalMoves = new List<CGTile>();
        foreach (CGTile move in allPossibleMoves)
        {
            if (!IsMoveIllegal(selectedTile, move, board))
                legalMoves.Add(move);
        }

        return legalMoves;
    }
    //black
    public List<CGTile> GetAllLegalMoves(CGTeam team, CGTile[,] board)
    {
        List<CGTile> allPossibleMoves = new List<CGTile>();

        //List<CGTile> legalMoves = new List<CGTile>();

        foreach (var piece in board)
        {
            if (piece.CurrentPiece == null) continue;
            if (piece.CurrentPiece.Team != team) continue;


            allPossibleMoves.AddRange(GetLegalMoves(piece,board));
        }
        return allPossibleMoves;
    }

    public void SwitchTurn()
    {

        ResetTilesColor();
        if (FindOpportnentKing(_activePlayer, Board) == null)
        {
            GameOver();
        }

        if (GameStateCanCheckOpportnent(_activePlayer, Board))
        {
            MoveRecords += "+";
            /* List<CGTile> allLegalMove = GetAllLegalMoves(_inactivePlayer, Board);
             if (allLegalMove.Count==0)
             {
                 MoveRecords += "#";
                 //GameOver();
             }*/

            
            if (GameStateCanCheckMateOpponent(_activePlayer))
            {
                if (_matchType == EMatchType.Competitive)
                {
                    var playerManager = CGPlayerManager.GetInstance();
                    playerManager.CalculatePlayerWin(_activePlayer);
                }
                
                MoveRecords += "#";
                GameOver();
            }
        }
        else
        {
            if (IsStalement(_inactivePlayer))
            {
                GameOver(true);
            }
        }

        _possibleMovesColors = null;
        _possibleMoves = null;
        if(_selectedTile!=null) { _selectedTile.CurrentPiece = null; }

        _isTileFocus = false;
        _selectedTile = null;
        _inactivePlayer = _activePlayer;
        _activePlayer = _activePlayer == CGTeam.White ? CGTeam.Black : CGTeam.White;
        _promotionInProgress = false;
        _possibleCastleOptions = null;

        var gameUI = _mainScene.FindEntity("game-ui").GetComponent<GameUI>();
        gameUI.player = _activePlayer.ToString();
        gameUI.ResetTimer();

        if(_matchType == EMatchType.AI && _activePlayer == CGTeam.Black) 
        {
            ChessAI chessAI = new ChessAI(_instMovementManager);

            (CGTile, CGTile) move = ChessAI.MakeBestMove(Board, _activePlayer);

            MovePiece(move.Item1,move.Item2);
            SwitchTurn();
        }
    }

    public void GameOver(bool stalement=false)
    {
        Core.StartSceneTransition(new FadeTransition(() => new CGGameOverScene(_inactivePlayer.ToString(),MoveRecords,stalement)));
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
    }
    private void MovePiece(CGTile start, CGTile end, bool castle = false)
    {
        if (castle == false)
        {
            if (IsMoveIllegal(start, end, Board))
                return;
            if (!end.IsEmpty)
                end.CurrentPiece.Destroy();

            if (_activePlayer == CGTeam.White)
            {
                NumOfMove++;
                MoveRecords += " " + NumOfMove + ".";
            }
            else
                MoveRecords += " ";

            string isCaptured = null;


            if (end.CurrentPiece != null && EnumHelper.GetDescription(start.CurrentPiece.Type) == "")
                isCaptured = char.ToLower(start.BoardPosition.GetFileName()) + "x";

            else if (end.CurrentPiece != null)
                isCaptured = EnumHelper.GetDescription(start.CurrentPiece.Type) + "x";

            MoveRecords += isCaptured + char.ToLowerInvariant(end.BoardPosition.GetFileName()) + end.BoardPosition.GetRankValue();

            end.CurrentPiece = start.CurrentPiece;
            end.CurrentPiece.Position = end.Position;
            end.CurrentPiece.Moved = true;
            if (_matchType == EMatchType.AI&& _activePlayer==CGTeam.Black)
            {
                start.CurrentPiece = null;
            }
        }
        else
        {
            int file = end.BoardPosition.GetFileName() - 65;
            int rank = 8 - end.BoardPosition.GetRankValue();

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
            MovePiece(_selectedTile, end);

            MoveRecords = MoveRecords.Split(',')[0];

        }
        
    }
    public bool IsMoveIllegal(CGTile start, CGTile end, CGTile[,] board)
    {
        var copiedBoard = MovementBoard(board,start, end);
        var oppTeam = start.CurrentPiece.Team == CGTeam.White ? CGTeam.Black : CGTeam.White;
        return GameStateCanCheckOpportnent(oppTeam, copiedBoard);
        
    }
    public CGTile FindOpportnentKing(CGTeam team, CGTile[,] board)
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
    public bool GameStateCanCheckOpportnent(CGTeam team, CGTile[,] board)
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
        CGTile oppKing = FindOpportnentKing(team, Board);
        List<CGTile> kingMoves = oppKing.CurrentPiece.GetMoves(oppKing, oppKing.CurrentPiece.Team, Board);


        List<CGTile> blackMoves = GetAllLegalMoves(oppKing.CurrentPiece.Team, Board);

        foreach (var move in blackMoves)
        {
            CGTile[,] replicatedBoards = MovementBoard(Board, oppKing, move);

            List<CGTile> allLegalMove = GetAllLegalMoves(oppKing.CurrentPiece.Team, replicatedBoards);
            if (allLegalMove.Count > 0)
            {
                return false;
            }
        }

        return true;
    }
    public bool CanCounterMove(CGTile piece, CGTile[,] board)
    {

        foreach (var p in board)
        {
            if (p.CurrentPiece == null || p.CurrentPiece.Team == piece.CurrentPiece.Team) continue;
            

            List<CGTile> currPossibleMove = GetLegalMoves(p, board);

            foreach (var move in currPossibleMove)
            {
                //if (move.CurrentPiece == null) continue;
                CGTile[,] simulatedBoard = MovementBoard(board, p, move);

                if (!GameStateCanCheckOpportnent(piece.CurrentPiece.Team, simulatedBoard))
                {
                    return true;  // There is a countermove
                }
              /*  if (move == piece)
                    return true;

                CGTile[,] replicatedBoards = MovementBoard(board, p, move);
                bool isBoardInCheck = GameStateCanCheckOpportnent(p.CurrentPiece.Team, replicatedBoards);

                if (isBoardInCheck == false)
                {
                 
                    return true;
                }*/
            }
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

    public bool CanBeCaptured(CGTile piece, CGTile[,] board) 
    {
        foreach (var p in board)
        {
            if (p.CurrentPiece == null) continue;
            if (p.CurrentPiece.Team == piece.CurrentPiece.Team) continue;

            List<CGTile> currPossibleMove = p.CurrentPiece.GetMoves(p, p.CurrentPiece.Team, board);

            foreach (var move in currPossibleMove)
            {
                if (move.CurrentPiece == null) continue;
                if (move == piece)
                    return true;
            }
        }
        return false;

    }
    public bool IsStalement(CGTeam team)
    {
        foreach(var piece in Board)
        {
            if( piece.CurrentPiece == null) continue;
            if( piece.CurrentPiece.Team != team) continue;

            List<CGTile> currPossibleMoves = piece.CurrentPiece.GetMoves(piece, piece.CurrentPiece.Team, Board);

            foreach (var move in currPossibleMoves)
            {
                if( IsMoveIllegal(piece, move, Board) == false )
                    return false;
            }
        }
        return true;
    }

    private void ResetTilesColor()
    {
        if(_selectedTile==null) return;
        if (_selectedTile.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
        {
            spriteRenderer.Color = _selectedTileColor;
        }
        
        foreach (CGTile tile in _possibleMoves)
        {
            if (tile.TryGetComponent<SpriteRenderer>(out var tileSpriteRenderer))
            {
                _possibleMovesColors.TryGetValue(tile, out var color);

                tileSpriteRenderer.Color = color;
            }
        }
        
        if (_possibleCastleOptions != null)
            foreach (CGTile tile in _possibleCastleOptions)
            {
                if (tile.TryGetComponent<SpriteRenderer>(out var tileSpriteRenderer))
                {
                    _possibleMovesColors.TryGetValue(tile, out var color);

                    tileSpriteRenderer.Color = color;
                }
            }
    }

    private void ColorPossibleMoves()
    {
        _possibleMovesColors = new Dictionary<CGTile, Color>();
        foreach (CGTile tile in _possibleMoves)
        {
            if (tile.TryGetComponent<SpriteRenderer>(out var tileSpriteRenderer))
            {
                _possibleMovesColors.TryAdd(tile, tileSpriteRenderer.Color);

                if (!tile.IsEmpty)
                {
                    tileSpriteRenderer.Color = Color.Red;
                }
                else
                {
                    tileSpriteRenderer.Color = Color.Green;
                }
            }
        }

        if (_possibleCastleOptions != null)
            foreach (CGTile tile in _possibleCastleOptions)
            {
                if (tile.TryGetComponent<SpriteRenderer>(out var tileSpriteRenderer))
                {
                    _possibleMovesColors.TryAdd(tile, tileSpriteRenderer.Color);
                    
                    tileSpriteRenderer.Color = Color.Yellow;
                }
            }
    }
}

  
