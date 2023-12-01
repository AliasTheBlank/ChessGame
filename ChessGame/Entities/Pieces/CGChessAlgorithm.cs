using ChessGame.Entities.Board;
using ChessGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Entities.Pieces
{
    public class ChessAI
    {
        private static CGMovementManager _moveMng;

        public ChessAI(CGMovementManager instMovementManager)
        {
            _moveMng = instMovementManager;
        }
        private const int MaxDepth =3; // Adjust the depth based on the desired level of difficulty


        public static (CGTile, CGTile) MakeBestMove(CGTile[,] board, CGTeam currentPlayer)
        {
            int bestScore = int.MinValue;
            (CGTile, CGTile) bestMove = (null,null);

            List<(CGTile, CGTile)> legalMoves = GetAllLegalMoves(currentPlayer, board);

            foreach (var move in legalMoves)
            {
                CGTile[,] newBoard = ApplyMove(board, move);
                
                int score = Minimax(newBoard, MaxDepth, int.MinValue, int.MaxValue, false);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }

            return bestMove;
        }
        /*        private static int Minimax(CGTile[,] board, int depth, int alpha, int beta, bool maximizingPlayer)
                {
                    if (depth == 0)
                    {
                        return EvaluateBoard(board);
                    }

                    List<(CGTile, CGTile)> legalMoves = GetAllLegalMoves(maximizingPlayer ? CGTeam.White : CGTeam.Black,board);

                    if (maximizingPlayer)
                    {
                        int maxScore = int.MinValue;
                        foreach (var move in legalMoves)
                        {
                            CGTile[,] newBoard = ApplyMove(board, move);
                            int score = Minimax(newBoard, depth - 1, alpha, beta, false);
                            maxScore = Math.Max(maxScore, score);
                            alpha = Math.Max(alpha, score);
                            if (beta <= alpha)
                                break;
                        }
                        return maxScore;
                    }
                    else
                    {
                        int minScore = int.MaxValue;
                        foreach (var move in legalMoves)
                        {
                            CGTile[,] newBoard = ApplyMove(board, move);
                            int score = Minimax(newBoard, depth - 1, alpha, beta, true);
                            minScore = Math.Min(minScore, score);
                            beta = Math.Min(beta, score);
                            if (beta <= alpha)
                                break;
                        }
                        return minScore;
                    }
                }*/
        private static int Minimax(CGTile[,] board, int depth, int alpha, int beta, bool maximizingPlayer)
        {
            if (depth == 0)
            {
                return EvaluateBoard(board);
            }

            List<(CGTile, CGTile)> legalMoves = GetAllLegalMoves(maximizingPlayer ? CGTeam.White : CGTeam.Black, board);

            foreach (var move in legalMoves)
            {
                CGTile[,] newBoard = ApplyMove(board, move);
                int score = Minimax(newBoard, depth - 1, alpha, beta, !maximizingPlayer);

                if (maximizingPlayer)
                {
                    alpha = Math.Max(alpha, score);
                    if (beta <= alpha)
                        break;
                }
                else
                {
                    beta = Math.Min(beta, score);
                    if (beta <= alpha)
                        break;
                }
            }

            return maximizingPlayer ? alpha : beta;
        }
/*        private static List<(CGTile, CGTile)> GetAllLegalMoves(CGTeam currentPlayer, CGTile[,] board)
        {
            List<(CGTile, CGTile)> legalMoves = new List<(CGTile, CGTile)>();

            foreach (var tile in board.Cast<CGTile>().Where(t => !t.IsEmpty && t.CurrentPiece.Team == currentPlayer))
            {
                List<CGTile> endLegalMoves = _moveMng.GetLegalMoves(tile, board);

                foreach (var endLegalMove in endLegalMoves)
                {
                    legalMoves.Add((tile, endLegalMove));
                }
            }

            return legalMoves;
        }*/
        private static List<(CGTile, CGTile)> GetAllLegalMoves(CGTeam currentPlayer, CGTile[,] board)
        {
            List<(CGTile, CGTile)> legalMoves = new List<(CGTile, CGTile)>();

            foreach (var tile in board)
            {
                if (!tile.IsEmpty && tile.CurrentPiece.Team == currentPlayer)
                {
                    List<(CGTile, CGTile)> moves = new List<(CGTile, CGTile)>();

                    List<CGTile> EndLegalMoves = _moveMng.GetLegalMoves(tile, board);
                    foreach (var elg in EndLegalMoves)
                    {
                        moves.Add((tile, elg));
                    }



                    legalMoves.AddRange(moves);
                }
            }

            return legalMoves;
        }

        private static CGTile[,] ApplyMove(CGTile[,] board, (CGTile, CGTile) move)
        {
            CGTile[,] newBoard = _moveMng.MovementBoard(board, move.Item1, move.Item2);

            return newBoard;
        }

        private static int EvaluateBoard(CGTile[,] board)
        {
            int whiteScore = 0;
            int blackScore = 0;

            foreach (var tile in board)
            {
                if (!tile.IsEmpty)
                {
                    int pieceValue =EnumHelper.GetPieceValue(tile.CurrentPiece.Type);

                    // Adjust the score based on the piece's team
                    if (tile.CurrentPiece.Team == CGTeam.White)
                        whiteScore += pieceValue;
                    else
                        blackScore += pieceValue;
                }
            }

            // Return the difference in material values
            return Math.Abs( whiteScore - blackScore);
            
        }
    }


}
