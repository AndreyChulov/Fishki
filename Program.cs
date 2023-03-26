using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Fishki
{
    internal class Program
    {
        static  BoardState _initialState = BoardState.Create();
        static  BoardState _winState = BoardState.Create(true);

        public static void Main(string[] args)
        {
            ConcurrentBag<BoardState> boardStates = new ConcurrentBag<BoardState> { _initialState };
            ConcurrentBag<int[]> winTurns = new ConcurrentBag<int[]>();
            Calc(boardStates, new int[] { }, winTurns);
            
        }

        public static void Calc(ConcurrentBag<BoardState> boardStates, int[] turns, ConcurrentBag<int[]> winTurns)
        {
            var lastBoardState = boardStates.Last();
            
            if (lastBoardState.Equals(_winState))
            {
                winTurns.Add(turns);
            }

            Parallel.For(1, 6,
                i =>
                {
                    var newBoardState = BoardState.Create(lastBoardState, i);
                    
                    if (boardStates.Contains(newBoardState))
                    {
                        return;
                    }
                    
                    boardStates.Add(newBoardState);
                    Calc(boardStates, turns.Append(i).ToArray(), winTurns);
                }
            );
        }
    }
}